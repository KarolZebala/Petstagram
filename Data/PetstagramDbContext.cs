using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Petstagram.Server.Data.Models;
using Petstagram.Server.Data.Models.Base;
using Petstagram.Server.Infrastructure.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Petstagram.Server.Data
{
    public class PetstagramDbContext : IdentityDbContext<User>
    {
        private readonly ICurrentUserService currentUser;
        public PetstagramDbContext(DbContextOptions<PetstagramDbContext> options, ICurrentUserService currentUser)
            : base(options)
        {
            this.currentUser = currentUser;
        }

        public DbSet<Pet> Pets { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Follow> Follows { get; set; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInformation();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInformation();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Pet>()
                .HasQueryFilter(c => !c.IsDeleted)//jak kasujemy pet to nie usuwamy go na stałe z bazy danych
                .HasOne(c => c.User)              //tylko ustawiamy status IsDeleted na true, tutaj odfitrowujemy Pet tak aby były tylko te które nie są IsDeleted
                .WithMany(u => u.Pets)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            //to jednak nie działa
            /* builder
                 .Entity<User>()
                 .OwnsOne(u => u.Profile)//dzięki temu User i Profile będą w jednej tabeli w bazie danych?
                 .WithOwner();*/

            builder
                .Entity<User>()
                .HasOne(c => c.Profile)
                .WithOne()
                .HasForeignKey<Profile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Follow>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Follow>()
                .HasOne(c => c.Follower)
                .WithMany()
                .HasForeignKey(u => u.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }

        private void ApplyAuditInformation()
            => this
                .ChangeTracker
                .Entries()
                .ToList()
                .ForEach(entry =>
                {
                    var userName = this.currentUser.GetUserName();

                    if (entry.Entity is IDeletableEntity deletableEntity)
                    {
                        if (entry.State == EntityState.Deleted)
                        {
                            deletableEntity.DeletedOn = DateTime.UtcNow;
                            deletableEntity.DeletedBy = userName;
                            deletableEntity.IsDeleted = true;

                            entry.State = EntityState.Modified;

                            return;//żeby nie powtórzyć modified niżej
                        }
                    }

                    if (entry.Entity is IEntity entity)
                    {
                        if(entry.State == EntityState.Added)
                        {
                            entity.CreatedOn = DateTime.UtcNow;
                            entity.CreatedBy = userName;
                        }
                        else if (entry.State == EntityState.Modified)
                        {
                            entity.ModyfiedOn = DateTime.UtcNow;
                            entity.ModifiedBy = userName;
                        }   
                    }

                });

        
    }
}

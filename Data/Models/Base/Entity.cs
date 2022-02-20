using System;
using System.ComponentModel.DataAnnotations;

namespace Petstagram.Server.Data.Models.Base
{
    public abstract class Entity : IEntity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime? ModyfiedOn { get; set; }


        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}

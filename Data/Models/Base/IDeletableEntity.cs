using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petstagram.Server.Data.Models.Base
{
    public interface IDeletableEntity:IEntity
    {
         DateTime? DeletedOn { get; set; }
         string DeletedBy { get; set; }
         bool IsDeleted { get; set; }
    }
}

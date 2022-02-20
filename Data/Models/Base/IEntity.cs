using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Petstagram.Server.Data.Models.Base
{
    public interface IEntity
    {
        DateTime CreatedOn { get; set; }
        DateTime? ModyfiedOn { get; set; }

    
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
    }
}

using System;

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

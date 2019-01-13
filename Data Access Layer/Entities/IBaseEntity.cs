using System;

namespace Data_Access_Layer
{
    public interface IBaseEntity
    {
        DateTime? CreatedAt { get; set; }
        User CreatedBy { get; set; }
        int? CreatedById { get; set; }

        DateTime? LastModifiedAt { get; set; }
        User LastModifiedBy { get; set; }
        int? LastModifiedById { get; set; }
    }
}

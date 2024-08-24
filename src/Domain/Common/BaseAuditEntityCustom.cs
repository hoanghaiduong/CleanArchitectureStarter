

namespace MyWebApi.Domain.Common
{
    public abstract class BaseAuditEntityCustom : BaseEntityCustom
    {
        public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;


        public string? CreatedBy { get; set; } = DateTime.Now.ToString();

        public DateTimeOffset LastModified { get; set; } = DateTimeOffset.UtcNow;

        public string? LastModifiedBy { get; set; } = DateTime.Now.ToString();
    }
}
using Education.Core.Models.Base;

namespace Education.Core.Models;

public class News : BaseAuditableEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ThumbnailPath { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }
}

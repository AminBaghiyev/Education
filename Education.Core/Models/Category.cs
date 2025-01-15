using Education.Core.Models.Base;

namespace Education.Core.Models;

public class Category : BaseAuditableEntity
{
    public string Title { get; set; }
    public ICollection<News> News { get; set; }
}

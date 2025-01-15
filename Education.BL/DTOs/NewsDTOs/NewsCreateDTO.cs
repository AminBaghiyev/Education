using Microsoft.AspNetCore.Http;

namespace Education.BL.DTOs;

public record NewsCreateDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IFormFile Thumbnail { get; set; }
    public int CategoryId { get; set; }
}

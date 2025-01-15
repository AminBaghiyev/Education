using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace Education.BL.DTOs;

public record NewsUpdateDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    [AllowNull]
    public IFormFile? Thumbnail { get; set; }
    public int CategoryId { get; set; }
}

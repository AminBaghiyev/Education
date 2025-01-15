namespace Education.BL.DTOs;

public record NewsViewItemDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ThumbnailPath { get; set; }
}

using Education.BL.DTOs;

namespace Education.PL.ViewModels;

public class HomeVM
{
    public IEnumerable<NewsViewItemDTO> News { get; set; }
}

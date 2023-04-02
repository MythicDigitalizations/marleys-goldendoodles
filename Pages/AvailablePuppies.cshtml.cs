using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace marleys_goldendoodles.Pages;

public class AvailablePuppiesModel : PageModel
{
    private readonly ILogger<AvailablePuppiesModel> _logger;

    public AvailablePuppiesModel(ILogger<AvailablePuppiesModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}


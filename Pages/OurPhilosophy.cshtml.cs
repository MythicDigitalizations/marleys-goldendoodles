using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace marleys_goldendoodles.Pages;

public class OurPhilosophyModel : PageModel
{
    private readonly ILogger<OurPhilosophyModel> _logger;

    public OurPhilosophyModel(ILogger<OurPhilosophyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}


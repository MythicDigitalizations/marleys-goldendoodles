using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace marleys_goldendoodles.Pages;

public class PoliciesModel : PageModel
{
    private readonly ILogger<PoliciesModel> _logger;

    public PoliciesModel(ILogger<PoliciesModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}


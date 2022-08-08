using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace marleys_goldendoodles.Pages;

public class PuppyWaitingListModel : PageModel
{
    private readonly ILogger<PuppyWaitingListModel> _logger;

    public PuppyWaitingListModel(ILogger<PuppyWaitingListModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}


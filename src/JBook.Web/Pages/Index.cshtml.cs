using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JBook.Web.Pages;
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;


    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        //_db.Books.Where(b => b.Data.Year > 1950);
    }
}

using Microsoft.AspNetCore.Mvc;
using static Fas7ny.Application.ServivesInterfaces.GeoapifyPropertiesIGeoapifySearchService;

namespace Fas7nyProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IGeoapifySearchService _geoapifySearchService;
        public SearchController(IGeoapifySearchService service)
        {
            _geoapifySearchService = service;

        }

        [HttpGet]
        public async Task<IActionResult> SearchAsync(string keyWord)
        {

            var search = await _geoapifySearchService.AutocompleteAsync(keyWord);
            if (search == null) return NotFound();
            return Ok(search);
        }
    }
}

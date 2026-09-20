using Microsoft.AspNetCore.Mvc.Rendering;

namespace Advertisements.Models.CreatorModels
{
    public class CreateCreatorVM : Creator
    {
        public IEnumerable<SelectListItem> CountryNames { get; set; }
        public IEnumerable<SelectListItem> StateNames { get; set; }
    }
}

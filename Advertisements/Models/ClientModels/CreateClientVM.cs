using Microsoft.AspNetCore.Mvc.Rendering;

namespace Advertisements.Models.ClientModels
{
    public class CreateClientVM : Client
    {
        public IEnumerable<SelectListItem> IndustryNames { get; set; }
        public IEnumerable<SelectListItem> CountryNames { get; set; }
        public IEnumerable<SelectListItem> StateNames { get; set; }

    }
}

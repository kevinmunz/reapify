using Microsoft.AspNetCore.Mvc.Rendering;

namespace Advertisements.Models.CampaignModels
{
    public class CreateCampaignVM : Campaign
    {
        public IEnumerable<SelectListItem> ClientNames { get; set; }
    }
}

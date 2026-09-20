using System.ComponentModel.DataAnnotations;

namespace Advertisements.Models.Enums
{
    public enum WebhookMessageType
    {
        [Display(Name = "Campaign Start")]
        CampaignStart = 1,
        [Display(Name = "Content Creation Start")]
        CCStart = 2,
        [Display(Name = "Content Creation End")]
        CCEnd = 3,
        [Display(Name = "Campaign End")]
        CampaignEnd = 4
    }
}

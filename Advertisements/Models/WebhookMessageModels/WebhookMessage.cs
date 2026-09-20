using Advertisements.Models.Enums;

namespace Advertisements.Models.WebhookMessageModels
{
    public class WebhookMessage
    {
        public int CampaignId { get; set; }
        public string WebhookURL { get; set; }
        public string ImageURL { get; set; }
        public WebhookMessageType WebhookMessageType { get; set; }
    }
}

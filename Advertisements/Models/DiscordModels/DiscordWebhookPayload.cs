namespace Advertisements.Models.DiscordModels
{
    public class DiscordWebhookPayload
    {
        public string content { get; set; }
        public List<DiscordEmbed> embeds { get; set; }
    }
}

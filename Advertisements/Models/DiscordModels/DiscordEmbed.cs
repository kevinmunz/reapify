namespace Advertisements.Models.DiscordModels
{
    public class DiscordEmbed
    {
        public string title { get; set; }
        public string description { get; set; }
        public int color { get; set; }
        public DiscordImage image { get; set; }
        public List<DiscordField> fields { get; set; }
        public DiscordFooter footer { get; set; }
    }
}

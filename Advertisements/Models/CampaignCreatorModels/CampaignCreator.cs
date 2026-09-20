namespace Advertisements.Models.CampaignCreatorModels
{
    public class CampaignCreator
    {
        public int CampaignCreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public int CampaignId { get; set; }
        public int CreatorId { get; set; }
        //Added fields
        public string CreatorName { get; set; }
        public string CreatorEmail { get; set; }
    }
}

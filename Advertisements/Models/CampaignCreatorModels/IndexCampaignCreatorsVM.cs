namespace Advertisements.Models.CampaignCreatorModels
{
    public class IndexCampaignCreatorsVM
    {
        public int CampaignId { get; set; }
        public IEnumerable<CampaignCreator> CampaignCreators { get; set; }
    }
}

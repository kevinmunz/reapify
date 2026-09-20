namespace Advertisements.Models.CreatorModels
{
    public class IndexCreatorsVM
    {
        public int CampaignId { get; set; }
        public IEnumerable<CreatorSelectionVM> Creators { get; set; }
    }
}

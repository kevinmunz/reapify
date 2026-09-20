namespace Advertisements.Models.MetricModels
{
    public class IndexMetricsVM
    {
        public int CampaignId { get; set; }
        public IEnumerable<Metric> Metrics { get; set; }
    }
}

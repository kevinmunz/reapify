using Microsoft.AspNetCore.Mvc.Rendering;

namespace Advertisements.Models.MetricModels
{
    public class ImportMetricsExcelVM
    {
        public IFormFile ExcelFile { get; set; }
        public int CampaignId { get; set; }
    }
}

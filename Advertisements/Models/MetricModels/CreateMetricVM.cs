using Microsoft.AspNetCore.Mvc.Rendering;

namespace Advertisements.Models.MetricModels
{
    public class CreateMetricVM : Metric
    {
        public IEnumerable<SelectListItem> CreatorNames { get; set; }
    }
}

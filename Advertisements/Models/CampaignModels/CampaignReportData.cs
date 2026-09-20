using Advertisements.Models.Enums;
using Advertisements.Models.MetricModels;

namespace Advertisements.Models.CampaignModels
{
    public record CampaignReportData
    (
        DateTime ReportDate,
        string ClientName,
        DateTime StartDate,
        DateTime EndDate,
        Platform Platform,
        int NumCreators,
        int TotalViews,
        int TotalEngagement,
        decimal EngagementRate,
        decimal ClientPaymentRate,
        decimal ClientBudget,
        decimal ExpectedClientPayment,
        decimal ClientPayment,
        decimal ExtraViewsOptimization,
        decimal AgencyOptimization,
        decimal TotalOptimization,

        IEnumerable<ReportMetricDto> ReportMetrics
    );
}

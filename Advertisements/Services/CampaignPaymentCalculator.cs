using Advertisements.Models.Enums;
using Advertisements.Models.MetricModels;

namespace Advertisements.Services;

public static class CampaignPaymentCalculator
{
    // Complete blocks only. Paid metrics retain their historical amounts.
    public static void Calculate(IEnumerable<Metric> metrics, decimal budget, decimal rate, decimal commission)
    {
        var items = metrics.ToList();
        if (budget < 0 || rate < 0 || commission < 0 || commission > 1)
            throw new ArgumentException("Budget and rate must be nonnegative; commission must be between 0 and 1.");
        if (items.Any(m => m.NumViews < 0 || m.CalculatedClientPayment < 0 || m.CalculatedCreatorPayout < 0))
            throw new ArgumentException("Views and payment amounts must be nonnegative.");
        var paid = items.Where(m => m.MetricStatus == MetricStatus.Settled).Sum(m => m.CalculatedClientPayment);
        if (paid > budget)
            throw new ArgumentException("The campaign budget is lower than its already settled amount.");
        var remaining = budget - paid;
        // Keep the existing views-first priority; break ties consistently by metric ID.
        foreach (var metric in items.OrderByDescending(m => m.NumViews).ThenBy(m => m.ProductId))
        {
            if (metric.MetricStatus == MetricStatus.Settled) continue;
            var amount = metric.MetricStatus == MetricStatus.Accepted
                ? Math.Min(remaining, decimal.Round((metric.NumViews / 1000) * rate, 2, MidpointRounding.AwayFromZero))
                : 0m;
            metric.CalculatedClientPayment = amount;
            metric.CalculatedCreatorPayout = decimal.Round(amount * (1 - commission), 2, MidpointRounding.AwayFromZero);
            remaining -= amount;
        }
    }
}

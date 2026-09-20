using Advertisements.Models.Enums;
using Advertisements.Models.MetricModels;
using Advertisements.Services;
static Metric M(int id, int views, MetricStatus status = MetricStatus.Accepted, decimal client = 0, decimal creator = 0)
    => new() { ProductId = id, NumViews = views, MetricStatus = status, CalculatedClientPayment = client, CalculatedCreatorPayout = creator };
static void Check(bool condition, string message) { if (!condition) throw new Exception(message); }
var blocks = new[] { M(1,999), M(2,1000), M(3,1500) };
CampaignPaymentCalculator.Calculate(blocks, 500, 10, .2m);
Check(blocks.Select(m=>m.CalculatedClientPayment).SequenceEqual(new[]{0m,10m,10m}), "Whole blocks");
var mix = new[] { M(1,3400,MetricStatus.Settled,30,24), M(2,5000), M(3,2000), M(4,9000,MetricStatus.Rejected,1,1), M(5,8000,MetricStatus.Pending) };
CampaignPaymentCalculator.Calculate(mix, 60, 10, .2m);
Check(mix[0].CalculatedClientPayment == 30 && mix[0].CalculatedCreatorPayout == 24, "Settled history");
Check(mix[1].CalculatedClientPayment == 30 && mix[2].CalculatedClientPayment == 0, "Remaining budget and priority");
Check(mix[3].CalculatedClientPayment == 0 && mix[4].CalculatedClientPayment == 0, "Unapproved metrics");
Check(mix.Sum(m=>m.CalculatedClientPayment) == 60 && mix.Sum(m=>m.CalculatedCreatorPayout) == 48, "Totals include settled");
CampaignPaymentCalculator.Calculate(mix, 60, 10, .2m);
Check(mix.Sum(m=>m.CalculatedClientPayment) == 60, "Repeat calculation");
var tie = new[]{M(2,1000),M(1,1000)};
CampaignPaymentCalculator.Calculate(tie, 10, 10, .2m);
Check(tie[1].CalculatedClientPayment == 10, "Stable tie order");
var rounded = new[]{M(1,1000)};
CampaignPaymentCalculator.Calculate(rounded, 100, 1.01m, .5m);
Check(rounded[0].CalculatedCreatorPayout == .51m, "Currency rounding");
try { CampaignPaymentCalculator.Calculate(mix, 29, 10, .2m); throw new Exception("Expected budget rejection"); }
catch (ArgumentException) { }
Check(mix[0].CalculatedClientPayment == 30, "Rejected calculation preserves history");
Console.WriteLine("PASS: blocks, settled history, budget cap, priority, rejected/pending, totals, repeat, ties, rounding, invalid budget.");

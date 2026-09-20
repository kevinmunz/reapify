using Advertisements.Models.Enums;
using Advertisements.Models.ProductModels;
using System.ComponentModel.DataAnnotations;

namespace Advertisements.Models.CampaignModels
{
    public class Campaign : Product
    {
        public int ClientId { get; set; }
        [Required]
        public Scale Scale { get; set; }
        [Required]
        public Currency Currency { get; set; }
        [Required]
        public decimal ComissionRate { get; set; }
        [Required]
        public decimal ClientBudget { get; set; }
        public decimal CreatorBudget { get; set; }
        [Required]
        public Platform Platform { get; set; }
        [Required]
        public string Goal { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public decimal ClientPaymentRate { get; set; }
        public decimal CreatorPayoutRate { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public string GoogleFormsLink { get; set; }
        [Required]
        public decimal ClientPayment { get; set; }
        public decimal CreatorPayout { get; set; }
        [Required]
        public string WebhookURL { get; set; }
        public decimal SalesGrowth { get; set; }
        [Required]
        public Stage Stage { get; set; }
        [Required]
        public CampaignStatus CampaignStatus { get; set; }
        //Added fields
        public string ClientName { get; set; }
    }
}

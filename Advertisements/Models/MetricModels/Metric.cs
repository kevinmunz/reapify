using Advertisements.Models.Enums;
using Advertisements.Models.ProductModels;
using System.ComponentModel.DataAnnotations;

namespace Advertisements.Models.MetricModels
{
    public class Metric : Product
    {
        [Required]
        public DateTime SubmissionTimestamp { get; set; }
        [Required]
        public int CampaignCreatorId { get; set; }
        [Required]
        public string ScreenshotLink { get; set; }
        [Required]
        public string VideoLink { get; set; }
        [Required]
        public int NumViews { get; set; }
        [Required]
        public int NumLikes { get; set; }
        [Required]
        public int NumComments { get; set; }
        [Required]
        public int NumShares { get; set; }
        [Required]
        public int NumSaves { get; set; }
        [Required]
        public int VideoDuration { get; set; }
        [Required]
        public string QRCodeLink { get; set; }
        [Required]
        public decimal CalculatedClientPayment { get; set; }
        [Required]
        public decimal CalculatedCreatorPayout { get; set; }
        [Required]
        public MetricStatus MetricStatus { get; set; }
        //Added Fields
        public int CampaignId { get; set; }
        public int CreatorId { get; set; }
        public string CreatorName { get; set; }

    }
}

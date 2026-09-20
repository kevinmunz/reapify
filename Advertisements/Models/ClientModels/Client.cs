using Advertisements.Models.EntityModels;
using System.ComponentModel.DataAnnotations;

namespace Advertisements.Models.ClientModels
{
    public class Client : Entity
    {
        public int IndustryId { get; set; }
        [Required]
        public string ReferenceLink { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string NIT { get; set; }
        public string TaxAddress { get; set; }
        //Added fields
        public string IndustryName { get; set; }
    }
}

using Advertisements.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Advertisements.Models.ProductModels
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public ProductType ProductType { get; set; }
    }
}

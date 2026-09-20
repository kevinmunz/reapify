using Advertisements.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Advertisements.Models.TransactionModels
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public TranType Type { get; set; }
        [Required]
        public Direction Direction { get; set; }
        [Required]
        public int EntityId { get; set; }
        [Required]
        public int ProductId { get; set; }
        public string Description { get; set; }
        //Added fields
        public string EntityName { get; set; }
        public EntityType EntityType { get; set; }
        public ProductType ProductType { get; set; }
    }
}

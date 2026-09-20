using Advertisements.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Advertisements.Models.EntityModels
{
    public class Entity
    {
        public int EntityId { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public EntityType EntityType { get; set; }
        [Required]
        [StringLength(maximumLength: 255)]
        public string Name { get; set; }
        [Required]
        public int StateId { get; set; }
        // Added fields
        public string StateName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
}

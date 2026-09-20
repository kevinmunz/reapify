using Advertisements.Models.EntityModels;
using Advertisements.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Advertisements.Models.CreatorModels
{
    public class Creator : Entity
    {
        [Required]
        public ContentType ContentType { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string CI { get; set; }
        [Required]
        public string RegistrationPhone { get; set; }
        [Required]
        public string RegistrationEmail { get; set; }
        [Required]
        public FollowerRange FollowerRange { get; set; }
        [Required]
        public CreatorStatus CreatorStatus { get; set; }

    }
}

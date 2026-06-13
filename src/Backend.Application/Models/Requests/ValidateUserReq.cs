using System.ComponentModel.DataAnnotations;

namespace Backend.Application.Models.Requests
{
    public class ValidateUserReq
    {
        [Required]
        [MaxLength(50)]
        public string EmailId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}

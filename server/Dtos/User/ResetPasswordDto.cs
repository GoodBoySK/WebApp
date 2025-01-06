using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace server.Dtos.User
{
    public class ResetPasswordDto
    {
        [Required]
        public required string OldPassword { get; set; }
        [Required]
        public required string NewPassword { get; set; }
        [Required]
        public required string Token { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
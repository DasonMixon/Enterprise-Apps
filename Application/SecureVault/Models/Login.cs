using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecureVault.Models
{
    public class Login
    {
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //https://www.youtube.com/watch?annotation_id=annotation_815470&feature=iv&src_vid=7RoJIgRcuOc&v=TNgbuE0VfXI
    }
}
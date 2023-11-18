using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zero_Hunger.Models
{
    public class loginDTO
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
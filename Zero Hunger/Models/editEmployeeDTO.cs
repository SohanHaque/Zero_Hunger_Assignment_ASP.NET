using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zero_Hunger.Models
{
    public class editEmployeeDTO
    {
        [Required]
        public int id { get; set; }
        [Required]
        [MinLength(5)]
        public string username { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public System.DateTime dob { get; set; }
    }
}
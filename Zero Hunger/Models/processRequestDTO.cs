using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zero_Hunger.Models
{
    public class processRequestDTO
    {
        [Required]
        public System.DateTime expire_datetime { get; set; }
    }
}
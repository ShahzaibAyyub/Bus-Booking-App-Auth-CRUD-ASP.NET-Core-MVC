using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace finalDeliverable.Models
{
    public class Admin
    {

        [Required(ErrorMessage = "Please enter valid user name.")]
        [Key]
        [Remote("nameExists", "name", "name is already taken.")]
        public string name { set; get; }
        [Required(ErrorMessage = "Please enter valid pasward.")]
        public string passward { set; get; }

        public string gender { set; get; }
        public int age { set; get; }


    }
}

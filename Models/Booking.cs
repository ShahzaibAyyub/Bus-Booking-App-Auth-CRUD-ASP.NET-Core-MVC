using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace finalDeliverable.Models
{
    public class Booking
    {

        [Key]
        public string bookingID { set; get; }
        public string busIdForeignkey { set; get; }
        public string userIdForeignkey { set; get; }
        public DateTime time { set; get; }
        public Customer user { get; set; }

        public Bus bus { get; set; }
    }
}

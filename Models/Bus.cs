using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace finalDeliverable.Models
{
    public class Bus
    {




        [Key]
        public string busID { set; get; }
        public string From { set; get; }
        public string To { set; get; }
        
        public int Capacity { set; get; }
        public int counter { set; get; }
        public int price { set; get; }

        public DateTime time { set; get; }

        public ICollection<Booking> BusBookingHistory { set; get; }

    }
}

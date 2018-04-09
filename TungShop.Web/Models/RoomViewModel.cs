using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TungShop.Web.Models
{
    public class RoomViewModel
    {
        public int RoomID { set; get; }

        [Required]
        public string RoomName { set; get; }

        [Required]
        public int AmountMax { set; get; }


        public int Amount { set; get; }

    }
}
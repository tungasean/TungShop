using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TungShop.Web.Models
{
    public class RoomViewModel
    {
        [Required]
        public string RoomID { set; get; }

        [Required]
        public string RoomName { set; get; }

        [Required]
        public int AmountMax { set; get; }


        public int Amount { set; get; }

    }
}
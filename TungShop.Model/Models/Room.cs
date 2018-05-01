using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TungShop.Model.Abstract;

namespace TungShop.Model.Models
{
    [Table("Rooms")]
    public class Room
    {
        [Key]
        [Required]
        public string RoomID { set; get; }
        
        public string RoomName { set; get; }

        [Required]
        public int AmountMax { set; get; } // so nguoi toi da

        public int Price { get; set; } // gia phong
        
        public int Amount { set; get; } // so nguoi hien tai

        public int Sex { get; set; } // phong nam hay nu 0 la nam, 1 la nu

    }
}
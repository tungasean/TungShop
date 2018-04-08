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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomID { set; get; }

        [Required]
        public int AmountMax { set; get; } // so nguoi toi da

        [Required]
        public int Amount { set; get; } // so nguoi hien tai

    }
}
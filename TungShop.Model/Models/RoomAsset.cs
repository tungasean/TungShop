using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TungShop.Model.Abstract;

namespace TungShop.Model.Models
{
    [Table("RoomAssets")]
    public class RoomAsset
    {
        // Tai san phong
        [Key]
        [Column(Order = 1)]
        [Required]
        public int RoomID { set; get; } // ma phong

        [Key]
        [Column(Order = 2)]
        [Required]
        public int AssetsID { set; get; } // ma loai tai san

        [Required]
        public int Amount { set; get; } // so luong

    }
}
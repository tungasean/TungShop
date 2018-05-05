using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TungShop.Web.Models
{
    public class RoomAssetViewModel
    {
        [Required]
        public string RoomID { set; get; } // ma phong
        
        [Required]
        public int AssetsID { set; get; } // ma loai tai san

        [Required]
        public int Amount { set; get; } // so luong

    }
}
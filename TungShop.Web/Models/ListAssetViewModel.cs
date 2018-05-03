using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TungShop.Web.Models
{
    public class ListAssetViewModel
    {
        public int AssetsID { set; get; } // ma loai tai san rieng cho tung phong

        [Required]
        public string AssetName { set; get; } // ten tai san

        [Required]
        public int Amount { set; get; } // so luong

        [Required]
        public int Status { set; get; } // hien trang

        [Required]
        public int AssetStype { set; get; } // loai tai san(co nhieu tai san chung 1 mã vì mỗi cái cua 1 phong khac nhau)

    }
}
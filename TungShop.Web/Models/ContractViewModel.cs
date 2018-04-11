using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TungShop.Web.Models
{
    public class ContractViewModel
    {
        [Required]
        public string StudentID { set; get; }

        [Required]
        public string RoomID { set; get; }

        [Required]
        public DateTime TimeSign { set; get; }

        [Required]
        public int Term { set; get; } // ky han hop dong theo tháng

        [Required]
        public int Status { set; get; } //tinh trang hop dong
        
        public string Note { set; get; }

    }
}
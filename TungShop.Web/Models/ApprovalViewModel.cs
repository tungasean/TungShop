using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TungShop.Web.Models
{
    public class ApprovalViewModel
    {
        public int ApprovalId { set; get; }

        public string StudentId { set; get; }

        public string Name { set; get; }

        public DateTime? BirthDay { set; get; }

        [MaxLength(15)]
        public string Sex { set; get; }

        [MaxLength(20)]
        public string CardNo { set; get; }

        [MaxLength(200)]
        public string Address { set; get; }

        public int Status { set; get; }

    }
}
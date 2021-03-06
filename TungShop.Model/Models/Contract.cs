﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TungShop.Model.Abstract;

namespace TungShop.Model.Models
{
    [Table("Contracts")]
    public class Contract
    {
        // hop dong
        [Key]
        [Required]
        public string StudentID { set; get; }
        
        [Required]
        public string RoomID { set; get; }

        [Required]
        public DateTime TimeSign { set; get; }

        [Required]
        public int Term { set; get; } // ky han hop dong theo tháng
        
        public int Status { set; get; } //tinh trang hop dong
        
        public string Note { set; get; }
    }
}
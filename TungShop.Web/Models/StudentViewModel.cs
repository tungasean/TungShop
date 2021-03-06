﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TungShop.Web.Models
{
    public class StudentViewModel
    {
        [Required]
        public string StudentID { set; get; }

        [Required]
        public string Name { set; get; }

        public DateTime? BirthDay { set; get; }

        public int Sex { set; get; }

        public string CardNo { set; get; }

        public string Address { set; get; }

        public int IsOut { set; get; }

    }
}
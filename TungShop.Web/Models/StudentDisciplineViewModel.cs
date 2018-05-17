using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TungShop.Web.Models
{
    public class StudentDisciplineViewModel
    {
        public int ID { set; get; }

        [Required]
        public string StudentID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        public string InfoDiscipline { set; get; }

    }
}
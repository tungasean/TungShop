using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TungShop.Model.Abstract;

namespace TungShop.Model.Models
{
    [Table("Students")]
    public class Student
    {
        [Key]
        [Required]
        public string StudentID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        public DateTime? BirthDay { set; get; }
        
        public int Sex { set; get; }

        [MaxLength(20)]
        public string CardNo { set; get; }

        [MaxLength(200)]
        public string Address { set; get; }
        
    }
}
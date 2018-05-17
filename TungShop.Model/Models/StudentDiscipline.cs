using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TungShop.Model.Abstract;

namespace TungShop.Model.Models
{
    [Table("StudentDisciplines")]
    public class StudentDiscipline
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        
        [Required]
        public string StudentID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }
        
        public string InfoDiscipline { set; get; }
        
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TungShop.Model.Abstract;

namespace TungShop.Model.Models
{
    [Table("Approvals")]
    public class Approval
    {
        // danh sach phe duyet vao ky tuc
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApprovalId { set; get; }
        
        public string StudentId { set; get; }

        public string Name { set; get; }

        public DateTime? BirthDay { set; get; }
        
        public int Sex { set; get; }

        [MaxLength(20)]
        public string CardNo { set; get; }

        [MaxLength(200)]
        public string Address { set; get; }

        public int Status { set; get; } // 1 la cho phe duyet, 2 la dong y, 3 la tu choi
    }
}
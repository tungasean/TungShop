using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TungShop.Model.Abstract;

namespace TungShop.Model.Models
{
    [Table("Invoices")]
    public class Invoice
    {
        // hoa don
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceID { set; get; }

        [Required]
        public int Type { set; get; }

        [Required]
        public int RoomID { set; get; }
        
        public int Amount { set; get; } //so tien

        [Required]
        public DateTime DayCreate { set; get; }

        public DateTime DayPay { set; get; } //ngay thanh toan

        public int UserCreate { set; get; }

        public int UserPay { set; get; } //Nguoi thanh toan

        [Required]
        public string Content { set; get; }//noi dung
    }
}
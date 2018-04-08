using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TungShop.Model.Abstract;

namespace TungShop.Model.Models
{
    [Table("ElectricityWaters")]
    public class ElectricityWater
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        public int RoomID { set; get; }

        [Required]
        public string Month { set; get; }

        [Required]
        public int WaterNew { set; get; }

        [Required]
        public int WaterOld { set; get; }

        [Required]
        public int EletricityOld { set; get; }

        [Required]
        public int EletricityNew { set; get; }

        [Required]
        public int Money { set; get; }
        

        public int UserID { set; get; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TungShop.Model.Abstract;

namespace TungShop.Model.Models
{
    [Table("ElectricityWaterHistorys")]
    public class ElectricityWaterHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        public string RoomID { set; get; }
        
        public string Month { set; get; }
        
        public int WaterNew { set; get; }
        
        public int WaterOld { set; get; }
        
        public int EletricityOld { set; get; }
        
        public int EletricityNew { set; get; }
        
        public int Money { set; get; }

        public int PriceElectricity { get; set; }

        public int PriceWater { get; set; }
        
        public int UserID { set; get; }

        public int IsPrint { set; get; }

        public DateTime TimeChange { set; get; }

    }
}
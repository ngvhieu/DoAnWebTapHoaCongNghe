﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoAnTapHoaCongNghe.Areas.Seller.Models
{
    [Table("SellerMenu")]
    public class SellerMenu
    {
        [Key]
        public int SellerMenuID { get; set; }
        public string? ItemName { get; set; }
        public int ItemLevel { get; set; }
        public int ParentLevel { get; set; }
        public int ItemOrder { get; set; }
        public bool? IsActive { get; set; }
        public string? ItemTarget { get; set; }
        public string? AreaName { get; set; }
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
        public string? Icon { get; set; }
        public string? IdName { get; set; }
    }
}

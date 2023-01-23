﻿using System.ComponentModel.DataAnnotations;
using MVCproject.Data.Enum;

namespace MVCproject.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public Products Category { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public decimal? AlcVolume { get; set; }
    }
}

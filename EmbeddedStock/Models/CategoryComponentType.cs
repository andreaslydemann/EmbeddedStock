using System;
using System.Collections;
using System.Collections.Generic;
using EmbeddedStock.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmbeddedStock.Models
{
    public class CategoryComponentType
    {
        [Key]
        [Column(Order = 0)]
        public long CategoryId { get; set; }
        public Category Category { get; set; }

        [Key]
        [Column(Order = 1)]
        public long ComponentTypeId { get; set; }
        public ComponentType ComponentType { get; set; }
    }
}
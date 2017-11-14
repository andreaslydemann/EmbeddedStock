using System;
using System.Collections;
using System.Collections.Generic;
using EmbeddedStock.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmbeddedStock.Models
{
    public class Category
    {
        public Category()
        {
            CategoryComponentTypes = new List<CategoryComponentType>();
        }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<CategoryComponentType> CategoryComponentTypes { get; protected set; }
    }
}
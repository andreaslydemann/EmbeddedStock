using System;
using System.Collections;
using System.Collections.Generic;
using EmbeddedStock.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EmbeddedStock.Models
{
    public class ComponentType
    {
        public ComponentType()
        {
            Components = new List<Component>();
            CategoryComponentTypes = new List<CategoryComponentType>();
        }

        public long ComponentTypeId { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string Location { get; set; }
        public ComponentTypeStatus Status { get; set; }
        public string Datasheet { get; set; }

        [DisplayName("Image Url")]
        public string ImageUrl { get; set; }
        public string Manufacturer { get; set; }

        [DisplayName("Wiki Link")]
        public string WikiLink { get; set; }

        [DisplayName("Admin Comment")]
        public string AdminComment { get; set; }
        public virtual ESImage Image { get; set; }
        public ICollection<Component> Components { get; protected set; }
        public ICollection<CategoryComponentType> CategoryComponentTypes { get; set; }
    }
}
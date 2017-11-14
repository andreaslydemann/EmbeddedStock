using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmbeddedStock.Models;

namespace EmbeddedStock.ViewModels
{
    public class ComponentTypeViewModel
    {
        public ComponentType ComponentType { get; set; }

        public List<string> SelectedCategories { get; set; }

        public List<SelectListItem> Categories { get; set; }
    }
}
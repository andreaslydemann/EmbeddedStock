using System;
using System.Collections;
using System.Collections.Generic;
using EmbeddedStock.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EmbeddedStock.Models
{
    public class Component
    {
        public long ComponentId { get; set; }

        [DisplayName("Component Type")]
        public long ComponentTypeId { get; set; }

        public int Number { get; set; }

        [DisplayName("Serial No.")]
        public string SerialNo { get; set; }

        public ComponentStatus Status { get; set; }

        [DisplayName("Admin Comment")]
        public string AdminComment { get; set; }

        [DisplayName("User Comment")]
        public string UserComment { get; set; }

        [DisplayName("Current Loan Information ID")]
        public long? CurrentLoanInformationId { get; set; }

        public ComponentType ComponentType { get; set; }
    }
}
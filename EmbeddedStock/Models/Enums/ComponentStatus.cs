using System;
namespace EmbeddedStock.Models.Enums
{
    public enum ComponentStatus
    {
        Available,
        ReservedLoaner,
        ReservedAdmin,
        Loaned,
        Defect,
        Trashed,
        Lost,
        NeverReturned
    }
}
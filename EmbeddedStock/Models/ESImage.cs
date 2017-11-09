using System;
using System.Collections;
using System.Collections.Generic;
using EmbeddedStock.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmbeddedStock.Models
{
    public class ESImage
    {
        public long ESImageId { get; set; }
        [MaxLength(128)]
        public string ImageMimeType { get; set; }
        public byte[] Thumbnail { get; set; }
        public byte[] ImageData { get; set; }
    }
}

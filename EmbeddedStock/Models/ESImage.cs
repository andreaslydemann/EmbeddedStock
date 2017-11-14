using System;
using System.Collections;
using System.Collections.Generic;
using EmbeddedStock.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EmbeddedStock.Models
{
    public class ESImage
    {
        public long ESImageId { get; set; }

        [MaxLength(128)]
        [DisplayName("Image Mime Type")]
        public string ImageMimeType { get; set; }
        public byte[] Thumbnail { get; set; }

        [DisplayName("Image Data")]
        public byte[] ImageData { get; set; }
    }
}

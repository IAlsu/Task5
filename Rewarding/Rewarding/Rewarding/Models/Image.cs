using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rewarding.Models
{
    [Table("Pictures")]
    public class Image
    {
        public int ImageId { get; set; }

        public string ImageName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }

        public byte[] Content { get; set; }
    }
}
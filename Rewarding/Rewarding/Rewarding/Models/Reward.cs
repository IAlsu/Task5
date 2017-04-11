using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rewarding.Models
{
    public class Reward
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Person> Persons { get; set; }
        public Reward()
        {
            Persons = new List<Person>();
        }

        public int? ImageId { get; set; }
        [ForeignKey("ImageId")]
        public Image Image { get; set; }
    }
}
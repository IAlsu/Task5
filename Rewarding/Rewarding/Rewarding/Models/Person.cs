using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rewarding.Models
{
    [Table("Persons")]
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthdate { get; set; }

        public int Age { get; set; }

        public virtual ICollection<Reward> Rewards { get; set; }
        public Person()
        {
            Rewards = new List<Reward>();
        }

        public int? PhotoId { get; set; }
        [ForeignKey("PhotoId")]
        public Image Photo { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rewarding.Models
{
    [Table("PersonRewards")]
    public class PersonReward
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int RewardId { get; set; }
        public Reward Reward { get; set; }
    }
}
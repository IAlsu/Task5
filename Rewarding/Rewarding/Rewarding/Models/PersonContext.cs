using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Rewarding.Models
{
    public class PersonContext:DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Reward> Rewards { get; set; }

        public DbSet<Image> Pictures { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reward>().HasMany(c => c.Persons)
                .WithMany(s => s.Rewards)
                .Map(t => t.MapLeftKey("RewardId")
                .MapRightKey("PersonId")
                .ToTable("PersonRewards"));
        }
    }
}

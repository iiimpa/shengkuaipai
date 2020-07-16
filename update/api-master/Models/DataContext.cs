using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Blacklist> Blacklists { get; set; }
        public DbSet<ClickPlan> ClickPlans { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<LoginLog> LoginLogs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<RechargePlan> RechargePlans { get; set; }
        public DbSet<ResidenPlan> ResidencePlans { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RecommendationPlan> RecommendationPlans { get; set; }
    }
}

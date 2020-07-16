using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Plan> Plans { get; set; }
        public DbSet<ClickPlan> ClickPlans { get; set; }
        public DbSet<Config> Configs { get; set; }
        public DbSet<Knowledge> Knowledges { get; set; }
        public DbSet<LoginLog> LoginLogs { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<RechargePlan> RechargePlans { get; set; }
        public DbSet<Carousel> Carousels { get; set; }
        public DbSet<FriendLink> FriendLinks { get; set; }


    }
}
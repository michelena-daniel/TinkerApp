using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TinkerAppProject.Areas.Identity.Data;
using TinkerAppProject.Models.Expenses;

namespace TinkerAppProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<TinkerAppProjectUser>
    {
        public DbSet<ExpenseModel> Expense { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ExpenseModel>()
                .HasOne(e => e.User)
                .WithMany(e => e.Expenses)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
        }
    }
}

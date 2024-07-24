using System.ComponentModel.DataAnnotations;
using TinkerAppProject.Areas.Identity.Data;

namespace TinkerAppProject.Models
{
    public class ExpenseModel
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Concept { get; set; }
        public CategoryEnum Category { get; set; }
        [Range(1, int.MaxValue)]
        public int AmountPaid { get; set; }
        public DateTime DayPaid { get; set; }
        public DateTime DayRegistered { get; set; }
        public TinkerAppProjectUser? User { get; set; } = null!;
    }
}

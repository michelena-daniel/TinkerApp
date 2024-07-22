namespace TinkerAppProject.Models
{
    public class ExpenseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Concept { get; set; }
        public CategoryEnum Category { get; set; }
        public int AmountPaid { get; set; }
        public DateTime DayPaid { get; set; }
        public DateTime DayRegistered { get; set; }
    }
}

namespace aa_finance_tracker.Data
{
    public class ExpenseType
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public ExpenseType(string Name, string Description)
        {
            this.Name = Name;
            this.Description = Description;
        }
    }
}
namespace aa_finance_tracker.Domains
{
    public class ExpenseCategory
    {
        protected ExpenseCategory()
        {
        }

        public ExpenseCategory(
            string name,
            string description,
            decimal budget,
            string colourHex)
        {
            Name = name;
            Description = description;
            Budget = budget;
            ColourHex = colourHex;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Budget { get; set; }
        public string ColourHex { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}

namespace AAFinanceTracker.Domain.Dtos;

public class PlaidDataTable
{
    public PlaidColumn[] Columns { get; init; } = [];

    public PlaidRow[] Rows { get; init; } = [];

    public PlaidDataTable() { }
};

public class PlaidColumn
{
    public string Title { get; set; } = string.Empty;

    public bool IsRight { get; set; }
}

public class PlaidRow
{
    public string[] Cells { get; set; } = [];

    public PlaidRow() { }

    public PlaidRow(params string[] cells)
    {
        Cells = cells;
    }
}


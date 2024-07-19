namespace AAFinanceTracker.Domain;

public class Account
{
    private int accountNumber;
    private string bankId;
    private string currency;
    private Transaction[]? transactions;

    public int AccountNumber { get => accountNumber; set => accountNumber = value; }
    public string BankId { get => bankId; set => bankId = value; }
    public string Currency { get => currency; set => currency = value; }
    public Transaction[]? Transactions { get => transactions; set => transactions = value; }

}

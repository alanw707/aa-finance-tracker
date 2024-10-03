namespace AAFinanceTracker.Domain.Entities;

public class Account(int accountNumber, string bankId, string currency, Transaction[]? transactions)
{
    public int AccountNumber { get; set; } = accountNumber;

    public string BankId { get; set; } = bankId;

    public string Currency { get; set; } = currency;

    public Transaction[]? Transactions { get; set; } = transactions;
}

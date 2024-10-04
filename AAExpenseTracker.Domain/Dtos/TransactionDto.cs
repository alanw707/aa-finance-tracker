using System.Runtime.Serialization;

namespace AAFinanceTracker.Domain.Dtos;

[DataContract]
public class Transaction 
{
    public int Id { get; set; }
}

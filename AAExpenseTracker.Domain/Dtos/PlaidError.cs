using System.Text.Json.Serialization;

namespace AAFinanceTracker.Domain.Dtos;

public class PlaidError
{
    public string error_type { get; set; } = string.Empty;
    public string error_code { get; set; } = string.Empty;
    public string error_message { get; set; } = string.Empty;
    public string? display_message { get; set; }
    public string? error_type_path { get; set; }

    [JsonIgnore]
    public string? Url => (error_type_path is not null) ? $"https://plaid.com/docs/errors/{error_type_path}/#{error_code.ToLowerInvariant()}" : null;
}


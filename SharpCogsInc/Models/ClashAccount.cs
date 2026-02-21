using System.Text.Json.Serialization;

namespace SharpCogsInc.Models;

public class ClashAccount
{
    [JsonPropertyName("username")]
    public required string Username { get; init; }

    [JsonPropertyName("token")]
    public required string Token { get; init; }

    [JsonPropertyName("id")]
    public required int Id { get; init; }


    public override string ToString()
    {
        return $"{Username}";
    }
}
using System.Text.Json.Serialization;

namespace SharpCogsInc.Models;

public class RegisterResponseBody
{
    [JsonPropertyName("status")]
    public required bool Status { get; init; }

    [JsonPropertyName("reason")]
    public required int Reason { get; init; }

    [JsonPropertyName("toonstep")]
    public bool Toonstep { get; init; }

    [JsonPropertyName("message")]
    public required string Message { get; init; }

    [JsonPropertyName("token")]
    public required string Token { get; init; }

    [JsonPropertyName("id")]
    public int Id { get; init; }


    public override string ToString()
        => $"Status: {Status} \n Reason: {Reason} \n  Message: {Message} \n Toonstep: ${Toonstep} \n token: {Token}";
}
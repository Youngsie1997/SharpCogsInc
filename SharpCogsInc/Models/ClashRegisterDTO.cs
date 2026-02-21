using System.Text.Json.Serialization;

namespace SharpCogsInc.Models;

public class ClashRegisterDto
{
    [JsonPropertyName("username")]
    public required string Username { get; set; }

    [JsonPropertyName("password")]
    public required string Password { get; set; }

    [JsonPropertyName("friendly")]
    public required string Friendly { get; set; }
}
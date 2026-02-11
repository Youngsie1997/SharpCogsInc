using System.Text.Json.Serialization;

namespace SharpCogsInc.Models;

public class RegisterResponseBody
{
    [JsonPropertyName("status")]
    public bool Status { get; set; }
    [JsonPropertyName("reason")]
    public int  Reason { get; set; }
    [JsonPropertyName("toonstep")]
    public bool Toonstep { get; set; }
    [JsonPropertyName("message")]
    public string Message { get; set; }
    [JsonPropertyName("token")]
    public string Token { get; set; }
    [JsonPropertyName("id")]
    public int? Id { get; set; }
    
    
    override public string ToString() => $"Status: {Status} \n Reason: {Reason} \n  Message: {Message} \n Toonstep: ${Toonstep} \n token: {Token}";
    
}

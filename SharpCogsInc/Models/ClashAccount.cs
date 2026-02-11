using System.Text.Json.Serialization;

namespace SharpCogsInc.Models;

public class ClashAccount
{
    [JsonPropertyName("username")]
    public string Username { get; set; }
    [JsonPropertyName("token")]
    public string Token { get; set; }
    [JsonPropertyName("id")]
    public int Id { get; set; }


    override public string ToString()
    {
        return Username;
    }
}


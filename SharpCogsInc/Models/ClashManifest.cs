using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace SharpCogsInc.Models;

public class ClashManifest
{
    [JsonPropertyName("files")]
    public List<ClashFile> Files { get; set; }
}
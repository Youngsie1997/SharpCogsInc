using System.Text.Json.Serialization;
namespace SharpCogsInc.Models;

public class ClashFile
{
    [JsonPropertyName("fileName")]
    private string FileName { get; set; }
    [JsonPropertyName("filePath")]
    private string FilePath { get; set; }
    [JsonPropertyName("sha1")]
    private string Sha1 { get; set; }
    [JsonPropertyName("compressed_sha1")]
    private string CompressedSha1 { get; set; }


    [JsonIgnore] public bool PlatformSpecific { get; set; } = false;
}
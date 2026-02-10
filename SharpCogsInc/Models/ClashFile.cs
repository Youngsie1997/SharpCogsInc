using System.Text.Json.Serialization;
namespace SharpCogsInc.Models;

public class ClashFile
{
    [JsonPropertyName("fileName")]
    public string FileName { get; set; }
    [JsonPropertyName("filePath")]
    public string FilePath { get; set; }
    [JsonPropertyName("sha1")]
    public string Sha1 { get; set; }
    [JsonPropertyName("compressed_sha1")]
    public string CompressedSha1 { get; set; }


    [JsonIgnore] public bool PlatformSpecific { get; set; } = false;


    override public string ToString()
    {
        return $"fileName: {FileName} \n FilePath: {FilePath} \n Sha1: {Sha1} \n Compress Sha1 {CompressedSha1} \n " +
               $"platformSpecific: {PlatformSpecific}";
    }
}
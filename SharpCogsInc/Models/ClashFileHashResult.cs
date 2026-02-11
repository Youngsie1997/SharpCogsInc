namespace SharpCogsInc.Models;

public class ClashFileHashResult
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public required string FilePath { get; set;}
    public required string? Sha1 { get; set; }

    public override string ToString()
    {
        return $"{FilePath}, {Success}";
    }
}
using System.Threading.Tasks;

namespace SharpCogsInc.Services;

public interface IFolderPickerService
{
    Task<string?> PickFolderAsync(string title = "Select Folder");
    Task<string?> PickFileAsync(string title = "Select File");

}
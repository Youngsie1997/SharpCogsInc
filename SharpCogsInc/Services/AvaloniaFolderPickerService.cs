using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
namespace SharpCogsInc.Services;

public class AvaloniaFolderPickerService(Func<TopLevel?> getTopLevel) : IFolderPickerService
{
   public async Task<string?> PickFolderAsync(string title = "Select Folder")
   {
      var topLevel = getTopLevel();
      if (topLevel?.StorageProvider is not { } storageProvider)
      {
         return null;
      }

      var folders = await storageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
      {
         Title = title,
         AllowMultiple = false

      });


      return folders.Count > 0 ? folders[0].Path.LocalPath : null;
   }

   public async Task<string?> PickFileAsync(string title = "Select File")
   {
      var topLevel = getTopLevel();
      if (topLevel?.StorageProvider is not { } storageProvider)
      {
         return null;
      }

      var files = await storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
      {
         Title = title,
         AllowMultiple = false,
      });

      return files.Count > 0 ? files[0].Path.LocalPath : null;
   }
   
}
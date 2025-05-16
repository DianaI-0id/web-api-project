using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using Avalonia.ReactiveUI;
using Presence.Desktop.ViewModels;
using ReactiveUI;
using System.Linq;
using System.Threading.Tasks;

namespace Presence.Desktop;

public partial class GroupView : ReactiveUserControl<GroupViewModel>
{
    public GroupView()
    {
        this.WhenActivated(action =>
        {
            action(ViewModel!.SelectFileInteraction.RegisterHandler(ShowFileDialog));
        });
        AvaloniaXamlLoader.Load(this);
    }

    private async Task ShowFileDialog(InteractionContext<string?, string?> context)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        var storageFile = await topLevel.StorageProvider.OpenFilePickerAsync(
            new FilePickerOpenOptions()
            {
                AllowMultiple = false,
                Title = context.Input
            }
        );
        context.SetOutput(storageFile.First().Path.ToString());
    }
}
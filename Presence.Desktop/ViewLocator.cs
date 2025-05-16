using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Presence.Desktop.ViewModels;
using ReactiveUI;
using System;

namespace Presence.Desktop
{
    public class ViewLocator : IViewLocator
    {
        public IViewFor? ResolveView<T>(T? viewModel, string? contract = null) => viewModel switch
        {
            GroupViewModel groupViewModel => new GroupView { DataContext = groupViewModel },
            //PresenceViewModel presenceViewModel => new PresenceView { DataContext = presenceViewModel },
            _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
        };
    }
}

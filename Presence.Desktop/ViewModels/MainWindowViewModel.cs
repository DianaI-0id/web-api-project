using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;

namespace Presence.Desktop.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IScreen
    {
        public RoutingState Router { get; } = new RoutingState();

        public MainWindowViewModel(IServiceProvider serviceProvider)
        {
            var groupViewModel = serviceProvider.GetRequiredService<GroupViewModel>();
            Router.Navigate.Execute(groupViewModel);
        }
    }
}

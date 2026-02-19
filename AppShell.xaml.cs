using Microsoft.Extensions.DependencyInjection;
using SmartFinancePro.Views;

namespace SmartFinancePro;

public partial class AppShell : Shell
{
    public AppShell(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        var tabBar = new TabBar();

        tabBar.Items.Add(new ShellContent
        {
            Title = "Dashboard",
            Content = serviceProvider.GetRequiredService<DashboardPage>()
        });

        tabBar.Items.Add(new ShellContent
        {
            Title = "Transactions",
            Content = serviceProvider.GetRequiredService<TransactionsPage>()
        });

        tabBar.Items.Add(new ShellContent
        {
            Title = "Reports",
            Content = serviceProvider.GetRequiredService<ReportsPage>()
        });

        tabBar.Items.Add(new ShellContent
        {
            Title = "Settings",
            Content = serviceProvider.GetRequiredService<SettingsPage>()
        });

        Items.Add(tabBar);
    }
}

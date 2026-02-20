using Microsoft.Extensions.Logging;
using SmartFinancePro.Interfaces;
using SmartFinancePro.Services;
using SmartFinancePro.ViewModels;
using SmartFinancePro.Views;

namespace SmartFinancePro;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Services
		builder.Services.AddSingleton<IDatabaseService, DatabaseService>();

		// ViewModels
		builder.Services.AddSingleton<DashboardViewModel>();
		builder.Services.AddSingleton<TransactionsViewModel>();
		builder.Services.AddSingleton<ReportsViewModel>();
		builder.Services.AddSingleton<SettingsViewModel>();
		builder.Services.AddTransient<EditTransactionViewModel>();

		// Pages
		builder.Services.AddSingleton<DashboardPage>();
		builder.Services.AddSingleton<TransactionsPage>();
		builder.Services.AddSingleton<ReportsPage>();
		builder.Services.AddSingleton<SettingsPage>();
		builder.Services.AddTransient<EditTransactionPage>();

		// Shell
		builder.Services.AddSingleton<AppShell>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

using Avalonia.Controls;

namespace Voenkomat_Kursach.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private void OnDoctor(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var doctorWindow = new DoctorWindow();
        doctorWindow.Show();
        this.Close();
    }

    private void OnRegistrator(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var registrarWindow = new RegistratorWindow();
        registrarWindow.Show();
        this.Close();
    }
}
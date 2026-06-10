using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Voenkomat_Kursach.Views;

namespace Voenkomat_Kursach.ViewModels;

public abstract partial class ViewModelBase : ObservableObject
{
    
    protected IServiceProvider _sp;
    
    public ViewModelBase(IServiceProvider sp)
    {
        _sp = sp;
    }
    
    [RelayCommand]
    protected abstract void GoBack();
    
    protected void GoToMain(Window UsedWin)
    {
        
        var win = ActivatorUtilities.CreateInstance<MainWindow>(_sp);
        var vm = ActivatorUtilities.CreateInstance<MainWindowViewModel>(_sp);
        
        win.DataContext = vm;
        vm.SetWin(win);
        win.Position = UsedWin.Position;

        UsedWin.Close();
        win.Show();

    }
    
}
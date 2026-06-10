using System;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using Voenkomat_Kursach.Models;
using Voenkomat_Kursach.ViewModels;
using Voenkomat_Kursach.Views;

namespace Voenkomat_Kursach.Interfaces;

public interface IBack
{

    

    /* эксперименты
    public void GoToTarget<T1, T2>(IServiceProvider sp)
    {

        var win = ActivatorUtilities.CreateInstance<T1>(sp);
        var vm =  ActivatorUtilities.CreateInstance<T2>(sp);
        
        (win as Window).DataContext = vm;

        (win as Window).Show();

    }
    */

}
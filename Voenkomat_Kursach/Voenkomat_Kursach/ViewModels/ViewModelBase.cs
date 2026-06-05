using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Voenkomat_Kursach.ViewModels;

public abstract class ViewModelBase : ObservableObject
{
    
    protected IServiceProvider _sp;
    
    public ViewModelBase(IServiceProvider sp)
    {
        _sp = sp;
    }
    
}
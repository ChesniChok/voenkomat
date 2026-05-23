using System;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.ViewModels;

public class DoctorViewModel : UserBaseViewModel
{

    private IServiceProvider _sp;
    
    public DoctorViewModel(IServiceProvider sp, User user) : base(user)
    {
        _sp = sp;
    }
    
}
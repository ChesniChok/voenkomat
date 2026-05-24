using Avalonia.Controls;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.ViewModels;

public abstract class UserBaseViewModel : ViewModelBase
{
    
    protected User _user;
    protected Window _win;

    public UserBaseViewModel(User user, Window win)
    {
        _user = user;
        _win = win;
    }
    
}
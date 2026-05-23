using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.ViewModels;

public abstract class UserBaseViewModel : ViewModelBase
{
    
    private User _user;

    public UserBaseViewModel(User user)
    {
        _user = user;
    }
    
}
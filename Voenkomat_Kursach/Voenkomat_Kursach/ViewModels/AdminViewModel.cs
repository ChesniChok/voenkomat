using Avalonia.Controls;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.ViewModels;

public class AdminViewModel : UserBaseViewModel
{
    public AdminViewModel(User user, Window win) : base(user, win)
    {
    }
}
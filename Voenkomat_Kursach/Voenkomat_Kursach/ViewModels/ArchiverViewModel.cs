using Avalonia.Controls;
using Voenkomat_Kursach.Models;
using Voenkomat_Kursach.Views;

namespace Voenkomat_Kursach.ViewModels;

public class ArchiverViewModel : UserBaseViewModel
{
    public ArchiverViewModel(User user, Window win, MainWindow backWin) : base(user, win, backWin)
    {
    }
}
using Avalonia.Controls;
using Voenkomat_Kursach.Models;
using Voenkomat_Kursach.Views;

namespace Voenkomat_Kursach.ViewModels;

public abstract class UserBaseViewModel : ViewModelBase
{
    
    protected User _user;
    protected Window _win;
    protected Recruit _rec;
    protected MainWindow _backWin;

    public UserBaseViewModel(User user, Window win, MainWindow backWin)
    {
        _user = user;
        _win = win;
        _backWin = backWin;
    }

    public void SetRec(Recruit rec)
    {
        _rec = rec;
    }
    
    protected void GoBack()
    {
        
        _backWin.Position = _win.Position;
        
        (_backWin.DataContext as MainWindowViewModel).Start();
        _backWin.Show();
        
        _win.Close();
        
    }
    
}
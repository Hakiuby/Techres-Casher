using DevExpress.Mvvm;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;

namespace TechresStandaloneSale.ViewModels
{
    public class BaseViewModel : ViewModelBase, INotifyPropertyChanged, ISupportParameter
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       

    }

}



class RelayCommand<T> : ICommand
{
    private readonly Predicate<T> _canExecute;
    private readonly Action<T> _execute;

    public RelayCommand(Predicate<T> canExecute, Action<T> execute)
    {
        if (execute == null)
            throw new ArgumentNullException("execute");
        _canExecute = canExecute;
        _execute = execute;
    }




    public bool CanExecute(object parameter)
    {
        try
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }
        catch (Exception ex)
        {
            WriteLog.logs("Lỗi là:" + ex.Message);
            return true;
        }
    }

    public void Execute(object parameter)
    {
        _execute((T)parameter);
    }
    
    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }
    public void OnExecute(object parameter)
    {
        var values = (object[])parameter;
        var a = values[0];
        var b = (values[1]);
    }
}


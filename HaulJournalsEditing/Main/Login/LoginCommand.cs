using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace HaulJournalsEditing.Main.Login
{
    //Команда для окна Login. По введенным в окне данным ищет пользователя в БД.
    //Команде в качестве параметра передается PasswordBox, откуда берется текст пароля
    //(который нигде не хранится)
    public class LoginCommand : ICommand
    {
        private readonly LoginViewModel _loginViewModel;

        public LoginCommand(LoginViewModel loginViewModel)
        {
            _loginViewModel = loginViewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            string password = passwordBox?.Password;
            _loginViewModel.Login(password);
        }
    }
}

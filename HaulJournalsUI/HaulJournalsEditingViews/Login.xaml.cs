using System.Windows.Controls;
using System.Windows.Input;
using HaulJournalsEditing.Main.Login;
using MyClassLibrary;

namespace HaulJournalsUI.HaulJournalsEditingViews
{
    /// <summary>
    /// Форма авторизации.
    /// По нажатию кнопки идет поиск в БД пользователя с введенным в TextBox логином и 
    /// введенным в PasswordBox паролем, если нет такого пользователя, выведится сообщение, и 
    /// будет возможность повторить попытку. Можно отказаться от авторизации, закрыв окно.
    /// </summary>
    public partial class Login
    {
        private readonly LoginViewModel _loginViewModel;

        public Login()
        {
            InitializeComponent();

            _loginViewModel = new LoginViewModel();
            DataContext = _loginViewModel;
            //При успешной авторизации закрытие формы
            LoginViewModel.OnLogin += (username, password) =>
            {
                Close();
            };
        }

        private void Grid_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //Отправить фокус на следующий элемент
                if (e.Source is TextBox)
                {
                    MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }
                //Вызов команды авторизации, если в поле пароля нажали Enter
                if (e.Source is PasswordBox p)
                {
                    _loginViewModel.LoginCommand.Execute(p);
                }
                e.Handled = true;
            }
        }
    }
}

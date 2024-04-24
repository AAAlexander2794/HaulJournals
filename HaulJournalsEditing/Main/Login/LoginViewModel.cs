using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using HaulJournalsDAL;
using HaulJournalsDAL.Concrete;
using HaulJournalsDAL.Properties;

namespace HaulJournalsEditing.Main.Login
{
    /// <summary>
    /// Модель-представление авторизации
    /// </summary>
    /// <remarks>
    /// Модель-представление сделана с намерением оставить программу в рамках MVVM,
    /// данная модель-представление предназначена для использование в классе, реализующем форму авторизации,
    /// которая хранится в проекте пользовательских интерфейсов и не видна другим моделям-представлениям, 
    /// которые хотели бы знать о событии авторизации.
    /// Сам процесс авторизации здесь никак не определен, кроме как некоторое событие, передающее 
    /// логин и пароль.
    ///  
    /// Принцип работы:
    /// Если какому-либо классу необходимо что-то сделать при событии авторизации,
    /// то он может подписать свой метод к событию <see cref="OnLogin"/>.
    /// </remarks>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="LoginCommand"/>
    public sealed class LoginViewModel : INotifyPropertyChanged
    {    
        /// <summary>
        /// Событие авторизации, передает логин и пароль, введенные пользователем
        /// </summary>
        public static event Action<string, string> OnLogin;

        //Имя пользователя, введенное в данный момент для этой VM
        private string _username;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        //Команда запуска процесса авторизации
        public LoginCommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new LoginCommand(this);
        }

        /// <summary>
        /// Запускает событие авторизации
        /// </summary>
        /// <param name="password">The password.</param>
        public void Login(string password)
        {
            try
            {
                OnLogin?.Invoke(Username, password);
            }
            catch (Exception e)
            {
                MessageBox.Show(MyClassLibrary.ExceptionMessagesHandler.GetMessages(e));
            }
            
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}

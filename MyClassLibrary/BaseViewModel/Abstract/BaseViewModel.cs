using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using MyClassLibrary.BaseViewModel.Commands;
using MyClassLibrary.BaseViewModel.Enums;
using MyClassLibrary.Properties;

namespace MyClassLibrary.BaseViewModel.Abstract
{
    /// <summary>
    /// Класс, предоставляющий логику работы с формой/коллекцией данных.
    /// 
    /// Как пользоваться:
    /// Controls привязываются к текущему элементу, коллекция привязывается
    /// к выбранному элементу.
    /// Свойства IsEnabled формы и коллекции привязываются к соответствующим 
    /// bool свойствам этого класса.
    /// 
    /// Что происходит:
    /// При переключении в режим создания нового элемента текущий элемент 
    /// привязывается к новому элементу, становится активна форма.
    /// При переключении в режим выбора текущий элемент привязывается к выбранному элементу 
    /// (из коллекции), становится активна коллекция.
    /// При переключении в режим редактирования элемента текущий элемент привязывается
    /// к выбранному элементу, становится активна форма.
    /// </summary>
    /// <typeparam name="T">Класс элементов в главной коллекции</typeparam>
    /// <typeparam name="TP">Класс родительского элемента, по которому фильтруется коллекция</typeparam>
    public abstract class BaseViewModel<T, TP> : INotifyPropertyChanged
    {
        protected BaseViewModel()
        {
            Commands = new Commands<T, TP>(this);
            _collection = new ObservableCollection<T>();
        }

        private TP _parentItem;

        /// <summary>
        /// Элемент, по которому должны фильтроваться данные для показа и редактирования
        /// </summary>
        public TP ParentItem
        {
            get => _parentItem;
            set
            {
                _parentItem = value;
                CollectionChangedInvoke();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Базовые команды
        /// </summary>
        public Commands<T, TP> Commands { get; }

        /// <summary>
        /// Вызывается при изменении <see cref="Collection"/> 
        /// (не только при изменении ссылки, но и при работе с 
        /// элементами коллекции)
        /// </summary>
        public event Action CollectionChanged;

        /// <summary>
        /// Вызывает срабатывание события изменения коллекции, чтобы можно было 
        /// вызвать из других классов
        /// </summary>
        public void CollectionChangedInvoke()
        {
            CollectionChanged?.Invoke();
        }

        private ObservableCollection<T> _collection;

        /// <summary>
        /// Главная коллекция элементов
        /// </summary>
        public ObservableCollection<T> Collection
        {
            get => _collection;
            set
            {
                _collection = value;
                CollectionChangedInvoke();
                OnPropertyChanged();
            }
        }


        private T _selectedItem;

        /// <summary>
        /// Выбранный элемент коллекции
        /// </summary>
        public T SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                SelectedItemChanged?.Invoke(_selectedItem);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Событие изменения <see cref="SelectedItem"/>
        /// </summary>
        public event Action<T> SelectedItemChanged;

        private T _newItem;

        /// <summary>
        /// Новый элемент, который в дальнейшем добавится в коллекцию
        /// </summary>
        public T NewItem
        {
            get => _newItem;
            set
            {
                _newItem = value;
                OnPropertyChanged();
            }
        }

        //Свойство, к которому привязывается IsEnabled коллекции (например, DataGrid.IsEnabled)
        private bool _collectionIsEnabled;

        public bool CollectionIsEnabled
        {
            get => _collectionIsEnabled;
            set
            {
                _collectionIsEnabled = value;
                OnPropertyChanged();
            }
        }

        //Свойство, к которому привязывается IsEnabled формы (например, Grid.IsEnabled, 
        //на котором расположены все controls формы)
        private bool _formIsEnabled;

        public bool FormIsEnabled
        {
            get => _formIsEnabled;
            set
            {
                _formIsEnabled = value;
                OnPropertyChanged();
            }
        }

        #region Abstract

        //Фильтр для отображения коллекции
        public abstract void FilterCollection(object sender, FilterEventArgs e);

        ///// <summary>
        ///// Сохранение изменений в хранилище (БД или файл)
        ///// </summary>
        //public abstract void SaveChanges();

        ////Обновление коллекций из репозитория
        //public abstract void Refresh();

        ///// <summary>
        ///// Обновление коллекций из хранилища (БД или файла)
        ///// </summary>
        //public abstract void Reload();

        /// <summary>
        /// Создать новый элемент
        /// </summary>
        public abstract void CreateNewItem();

        ///// <summary>
        ///// Добавить созданный элемент в коллекцию
        ///// </summary>
        //public abstract void AddCreatedItem();

        ///// <summary>
        ///// Удалить выбранный элемент
        ///// </summary>
        //public abstract void DeleteSelectedItem();

        ////Подтвердить редактирование элемента (выбранного)
        //public abstract void ConfirmEditingItem();

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

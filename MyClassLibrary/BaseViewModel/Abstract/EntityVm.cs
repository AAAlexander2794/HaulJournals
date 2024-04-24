using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using MyClassLibrary.Annotations;
using MyClassLibrary.BaseViewModel.Concrete;

namespace MyClassLibrary.BaseViewModel.Abstract
{
    /// <summary>
    /// Базовый класс RowViewModel сущности.
    /// Содержит инкапсулированную сущность и ее IEntity.EntityState
    /// </summary>
    /// <typeparam name="T">Инкапсулированная сущность</typeparam>
    public abstract class EntityVm<T> : IEntity, INotifyPropertyChanged where T : new()
    {
        private EntityState _entityState;

        public EntityState EntityState
        {
            get => _entityState;
            set
            {
                _entityState = value;
                OnPropertyChanged();
            }
        }

        protected readonly T _substance;

        public T Substance => _substance;



        protected EntityVm()
        {
            _substance = new T();
            //Привязываем свойство EntityState инкапсулированной сущности к открытому свойству
            _entityState = ((IEntity) _substance).EntityState;
            //Привязываем метод обработки EntityState к событию изменения свойств
            PropertyChanged += EntityPropertyChangedHandler.ProcessEntity;
        }

        protected EntityVm(T substance) : this()
        {
            _substance = substance;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

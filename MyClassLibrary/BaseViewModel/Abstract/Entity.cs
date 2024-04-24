using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Reflection;
using System.Runtime.CompilerServices;
using MyClassLibrary.BaseViewModel.Concrete;
using MyClassLibrary.Properties;

namespace MyClassLibrary.BaseViewModel.Abstract
{
    /// <summary>
    /// </summary>
    public abstract class Entity : INotifyPropertyChanged, IEntity
    {
        private EntityState _entityState;

        [NotMapped]
        public EntityState EntityState
        {
            get => _entityState;
            set
            {
                _entityState = value;
                OnPropertyChanged();
            }
        }

        protected Entity()
        {
            //Привязываем метод обработки EntityState к событию изменения свойств
            PropertyChanged += EntityPropertyChangedHandler.ProcessEntity;
        }

        /// <summary>
        /// Устанавливает все навигационные свойства сущности в null
        /// (кроме зависимых). Нужно, чтобы при добавлении чисто журнальных данных 
        /// не аттачить справочники к контексту
        /// </summary>
        public virtual void SetNavigationPropertiesToNull()
        {
            
        }

        /// <summary>
        /// Очищает все коллекции сущности
        /// </summary>
        public virtual void ClearCollections()
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

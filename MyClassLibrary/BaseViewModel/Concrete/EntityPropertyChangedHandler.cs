using System;
using System.ComponentModel;
using System.Data.Entity;
using MyClassLibrary.BaseViewModel.Abstract;

namespace MyClassLibrary.BaseViewModel.Concrete
{
    public static class EntityPropertyChangedHandler
    {
        public static void ProcessEntity(object sender, PropertyChangedEventArgs args)
        {
            if (!(sender is IEntity entity)) throw new Exception("Изменение EntityState невозможно, sender не является IEntity.");
            //Если изменяется само свойство EntityState, то не реагировать
            if (args.PropertyName == "EntityState") return;
            //Если сущность числится как добавленная, удаленная или неотслеживаемая, то статус не менять
            if (entity.EntityState == EntityState.Added
                || entity.EntityState == EntityState.Deleted
                || entity.EntityState == EntityState.Detached) return;
            //Отметить сущность как измененную
            entity.EntityState = EntityState.Modified;
        }
    }
}

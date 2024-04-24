using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClassLibrary.BaseViewModel.Abstract;

namespace MyClassLibrary.BaseViewModel.Commands
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TVm">Класс элементов основной коллекции</typeparam>
    /// <typeparam name="T">Класс родительского элемента</typeparam>
    public class Commands<TVm, T>
    {
        public AddEmptyCommand<TVm, T> AddEmptyItem { get; }

        public DeleteCommand<TVm, T> Delete { get; }

        //public SaveChanges<TVm, T> SaveChanges { get; }

        //public Reload<TVm, T> Reload { get; }

        public Commands(BaseViewModel<TVm, T> viewModel)
        {
            AddEmptyItem = new AddEmptyCommand<TVm, T>(viewModel);
            Delete = new DeleteCommand<TVm, T>(viewModel);
            //SaveChanges = new SaveChanges<TVm, T>(viewModel);
            //Reload = new Reload<TVm, T>(viewModel);
        }
    }
}

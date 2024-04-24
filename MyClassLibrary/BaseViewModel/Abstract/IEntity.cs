using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.BaseViewModel.Abstract
{
    public interface IEntity
    {
        EntityState EntityState { get; set; }
    }
}

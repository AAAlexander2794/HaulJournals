namespace MyClassLibrary.BaseViewModel.Enums
{
    /// <summary>
    /// Виды процессов в соответствии с CRUD
    /// (Create, Read, Update, Delete),
    /// но измененные в соответствие с состояниями работы с
    /// коллекцией элементов и формой данных одного элемента
    /// </summary>
    public enum CrudStatus  
    {
        //Статус неизвествен
        Null,
        //Создание элемента
        Create,
        //Выбор элемента из коллекции
        Select,
        //Редактирование выбранного из коллекции элемента
        Update
    }
}

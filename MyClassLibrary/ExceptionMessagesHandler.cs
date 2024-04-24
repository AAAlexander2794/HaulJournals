using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary
{
    /// <summary>
    /// Класс, содержащий статические методы различной обработки исключений
    /// (например, возвратить все сообщения внутренних исключений в одной строке)
    /// </summary>
    public static class ExceptionMessagesHandler
    {
        /// <summary>
        /// Метод собирает все сообщения исключения в одну строку
        /// </summary>
        /// <param name="e">Исключение, из которого надо возвратить все сообщения,
        /// включая сообщения внутренних исключений</param>
        /// <returns>Строка с текстом главного и всех внутренних исключений</returns> 
        public static string GetMessages(Exception e)
        {
            string info = e.Message;
            Exception excp = e;
            int i = 1;
            while (excp != null)
            {
                info += $"\n\nИсключение {i}:\n{excp.Message}";
                i++;
                excp = excp.InnerException;
            }
            return info;
        }
    }
}

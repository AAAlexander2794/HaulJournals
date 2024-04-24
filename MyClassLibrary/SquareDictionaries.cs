using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary
{
    //Словари координат квадратов (сейчас только один - для Стандартного разделения Азовского моря)
    public static class SquareDictionaries
    {
        public static class AzovSea
        {
            public static class Standart
            {
                //Буквы, используемые для обозначения квадратов в картах
                
                private static readonly string[] _arrayChars = {"а", "б", "в", "г", "д", "е", "ж", "з", "и", "к", "л", "м", "н", "о", "п", "р",
                    "с", "т", "у", "ф", "х", "ц", "ч", "щ"};

                //Стандартное разделение Азовского моря

                public static void GetCoords(string square, out decimal n, out decimal e)
                {
                    //Север - 47,25; На юг - на уменьшение
                    //Запад - 34,75; На восток - на увеличение
                    //Ширина квадрата - 5', то есть 1/12 градуса

                    //Буква квадрата
                    var squareChar = square[2];
                    //Две цифры квадрата
                    var squareDigit = Int32.Parse(square[0].ToString() + square[1].ToString());
                    //Широта
                    n = 47.25m - 1 / 24m;
                    for (var i = 0; i < _arrayChars.Length; i++)
                    {
                        if (squareChar.ToString() == _arrayChars[i])
                            break;
                        n = n - 1 / 12m;
                    }
                    //Долгота
                    e = 34.75m + 1 / 24m;
                    //ЦИФРЫ: от 1 по 56 включительно
                    for (var i = 1; i < 57; i++)
                    {
                        if (squareDigit == i)
                            break;
                        e = e + 1 / 12m;
                    }
                }
            }
        }
    }
}

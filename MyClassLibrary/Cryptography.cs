using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary
{
    /// <summary>
    /// Класс с методами, шифрующими данные
    /// </summary>
    public static class Cryptography
    {

        public static string EncryptString(string text, string key)
        {
            string outputText = null;

            Rijndael crypto = Rijndael.Create();

            crypto.IV = Encoding.ASCII.GetBytes("UserName".PadRight(16, 'x'));
            crypto.Key = Encoding.ASCII.GetBytes(key.PadRight(16, 'x'));
            crypto.Padding = PaddingMode.Zeros;

            using (var memoryStream = new MemoryStream())
            {
                //Подключаем криптопоток к потоку, хранящемся в памяти
                using (CryptoStream cryptoStream =
                    new CryptoStream(memoryStream, crypto.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    //Записываем строку в криптопоток с помощью записывающего потока
                    using (var streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(text);
                    }
                    cryptoStream.Flush();
                    cryptoStream.Close(); 
                }
                using (var streamReader = new StreamReader(memoryStream))
                {
                    outputText = streamReader.ReadToEnd();
                }
            }
            return outputText;
        }

        public static string DecryptString(string text, string key)
        {
            string outputText = null;

            Rijndael crypto = Rijndael.Create();

            crypto.IV = Encoding.ASCII.GetBytes("UserName".PadRight(16, 'x'));
            crypto.Key = Encoding.ASCII.GetBytes(key.PadRight(16, 'x'));
            crypto.Padding = PaddingMode.Zeros;

            using (var memoryStream = new MemoryStream())
            {
                //Записываем в поток память строку
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    streamWriter.Write(text);
                }
                //Подключаем криптопоток к потоку, хранящемся в памяти
                using (CryptoStream cryptoStream =
                    new CryptoStream(memoryStream, crypto.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (var streamReader = new StreamReader(cryptoStream))
                    {
                        outputText = streamReader.ReadToEnd();
                    }
                    cryptoStream.Flush();
                    cryptoStream.Close();
                }
            }
            return outputText;
        }

        //Шифрует данные из DataTable с помощью ключа key и сохраняет в xml-формате в файл file
        public static void EncryptDataTable(string file, DataTable dt, string key)
        {
            Rijndael crypto = Rijndael.Create();

            crypto.IV = Encoding.ASCII.GetBytes("UserName".PadRight(16, 'x'));
            crypto.Key = Encoding.ASCII.GetBytes(key.PadRight(16, 'x'));
            crypto.Padding = PaddingMode.Zeros;


            using (FileStream stream = new FileStream(file, FileMode.Create))
            {
                using (CryptoStream cryptoStream = new CryptoStream(stream, crypto.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    dt.WriteXml(cryptoStream);
                    cryptoStream.Flush();
                    stream.Flush();
                    cryptoStream.Close();
                    stream.Close();
                }
            }
        }

        //Расшифровывает данные из xml-файла file с помощью ключа key и сохраняет их в таблицу DataTable
        public static void DecryptDataTable(string file, DataTable dt, string key)
        {
            Rijndael crypto = Rijndael.Create();

            crypto.IV = Encoding.ASCII.GetBytes("UserName".PadRight(16, 'x'));
            crypto.Key = Encoding.ASCII.GetBytes(key.PadRight(16, 'x'));
            crypto.Padding = PaddingMode.Zeros;

            using (FileStream stream = new FileStream(file, FileMode.Open))
            {
                using (CryptoStream cryptoStream = new CryptoStream(stream, crypto.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    dt.ReadXml(cryptoStream);
                    cryptoStream.Close();
                    stream.Close();
                }
            }
        }
    }
}

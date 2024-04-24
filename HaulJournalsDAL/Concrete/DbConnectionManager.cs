using System;
using System.Configuration;
using System.Linq;
using System.Net.NetworkInformation;
using HaulJournalsDAL.Concrete;

namespace HaulJournalsDAL.Concrete
{
    /// <summary>
    /// Типы подключений
    /// </summary>
    public enum ConnectionTypes
    {
        /// <summary>
        /// Локальное подключение
        /// </summary>
        Local,
        /// <summary>
        /// Удаленное подключение
        /// </summary>
        Remote
    }

    /// <summary>
    /// Менеджер подключений к БД
    /// </summary>
    public static class DbConnectionManager
    {
        /// <summary>
        /// Внутренний IP для подключения к БД
        /// </summary>
        public static string InnerIp { get; }

        /// <summary>
        /// Внешний IP для подключения к БД
        /// </summary>
        public static string OuterIp { get; }

        /// <summary>
        /// Имя параметра в App.config, в котором хранится информация
        /// о том, по какому IP (внешнему или внутреннему) было 
        /// произведено подключение в прошлый раз.
        /// True - подключение через внутренний IP
        /// False - подключение через внешний IP
        /// Null - статус неизвестен
        /// </summary>
        private static string ConnectionParameterName { get; }

        /// <summary>
        /// Имя строки подключения к БД через внутренний IP
        /// </summary>
        private static string NameOfConnectionStringInner { get; }

        /// <summary>
        /// Имя строки подключения к БД через внешний IP
        /// </summary>
        private static string NameOfConnectionStringOuter { get; }

        /// <summary>
        /// Имя строки подключения к локальной БД
        /// </summary>
        private static string NameOfConnectionStringLocal { get; }

        static DbConnectionManager()
        {
            InnerIp = "213.241.204.105";
            OuterIp = "213.241.204.105";
            ConnectionParameterName = "ConnectionIsInner";
            NameOfConnectionStringInner = "HaulJournalsDbInner";
            NameOfConnectionStringOuter = "HaulJournalsDbOuter";
            NameOfConnectionStringLocal = "HaulJournalsDAL.Properties.Settings.HaulJournalsMySQLConnectionString";
        }

        /// <summary>
        /// Создает подключение к локальной или удаленной БД
        /// в зависимости от параметра
        /// </summary>
        /// <param name="connectionType">
        /// "local" - вернуть локальный контекст данных;
        /// null - возвращает <see cref="CreateRemoteConnection"/>;
        /// default - возвращает исключение.
        /// </param>
        /// <returns>Контекст данных</returns>
        public static HaulJournalsModel CreateConnection(ConnectionTypes connectionType)
        {
            switch (connectionType)
            {
                case ConnectionTypes.Local:
                    var ctx = new HaulJournalsModel(NameOfConnectionStringLocal);
                    return ctx;
                case ConnectionTypes.Remote:
                    return CreateRemoteConnection();
                default:
                    throw new Exception("Непредусмотренный тип соединения");
            }
        }

        /// <summary>
        /// Создание подключения к БД с использованием одного из двух IP (внешний и внутренный)
        /// </summary>
        /// <returns></returns>
        private static HaulJournalsModel CreateRemoteConnection()
        {
            //Проверяем, пингуется ли какой либо из IP
            var connectionIsInner = CheckOrCreateParameter(ConnectionParameterName);
            //Если соединение есть
            if (!connectionIsInner.HasValue)
                throw new Exception("Ни одно из подключений не прошло успешно. \n" +
                                    "Проверьте наличие подключения к Интернету.");
            //Соединилось по внутреннему IP
            if (connectionIsInner.Value)
            {
                var ctx = new HaulJournalsModel(NameOfConnectionStringInner);
                //Проверяем наличие нужной базы данных
                if (ctx.Database.Exists()) return ctx;
            }
            //Если соединилось по внешнему IP
            if (!connectionIsInner.Value)
            {
                var ctx = new HaulJournalsModel(NameOfConnectionStringOuter);
                //Проверяем наличие нужной базы данных
                if (ctx.Database.Exists()) return ctx;
            }
            throw new Exception("База данных не была найдена.");
        }

        /// <summary>
        /// Ищем параметр в конфиге и проверяем его актуальность и исправляем, если что.
        /// Указывает, какое подключение использовалось ранее, внутреннее или внешнее
        /// </summary>
        /// <param name="nameOfParameter">
        /// Имя проверяемого параметра (ожидается <see cref="ConnectionParameterName"/>) 
        /// </param>
        /// <returns>
        /// true - подключение по локальному IP;
        /// false - подключение по внешнему IP;
        /// null - обе попытки подключения не увенчались успехом
        /// </returns>
        private static bool? CheckOrCreateParameter(string nameOfParameter)
        {
            if (!ConfigurationManager.AppSettings.AllKeys.Contains(nameOfParameter))
            {
                // открываем текущий конфиг специальным обьектом
                System.Configuration.Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                // добавляем позицию в раздел AppSettings
                currentConfig.AppSettings.Settings.Add(nameOfParameter, "");
                //сохраняем
                currentConfig.Save(ConfigurationSaveMode.Full);
                //принудительно перезагружаем соотвествующую секцию
                ConfigurationManager.RefreshSection("appSettings");
            }
            //Если в конфиге запомнено, что соединение по внутреннему IP
            if (ConfigurationManager.AppSettings[nameOfParameter] == "true")
            {
                //Проверяем соединение по внутреннему IP
                if (PingConnection(InnerIp))
                {
                    return true;
                }
                //Если по внутреннему не соединилось, проверяем по внешнему
                if (PingConnection(OuterIp))
                {
                    SetParameter(nameOfParameter, "false");
                    return false;
                }
            }
            //Если в конфиге запомнено, что соединение по внешнему IP
            if (ConfigurationManager.AppSettings[nameOfParameter] == "false")
            {
                //Проверяем по внешнему IP
                if (PingConnection(OuterIp))
                {
                    return false;
                }
                //Проверяем соединение по внутреннему IP
                if (PingConnection(InnerIp))
                {
                    SetParameter(nameOfParameter, "true");
                    return true;
                }
            }
            //Если не известно, какое подключение было успешным в прошлый запуск
            //Проверяем соединение по внутреннему IP
            if (PingConnection(InnerIp))
            {
                SetParameter(nameOfParameter, "true");
                return true;
            }
            //Проверяем по внешнему IP
            if (PingConnection(OuterIp))
            {
                SetParameter(nameOfParameter, "false");
                return false;
            }
            //Если ни одно из подключений не прошло
            return null;
        }

        /// <summary>
        /// Запись нового значение параметра в файле конфига
        /// </summary>
        /// <param name="nameOfParameter">Имя параметра в конфиге</param>
        /// <param name="newValue">Новое значение параметра</param>
        private static void SetParameter(string nameOfParameter, string newValue)
        {
            System.Configuration.Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            currentConfig.AppSettings.Settings[nameOfParameter].Value = newValue;
            currentConfig.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// Пингуем IP (~ 5 sec)
        /// </summary>
        /// <param name="ip">IP, который надо пропинговать</param>
        /// <returns>
        /// Результат проверки 
        /// </returns>
        public static bool PingConnection(string ip)
        {
            Ping pingSender = new Ping();
            try
            {
                PingReply reply = pingSender.Send(ip);
                return reply != null && reply.Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }
    }
}

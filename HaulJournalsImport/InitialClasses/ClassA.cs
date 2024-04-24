using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaulJournalsImport.InitialClasses
{
    /// <summary>
    /// Строка из таблицы с префиксом "a"
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ClassA
    {
        /// <summary>
        /// Номер формы (аттавизм)
        /// </summary>
        /// <value>
        /// The cform.
        /// </value>
        public string CFORM { get; set; }

        /// <summary>
        /// Код орудия лова
        /// </summary>
        /// <value>
        /// The corl.
        /// </value>
        public string CORL { get; set; }

        /// <summary>
        /// Дата начала рейса
        /// </summary>
        /// <value>
        /// The ndatar.
        /// </value>
        public string NDATAR { get; set; }

        /// <summary>
        /// Дата окончания рейса
        /// </summary>
        /// <value>
        /// The kdatar.
        /// </value>
        public string KDATAR { get; set; }

        /// <summary>
        /// Номер рейса
        /// </summary>
        /// <value>
        /// The nr.
        /// </value>
        public string NR { get; set; }

        public string NG { get; set; }

        /// <summary>
        /// Номер судна
        /// </summary>
        /// <value>
        /// The nsud.
        /// </value>
        public string NSUD { get; set; }

        /// <summary>
        /// Скорость раскрытия трала
        /// </summary>
        /// <value>
        /// The skor.
        /// </value>
        public string SKOR { get; set; }

        /// <summary>
        /// Время траления
        /// </summary>
        /// <value>
        /// The tim.
        /// </value>
        public string TIM { get; set; }

        /// <summary>
        /// Площадь облова
        /// </summary>
        /// <value>
        /// The squa.
        /// </value>
        public string SQUA { get; set; }

        /// <summary>
        /// Номер станции
        /// </summary>
        /// <value>
        /// The NST.
        /// </value>
        public string NST { get; set; }

        /// <summary>
        /// Квадрат лова
        /// </summary>
        /// <value>
        /// The KVL.
        /// </value>
        public string KVL { get; set; }

        /// <summary>
        /// Дата станции
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public string DATA { get; set; }

        /// <summary>
        /// Начало станции (часы)
        /// </summary>
        /// <value>
        /// The NCH.
        /// </value>
        public string NCH { get; set; }

        /// <summary>
        /// Начало станции (минуты)
        /// </summary>
        /// <value>
        /// The nmin.
        /// </value>
        public string NMIN { get; set; }

        /// <summary>
        /// Конец станции (часы)
        /// </summary>
        /// <value>
        /// The KCH.
        /// </value>
        public string KCH { get; set; }

        /// <summary>
        /// Конец станции (минуты)
        /// </summary>
        /// <value>
        /// The kmin.
        /// </value>
        public string KMIN { get; set; }

        /// <summary>
        /// Глубина
        /// </summary>
        /// <value>
        /// The glub.
        /// </value>
        public string GLUB { get; set; }

        /// <summary>
        /// Код грунта
        /// </summary>
        /// <value>
        /// The grunt.
        /// </value>
        public string GRUNT { get; set; }

        /// <summary>
        /// Температура воздуха
        /// </summary>
        /// <value>
        /// The tvozd.
        /// </value>
        public string TVOZD { get; set; }

        /// <summary>
        /// Направление ветра
        /// </summary>
        /// <value>
        /// The naprv.
        /// </value>
        public string NAPRV { get; set; }

        /// <summary>
        /// Сила ветра (в баллах)
        /// </summary>
        /// <value>
        /// The silav.
        /// </value>
        public string SILAV { get; set; }

        /// <summary>
        /// Волнение (в баллах)
        /// </summary>
        /// <value>
        /// The voln.
        /// </value>
        public string VOLN { get; set; }

        /// <summary>
        /// Температура воды на поверхности
        /// </summary>
        /// <value>
        /// The tvodp.
        /// </value>
        public string TVODP { get; set; }

        /// <summary>
        /// Температура воды на дне
        /// </summary>
        /// <value>
        /// The tvodd.
        /// </value>
        public string TVODD { get; set; }

        /// <summary>
        /// Прозрачность воды
        /// </summary>
        /// <value>
        /// The prozr.
        /// </value>
        public string PROZR { get; set; }

        /// <summary>
        /// Длина раскрытия трала
        /// </summary>
        /// <value>
        /// The dlrarskr.
        /// </value>
        public string DLRARSKR { get; set; }

        /// <summary>
        /// Ед. изм. улова (гр, кг)
        /// </summary>
        /// <value>
        /// The edanv.
        /// </value>
        public string EDANV { get; set; }

        /// <summary>
        /// Коэффициент пересчета (видимо как кол-во проб)
        /// </summary>
        /// <value>
        /// The KPCH.
        /// </value>
        public string KPCH { get; set; }

        /// <summary>
        /// Код рыбы
        /// </summary>
        /// <value>
        /// The kodan.
        /// </value>
        public string KODAN { get; set; }

        /// <summary>
        /// 2 - улов;
        /// 1 - заполнить <see cref="KPCH"/>
        /// </summary>
        /// <value>
        /// The kpu.
        /// </value>
        public string KPU { get; set; }

        /// <summary>
        /// Вес выловленной рыбы
        /// </summary>
        /// <value>
        /// The vesan.
        /// </value>
        public string VESAN { get; set; }

        /// <summary>
        /// Количество выловленной рыбы
        /// </summary>
        /// <value>
        /// The shtan.
        /// </value>
        public string SHTAN { get; set; }

        /// <summary>
        /// Расчетный вес = <see cref="KPCH"/>*<see cref="VESAN"/>
        /// </summary>
        /// <value>
        /// The rvesan.
        /// </value>
        public string RVESAN { get; set; }

        /// <summary>
        /// Расчетное количество = <see cref="KPCH"/>*<see cref="SHTAN"/>
        /// </summary>
        /// <value>
        /// The rshtan.
        /// </value>
        public string RSHTAN { get; set; }
    }
}

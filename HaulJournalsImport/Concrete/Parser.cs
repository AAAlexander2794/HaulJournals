using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaulJournalsDAL.Entities;
using HaulJournalsImport.InitialClasses;

namespace HaulJournalsImport.Concrete
{
    /// <summary>
    /// Парсит старые данные из программы на FoxPro в файлах .xlsx в сущности для БД
    /// </summary>
    /// <remarks>
    /// Порядок действий:
    /// 1. 
    /// <see cref="ParseDataViewA"/>
    /// <see cref="ParseDataViewB"/>
    /// <see cref="ParseDataViewP"/>
    /// <see cref="ParseDataViewV"/>
    /// 2.
    /// <see cref="ParseClassAList"/>
    /// <see cref="ParseClassBList"/>
    /// <see cref="ParseClassPList"/>
    /// <see cref="ParseClassVList"/>
    /// 3.
    /// <see cref="UniteClassEntities"/>
    /// </remarks>
    public static class Parser
    {
        #region Методы парсинга из датасета в первичные классы

        public static List<ClassA> ParseDataViewA(DataView dataViewA)
        {
            List<ClassA> result = new List<ClassA>();
            foreach (DataRowView rowView in dataViewA)
            {
                //Достаем из вьюшки обычную строку, которую можно обработать
                DataRow row = rowView.Row;
                //Создаем класс "а" на основе строки
                var classA = new ClassA()
                {
                    CFORM = row[0].ToString(),
                    CORL = row[1].ToString(),
                    NDATAR = row[2].ToString(),
                    KDATAR = row[3].ToString(),
                    NR = row[4].ToString(),
                    //NG = row[5].ToString(),
                    NSUD = row[5].ToString(),
                    SKOR = row[6].ToString(),
                    TIM = row[7].ToString(),
                    SQUA = row[8].ToString(),
                    NST = row[9].ToString(),
                    KVL = row[10].ToString(),
                    DATA = row[11].ToString(),
                    NCH = row[12].ToString(),
                    NMIN = row[13].ToString(),
                    KCH = row[14].ToString(),
                    KMIN = row[15].ToString(),
                    GLUB = row[16].ToString(),
                    GRUNT = row[17].ToString(),
                    TVOZD = row[18].ToString(),
                    NAPRV = row[19].ToString(),
                    SILAV = row[20].ToString(),
                    VOLN = row[21].ToString(),
                    TVODP = row[22].ToString(),
                    TVODD = row[23].ToString(),
                    PROZR = row[24].ToString(),
                    DLRARSKR = row[25].ToString(),
                    EDANV = row[26].ToString(),
                    KPCH = row[27].ToString(),
                    KODAN = row[28].ToString(),
                    KPU = row[29].ToString(),
                    VESAN = row[30].ToString(),
                    SHTAN = row[31].ToString(),
                    RVESAN = row[32].ToString(),
                    RSHTAN = row[33].ToString()
                };
                //Проверка логических правил
                if (string.IsNullOrWhiteSpace(classA.VESAN) && string.IsNullOrWhiteSpace(classA.SHTAN))
                {
                    throw new Exception($"Вес и кол-во пусты");
                }
                //Добавляем новый экземпляр
                result.Add(classA);
            }
            return result;
        }

        public static List<ClassB> ParseDataViewB(DataView dataViewB)
        {
            List<ClassB> result = new List<ClassB>();
            foreach (DataRowView rowView in dataViewB)
            {
                DataRow row = rowView.Row;
                result.Add(new ClassB()
                {
                    CFORM = row[0].ToString(),
                    CORL = row[1].ToString(),
                    NDATAR = row[2].ToString(),
                    KDATAR = row[3].ToString(),
                    NR = row[4].ToString(),
                    //NG = row[5].ToString(),
                    NSUD = row[5].ToString(),
                    NST = row[6].ToString(),
                    KVL = row[7].ToString(),
                    DATA = row[8].ToString(),
                    KODBA = row[9].ToString(),
                    NOMR = row[10].ToString(),
                    DLINA = row[11].ToString(),
                    DLINA1 = row[12].ToString(),
                    VESBA = row[13].ToString(),
                    POL = row[14].ToString(),
                    ZREL = row[15].ToString(),
                    GIR = row[16].ToString(),
                    KISHN = row[17].ToString(),
                    NORM = row[18].ToString(),
                    NORMPR = row[19].ToString(),
                    GEL1GR = row[20].ToString(),
                    GEL2GR = row[21].ToString(),
                    GEL3GR = row[22].ToString(),
                    KISHGR = row[23].ToString(),
                    WOZR = row[24].ToString(),
                    ZREL1 = row[25].ToString(),
                    ZREL2 = row[26].ToString(),
                    ZREL3 = row[27].ToString(),
                    ZREL4 = row[28].ToString(),
                    ZREL5 = row[29].ToString()
                });
            }
            return result;
        }

        public static List<ClassP> ParseDataViewP(DataView dataViewP)
        {
            List<ClassP> result = new List<ClassP>();
            foreach (DataRowView rowView in dataViewP)
            {
                DataRow row = rowView.Row;
                result.Add(new ClassP()
                {
                    CFORM = row[0].ToString(),
                    CORL = row[1].ToString(),
                    NDATAR = row[2].ToString(),
                    KDATAR = row[3].ToString(),
                    NR = row[4].ToString(),
                    //NG = row[5].ToString(),
                    NSUD = row[5].ToString(),
                    NST = row[6].ToString(),
                    KVL = row[7].ToString(),
                    DATA = row[8].ToString(),
                    EDPD = row[9].ToString(),
                    EDPV = row[10].ToString(),
                    KODP = row[11].ToString(),
                    DLINAP1 = row[12].ToString(),
                    DLINAP2 = row[13].ToString(),
                    DLMAXP1 = row[14].ToString(),
                    DLMAXP2 = row[15].ToString(),
                    SHTP = row[16].ToString(),
                    VESP = row[17].ToString()
                });
            }
            return result;
        }

        public static List<ClassV> ParseDataViewV(DataView dataViewV)
        {
            List<ClassV> result = new List<ClassV>();
            foreach (DataRowView rowView in dataViewV)
            {
                DataRow row = rowView.Row;
                result.Add(new ClassV()
                {
                    CFORM = row[0].ToString(),
                    CORL = row[1].ToString(),
                    NDATAR = row[2].ToString(),
                    KDATAR = row[3].ToString(),
                    NR = row[4].ToString(),
                    NSUD = row[5].ToString(),
                    //NG = row[5].ToString(),
                    NST = row[6].ToString(),
                    KVL = row[7].ToString(),
                    DATA = row[8].ToString(),
                    EDVRD = row[9].ToString(),
                    EDVRV = row[10].ToString(),
                    KODVR = row[11].ToString(),
                    DLINAVR1 = row[12].ToString(),
                    DLINAVR2 = row[13].ToString(),
                    SHTVR = row[14].ToString(),
                    VESVR = row[15].ToString()
                });
            }
            return result;
        }

        #endregion

        #region Методы парсинга первичных классов

        /// <summary>
        /// Парсит список первичного класса "а" в список троек рейс-станция-улов (без агрегации сущностей друг в друга)
        /// </summary>
        /// <param name="classAList">The class a list.</param>
        /// <param name="squaresList">The squares list.</param>
        /// <param name="windDirects"></param>
        /// <param name="groundsList"></param>
        /// <param name="gearsList"></param>
        /// <param name="fishesList"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static List<ClassAEntities> ParseClassAList(List<ClassA> classAList, 
            List<Squares> squaresList, List<WindDirects> windDirects, List<Grounds> groundsList,
            List<Gears> gearsList, List<Fishes> fishesList, out int k)
        {
            List<ClassAEntities> result = new List<ClassAEntities>();
            k = 0;
            foreach (var rowA in classAList)
            {
                k++;
                var entity = new ClassAEntities()
                {
                    //Рейс
                    Voyage = new Voyages()
                    {
                        //Константные данные
                        VoyageTypeId = 1,
                        IsFinalized = false,
                        UserId = 6,
                        //Данные из строки
                        DateStart = ToDateVoyage(rowA.NDATAR),
                        DateEnd = ToDateNullableVoyage(rowA.KDATAR),
                        VesselId = Convert.ToInt32(rowA.NSUD),
                        //Заметка об импортировании
                        Note = $"Импортирован {DateTime.Now}"
                    },
                    //Станция
                    Station = new Stations()
                    {
                        Date = ToDateStation(rowA.DATA),
                        //GearId = Convert.ToInt32(rowA.CORL),
                        GearSpeed = ToFloatNullable(rowA.SKOR),
                        GearSquare = ToFloatNullable(rowA.SQUA),
                        StationNumber = ToIntNullable(rowA.NST),
                        Square = FindSquare(rowA.KVL, squaresList),
                        WindDirect = FindWindDirect(rowA.NAPRV, windDirects),
                        WindSpeed = ToIntNullable(rowA.SILAV),
                        Waving = ToIntNullable(rowA.VOLN),
                        Depth = ToFloatNullable(rowA.GLUB),
                        Ground = FindGround(rowA.GRUNT, groundsList),
                        TempAir = ToDecimalNullable(rowA.TVOZD),
                        TempWaterSurface = ToDecimalNullable(rowA.TVODP),
                        TempWaterBottom = ToDecimalNullable(rowA.TVODD),
                        Gear = FindGear(rowA.CORL, gearsList),
                        Transparency = ToFloatNullable(rowA.PROZR),
                        Note = "(a)От улова"
                    },
                    //Улов
                    Haul = new Hauls()
                    {
                        Fish = FindFish(rowA.KODAN, fishesList),
                        Weight = ToWeight(rowA.EDANV, rowA.VESAN),
                        Pieces = ToIntNullable(rowA.SHTAN),
                        NumOfSamples = ToFloatNullable(rowA.KPCH),
                        Note = "(a)От улова"
                    }
                };
                if (entity.Haul.Weight == null && entity.Haul.Pieces == null)
                {
                    throw new Exception($"Вес и кол-во пусты");
                }
                //if (entity.Station.gr)
                var fishidtext = rowA.KODAN;
                if (entity.Haul.FishId == 0) throw new Exception("ID рыбы не казан");
                result.Add(entity);
            }
            return result;
        }

        public static List<ClassBEntities> ParseClassBList(List<ClassB> classBList,
            List<Squares> squaresList, List<Gears> gearsList, List<Fishes> fishesList, 
            List<Ages> agesList, List<GonadsStages> gonadsStagesList, out int k)
        {
            List<ClassBEntities> result = new List<ClassBEntities>();
            k = 0;
            foreach (var rowB in classBList)
            {
                k++;
                var bioAnOld = new BioAnOld()
                {
                    Dlina1 = rowB.DLINA1,
                    Gel1Gr = rowB.GEL1GR,
                    Gel2Gr = rowB.GEL2GR,
                    Gel3Gr = rowB.GEL3GR,
                    Gir = rowB.GIR,
                    Kishgr = rowB.KISHGR,
                    Kishn = rowB.KISHN,
                    Norm = rowB.NORM,
                    Normpr = rowB.NORMPR,
                    Zrel1 = rowB.ZREL1,
                    Zrel2 = rowB.ZREL2,
                    Zrel3 = rowB.ZREL3,
                    Zrel4 = rowB.ZREL4,
                    Zrel5 = rowB.ZREL5
                };
                var entity = new ClassBEntities()
                {
                    //Рейс
                    Voyage = new Voyages()
                    {
                        //Константные данные
                        VoyageTypeId = 1,
                        IsFinalized = false,
                        UserId = 6,
                        //Данные из строки
                        DateStart = ToDateVoyage(rowB.NDATAR),
                        DateEnd = ToDateNullableVoyage(rowB.KDATAR),
                        VesselId = Convert.ToInt32(rowB.NSUD),
                        //Заметка об импортировании
                        Note = $"Импортирован {DateTime.Now}"
                    },
                    //Станция
                    Station = new Stations()
                    {
                        Date = ToDateStation(rowB.DATA),
                        //GearId = Convert.ToInt32(rowB.CORL),
                        StationNumber = ToIntNullable(rowB.NST),
                        Square = FindSquare(rowB.KVL, squaresList),
                        Gear = FindGear(rowB.CORL, gearsList),
                        Note = "(b)От био. анализа"
                    },
                    //Улов
                    Haul = new Hauls()
                    {
                        Fish = FindFish(rowB.KODBA, fishesList),
                        Note = "(b)От био. анализа"
                    },
                    //
                    BioAn = new BioAn()
                    {
                        Length = ToDoubleNullable(rowB.DLINA),
                        Weight = ToDoubleNullable(rowB.VESBA),
                        Sex = ToSex(rowB.POL),
                        Age = FindAge(rowB.WOZR, agesList),
                        GonadsStage = FindGonadsStage(rowB.ZREL, gonadsStagesList),
                        RichnessBall = ToIntNullable(rowB.GIR),
                        BioAnOld = new List<BioAnOld>() { bioAnOld}
                    },
                    BioAnOld = bioAnOld
                };
                entity.BioAnOld.BioAn = entity.BioAn;
                result.Add(entity);
            }
            return result;
        }

        public static List<ClassPEntities> ParseClassPList(List<ClassP> classPList,
            List<Squares> squaresList,
            List<Gears> gearsList, List<Fishes> fishesList, out int k)
        {
            List<ClassPEntities> result = new List<ClassPEntities>();
            k = 0;
            foreach (var rowP in classPList)
            {
                //try
                //{
                    k++;
                    var entity = new ClassPEntities()
                    {
                        //Рейс
                        Voyage = new Voyages()
                        {
                            //Константные данные
                            VoyageTypeId = 1,
                            IsFinalized = false,
                            UserId = 6,
                            //Данные из строки
                            DateStart = ToDateVoyage(rowP.NDATAR),
                            DateEnd = ToDateNullableVoyage(rowP.KDATAR),
                            VesselId = Convert.ToInt32(rowP.NSUD),
                            //Заметка об импортировании
                            Note = $"Импортирован {DateTime.Now}"
                        },
                        //Станция
                        Station = new Stations()
                        {
                            Date = ToDateStation(rowP.DATA),
                            //GearId = Convert.ToInt32(rowP.CORL),
                            StationNumber = ToIntNullable(rowP.NST),
                            Square = FindSquare(rowP.KVL, squaresList),
                            Gear = FindGear(rowP.CORL, gearsList),
                            Note = "(p)От промеров"
                        },
                        //Улов
                        Haul = new Hauls()
                        {
                            Fish = FindFish(rowP.KODP, fishesList),
                            Note = "(p)От промеров"
                        },
                        //
                        Measurement = new Measurements()
                        {
                            Length1 = ToLength(rowP.EDPD, rowP.DLINAP1),
                            Length2 = ToLength(rowP.EDPD, rowP.DLINAP2),
                            LengthMin = ToLength(rowP.EDPD, rowP.DLMAXP1),
                            LengthMax = ToLength(rowP.EDPD, rowP.DLMAXP2),
                            Pieces = ToIntNullable(rowP.SHTP),
                            Weight = ToWeight(rowP.EDPV, rowP.VESP)
                        }
                    };
                    //if (entity.Voyage.DateStart.Year != DateTime.Parse("01.01.2000").Year || entity.Voyage.DateEnd == null)
                    //    throw new Exception("Проблемы");
                    result.Add(entity);

                //}
                //catch
                //{
                //    throw new Exception($"{rowP.DATA}");
                //    // ignored
                //    k++;
                //}
            }
            return result;
        }

        public static List<ClassVEntities> ParseClassVList(List<ClassV> classVList,
            List<Squares> squaresList,
            List<Gears> gearsList, List<Fishes> fishesList, out int k)
        {
            List<ClassVEntities> result = new List<ClassVEntities>();
            k = 0;
            foreach (var rowV in classVList)
            {
                var voyage = new Voyages()
                {
                    //Константные данные
                    VoyageTypeId = 1,
                    IsFinalized = false,
                    UserId = 6,
                    //Данные из строки
                    DateStart = ToDateVoyage(rowV.NDATAR),
                    DateEnd = ToDateNullableVoyage(rowV.KDATAR),
                    VesselId = Convert.ToInt32(rowV.NSUD),
                    //Заметка об импортировании
                    Note = $"Импортирован {DateTime.Now}"
                };
                var station = new Stations()
                {
                    Date = ToDateStation(rowV.DATA),
                    //GearId = Convert.ToInt32(rowA.CORL),
                    StationNumber = ToIntNullable(rowV.NST),
                    Square = FindSquare(rowV.KVL, squaresList),
                    Gear = FindGear(rowV.CORL, gearsList),
                    Note = "(v)От вар. ряда"
                };
                var haul = new Hauls()
                {
                    Fish = FindFish(rowV.KODVR, fishesList),
                    Note = "(v)От вар. ряда"
                };
                var varSeries = new VarSeries()
                {
                    Length1 = ToLength(rowV.EDVRD, rowV.DLINAVR1),
                    Length2 = ToLength(rowV.EDVRD, rowV.DLINAVR2),
                    Pieces = ToIntNullable(rowV.SHTVR),
                    Weight = ToWeight(rowV.EDVRV, rowV.VESVR)
                };
                result.Add(new ClassVEntities()
                {
                    //Рейс
                    Voyage = voyage,
                    //Станция
                    Station = station,
                    //Улов
                    Haul = haul,
                    //
                    VarSeries = varSeries
                });
                k++;
            }
            return result;
        }

        ///// <summary>
        ///// Парсит строку из таблицы "a" в рейс, станцию и улов (без агрегации сущностей друг в друга)
        ///// </summary>
        ///// <param name="rowA">The row a.</param>
        ///// <param name="squaresList"></param>
        ///// <param name="windDirects"></param>
        ///// <param name="groundsList"></param>
        ///// <param name="gearsList"></param>
        ///// <returns></returns>
        //private static ClassAEntities ParseRowA(ClassA rowA, 
        //    List<Squares> squaresList, List<WindDirects> windDirects, List<Grounds> groundsList,
        //    List<Gears> gearsList)
        //{
        //    ClassAEntities result = new ClassAEntities()
        //    {
        //        //Рейс
        //        Voyage = new Voyages()
        //        {
        //            //Константные данные
        //            VoyageTypeId = 1,
        //            IsFinalized = false,
        //            UserId = 6,
        //            //Данные из строки
        //            DateStart = ToDateVoyage(rowA.NDATAR),
        //            DateEnd = ToDateNullableVoyage(rowA.KDATAR),
        //            VesselId = Convert.ToInt32(rowA.NSUD),
        //            //Заметка об импортировании
        //            Note = $"Импортирован {DateTime.Today}"
        //        },
        //        //Станция
        //        Station = new Stations()
        //        {
        //            Date = Convert.ToDateTime(rowA.DATA),
        //            GearId = Convert.ToInt32(rowA.CORL),
        //            GearSpeed = ToFloatNullable(rowA.SKOR),
        //            GearSquare = ToFloatNullable(rowA.SQUA),
        //            StationNumber = ToIntNullable(rowA.NST),
        //            Squares = FindSquare(rowA.KVL, squaresList),
        //            WindDirects = FindWindDirect(rowA.NAPRV, windDirects),
        //            WindSpeed = ToIntNullable(rowA.SILAV),
        //            Waving = ToIntNullable(rowA.VOLN),
        //            Depth = ToFloatNullable(rowA.GLUB),
        //            Grounds = FindGround(rowA.GRUNT, groundsList),
        //            TempAir = ToDecimalNullable(rowA.TVOZD),
        //            TempWaterSurface = ToDecimalNullable(rowA.TVODP),
        //            TempWaterBottom = ToDecimalNullable(rowA.TVODD),
        //            Gears = FindGear(rowA.CORL, gearsList),
        //            Transparency = ToFloatNullable(rowA.PROZR)
        //        }
        //    };
        //    return result;
        //}

        #endregion

        #region Парсеры конкретных значений

        /// <summary>
        /// Возвращает целое число или null
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static int? ToIntNullable(object input)
        {
            input = input.ToString().Trim();
            if (string.IsNullOrEmpty(input?.ToString())) return null;
            return int.Parse(input.ToString());
        }

        /// <summary>
        /// To the float nullable.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private static float? ToFloatNullable(object input)
        {
            input = input.ToString().Trim();
            if (string.IsNullOrEmpty(input?.ToString())) return null;
            return float.Parse(input.ToString());
        }

        private static decimal? ToDecimalNullable(object input)
        {
            input = input.ToString().Trim();
            if (string.IsNullOrEmpty(input?.ToString())) return null;
            return decimal.Parse(input.ToString());
        }

        private static double? ToDoubleNullable(object input)
        {
            input = input.ToString().Trim();
            if (string.IsNullOrEmpty(input?.ToString())) return null;
            return double.Parse(input.ToString());
        }

        /// <summary>
        /// Ищет квадрат из списка по названию
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="squaresList">The squares list.</param>
        /// <returns></returns>
        private static Squares FindSquare(string input, List<Squares> squaresList)
        {
            input = input.Trim();
            return squaresList.FirstOrDefault(s => s.Districts.PartitionVariantId == 1
                                                   && s.Name == input);
        }

        private static WindDirects FindWindDirect(string input, List<WindDirects> windDirectsList)
        {
            input = input.Trim();
            if (string.IsNullOrWhiteSpace(input) 
                || Convert.ToInt32(input) == 0) return null;
            return windDirectsList.FirstOrDefault(w => w.Id == Convert.ToInt32(input));
        }

        private static Grounds FindGround(string input, List<Grounds> groundsList)
        {
            input = input.Trim();
            if (string.IsNullOrWhiteSpace(input)
                || Convert.ToInt32(input) == 0) return null;
            return groundsList.FirstOrDefault(g => g.Id == Convert.ToInt32(input));
        }

        private static Gears FindGear(string input, List<Gears> gearsList)
        {
            input = input.Trim();
            if (string.IsNullOrWhiteSpace(input)
                || Convert.ToInt32(input) == 0) return null;
            return gearsList.FirstOrDefault(g => g.Id == Convert.ToInt32(input));
        }

        private static Fishes FindFish(string input, List<Fishes> fishesList)
        {
            input = input.Trim();
            if (string.IsNullOrWhiteSpace(input)
                || Convert.ToInt32(input) == 0) return null;
            return fishesList.FirstOrDefault(g => g.Id == Convert.ToInt32(input));
        }

        private static Ages FindAge(string input, List<Ages> agesList)
        {
            input = input.Trim();
            if (string.IsNullOrWhiteSpace(input)
                || Convert.ToInt32(input) == 0) return null;
            switch (Convert.ToInt32(input))
            {
                case 0:
                    return agesList.FirstOrDefault(a => a.Id == 1);
                case 1:
                    return agesList.FirstOrDefault(a => a.Id == 2);
                case 2:
                    return agesList.FirstOrDefault(a => a.Id == 3);
                case 3:
                    return agesList.FirstOrDefault(a => a.Id == 4);
                case 5:
                    return agesList.FirstOrDefault(a => a.Id == 11);
                case 6:
                    return agesList.FirstOrDefault(a => a.Id == 22);
                case 7:
                    return agesList.FirstOrDefault(a => a.Id == 33);
                case 8:
                    return agesList.FirstOrDefault(a => a.Id == 44);
                default:
                    return null;
            }
        }

        private static GonadsStages FindGonadsStage(string input, List<GonadsStages> gonadsStagesList)
        {
            input = input.Trim();
            if (string.IsNullOrWhiteSpace(input)
                || Convert.ToInt32(input) == 0) return null;
            return gonadsStagesList.FirstOrDefault(g => g.Id == Convert.ToUInt16(input));
        }

        /// <summary>
        /// Возвращает дату начала рейса
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static DateTime ToDateVoyage(object input)
        {
            return DateTime.Parse(input.ToString().Substring(6, 2) + "." + input.ToString().Substring(4, 2) + "." + input.ToString().Substring(0, 4));
        }

        /// <summary>
        /// Возвращает дату окончания рейса
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static DateTime? ToDateNullableVoyage(object input)
        {
            if (input == null || input == DBNull.Value) return null;
            return DateTime.Parse(input.ToString().Substring(6, 2) + "." + input.ToString().Substring(4, 2) + "." + input.ToString().Substring(0, 4));
        }

        public static DateTime? ToDateStation(object input)
        {
            if (input == null || input == DBNull.Value) return null;
            if (string.IsNullOrWhiteSpace(input.ToString())) return null;
            try
            {
                return Convert.ToDateTime(input.ToString());
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Возвращает вес в граммах на основе поля единиц измерения и поля значения
        /// </summary>
        /// <param name="weightUnitString">The weight unit string.</param>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static double? ToWeight(object weightUnitString, object input)
        {
            var unit = weightUnitString as string;
            if (string.IsNullOrEmpty(input.ToString()) ||
                !float.TryParse(input.ToString(), out float value))
            {
                return null;
            }
            switch (unit)
            {
                case "гр":
                    return Convert.ToInt32(Math.Round(value, 0));
                case "кг":
                    return Convert.ToInt32(Math.Round(value * 1000, 0));
                default:
                    return Convert.ToInt32(Math.Round(value, 0));
            }
        }

        

        public static double? ToLength(object lengthUnitString, object input)
        {
            var unit = lengthUnitString as string;
            if (string.IsNullOrEmpty(input.ToString()) ||
                !float.TryParse(input.ToString(), out float value))
            {
                return null;
            }
            switch (unit)
            {
                case "мм":
                    return Convert.ToInt32(Math.Round(value, 0));
                case "см":
                    return Convert.ToInt32(Math.Round(value * 10, 0));
                default:
                    return Convert.ToInt32(Math.Round(value, 0));
            }
        }

        public static int? ToSex(object input)
        {
            if (string.IsNullOrEmpty(input?.ToString()) ||
                int.TryParse(input.ToString(), out int result)) return null;
            switch (result)
            {
                case 1:
                    return 1;
                case 2:
                    return 2;
                case 4:
                    return 3;
                default:
                    return null;
            }
        }

        #endregion

        /// <summary>
        /// Собирает данные из всех 4-х списков в один список рейсов
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="p">The p.</param>
        /// <param name="v">The v.</param>
        /// <returns></returns>
        public static List<Voyages> UniteClassEntities(List<ClassAEntities> a, List<ClassBEntities> b, List<ClassPEntities> p,
            List<ClassVEntities> v)
        {
            var result = new List<Voyages>();

            //По таблице "a"
            foreach (var aEntities in a)
            {
                //Ищем похожий (дубликат) рейс
                var sameVoyage = result.FirstOrDefault(voyage => voyage.Equals(aEntities.Voyage));
                //Если рейс новый
                if (sameVoyage == null)
                {
                    aEntities.Station.Hauls.Add(aEntities.Haul);
                    aEntities.Voyage.Stations.Add(aEntities.Station);
                    result.Add(aEntities.Voyage);
                }
                //Если рейс старый
                else
                {
                    var sameStation = sameVoyage.Stations.FirstOrDefault(station => station.Equals(aEntities.Station));
                    //Если рейс старый, а станция новая
                    if (sameStation == null)
                    {
                        aEntities.Station.Hauls.Add(aEntities.Haul);
                        sameVoyage.Stations.Add(aEntities.Station);
                    }
                    //Если рейс старый и станция старая
                    else
                    {
                        sameStation.Hauls.Add(aEntities.Haul);
                    }
                }
            }
            //По таблице "p"
            foreach (var pEntities in p)
            {

                //Ищем похожий (дубликат) рейс
                var sameVoyage = result.FirstOrDefault(voyage => voyage.Equals(pEntities.Voyage));
                //Если рейс новый
                if (sameVoyage == null)
                {
                    //в улов
                    pEntities.Haul.Measurements.Add(pEntities.Measurement);
                    //в станцию
                    pEntities.Station.Hauls.Add(pEntities.Haul);
                    //в рейс
                    pEntities.Voyage.Stations.Add(pEntities.Station);
                    result.Add(pEntities.Voyage);

                }
                //Если рейс старый
                else
                {
                    var sameStation = sameVoyage.Stations.FirstOrDefault(station => station.Equals(pEntities.Station));
                    //Если рейс старый, а станция новая
                    if (sameStation == null)
                    {
                        //в улов
                        pEntities.Haul.Measurements.Add(pEntities.Measurement);
                        //в станцию
                        pEntities.Station.Hauls.Add(pEntities.Haul);
                        //в рейс
                        sameVoyage.Stations.Add(pEntities.Station);
                    }
                    //Если рейс старый и станция старая
                    else
                    {
                        var sameHaul = sameStation.Hauls.FirstOrDefault(haul => haul.Equals(pEntities.Haul));
                        //Если рейс старый, станция старая, а улов новый
                        if (sameHaul == null)
                        {
                            pEntities.Haul.Measurements.Add(pEntities.Measurement);
                            sameStation.Hauls.Add(pEntities.Haul);
                        }
                        //Если рейс старый, станция старая и улов старый
                        else
                        {
                            sameHaul.Measurements.Add(pEntities.Measurement);
                            //sameStation.Hauls.Add(sameHaul);
                            //sameVoyage.Stations.Add(sameStation);
                        }
                    }
                }
            }
            //По таблице "b"
            foreach (var bEntities in b)
            {
                //Ищем похожий (дубликат) рейс
                var sameVoyage = result.FirstOrDefault(voyage => voyage.Equals(bEntities.Voyage));
                //Если рейс новый
                if (sameVoyage == null)
                {
                    bEntities.BioAn.BioAnOld.Add(bEntities.BioAnOld);
                    bEntities.Haul.BioAn.Add(bEntities.BioAn);
                    bEntities.Station.Hauls.Add(bEntities.Haul);
                    bEntities.Voyage.Stations.Add(bEntities.Station);
                    result.Add(bEntities.Voyage);
                }
                //Если рейс старый
                else
                {
                    var sameStation = sameVoyage.Stations.FirstOrDefault(station => station.Equals(bEntities.Station));
                    //Если рейс старый, а станция новая
                    if (sameStation == null)
                    {
                        bEntities.BioAn.BioAnOld.Add(bEntities.BioAnOld);
                        bEntities.Haul.BioAn.Add(bEntities.BioAn);
                        bEntities.Station.Hauls.Add(bEntities.Haul);
                        sameVoyage.Stations.Add(bEntities.Station);
                    }
                    //Если рейс старый и станция старая
                    else
                    {
                        var sameHaul = sameStation.Hauls.FirstOrDefault(haul => haul.Equals(bEntities.Haul));
                        //Если рейс старый, станция старая, а улов новый
                        if (sameHaul == null)
                        {
                            bEntities.BioAn.BioAnOld.Add(bEntities.BioAnOld);
                            bEntities.Haul.BioAn.Add(bEntities.BioAn);
                            sameStation.Hauls.Add(bEntities.Haul);
                        }
                        //Если рейс старый, станция старая и улов старый
                        else
                        {
                            bEntities.BioAn.BioAnOld.Add(bEntities.BioAnOld);
                            sameHaul.BioAn.Add(bEntities.BioAn);
                        }
                    }
                }
            }
            //По таблице "v"
            foreach (var vEntities in v)
            {
                //Ищем похожий (дубликат) рейс
                var sameVoyage = result.FirstOrDefault(voyage => voyage.Equals(vEntities.Voyage));
                //Если рейс новый
                if (sameVoyage == null)
                {
                    vEntities.Haul.VarSeries.Add(vEntities.VarSeries);
                    vEntities.Station.Hauls.Add(vEntities.Haul);
                    vEntities.Voyage.Stations.Add(vEntities.Station);
                    result.Add(vEntities.Voyage);
                }
                //Если рейс старый
                else
                {
                    var sameStation = sameVoyage.Stations.FirstOrDefault(station => station.Equals(vEntities.Station));
                    //Если рейс старый, а станция новая
                    if (sameStation == null)
                    {
                        vEntities.Haul.VarSeries.Add(vEntities.VarSeries);
                        vEntities.Station.Hauls.Add(vEntities.Haul);
                        sameVoyage.Stations.Add(vEntities.Station);
                    }
                    //Если рейс старый и станция старая
                    else
                    {
                        var sameHaul = sameStation.Hauls.FirstOrDefault(haul => haul.Equals(vEntities.Haul));
                        //Если рейс старый, станция старая, а улов новый
                        if (sameHaul == null)
                        {
                            vEntities.Haul.VarSeries.Add(vEntities.VarSeries);
                            sameStation.Hauls.Add(vEntities.Haul);
                        }
                        //Если рейс старый, станция старая и улов старый
                        else
                        {
                            sameHaul.VarSeries.Add(vEntities.VarSeries);
                        }
                    }
                }
            }

            foreach (var voyage in result)
            {
                voyage.IsTest = false;
            }

            return result;
        }
    }
}

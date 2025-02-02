﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaulJournalsImport.Concrete
{
    public static class DataImporter
    {
        ////Первый лист Экселя в DataTable, входная информация: путь к файлу экселя
        ////Взято с:
        ////http://stackoverflow.com/questions/11239805/how-convert-stream-excel-file-to-datatable-c/11239895#11239895
        //public static DataTable getDataTableFromExcel(string path)
        //{
        //    using (var pck = new OfficeOpenXml.ExcelPackage())
        //    {
        //        using (var stream = File.OpenRead(path))
        //        {
        //            pck.Load(stream);
        //        }
        //        var ws = pck.Workbook.Worksheets.First();
        //        DataTable tbl = new DataTable();
        //        bool hasHeader = true; // adjust it accordingly( i've mentioned that this is a simple approach)
        //        foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
        //        {
        //            tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
        //        }
        //        var startRow = hasHeader ? 2 : 1;
        //        for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
        //        {
        //            var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
        //            var row = tbl.NewRow();
        //            foreach (var cell in wsRow)
        //            {
        //                row[cell.Start.Column - 1] = cell.Text;
        //            }
        //            tbl.Rows.Add(row);
        //        }
        //        return tbl;
        //    }
        //}

        //Чтение из XLSX файла, возвращает датасет
        public static DataSet ReadXlsx(string filePath)
        {
            var myConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + @";Extended Properties=""Excel 12.0 XML;""");
            var myCommand = new OleDbDataAdapter("select * from [Sheet1$]", myConnection);
            myCommand.TableMappings.Add("Table", "Net");
            var dtSet = new DataSet();
            myCommand.Fill(dtSet);
            myConnection.Close();
            return dtSet;
        }
    }
}

using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLSToCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            // source
            // file!sheet1



            var dir = @"x:\sdcard\EIC_LP";

            var o = new StreamWriter(File.OpenWrite(dir + "/data.csv"));

            var header = false;


            foreach (var filePath in Directory.GetFiles(dir, "*.xls"))
            {
                Console.WriteLine(new { filePath });

                // http://exceldatareader.codeplex.com/
                // https://github.com/ExcelDataReader/ExcelDataReader

                FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

                //1. Reading from a binary Excel file ('97-2003 format; *.xls)
                IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);

                //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                //IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                //3. DataSet - The result of each spreadsheet will be created in the result.Tables
                //DataSet result = excelReader.AsDataSet();

                //4. DataSet - Create column names from first row
                excelReader.IsFirstRowAsColumnNames = true;
                DataSet result = excelReader.AsDataSet();



                foreach (DataTable table in result.Tables)
                {
                    #region header
                    if (!header)
                    {
                        o.Write("source;");

                        foreach (DataColumn item in table.Columns)
                        {
                            o.Write(item.ColumnName + ";");
                        }

                        o.WriteLine();
                        header = true;
                    }
                    #endregion




                    foreach (DataRow item in table.Rows)
                    {
                        o.Write("\"" + filePath + "!" + table.TableName + "\";");

                        foreach (DataColumn column in table.Columns)
                        {
                            o.Write(item[column] + ";");
                        }

                        o.WriteLine();
                    }
                }



                //6. Free resources (IExcelDataReader is IDisposable)
                excelReader.Close();
            }


        }
    }
}

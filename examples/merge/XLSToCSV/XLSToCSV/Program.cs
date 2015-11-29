using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared;

namespace XLSToCSV
{
    static class __System_Data
    {
        public static IEnumerable<DataColumn> AsEnumerable(this DataColumnCollection c)
        {
            for (int i = 0; i < c.Count; i++)
            {
                yield return c[i];
            }
        }

        public static IEnumerable<DataRow> AsEnumerable(this DataRowCollection c)
        {
            for (int i = 0; i < c.Count; i++)
            {
                yield return c[i];
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // source
            // file!sheet1




            // R:\sdcard\mikrotootjad

            var dir = @"x:\sdcard\mikrotootjad";
            //var dir = @"x:\sdcard\EIC_LP";

            var o = new StreamWriter(File.OpenWrite(dir + "/data.csv"));

            var header = false;


            //foreach (var filePath in Directory.GetFiles(dir, "*.xls"))
            foreach (var filePath in Directory.GetFiles(dir, "*.xlsx"))
            {
                Console.WriteLine(new { filePath });

                // http://exceldatareader.codeplex.com/
                // https://github.com/ExcelDataReader/ExcelDataReader

                FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

                //1. Reading from a binary Excel file ('97-2003 format; *.xls)
                IExcelDataReader excelReader = filePath.EndsWith(".xls")
                    ? ExcelReaderFactory.CreateBinaryReader(stream)
                    : ExcelReaderFactory.CreateOpenXmlReader(stream)
                    ;

                //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                //IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                //3. DataSet - The result of each spreadsheet will be created in the result.Tables
                //DataSet result = excelReader.AsDataSet();

                //4. DataSet - Create column names from first row
                excelReader.IsFirstRowAsColumnNames = true;
                DataSet result = excelReader.AsDataSet();



                foreach (DataTable table in result.Tables)
                {
                    var ColumnNames = table.Columns.AsEnumerable().Select(x => x.ColumnName).ToArray();
                    var rows = table.Rows.AsEnumerable().ToArray();
                    #region header
                    if (!header)
                    {
                        // [0] = {Column0}
                        // IsFirstRowAsColumnNames failed us.
                        var MissingColumnNames = ColumnNames[0] == "Column0";

                        if (MissingColumnNames)
                        {
                            // skip empty rows to find the first line with column names.

                            var HeaderRowAndBeyond = rows.SkipWhile(
                                r =>
                                {
                                    return !ColumnNames.Any(ColumnName => !string.IsNullOrEmpty("" + r[ColumnName]));
                                }
                            );

                            rows = HeaderRowAndBeyond.Skip(1).ToArray();

                            var HeaderRow = HeaderRowAndBeyond.FirstOrDefault();


                            // [2] = "Objekti aadress"


                            o.Write("source;");

                            ColumnNames = ColumnNames.Where(
                                ColumnName =>
                                {
                                    var x = "" + HeaderRow[ColumnName];

                                    if (string.IsNullOrEmpty(x))
                                        return false;

                                    // use the detected name
                                    o.Write(x + ";");

                                    return true;
                                }
                            ).ToArray();

                            o.WriteLine();
                            header = true;

                            ;
                        }
                        else
                        {

                            o.Write("source;");

                            foreach (var ColumnName in ColumnNames)
                            {
                                o.Write(ColumnName + ";");
                            }

                            o.WriteLine();
                            header = true;
                        }
                    }
                    #endregion




                    foreach (DataRow item in rows)
                    {
                        o.Write("\"" + filePath + "!" + table.TableName + "\";");

                        foreach (var ColumnName in ColumnNames)
                        {
                            o.Write(item[ColumnName] + ";");
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

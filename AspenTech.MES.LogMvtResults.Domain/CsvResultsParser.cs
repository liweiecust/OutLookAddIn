using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace AspenTech.MES.LogMvtResults.Domain
{
    public class CsvResultsParser
    {
        public CsvResultsParser(string filePath)
        {
            if (filePath == null) throw new ArgumentNullException("file path is null");
            if (!File.Exists(filePath)) throw new FileNotFoundException($"{filePath} can't found!");
            _filePath = filePath;
        }

        public List<CsvResultItem> Parse()
        {
            var csvData = new List<CsvResultItem>();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(_filePath))
                {
                    int idIndex = 0;
                    int descriptionIndex = 1;
                    int ResultIndex = 7;
                    int noteIndex = 10;
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields;

                    while (!(colFields = csvReader.ReadFields())[0].StartsWith("id")) ;
                    List<string> requiredColumns = new List<string>()
                    {
                        "id",
                        "result",
                        "note",
                        "description"
                    };

                    foreach (var item in requiredColumns)
                    {
                        if (!colFields.Contains(item, StringComparer.OrdinalIgnoreCase)) throw new Exception($"Header columns don't contains {item}");
                    }

                    for (int i = 0; i < colFields.Length; i++)
                    {
                        switch(colFields[i].ToLower().Trim())
                        {
                            case "id":
                                idIndex = i;
                                break;
                            case "result":
                                ResultIndex = i;
                                break;
                            case "note":
                                noteIndex = i;
                                break;
                            case "description":
                                descriptionIndex = i;
                                break;
                        }
                    }

                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        var item = new CsvResultItem();
                        item.Id = fieldData[idIndex];
                        item.Description = fieldData[descriptionIndex];
                        item.Result = fieldData[ResultIndex];
                        item.Note = fieldData[noteIndex];
                        csvData.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return csvData;
        }
    

        private string _filePath;
    }
}

using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

namespace CSV
{
    enum CsvInfo
    {
        Ok = 0,
        FileIncorrect = 1,
        Error = 404,
    }
    internal class CSVLib
    {
        public CSVLib(string pathCsv, char delimetr=';')
        {
            _path = pathCsv;
            _delimetr = delimetr;
        }

        private string _path;
        private char _delimetr;
        private int[] _mainIndexes = { 0 };
        
        private string[] _fields = null;
        public IEnumerable<string> Fields 
        { 
            get { return _fields; } 
            set { _fields = (string[])value; }
        }
        public IEnumerable<int> MainIndexes
        {
            get { return _mainIndexes; }
            set { _mainIndexes = (int[])value; }
        }

        public void AddFields(string item)
        {
            if (Fields == null) { Fields = new string[] { item }; return; }
            string[] newColumns = new string[_fields.Length+1];
            _fields.CopyTo(newColumns, 0);
            new string[] { item }.CopyTo(newColumns, _fields.Length);
            Fields = newColumns;
        }
        public void AddFields(string[] items)
        {
            if (Fields == null) { Fields = items; return; }
            string[] newColumns = new string[_fields.Length + items.Length];
            _fields.CopyTo(newColumns, 0);
            items.CopyTo(newColumns, _fields.Length);
            Fields = newColumns;
        }
        public string ArrayToString(string[] array) => string.Join(";", array);
        public string[] StringToArray(string text) => text.Split(_delimetr);
        private CsvInfo CreateCsv()
        {
            try
            {
                StreamWriter sw = new StreamWriter(_path);
                foreach (string column in Fields)
                {
                    sw.WriteLine(column);
                }
                sw.Close();
                return CsvInfo.Ok;
            }
            catch
            {
                return CsvInfo.Error;
            }
        }
        private string ReadFile()
        {
            try
            {
                if (!File.Exists(_path)) CreateCsv();
                StreamReader file = new StreamReader(_path);
                string text = file.ReadToEnd();
                file.Close();
                return text;
            }
            catch { return null; }
        }
        private bool CheckMatchLine(string lineCSV, string lineIn)
        {
            string[] lineCSVArray = StringToArray(lineCSV);
            string[] lineInArray = StringToArray(lineIn);
            foreach (int i in _mainIndexes)
                if (!Equals(lineCSVArray[i], lineInArray[i])) return false;
            return true;
        }
        public CsvInfo WriteCsv(string data)
        {
            string fileData = ReadFile()??null;
            if (fileData is null) return CsvInfo.Error;

            string[] fileDataArray = fileData.Split(new char[] { '\r','\n' } , StringSplitOptions.RemoveEmptyEntries);

            string[] fieldLines = new string[_fields.Length];

            string[] dataLines = new string[fileDataArray.Length - _fields.Length];

            Array.Copy(fileDataArray, 0, fieldLines, 0, _fields.Length);


            if (!_fields.SequenceEqual(fieldLines)) return CsvInfo.Error;
            if (Equals(StringToArray(data).Length, StringToArray(_fields[0]).Length) == false) return CsvInfo.Error;
            StreamWriter sw = new StreamWriter(_path);
            foreach (string line in Fields) sw.WriteLine(line);
            bool alreadyIn = false;
            if (fileDataArray.Length - _fields.Length != 0)
            {
                Array.Copy(fileDataArray, _fields.Length, dataLines, 0, dataLines.Length);
                foreach (string line in dataLines)
                {
                    if (Equals(line, data)) 
                    { 
                        sw.WriteLine(line); 
                        alreadyIn = true; 
                    }
                    else
                    {
                        if (!CheckMatchLine(line, data)) sw.WriteLine(line);
                        else
                        {
                            sw.WriteLine(data);
                            alreadyIn = true;
                        }
                    }
                }
            }
            if (!alreadyIn) sw.WriteLine(data);
            sw.Close();
            return CsvInfo.Ok;
        }
    }
}

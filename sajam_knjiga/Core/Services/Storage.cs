using Core.Utils;
using System;
using System.Collections.Generic;
using Core.Interfaces;

namespace Core.Services
{
    public class Storage<T> where T : ISerijalizacija, new()
    {
        private readonly string _filePath =
    $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}Core{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}{{0}}";
        private readonly Serializer<T> serializer = new();

        public Storage(string fileName)
        {
            _filePath = string.Format(_filePath, fileName);
      
        }

        public List<T> Load()
        {
            if (!File.Exists(_filePath))
            {
                FileStream fss = File.Create(_filePath);
                fss.Close();
            }

            IEnumerable<string> lines = File.ReadLines(_filePath);
            List<T> objekat = serializer.FromCSV(lines);

            return objekat;
        }

        public void Save(List<T> objekat)
        {
            string serializovaniObj = serializer.ToCSV(objekat);
            using (StreamWriter streamWriter = new StreamWriter(_filePath))
            {
                streamWriter.Write(serializovaniObj);
            }
        }
    }
}

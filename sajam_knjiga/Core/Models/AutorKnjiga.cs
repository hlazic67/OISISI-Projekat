using System;
using System.Collections.Generic;
using System.Text;
using Core.Interfaces;

namespace Core.Models
{
    public class AutorKnjiga : ISerijalizacija
    {
        public int AutorId { get; set; }
        public string ISBN { get; set; }

        public AutorKnjiga() { }
        public AutorKnjiga(int autorId, string iSBN)
        {
            AutorId = autorId;
            ISBN = iSBN;
        }

        public string[] ToCSV()
        {
            return new[]
            {
                AutorId.ToString(),
                ISBN
            };
        }

        public void FromCSV(string[] values)
        {
            AutorId = int.Parse(values[0]);
            ISBN = values[1];
        }
    }
}

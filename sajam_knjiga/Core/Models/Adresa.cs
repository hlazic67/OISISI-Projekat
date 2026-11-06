using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Models
{
    public class Adresa
    {
        public int Id { get; set; }
        public string Ulica { get; set; }
        public string Broj { get; set; }
        public string Drzava { get; set; }
        public string Grad { get; set; }

        public Adresa() { }

        public Adresa(int id, string ulica, string broj, string drzava, string grad)
        {
            Id = id;
            Ulica = ulica;
            Drzava = drzava;
            Broj = broj;
            Grad = grad;
        }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Ulica,
                Broj,
                Drzava,
                Grad
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Ulica = values[1];
            Broj = values[2];
            Drzava = values[3];
            Grad = values[4];
        }

        public override string ToString()
        {
            return $"{Ulica} {Broj}, {Grad}, {Drzava}";
        }
    }
}

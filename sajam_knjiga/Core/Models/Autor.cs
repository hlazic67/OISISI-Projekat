using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Core.Models
{
    public class Autor
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateOnly DatumRodjenja { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string BrojLicneKarte { get; set; }
        public int GodineIskustva { get; set; }

        public int AdresaID { get; set; }
        public Adresa Adresa { get; set; }

        public List<Knjiga> Knjige { get; set; }


        public Autor() { 
       
            Knjige = new List<Knjiga>();
        }

            public string[] ToCSV()
        {
            return new[]
            {
                Id.ToString(),
                Ime,
                Prezime,
                DatumRodjenja.ToString(),
                Telefon.ToString(),
                Email,
                BrojLicneKarte,
                GodineIskustva.ToString(),
                AdresaID.ToString()
            };
        }

        public void FromCSV(string[] values) {
            Id = int.Parse(values[0]);
            Ime = values[1];
            Prezime = values[2];
            DatumRodjenja = DateTime.Parse(values[3]);
            Telefon = values[4];
            Email = values[5];
            BrojLicneKarte = values[6];
            GodineIskustva = int.Parse((string)values[7]);
            AdresaID = int.Parse(values[8]);
        }
       
    }

}

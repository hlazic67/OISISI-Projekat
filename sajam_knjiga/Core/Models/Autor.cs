using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Core.Models
{
    public class Autor : ISerijalizacija
    {
        private const string DateFormat = "yyyy-MM-dd";
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateOnly DatumRodjenja { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string BrojLicneKarte { get; set; }
        public int GodineIskustva { get; set; }

        public int AdresaId { get; set; }
        public Adresa Adresa { get; set; }

        public List<Knjiga> Knjige { get; set; }


        public Autor()
        { 
            Knjige = new List<Knjiga>();
        }

         public string[] ToCSV()
        {
            return new[]
            {
                Id.ToString(),
                Ime,
                Prezime,
                DatumRodjenja.ToString(DateFormat, CultureInfo.InvariantCulture),
                Telefon,
                Email,
                BrojLicneKarte,
                GodineIskustva.ToString(),
                AdresaId.ToString()
            };
        }

        public void FromCSV(string[] values) {
            Id = int.Parse(values[0]);
            Ime = values[1];
            Prezime = values[2];
            DatumRodjenja = DateOnly.ParseExact(values[3], DateFormat, CultureInfo.InvariantCulture);
            Telefon = values[4];
            Email = values[5];
            BrojLicneKarte = values[6];
            GodineIskustva = int.Parse(values[7]);
            AdresaId = int.Parse(values[8]);
        }

        public override string ToString()
        {
            return $"ID: {Id}, {Ime}, {Prezime}, rodjen/a: {DatumRodjenja:dd.MM.yyyy}, " +
                   $"tel: {Telefon}, email: {Email}, Licna Karta: {BrojLicneKarte}, iskustvo: {GodineIskustva}";
        }
       
    }

}

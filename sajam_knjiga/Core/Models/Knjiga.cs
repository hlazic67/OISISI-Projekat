using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Core.Models
{
    public class Knjiga : ISerijalizacija
    {
        public string ISBN { get; set; }
        public string Naziv { get; set; }
        public string Zanr { get; set; }
        public int GodinaIzdanja { get; set; }
        public double Cena { get; set; }
        public int BrojStrana { get; set; }
        public List<Autor> Autori { get; set; }
        public Izdavac Izdavac { get; set; }
        public int IzdavacSifra { get; set; }

        public Knjiga()
        {
            Autori = new List<Autor>();
        }

        public Knjiga(string isbn, string naziv, string zanr, int godinaIzdanja, double cena, int brojStrana, int izdavacSifra)
        {
            ISBN = isbn;
            Naziv = naziv;
            Zanr = zanr;
            GodinaIzdanja = godinaIzdanja;
            Cena = cena;
            BrojStrana = brojStrana;
            IzdavacSifra = izdavacSifra;
            Autori = new List<Autor>();
        }

        public string[] ToCSV()
        {
            return new[]
            {
                ISBN,
                Naziv,
                Zanr,
                GodinaIzdanja.ToString(),
                Cena.ToString(CultureInfo.InvariantCulture),
                BrojStrana.ToString(),
                IzdavacSifra.ToString()
            };
        }

        public void FromCSV(string[] values)
        {
            ISBN = values[0];
            Naziv = values[1];
            Zanr = values[2];
            GodinaIzdanja = int.Parse(values[3]);
            Cena = double.Parse(values[4], CultureInfo.InvariantCulture);
            BrojStrana = int.Parse(values[5]);
            IzdavacSifra = int.Parse(values[6]);
        }

        public override string ToString()
        {
            string autori = "";
            for (int i = 0; i < Autori.Count; i++)
            {
                autori += $"{Autori[i].Ime} {Autori[i].Prezime}";
                if (i < Autori.Count - 1)
                    autori += ", ";
            }
            return $"ISBN: {ISBN}, {Naziv}, zanr: {Zanr}, godina: {GodinaIzdanja}, " +
                   $"cena: {Cena}, strana: {BrojStrana}, autori: {autori}";
        }
    }

}

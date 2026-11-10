using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Core.Models
{
    public class Izdavac : ISerijalizacija
    {
        public int Sifra {  get; set; }

        public string Naziv {  get; set; }

        public Autor SefIzdavaca { get; set; }

        public List<Autor> SpisakAutora {  get; set; }

        public List<Knjiga> SpisakKnjiga { get; set; }

        public int SefID { get; set; }



        public Izdavac()
        {
        
            SpisakAutora = new List<Autor>();
            SpisakKnjiga = new List<Knjiga>();
        
        }

        public Izdavac(int sifra, string naziv, Autor sefIzdavaca, int sefID)
        {
            Sifra = sifra;
            Naziv = naziv;
            SefIzdavaca = sefIzdavaca;
            SpisakAutora = new List<Autor>();
            SpisakKnjiga = new List<Knjiga>();
            SefID = sefID;
        }

        public string[] ToCSV()
        {
          
            string[] csvValue =
            {
                Sifra.ToString(),
                Naziv,
                SefID.ToString()
            };
            return csvValue;

        }

        public void FromCSV(string[] values)
        {

            Sifra = int.Parse(values[0]);
            Naziv = values[1];
            SefID = int.Parse(values[2]);
            SpisakAutora = new List<Autor>();
            SpisakKnjiga = new List<Knjiga>();
            SefIzdavaca = null;

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Sifra: {Sifra}, ");
            sb.Append($"Naziv: {Naziv}, ");
            sb.Append($"Sef izdavaca: {SefID}, ");
            sb.Append($"Spisak autora:");
            sb.AppendJoin(", ", SpisakAutora.Select(autor => $"{ autor.Ime} {autor.Prezime}"));
            sb.Append(", ");
            sb.Append($"Spisak knjiga:");
            sb.AppendJoin(", ", SpisakKnjiga.Select(knjiga => knjiga.Naziv));

            return sb.ToString();
        }




    }
}

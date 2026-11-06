using Core.Interfaces;
using System;
using System.Text;

namespace Core.Models
{
    public class Kupovina : ISerijalizacija
    {
        public Posetilac Posetilac {  get; set; }

        public Knjiga Knjiga { get; set; }

        public DateOnly DatumKupovine { get; set; }

        public int Ocena { get; set; }

        public string Komentar {  get; set; }

        public int PosetilacID { get; set; }

        public int KnjigaID {  get; set; }



        public Kupovina()
        {

        }

        public string[] ToCSV()
        {
            string[] csvValue =
            {
                PosetilacID.ToString(),
                KnjigaID.ToString(),
                DatumKupovine.ToString(),
                Ocena.ToString(),
                Komentar
            };
            return csvValue;

        }

        public void FromCSV(string[] values)
        {
          
            PosetilacID = int.Parse(values[0]);
            KnjigaID= int.Parse(values[1]);
            DatumKupovine=DateOnly.Parse(values[2]);
            Ocena = int.Parse(values[3]);
            Komentar=values[4];
      
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Posetilac: {Posetilac.Ime} {Posetilac.Prezime}, ");
            sb.Append($"Knjiga: {Knjiga.Naziv}, ");
            sb.Append($"Datum kupovine: {DatumKupovine}, ");
            sb.Append($"Ocena: {Ocena}, ");
            sb.Append($"Komentar: {Komentar}, ");

            return sb.ToString();
        }

    }


}

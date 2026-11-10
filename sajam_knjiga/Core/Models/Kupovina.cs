using Core.Interfaces;
using System;
using System.Text;
using System.Linq;

namespace Core.Models
{
    public class Kupovina : ISerijalizacija
    {
        public Posetilac Posetilac {  get; set; }

        public Knjiga Knjiga { get; set; }

        public DateOnly DatumKupovine { get; set; }

        public int Ocena { get; set; }

        public string Komentar {  get; set; }

        public string PosetilacBrojClanskeKarte { get; set; }

        public string KnjigaISBN { get; set; }



        public Kupovina()
        {

        }

        public Kupovina(string posetilacBrClanskeK, string knjigaISBN, DateOnly datKupovine, int ocena, string kom)
        {
            PosetilacBrojClanskeKarte = posetilacBrClanskeK;
            KnjigaISBN = knjigaISBN;
            DatumKupovine = datKupovine;
            if (ocena < 1 || ocena > 5)
                throw new ArgumentOutOfRangeException(nameof(ocena), "Ocena mora biti između 1 i 5.");
            Ocena = ocena;
            Ocena = ocena;
            Komentar = kom;

            Posetilac = null;
            Knjiga = null;
        }

        public string[] ToCSV()
        {
            string brojClanskeKarte;
            string isbn;

            if (Posetilac != null)
            {
                brojClanskeKarte = Posetilac.BrojClanskeKarte;
            }
            else
            {
                brojClanskeKarte = PosetilacBrojClanskeKarte;
            }

            if (Knjiga != null)
            {
                isbn = Knjiga.ISBN;
            }
            else
            {
                isbn = KnjigaISBN;
            }

            string[] csvValue =
            {
                brojClanskeKarte,
                isbn,
                DatumKupovine.ToString("yyyy-MM-dd"),
                Ocena.ToString(),
                Komentar
            };
            return csvValue;

        }

        public void FromCSV(string[] values)
        {

            PosetilacBrojClanskeKarte = values[0];
            KnjigaISBN = values[1];
            DatumKupovine = DateOnly.ParseExact(values[2], "yyyy-MM-dd");
            Ocena = int.Parse(values[3]);
            Ocena = int.Parse(values[3]);
            if (Ocena < 1 || Ocena > 5)
                throw new FormatException("Ocena mora biti između 1 i 5.");
            Komentar =values[4];

            Posetilac = null;
            Knjiga = null;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Broj clanske karte posetioca: {PosetilacBrojClanskeKarte}, ");
            sb.Append($"ISBN knjige: {KnjigaISBN}, ");
            sb.Append($"Datum kupovine: {DatumKupovine}, ");
            sb.Append($"Ocena: {Ocena}, ");
            sb.Append($"Komentar: {Komentar}, ");

            return sb.ToString();
        }

    }


}

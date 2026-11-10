using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;



namespace Core.Models
{
    public enum TStatus { R, V }
    
    public class Posetilac: ISerijalizacija
    {

        public string Ime { get; set; }

        public string Prezime { get; set; }

        public DateOnly DatumRodjenja { get; set; }

        public Adresa Adresa { get; set; }

        public int AdresaId { get; set; }

        public string KontaktTelefon { get; set; }

        public string EMail { get; set;}

        public string BrojClanskeKarte { get; set; }

        public int TrenutnaGodinaClanstva {  get; set; }

        public TStatus Status { get; set; }

        public double ProsecnaOcenaRecenzija { get; set; }

        public List<Kupovina> SpisakKupljenihKnjiga { get; set; }

        public List<Knjiga> ListaZelja {  get; set; }


        public Posetilac()
        {
            SpisakKupljenihKnjiga = new List<Kupovina>();
            ListaZelja = new List<Knjiga>();
        }

        public Posetilac(string ime, string prez, DateOnly datumRodj, Adresa adr, int adresaId, string tel, string mail, string brojClanske,int trenutnaGod, TStatus status, double prosecnaOcenaR)
        {
            Ime = ime;
            Prezime = prez;
            DatumRodjenja = datumRodj;
            Adresa = adr;
            AdresaId = adresaId;
            KontaktTelefon = tel;
            EMail = mail;
            BrojClanskeKarte = brojClanske;
            TrenutnaGodinaClanstva = trenutnaGod;
            Status = status;
            ProsecnaOcenaRecenzija = prosecnaOcenaR;
            SpisakKupljenihKnjiga = new List<Kupovina>();
            ListaZelja = new List<Knjiga>();
        }


        public string[] ToCSV()
        {

            string[] csvValue =
            {
                Ime,
                Prezime,
                DatumRodjenja.ToString("yyyy-MM-dd"),
                AdresaId.ToString(),
                Adresa?.Ulica ?? string.Empty,
                Adresa?.Broj ?? string.Empty,
                Adresa?.Drzava ?? string.Empty,
                Adresa?.Grad ?? string.Empty,
                KontaktTelefon,
                EMail,
                BrojClanskeKarte,
                TrenutnaGodinaClanstva.ToString(),
                Status.ToString(),
                ProsecnaOcenaRecenzija.ToString(System.Globalization.CultureInfo.InvariantCulture),

            };
            return csvValue;

        }

        public void FromCSV(string[] values)
        {
            Ime= values[0];
            Prezime= values[1];
            DatumRodjenja = DateOnly.ParseExact(values[2], "yyyy-MM-dd"); 
            AdresaId =int.Parse(values[3]);
            Adresa = new Adresa(AdresaId,values[4],values[5], values[6],values[7]);
            KontaktTelefon = values[8];
            EMail= values[9];
            BrojClanskeKarte= values[10];
            TrenutnaGodinaClanstva=int.Parse(values[11]);
            Status=Enum.Parse<TStatus>(values[12]);
            ProsecnaOcenaRecenzija=double.Parse(values[13], System.Globalization.CultureInfo.InvariantCulture);
            ListaZelja = new List<Knjiga>();
            SpisakKupljenihKnjiga = new List<Kupovina>();

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Ime: {Ime} ");
            sb.AppendLine($"Prezime: {Prezime} ");
            sb.AppendLine($"Datum rodjenja: {DatumRodjenja} ");
            sb.AppendLine($"Adresa: {Adresa}");
            sb.AppendLine($"Telefon: {KontaktTelefon} ");
            sb.AppendLine($"E-Mail: {EMail}");
            sb.AppendLine($"Broj clanske karte: {BrojClanskeKarte} ");
            sb.AppendLine($"Trenutna godina clanstva: {TrenutnaGodinaClanstva} ");
            sb.AppendLine($"Status: {Status}");
            sb.AppendLine($"Prosecna ocena recenzija: {ProsecnaOcenaRecenzija} ");

            return sb.ToString();
        }


    }
}

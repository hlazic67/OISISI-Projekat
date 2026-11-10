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
            KontaktTelefon = values[4];
            EMail= values[5];
            BrojClanskeKarte= values[6];
            TrenutnaGodinaClanstva=int.Parse(values[7]);
            Status=Enum.Parse<TStatus>(values[8]);
            ProsecnaOcenaRecenzija=double.Parse(values[9], System.Globalization.CultureInfo.InvariantCulture);
            ListaZelja = new List<Knjiga>();
            SpisakKupljenihKnjiga = new List<Kupovina>();

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Ime: {Ime}, ");
            sb.AppendLine($"Prezime: {Prezime}, ");
            sb.AppendLine($"Datum rodjenja: {DatumRodjenja}, ");
            sb.AppendLine($"Adresa: {Adresa}, ");
            sb.Append($"Telefon: {KontaktTelefon}, ");
            sb.Append($"E-Mail: {   EMail}, ");
            sb.Append($"Broj clanske karte: {BrojClanskeKarte}, ");
            sb.Append($"Trenutna godina clanstva: {TrenutnaGodinaClanstva}, ");
            sb.Append($"Status: {Status}, ");
            sb.Append($"Prosecna ocena recenzija: {ProsecnaOcenaRecenzija}, ");

            return sb.ToString();
        }


    }
}

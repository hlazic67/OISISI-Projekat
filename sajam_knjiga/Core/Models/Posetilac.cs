using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Core.Models
{
    public enum TStatus { R, V }
    public class Posetilac
    {

        public string Ime { get; set; }

        public string Prezime { get; set; }

        public DateTime DatumRodjenja { get; set; }

        public Adresa Adresa { get; set; }

        public string KontaktTelefon { get; set; }

        public string E_mail { get; set;}

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



    }
}

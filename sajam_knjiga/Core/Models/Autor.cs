using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Autor
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string BrojLicneKarte { get; set; }
        public int GodineIskustva { get; set; }

        public int AdresaID { get; set; }
        public Adresa Adresa { get; set; }

    }

}

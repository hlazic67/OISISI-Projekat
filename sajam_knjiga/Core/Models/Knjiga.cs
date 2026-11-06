using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Knjiga
    {
        public string ISBN { get; set; }
        public string Naziv { get; set; }
        public string Zanr { get; set; }
        public int GodinaIzdanja { get; set; }
        public double Cena { get; set; }
        public int BrojStrana { get; set; }
        public List<Autor> Autori { get; set; }
        public Izdavac Izdavac { get; set; }
        public string IzdavacSifra { get; set; }

    }

    }
}

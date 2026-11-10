using Core.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.DAO
{
    public class IzdavacDAO
    {
        private List<Izdavac> _izdavaci;
        private Storage<Izdavac> _storage;

        public IzdavacDAO()
        {
            _storage = new Storage<Izdavac>("izdavaci.csv");
            _izdavaci = _storage.Load();
        }

        public Izdavac DodajIzdavaca(Izdavac izdavac)
        {
            izdavac.Sifra = GenerisiSifru();
            _izdavaci.Add(izdavac);
            _storage.Save(_izdavaci);
            return izdavac;
        }

        public Izdavac? IzmeniIzdavaca(Izdavac izdavac)
        {
            Izdavac? stariIzdavac = GetIzdavacbySifra(izdavac.Sifra);

            if (stariIzdavac == null) return null;

            stariIzdavac.Naziv = izdavac.Naziv;
            stariIzdavac.SefID = izdavac.SefID;
            _storage.Save(_izdavaci);
            return stariIzdavac;
        }

        public Izdavac? UkloniIzdavaca(int sifra)
        {
            Izdavac? izdavac = GetIzdavacbySifra(sifra);

            if (izdavac == null) return null;
            _izdavaci.Remove(izdavac);
            _storage.Save(_izdavaci);
            return izdavac;
        }

        public List<Izdavac> GetAllIzdavaci()
        {
            return _izdavaci;
        }

        private int GenerisiSifru()
        {
            if (_izdavaci.Count == 0) return 0;
            return _izdavaci.Max(i => i.Sifra) + 1;
        }

        private Izdavac? GetIzdavacbySifra(int sifra)
        {
            return _izdavaci.Find(i => i.Sifra == sifra);
        }
    }
}

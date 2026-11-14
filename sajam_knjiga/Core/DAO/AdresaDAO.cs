using System;
using System.Collections.Generic;
using System.Text;
using Core.Models;
using Core.Services;

namespace Core.DAO
{
    public class AdresaDAO
    {
        private List<Adresa> _adrese;
        private Storage<Adresa> _storage;

        public AdresaDAO()
        {
            _storage = new Storage<Adresa>("adrese.csv");
            _adrese = _storage.Load();
        }
        public Adresa DodajAdresu(Adresa adresa)
        {
            adresa.Id = GenerisiId();
            _adrese.Add(adresa);
            _storage.Save(_adrese);
            return adresa;
        }

        public Adresa? IzmeniAdresu(Adresa adresa)
        {
            Adresa? staraAdresa = GetAdresaById(adresa.Id);

            if (staraAdresa == null) return null;

            staraAdresa.Ulica = adresa.Ulica;
            staraAdresa.Broj = adresa.Broj;
            staraAdresa.Grad = adresa.Grad;
            staraAdresa.Drzava = adresa.Drzava;
            _storage.Save(_adrese);
            return staraAdresa;
        }

        public Adresa? UkloniAdresi(int id)
        {
            Adresa? adresa = GetAdresaById(id);

            if(adresa == null) return null;

            _adrese.Remove(adresa);
            _storage.Save(_adrese);
            return adresa;
        }

        public List<Adresa> GetAllAdrese()
        {
            return _adrese;
        }

        public Adresa? GetAdresaById(int id)
        {
            return _adrese.Find(a => a.Id == id);
        }

        public int GenerisiId()
        {
            if(_adrese.Count == 0) return 0;
            return _adrese.Max(a => a.Id) + 1;
        }

    }
}

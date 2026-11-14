using System;
using System.Collections.Generic;
using System.Text;
using Core.Models;
using Core.Services;

namespace Core.DAO
{
    public class AutorDAO
    {
        private List<Autor> _autori;
        private Storage<Autor> _storage;

        public AutorDAO()
        {
            _storage = new Storage<Autor>("autori.csv");
            _autori = _storage.Load();
        }

        public Autor DodajAutora(Autor autor)
        {
            autor.Id = GenerisiId();
            _autori.Add(autor);
            _storage.Save(_autori);
            return autor;
        }

        public Autor? IzmeniAutora(Autor autor)
        {
            Autor? stariAutor = GetAutorById(autor.Id);

            if (stariAutor == null) return null;

            stariAutor.Ime = autor.Ime;
            stariAutor.Prezime = autor.Prezime;
            stariAutor.DatumRodjenja = autor.DatumRodjenja;
            stariAutor.Telefon = autor.Telefon;
            stariAutor.Email = autor.Email;
            stariAutor.BrojLicneKarte = autor.BrojLicneKarte;
            stariAutor.GodineIskustva = autor.GodineIskustva;
            stariAutor.AdresaId = autor.AdresaId;
            _storage.Save(_autori);

            return stariAutor;
        }

        public Autor? UkloniAutora(int id)
        {
            Autor? autor = GetAutorById(id);

            if(autor == null) return null;

            _autori.Remove(autor);
            _storage.Save(_autori);
            return autor;
        }

        public List<Autor> GetAllAutori()
        {
            return _autori;
        }

        public Autor? GetAutorById(int id)
        {
            return _autori.Find(a => a.Id == id);
        }

        private int GenerisiId()
        {
            if (_autori.Count == 0) return 0;
            return _autori.Max(a => a.Id) + 1;
        }

    }
}

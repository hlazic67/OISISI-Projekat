using System;
using System.Collections.Generic;
using System.Text;
using Core.Models;
using Core.Services;

namespace Core.DAO
{
    public class KnjigaDAO
    {
        private List<Knjiga> _knjige;
        private Storage<Knjiga> _storage;

        public KnjigaDAO()
        {
            _storage = new Storage<Knjiga>("knjige.csv");
            _knjige = _storage.Load();
        }

        public Knjiga? DodajKnjigu(Knjiga knjiga)
        {
            if (GetKnjigaByIsbn(knjiga.ISBN) != null) return null;

            _knjige.Add(knjiga);
            _storage.Save(_knjige);

            return knjiga;
        }

        public Knjiga? IzmeniKnjigu(Knjiga knjiga)
        {
            Knjiga? staraKnjiga = GetKnjigaByIsbn(knjiga.ISBN);

            if (staraKnjiga == null) return null;

            staraKnjiga.Naziv = knjiga.Naziv;
            staraKnjiga.Zanr = knjiga.Zanr;
            staraKnjiga.GodinaIzdanja = knjiga.GodinaIzdanja;
            staraKnjiga.Cena = knjiga.Cena;
            staraKnjiga.BrojStrana = knjiga.BrojStrana;
            staraKnjiga.IzdavacSifra = knjiga.IzdavacSifra;
            _storage.Save(_knjige);

            return knjiga;
        }

        public Knjiga? UkloniKnjigu(string isbn)
        {
            Knjiga? knjiga = GetKnjigaByIsbn(isbn);

            if (knjiga == null) return null;

            _knjige.Remove(knjiga);
            _storage.Save(_knjige);
            return knjiga;
        }

        public List<Knjiga> GetAllKnjige()
        {
            return _knjige;
        }

        public Knjiga? GetKnjigaByIsbn(string isbn)
        {
            return _knjige.Find(k => k.ISBN == isbn);
        }
    }
}

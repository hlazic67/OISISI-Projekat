using Core.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAO
{


    public class KupovinaDAO
    {
        private List<Kupovina> _kupovine;
        private Storage<Kupovina> _storage;

        public KupovinaDAO()
        {
            _storage = new Storage<Kupovina>("kupovine.csv");
            _kupovine = _storage.Load();
        }


        public Kupovina DodajKupovinu(Kupovina kupovina)
        {
            _kupovine.Add(kupovina);
            _storage.Save(_kupovine);
            return kupovina;
        }

        private Kupovina? GetKupovina(string brojClanskeKarte,string isbn,DateOnly datumKupovine)
        {
            return _kupovine.Find(k => k.PosetilacBrojClanskeKarte == brojClanskeKarte && k.KnjigaISBN == isbn && k.DatumKupovine == datumKupovine);
        }

        public Kupovina? IzmeniKupovinu(Kupovina kupovina)
        {
            if (kupovina.Ocena < 1 || kupovina.Ocena > 5)
                throw new ArgumentOutOfRangeException(nameof(kupovina.Ocena));

            Kupovina? staraKupovina = GetKupovina(kupovina.PosetilacBrojClanskeKarte, kupovina.KnjigaISBN,kupovina.DatumKupovine);

            if (staraKupovina == null) return null;

            staraKupovina.Ocena = kupovina.Ocena;
            staraKupovina.Komentar = kupovina.Komentar;
            _storage.Save(_kupovine);
            return staraKupovina;
        }

        public Kupovina? UkloniKupovinu( string brojClanskeKarte,string isbn,DateOnly datumKupovine)
        {
            Kupovina? kupovina = GetKupovina(brojClanskeKarte, isbn, datumKupovine);

            if (kupovina == null) return null;

            _kupovine.Remove(kupovina);
            _storage.Save(_kupovine);
            return kupovina;
        }

        public List<Kupovina> GetAllKupovine()
        {
            return _kupovine;
        }


    }
}


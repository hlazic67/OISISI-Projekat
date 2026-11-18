using Core.DAO;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class AutorService
    {
        private readonly AutorDAO _autorDAO;
        private readonly AdresaDAO _adresaDAO;
        private readonly KnjigaDAO _knjigaDAO;
        private readonly AutorKnjigaDAO _autorKnjigaDAO;
        private readonly IzdavacDAO _izdavacDAO;

        public AutorService(AutorDAO autorDAO, AdresaDAO adresaDAO, KnjigaDAO knjigaDAO, AutorKnjigaDAO autorKnjigaDAO, IzdavacDAO izdavacDAO)
        {
            _autorDAO = autorDAO;
            _adresaDAO = adresaDAO;
            _knjigaDAO = knjigaDAO;
            _autorKnjigaDAO = autorKnjigaDAO;
            _izdavacDAO = izdavacDAO;
        }

        public void PoveziPodatke()
        {
            foreach (Autor autor in _autorDAO.GetAllAutori())
            {
                autor.Adresa = _adresaDAO.GetAdresaById(autor.AdresaId);
                autor.Knjige.Clear();
            }

            foreach (Knjiga knjiga in _knjigaDAO.GetAllKnjige())
            {
                knjiga.Autori.Clear();
            }

            foreach (AutorKnjiga veza in _autorKnjigaDAO.GetAllVeze())
            {
                Autor? autor = _autorDAO.GetAutorById(veza.AutorId);
                Knjiga? knjiga = _knjigaDAO.GetKnjigaByIsbn(veza.ISBN);

                if (autor == null || knjiga == null) continue;

                autor.Knjige.Add(knjiga);
                knjiga.Autori.Add(autor);
            }
        }

        public List<Autor> GetAllAutori()
        {
            return _autorDAO.GetAllAutori();
        }

        public Autor? GetAutorById(int id)
        {
            return _autorDAO.GetAutorById(id);
        }

        public Autor DodajAutora(Autor autor,Adresa adresa)
        {
            Adresa dodataAdresa = _adresaDAO.DodajAdresu(adresa);

            autor.AdresaId = dodataAdresa.Id;
            autor.Adresa = dodataAdresa;

            return _autorDAO.DodajAutora(autor);
        }

        public bool IzmeniAutora(Autor autor, Adresa novaAdresa)
        {
            Autor? postojeciAutor = _autorDAO.GetAutorById(autor.Id);
            if (postojeciAutor == null) { return false; }

            Adresa? postojecaAdresa = _adresaDAO.GetAdresaById(postojeciAutor.Id);
            Adresa adresa;

            if (postojecaAdresa == null)
            {
                adresa = _adresaDAO.DodajAdresu(novaAdresa);
            }
            else
            {
                novaAdresa.Id = postojecaAdresa.Id;
                adresa = _adresaDAO.IzmeniAdresu(novaAdresa) ?? novaAdresa;
            }

            autor.AdresaId = adresa.Id;
            Autor? izmenjenAutor = _autorDAO.IzmeniAutora(autor);
            if (izmenjenAutor == null) return false;

            izmenjenAutor.Adresa = adresa;
            return true;
        }

        public bool ObrisiAutora(int id, out string poruka)
        {
            Autor? autor = _autorDAO.GetAutorById(id);
            if(autor == null)
            {
                poruka = "Autor nije pronadjen.";
                return false;
            }

            if(_izdavacDAO.GetAllIzdavaci().Exists(i => i.SefID == id))
            {
                poruka = "Autor je sef izdavaca i ne moze da se obrise.";
                return false;
            }

            foreach(Knjiga knjiga in autor.Knjige)
            {
                knjiga.Autori.Remove(autor);
            }

            _autorKnjigaDAO.UkloniVezeAutora(id);
            _autorDAO.UkloniAutora(id);

            poruka = "Autor je uspesno obrisan.";
            return true;
       
        }
    }
}

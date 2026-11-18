using System;
using Core.DAO;
using Core.Models;

namespace Core.Services
{
    public class IzdavacService
    {
        private readonly IzdavacDAO _izdavacDAO;
        private readonly AutorDAO _autorDAO;

        public IzdavacService(IzdavacDAO izdavacDAO,AutorDAO autorDAO)
        {
            _izdavacDAO = izdavacDAO;
            _autorDAO = autorDAO;
        }

        public string? DodajIzdavaca(Izdavac izdavac)
        {
            string? greska = ProveriUnos(izdavac);
            if (greska != null) return greska;


            _izdavacDAO.DodajIzdavaca(izdavac);

            return null;
        }

        public string? IzmeniIzdavaca(Izdavac izdavac)
        {
            string? greska = ProveriUnos(izdavac);

            if (greska != null) return greska;

            Izdavac? postojeci = _izdavacDAO.GetAllIzdavaci().Find(i => i.Sifra == izdavac.Sifra);

            if (postojeci == null)
               return "Izdavac ne postoji.";

            if (_izdavacDAO.IzmeniIzdavaca(izdavac) == null)
               return "Izmena izdavaca nije uspela.";

            return null;
        }

        public bool UkloniIzdavaca(int sifra)
        {
            return _izdavacDAO.UkloniIzdavaca(sifra) != null;
        }

        private string? ProveriUnos(Izdavac izdavac)
        {
            if (string.IsNullOrWhiteSpace(izdavac.Naziv))
               return "Naziv izdavaca je obavezan.";

            Autor? sef = _autorDAO.GetAutorById(izdavac.SefID);

            if (sef == null)
               return "Autor koji je sef izdavaca ne postoji.";

            return null;
        }
    }
}


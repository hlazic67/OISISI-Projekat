using Core.DAO;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class KnjigaService
    {
        private readonly KnjigaDAO _knjigaDAO;
        private readonly AutorKnjigaDAO _autorKnjigaDAO;
        private readonly IzdavacDAO _izdavacDAO;

        public KnjigaService(KnjigaDAO knjigaDAO, AutorKnjigaDAO autorKnjigaDAO, IzdavacDAO izdavacDAO)
        {
            _knjigaDAO = knjigaDAO;
            _autorKnjigaDAO = autorKnjigaDAO;
            _izdavacDAO = izdavacDAO;
        }

        public List<Knjiga> GetAllKnjige()
        {
            return _knjigaDAO.GetAllKnjige(); 
        }

        public Knjiga? GetKnjigaByIsbn(string isbn)
        {
            return _knjigaDAO.GetKnjigaByIsbn(isbn);
        }

        public bool PostojiIzdavac(int sifra)
        {
            return _izdavacDAO.GetAllIzdavaci().Exists(i => i.Sifra == sifra);
        }

        public bool PostojeIzdavaci()
        {
            return _izdavacDAO.GetAllIzdavaci().Count > 0;
        }

        public bool DodajKnjigu(Knjiga knjiga, out string poruka)
        {
            if (string.IsNullOrWhiteSpace(knjiga.ISBN))
            {
                poruka = "ISBN ne sme da bude prazan.";
                return false;
            }

            if (!PostojiIzdavac(knjiga.IzdavacSifra)){
                poruka = "Izdavac ne postoji.";
                return false;
            }

            if(_knjigaDAO.DodajKnjigu(knjiga) == null)
            {
                poruka = "Knjiga sa tim ISBN-om vec postoji.";
                return false;
            }

            poruka = "Knjiga je uspesno dodata.";
            return true;
        }

        public bool IzmeniKnjigu(Knjiga knjiga, out string poruka)
        {
            if (!PostojiIzdavac(knjiga.IzdavacSifra))
            {
                poruka = "Izdavac ne postoji.";
                return false;
            }

            if(_knjigaDAO.IzmeniKnjigu(knjiga) == null)
            {
                poruka = "Izmena nije uspesna.";
                return false;
            }

            poruka = "Knjiga je uspesno izmenjena.";
            return true;
        }

        public bool ObrisiKnjigu(string isbn, out string poruka)
        {
            Knjiga? knjiga = _knjigaDAO.GetKnjigaByIsbn(isbn);
            if (knjiga == null)
            {
                poruka = "Knjiga nije pronadjena.";
                return false;
            }

            foreach (Autor autor in knjiga.Autori)
            {
                autor.Knjige.Remove(knjiga);
            }

            _autorKnjigaDAO.UkloniVezeKnjige(isbn);
            _knjigaDAO.UkloniKnjigu(isbn);

            poruka = "Knjiga je uspesno obrisana.";
            return true;
        }
    }
}

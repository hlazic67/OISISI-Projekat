using Core.DAO;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Core.Services
{
    public class AdresaService
    {
        public readonly AdresaDAO _adresaDAO;
        private readonly AutorDAO _autorDAO;

        public AdresaService(AdresaDAO adresaDAO, AutorDAO autorDAO)
        {
            _adresaDAO = adresaDAO;
            _autorDAO = autorDAO;
        }

        public List<Adresa> GetAllAdrese()
        {
            return _adresaDAO.GetAllAdrese();
        }

        public Adresa? GetAdresaById(int id)
        {
            return _adresaDAO.GetAdresaById(id);
        }

        public Adresa DodajAdresu(Adresa adresa)
        {
            return _adresaDAO.DodajAdresu(adresa);
        }

        public bool IzmeniAdresu(Adresa adresa)
        {
            return _adresaDAO.IzmeniAdresu(adresa) != null;
        }

        public bool UkloniAdresu(int id, out string poruka)
        {
            if(_adresaDAO.GetAdresaById(id) == null)
            {
                poruka = "Adresa nije pronadjena.";
                return false;
            }

            if(_autorDAO.GetAllAutori().Exists(a => a.AdresaId == id))
            {
                poruka = "Adresu koristi autor i adresa ne moze da se obrise;";
                return false;
            }

            _adresaDAO.UkloniAdresu(id);
            poruka = "Adresa je uspesno obrisana.";
            return true;
        }
    }
}

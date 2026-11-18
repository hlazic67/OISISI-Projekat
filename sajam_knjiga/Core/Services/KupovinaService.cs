using System;
using Core.DAO;
using Core.Models;


    namespace Core.Services
    {
        public class KupovinaService
        {
            private readonly KupovinaDAO _kupovinaDAO;
            private readonly PosetilacDAO _posetilacDAO;
            private readonly KnjigaDAO _knjigaDAO;

            public KupovinaService(KupovinaDAO kupovinaDAO,PosetilacDAO posetilacDAO,KnjigaDAO knjigaDAO)
            {
                _kupovinaDAO = kupovinaDAO;
                _posetilacDAO = posetilacDAO;
                _knjigaDAO = knjigaDAO;
            }

            public string? DodajKupovinu(Kupovina kupovina)
            {
                string? greska = ProveriUnos(kupovina);

                if (greska != null) return greska;

                Kupovina? postojeca = _kupovinaDAO.GetAllKupovine().Find(k => 
                 k.PosetilacBrojClanskeKarte == kupovina.PosetilacBrojClanskeKarte &&
                 k.KnjigaISBN == kupovina.KnjigaISBN &&
                 k.DatumKupovine == kupovina.DatumKupovine);

                if (postojeca != null)
                  return "Kupovina vec postoji.";


                _kupovinaDAO.DodajKupovinu(kupovina);

                return null;
            }

            public string? IzmeniKupovinu(Kupovina kupovina)
            {
                string? greska = ProveriUnos(kupovina);

                if (greska != null) return greska;

                Kupovina? postojeca = _kupovinaDAO.GetAllKupovine().Find(k =>
                  k.PosetilacBrojClanskeKarte == kupovina.PosetilacBrojClanskeKarte &&
                  k.KnjigaISBN == kupovina.KnjigaISBN &&
                  k.DatumKupovine == kupovina.DatumKupovine);

                if (postojeca == null) return "Kupovina ne postoji.";

                if (_kupovinaDAO.IzmeniKupovinu(kupovina) == null)
                    return "Izmena kupovine nije uspela.";

                return null;
            }

            public bool UkloniKupovinu(string brojClanskeKarte, string isbn, DateOnly datumKupovine)
            {
                return _kupovinaDAO.UkloniKupovinu(brojClanskeKarte, isbn, datumKupovine) != null;
            }

            private string? ProveriUnos(Kupovina kupovina)
            {
                if (string.IsNullOrWhiteSpace(kupovina.PosetilacBrojClanskeKarte))
                    return "Broj clanske karte je obavezan.";

                if (string.IsNullOrWhiteSpace(kupovina.KnjigaISBN))
                    return "ISBN knjige je obavezan.";

                bool posetilacPostoji = _posetilacDAO.GetAllPosetioci().Exists(p =>
                        p.BrojClanskeKarte == kupovina.PosetilacBrojClanskeKarte);

                if (!posetilacPostoji)
                    return "Posetilac ne postoji.";

                bool knjigaPostoji = _knjigaDAO.GetAllKnjige().Exists(k => k.ISBN == kupovina.KnjigaISBN);

                if (!knjigaPostoji)
                    return "Knjiga ne postoji.";

                if (kupovina.DatumKupovine > DateOnly.FromDateTime(DateTime.Today))
                    return "Datum kupovine nije ispravan (>trenutni).";

                if (kupovina.Ocena < 1 || kupovina.Ocena > 5)
                    return "Ocena mora biti izmedju 1 i 5.";

                return null;
            }
        }
    }



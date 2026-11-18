using Core.DAO;
using Core.Models;
using System;


namespace Core.Services
    {
        public class PosetilacService
        {
            private readonly PosetilacDAO _dao;

            public PosetilacService(PosetilacDAO DAO)
            {
              _dao = DAO;
            }

            public string? DodajPosetioca(Posetilac posetilac)
            {
                string? greska = ProveriUnos(posetilac);

                if (greska != null) return greska;
                 _dao.DodajPosetioca(posetilac);
                  return null;
            }

            public string? IzmeniPosetioca(string brojClanske, Posetilac posetilac)
            {
               string? greska = ProveriUnos(posetilac);

               if (greska != null) return greska;

               Posetilac? postojeci = _dao.GetAllPosetioci().Find(p => p.BrojClanskeKarte == brojClanske);

                if (postojeci == null)
                 return "Posetilac ne postoji.";
                 posetilac.BrojClanskeKarte = brojClanske;

                if (_dao.IzmeniPosetioca(posetilac) == null)
                 return "Izmena posetioca neuspesna.";
                 return null;

            }

            public bool UkloniPosetioca(string brojClanskeK)
            {
                return _dao.UkloniPosetioca(brojClanskeK) != null;
            }

            private string? ProveriUnos(Posetilac posetilac)
            {
                if (string.IsNullOrWhiteSpace(posetilac.Ime))
                    return "Ime je obavezno.";

                if (string.IsNullOrWhiteSpace(posetilac.Prezime))
                    return "Prezime je obavezno.";

                if (string.IsNullOrWhiteSpace(posetilac.EMail))
                    return "E-mail je obavezan.";

                int at = posetilac.EMail.IndexOf('@');
                 int dot;

                if (at < 0)
                dot = -1;
    
                else
                dot = posetilac.EMail.IndexOf('.', at);

               if (at <= 0 || dot == posetilac.EMail.Length - 1 || dot < at + 2)
                    return "E-mail adresa je neispravna.";

               if (posetilac.TrenutnaGodinaClanstva <= 0)
                    return "Godina clanstva je nepostojeca (>0).";

               if (posetilac.DatumRodjenja > DateOnly.FromDateTime(DateTime.Today))
                    return "Datum rodjenja neispravan (>trentni).";

                return null;
            }

        public string? DodajKnjiguNaListuZelja(string brojClanskeKarte, string isbn, KnjigaDAO knjigaDAO)
        {
            bool uspesno = _dao.DodajKnjiguNaListuZelja(brojClanskeKarte, isbn, knjigaDAO);

            if (!uspesno)
                return "Nije moguce dodati knjigu. Provjerite broj clanske karte i ISBN.";

            return null;
        }
    }
    }


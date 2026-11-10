using Core.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Core.DAO
{

    public class PosetilacDAO
    {
        private List<Posetilac> posetioci;
        private Storage<Posetilac> _storage;

        public PosetilacDAO()
        {
            _storage = new Storage<Posetilac>("posetioci.csv");
            posetioci = _storage.Load();
        }


        private string KreirajBrojClanskeKarte()
        {
            int godina = DateTime.Now.Year;

            int redniBroj = posetioci.Where(p => p.BrojClanskeKarte.EndsWith(godina.ToString())).Select(p =>
                                            int.Parse(p.BrojClanskeKarte.Split('-')[1])).DefaultIfEmpty(0).Max() + 1;

            return $"SK-{redniBroj}-{godina}";
        }


        public Posetilac DodajPosetioca(Posetilac posetilac)
        {
            posetilac.BrojClanskeKarte = KreirajBrojClanskeKarte();
            posetioci.Add(posetilac);
            _storage.Save(posetioci);
            return posetilac;
        }


        public Posetilac? IzmeniPosetioca(Posetilac posetilac)
        {
            Posetilac? stariPosetilac = GetPosetilacByBrojClanskeKarte(posetilac.BrojClanskeKarte);

            if (stariPosetilac == null) return null;

            stariPosetilac.Ime = posetilac.Ime;
            stariPosetilac.Prezime = posetilac.Prezime;
            stariPosetilac.DatumRodjenja = posetilac.DatumRodjenja;
            stariPosetilac.Adresa = posetilac.Adresa;
            stariPosetilac.AdresaId = posetilac.AdresaId;
            stariPosetilac.KontaktTelefon = posetilac.KontaktTelefon;
            stariPosetilac.EMail = posetilac.EMail;
            stariPosetilac.TrenutnaGodinaClanstva = posetilac.TrenutnaGodinaClanstva;
            stariPosetilac.Status = posetilac.Status;
            stariPosetilac.ProsecnaOcenaRecenzija = posetilac.ProsecnaOcenaRecenzija;

            _storage.Save(posetioci);

            return stariPosetilac;
        }


        public Posetilac? UkloniPosetioca(string brojClanskeKarte)
        {
            Posetilac? posetilac = GetPosetilacByBrojClanskeKarte(brojClanskeKarte);

            if (posetilac == null) return null;

            posetioci.Remove(posetilac);
            _storage.Save(posetioci);
            return posetilac;
        }

        private Posetilac? GetPosetilacByBrojClanskeKarte(string brojClanskeKarte)
        {
            return posetioci.Find(p => p.BrojClanskeKarte == brojClanskeKarte);
        }

        public List<Posetilac> GetAllPosetioci()
        {
            return posetioci;
        }




    }
}




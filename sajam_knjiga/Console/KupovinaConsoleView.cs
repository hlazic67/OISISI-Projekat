using Core.DAO;
using Core.Models;
using Core.Services;
using Sajam.Console;
using System;
using System.Collections.Generic;

namespace SajamKnjiga.ConsoleUI
{
    class KupovinaConsoleView
    {
        private readonly KupovinaDAO _kupovinaDAO;
        private readonly PosetilacDAO _posetilacDAO;
        private readonly KnjigaDAO _knjigaDAO;
        private readonly KupovinaService _kupovinaService;

        public KupovinaConsoleView(KupovinaDAO kupovinaDAO, PosetilacDAO posetilacDAO,KnjigaDAO knjigaDAO,KupovinaService kupovinaService)
        {
            _kupovinaDAO = kupovinaDAO;
            _posetilacDAO = posetilacDAO;
            _knjigaDAO = knjigaDAO;
            _kupovinaService = kupovinaService;
        }

        public void RunKupovineMenu()
        {
            while (true)
            {
                ShowMenu();

                string userInput = System.Console.ReadLine() ?? "0";

                if (userInput == "0")
                    break;

                HandleMenuInput(userInput);
            }
        }

        private void ShowMenu()
        {
            System.Console.WriteLine("\n===== KUPOVINE =====");
            System.Console.WriteLine("1: Prikaz svih kupovina");
            System.Console.WriteLine("2: Prikaz kupovina pojedinacnog posetioca");
            System.Console.WriteLine("3: Dodavanje kupovine");
            System.Console.WriteLine("4: Izmena kupovine");
            System.Console.WriteLine("5: Brisanje kupovine");
            System.Console.WriteLine("0: Povratak na glavni meni");
        }

        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    PrikaziSveKupovine();
                    break;

                case "2":
                    PrikaziKupovinePosetioca();
                    break;

                case "3":
                    DodajKupovinu();
                    break;

                case "4":
                    IzmeniKupovinu();
                    break;

                case "5":
                    UkloniKupovinu();
                    break;

                default:
                    System.Console.WriteLine("Nepostojeca opcija!");
                    break;
            }
        }

        private void PrikaziSveKupovine()
        {
            System.Console.WriteLine("\n===== KUPOVINE =====");

            foreach (Kupovina k in _kupovinaDAO.GetAllKupovine())
            {

                Posetilac? posetilac = _posetilacDAO.GetAllPosetioci().Find(p => p.BrojClanskeKarte == k.PosetilacBrojClanskeKarte);
                Knjiga? knjiga = _knjigaDAO.GetKnjigaByIsbn(k.KnjigaISBN);

                if (posetilac != null)
                    System.Console.WriteLine($"Posetilac: {posetilac.Ime} {posetilac.Prezime}");

                if (knjiga != null)
                    System.Console.WriteLine($"Knjiga: {knjiga.Naziv}");

                System.Console.WriteLine(k);
                System.Console.WriteLine("-------------------------");
            }
        }

        private void PrikaziKupovinePosetioca()
        {
            System.Console.WriteLine("Unesite broj clanske karte:");
            string brojClanskeKarte = System.Console.ReadLine() ?? string.Empty;

            Posetilac? posetilac = _posetilacDAO.GetAllPosetioci().Find(p => p.BrojClanskeKarte == brojClanskeKarte);

            if (posetilac == null)
            {
                System.Console.WriteLine("Posetilac nije pronadjen.");
                return;
            }

            List<Kupovina> kupovine = _kupovinaDAO.GetKupovinePosetioca(brojClanskeKarte);

            if (kupovine.Count == 0)
            {
                System.Console.WriteLine("Posetilac jos nema kupovina.");
                return;
            }

            System.Console.WriteLine($"\n===== KUPOVINE POSETIOCA: {posetilac.Ime} {posetilac.Prezime} =====");

            foreach (Kupovina k in kupovine)
            {
                Knjiga? knjiga = _knjigaDAO.GetKnjigaByIsbn(k.KnjigaISBN);

                if (knjiga != null)
                    System.Console.WriteLine($"Knjiga: {knjiga.Naziv}");

                System.Console.WriteLine(k);
                System.Console.WriteLine("-------------------------");
            }
        }

        private void DodajKupovinu()
        {
            Kupovina kupovina = UnesiKupovinu();

            string? greska = _kupovinaService.DodajKupovinu(kupovina);

            if (greska != null)
            {
                System.Console.WriteLine(greska);
                return;
            }

            System.Console.WriteLine("Kupovina je uspesno dodata.");
        }

        private Kupovina UnesiKupovinu()
        {
            System.Console.WriteLine("Unesite broj clanske karte:");
            string brojClanskeKarte = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite ISBN knjige:");
            string isbn = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite datum kupovine (yyyy-MM-dd):");
            DateOnly datumKupovine = DateOnly.Parse(System.Console.ReadLine()!);

            System.Console.WriteLine("Unesite ocenu:");
            int ocena = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Unesite komentar:");
            string komentar = System.Console.ReadLine() ?? string.Empty;

            return new Kupovina(brojClanskeKarte, isbn, datumKupovine, ocena, komentar);
        }

        private Kupovina UnesiIzmenuK()
        {
            System.Console.WriteLine("Unesite broj clanske karte:");
            string brojClanskeKarte = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite ISBN knjige:");
            string isbn = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite datum kupovine (yyyy-MM-dd):");
            DateOnly datumKupovine = DateOnly.Parse(System.Console.ReadLine()!);

            System.Console.WriteLine("Unesite novu ocenu:");
            int ocena = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Unesite novi komentar:");
            string komentar = System.Console.ReadLine() ?? string.Empty;

            return new Kupovina(brojClanskeKarte, isbn, datumKupovine, ocena, komentar);
        }

        private void IzmeniKupovinu()
        {
            Kupovina kupovina = UnesiIzmenuK();

            string? greska = _kupovinaService.IzmeniKupovinu(kupovina);

            if (greska != null)
            {
                System.Console.WriteLine(greska);
                return;
            }

            System.Console.WriteLine("Kupovina je uspesno izmenjena.");
        }

        private void UkloniKupovinu()
        {
            System.Console.WriteLine("Unesite broj clanske karte:");
            string brojClanskeKarte = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite ISBN knjige:");
            string isbn = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite datum kupovine (yyyy-MM-dd):");
            DateOnly datumKupovine = DateOnly.Parse(System.Console.ReadLine()!);

            bool uspesno = _kupovinaService.UkloniKupovinu(brojClanskeKarte, isbn, datumKupovine);

            if (!uspesno)
            {
                System.Console.WriteLine("Kupovina ne postoji.");
                return;
            }

            System.Console.WriteLine("Kupovina je uspesno obrisana.");
        }

    }
}

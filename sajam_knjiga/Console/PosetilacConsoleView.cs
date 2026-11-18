using System;
using Core.DAO;
using Core.Models;
using Core.Services;
using Sajam.Console;
using System.Collections.Generic;
using System.Linq;

namespace SajamKnjiga.ConsoleUI
{ 

    class PosetilacConsoleView
    {
        private readonly PosetilacDAO _posetilacDAO;
        private readonly KupovinaDAO _kupovinaDAO;
        private readonly KnjigaDAO _knjigaDAO;
        private readonly PosetilacService _posetilacService;

        public PosetilacConsoleView(PosetilacDAO posetilacDAO,KupovinaDAO kupovinaDAO,KnjigaDAO knjigaDAO,PosetilacService posetilacService)
        {
            _posetilacDAO = posetilacDAO;
            _kupovinaDAO = kupovinaDAO;
            _knjigaDAO = knjigaDAO;
            _posetilacService = posetilacService;
        }

        public void RunPosetiociMenu()
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
            System.Console.WriteLine("\n===== POSETIOCI =====");
            System.Console.WriteLine("1: Prikaz svih posetilaca");
            System.Console.WriteLine("2: Prikaz pojedinacnog posetioca");
            System.Console.WriteLine("3: Dodavanje posetioca");
            System.Console.WriteLine("4: Izmena posetioca");
            System.Console.WriteLine("5: Brisanje posetioca");
            System.Console.WriteLine("6: Dodavanje knjige na listu zelja");
            System.Console.WriteLine("0: Povratak na glavni meni");
        }

        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    PrikaziSvePosetioce();
                    break;

                case "2":
                    PrikaziPosetioca();
                    break;

                case "3":
                    DodajPosetioca();
                    break;

                case "4":
                    IzmeniPosetioca();
                    break;

                case "5":
                    UkloniPosetioca();
                    break;

                case "6":
                    DodajKnjiguNaListuZelja();
                    break;

                default:
                    System.Console.WriteLine("Nepostojeca opcija!");
                    break;
            }
        }

        private void PrikaziSvePosetioce()
        {
            System.Console.WriteLine("\n===== POSETIOCI =====");

            foreach (Posetilac p in _posetilacDAO.GetAllPosetioci())
            {
                System.Console.WriteLine(p);
                System.Console.WriteLine("Spisak kupljenih knjiga:");
                List<Kupovina> kupovine = _kupovinaDAO.GetAllKupovine().Where(k => k.PosetilacBrojClanskeKarte == p.BrojClanskeKarte).ToList();

                foreach (Kupovina k in kupovine)
                {
                    Knjiga? knjiga = _knjigaDAO.GetKnjigaByIsbn(k.KnjigaISBN);

                    if (knjiga != null)
                        System.Console.WriteLine(knjiga.Naziv);
                }

                System.Console.WriteLine("\nLista želja:");

                foreach (Knjiga zknjiga in p.ListaZelja)
                {
                    Knjiga? knjiga = _knjigaDAO.GetKnjigaByIsbn(zknjiga.ISBN);

                    if (knjiga != null)
                        System.Console.WriteLine(knjiga.Naziv);
                }
                System.Console.WriteLine("-------------------------");
            }
        }

        private void PrikaziPosetioca()
        {
            string brojClanskeKarte = UnesiBrojClanskeKarte();

            Posetilac? p = _posetilacDAO.GetAllPosetioci().Find(p => p.BrojClanskeKarte == brojClanskeKarte);

            if (p == null)
            {
                System.Console.WriteLine("Posetilac nije pronadjen.");
                return;
            }

            System.Console.WriteLine("\n===== PODACI O POSETIOCU =====");
            System.Console.WriteLine(p);

            System.Console.WriteLine("\nSpisak kupljenih knjiga:");

            List<Kupovina> kupovine = _kupovinaDAO.GetAllKupovine() .Where(k =>
                           k.PosetilacBrojClanskeKarte == p.BrojClanskeKarte).ToList();

            foreach (Kupovina k in kupovine)
            {
                Knjiga? knjiga = _knjigaDAO.GetKnjigaByIsbn(k.KnjigaISBN);

                if (knjiga != null)
                    System.Console.WriteLine(knjiga.Naziv);
            }

            System.Console.WriteLine("\nLista zelja:");

            foreach (Knjiga zknjiga in p.ListaZelja)
            {
                Knjiga? knjiga = _knjigaDAO.GetKnjigaByIsbn(zknjiga.ISBN);

                if (knjiga != null)
                    System.Console.WriteLine(knjiga.Naziv);
            }

            System.Console.WriteLine("-------------------------");
        }

        private void DodajPosetioca()
        {
            Posetilac posetilac = UnesiPosetioca();

            string? greska = _posetilacService.DodajPosetioca(posetilac);

            if (greska != null)
            {
                System.Console.WriteLine(greska);
                return;
            }

            System.Console.WriteLine("Posetilac je uspesno dodat.");
        }

        private Posetilac UnesiPosetioca()
        {
            System.Console.WriteLine("Unesite ime:");
            string ime = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite prezime:");
            string prezime = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite datum rodjenja (yyyy-MM-dd):");
            DateOnly datumRodjenja = DateOnly.Parse(System.Console.ReadLine()!);

            System.Console.WriteLine("Unesite ulicu:");
            string ulica = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite broj:");
            string broj = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite grad:");
            string grad = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite drzavu:");
            string drzava = System.Console.ReadLine() ?? string.Empty;

            Adresa adresa = new Adresa(0, ulica, broj, drzava, grad);

            System.Console.WriteLine("Unesite kontakt telefon:");
            string telefon = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite e-mail:");
            string email = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite trenutnu godinu clanstva:");
            int godinaClanstva = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Unesite status (R ili V):");
            TStatus status = Enum.Parse<TStatus>(System.Console.ReadLine()!, true);

            System.Console.WriteLine("Unesite prosecnu ocenu recenzija:");
            double ocena = double.Parse(System.Console.ReadLine()!);

            return new Posetilac(ime, prezime, datumRodjenja, adresa, 0,
                telefon, email, string.Empty, godinaClanstva, status, ocena
            );
        }

        private void IzmeniPosetioca()
        {
            string brojClanskeKarte = UnesiBrojClanskeKarte();

            Posetilac posetilac = UnesiPosetioca();

            string? greska = _posetilacService.IzmeniPosetioca(brojClanskeKarte, posetilac);

            if (greska != null)
            {
                System.Console.WriteLine(greska);
                return;
            }

            System.Console.WriteLine("Posetilac je uspesno izmenjen.");
        }

        private void UkloniPosetioca()
        {
            string brojClanskeKarte = UnesiBrojClanskeKarte();

            bool uspesno = _posetilacService.UkloniPosetioca(brojClanskeKarte);

            if (!uspesno)
            {
                System.Console.WriteLine("Posetilac nije pronadjen.");
                return;
            }

            System.Console.WriteLine("Posetilac je uspesno obrisan.");
        }

        private void DodajKnjiguNaListuZelja()
        {
            string brojClanskeKarte = UnesiBrojClanskeKarte();

            System.Console.WriteLine("Unesite ISBN knjige:");
            string isbn = System.Console.ReadLine() ?? string.Empty;

            string? greska = _posetilacService.DodajKnjiguNaListuZelja(brojClanskeKarte, isbn, _knjigaDAO);

            if (greska != null)
            {
                System.Console.WriteLine(greska);
                return;
            }

            System.Console.WriteLine("Knjiga je dodata na listu zelja.");
        }



        private string UnesiBrojClanskeKarte()
        {
            System.Console.WriteLine("Unesite broj clanske karte:");
            return System.Console.ReadLine() ?? string.Empty;
        }
    }
}

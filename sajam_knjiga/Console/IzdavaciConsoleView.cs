using Core.DAO;
using Core.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SajamKnjiga.ConsoleUI
{
     class IzdavaciConsoleView
    {
        private readonly IzdavacDAO _izdavacDAO;
        private readonly AutorDAO _autorDAO;
        
        private readonly KnjigaDAO _knjigaDAO;
        private readonly IzdavacService _izdavacService;

        private readonly AutorKnjigaDAO _autorKnjigaDAO;

        public IzdavaciConsoleView(IzdavacDAO izdavacDAO,AutorDAO autorDAO, KnjigaDAO knjigaDAO, AutorKnjigaDAO autorKnjigaDAO, IzdavacService izdavacService)
        {
            _izdavacDAO = izdavacDAO;
            _autorDAO = autorDAO;
            _knjigaDAO = knjigaDAO;
            _autorKnjigaDAO = autorKnjigaDAO;
            _izdavacService = izdavacService;
        }

        public void RunIzdavaciMenu()
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
            System.Console.WriteLine("\n===== IZDAVACI =====");
            System.Console.WriteLine("1: Prikaz svih izdavaca");
            System.Console.WriteLine("2: Dodavanje izdavaca");
            System.Console.WriteLine("3: Prikaz pojedinacnog izdavaca");
            System.Console.WriteLine("4: Izmena izdavaca");
            System.Console.WriteLine("5: Brisanje izdavaca");
            System.Console.WriteLine("0: Povratak na glavni meni");
        }

        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    PrikaziSveIzdavace();
                    break;

                case "2":
                    DodajIzdavaca();
                    break;

                case "3":
                    PrikaziPojedinacnogIzdavaca();
                    break;

                case "4":
                    IzmeniIzdavaca();
                    break;

                case "5":
                    UkloniIzdavaca();
                    break;

                default:
                    System.Console.WriteLine("Nepostojeca opcija!");
                    break;
            }
        }
        private void PrikaziSveIzdavace()
        {
            System.Console.WriteLine("\n===== IZDAVACI =====");

            foreach (Izdavac i in _izdavacDAO.GetAllIzdavaci())
            {
                Autor? sef = _autorDAO.GetAutorById(i.SefID);

                System.Console.WriteLine($"\nSifra: {i.Sifra}");
                System.Console.WriteLine($"Naziv: {i.Naziv}");
                System.Console.WriteLine($"Sef izdavaca: {sef?.ToString() ?? "-"}");

                System.Console.WriteLine("Knjige:");

                List<Knjiga> knjige = _knjigaDAO.GetAllKnjige().Where(k => k.IzdavacSifra == i.Sifra).ToList();

                foreach (Knjiga k in knjige)
                {
                    System.Console.WriteLine($"- {k.Naziv}");

                    List<AutorKnjiga> veze = _autorKnjigaDAO.GetAllVeze().Where(v => v.ISBN == k.ISBN).ToList();

                    System.Console.WriteLine(" ID-jevi autora:");

                    if (veze.Count == 0)
                    {
                        System.Console.WriteLine(" Nema povezanih autora.");
                    }
                    else
                    {
                        foreach (AutorKnjiga v in veze)
                        {
                            System.Console.WriteLine($"  - {v.AutorId}");
                        }
                    }
                }

                System.Console.WriteLine("-------------------------");
            }
        }

        private void DodajIzdavaca()
        {
            Izdavac izdavac = UnesiIzdavaca();

            string? greska = _izdavacService.DodajIzdavaca(izdavac);

            if (greska != null)
            {
                System.Console.WriteLine(greska);
                return;
            }

            System.Console.WriteLine("Izdavac je uspesno dodat.");
        }

        private Izdavac UnesiIzdavaca()
        {
            System.Console.WriteLine("Unesite naziv izdavaca:");
            string naziv = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite ID sefa izdavaca:");
            int sefID;

            while (!int.TryParse(System.Console.ReadLine(), out sefID))
            {
                System.Console.WriteLine("Unesite ispravan ID:");
            }

            Autor? sef = _autorDAO.GetAutorById(sefID);

            return new Izdavac(0, naziv, sef!, sefID);
        }

        private void PrikaziPojedinacnogIzdavaca()
        {
            System.Console.WriteLine("Unesite sifru izdavaca:");
            int sifra;

            while (!int.TryParse(System.Console.ReadLine(), out sifra))
            {
                System.Console.WriteLine("Unesite ispravnu sifru:");
            }

            Izdavac? izdavac = _izdavacDAO.GetAllIzdavaci().Find(i => i.Sifra == sifra);

            if (izdavac == null)
            {
                System.Console.WriteLine("Izdavac ne postoji.");
                return;
            }

            Autor? sef = _autorDAO.GetAutorById(izdavac.SefID);

            System.Console.WriteLine("\n===== IZDAVAC =====");
            System.Console.WriteLine($"Sifra: {izdavac.Sifra}");
            System.Console.WriteLine($"Naziv: {izdavac.Naziv}");
            System.Console.WriteLine($"Sef izdavaca: {sef?.ToString() ?? "-"}");

            List<Knjiga> knjige = _knjigaDAO.GetAllKnjige().Where(k => k.IzdavacSifra == izdavac.Sifra).ToList();

            System.Console.WriteLine("\nKnjige:");

            if (knjige.Count == 0)
            {
                System.Console.WriteLine("Izdavac nema knjiga.");
            }

            foreach (Knjiga k in knjige)
            {
                System.Console.WriteLine($"- {k.Naziv} (ISBN: {k.ISBN})");

                List<AutorKnjiga> veze = _autorKnjigaDAO.GetAllVeze().Where(v => v.ISBN == k.ISBN).ToList();

                System.Console.WriteLine(" ID-jevi autora:");

                if (veze.Count == 0)
                {
                    System.Console.WriteLine(" Nema povezanih autora.");
                }
                else
                {
                    foreach (AutorKnjiga v in veze)
                    {
                        System.Console.WriteLine($"  - {v.AutorId}");
                    }
                }
            }
        }
        private void IzmeniIzdavaca()
        {
            System.Console.WriteLine("Unesite sifru izdavaca za izmeniti:");
            int sifra;

            while (!int.TryParse(System.Console.ReadLine(), out sifra))
            {
                System.Console.WriteLine("Unesite ispravnu sifru:");
            }

            Izdavac? izdavac = _izdavacDAO.GetAllIzdavaci().Find(i => i.Sifra == sifra);

            if (izdavac == null)
            {
                System.Console.WriteLine("Izdavac ne postoji.");
                return;
            }

            System.Console.WriteLine("Unesite novi naziv izdavaca:");
            string naziv = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite novi ID sefa izdavaca:");
            int sefID;

            while (!int.TryParse(System.Console.ReadLine(), out sefID))
            {
                System.Console.WriteLine("Unesite ispravan ID:");
            }

            Autor? sef = _autorDAO.GetAutorById(sefID);

            Izdavac izmena = new Izdavac(sifra, naziv, sef!, sefID);

            string? greska = _izdavacService.IzmeniIzdavaca(izmena);

            if (greska != null)
            {
                System.Console.WriteLine(greska);
                return;
            }

            System.Console.WriteLine("Izdavac je uspesno izmenjen.");
        }

        private void UkloniIzdavaca()
        {
            System.Console.WriteLine("Unesite sifru izdavaca:");
            int sifra;

            while (!int.TryParse(System.Console.ReadLine(), out sifra))
            {
                System.Console.WriteLine("Unesite ispravnu sifru:");
            }

            bool obrisan = _izdavacService.UkloniIzdavaca(sifra);

            if (obrisan)
            {
                System.Console.WriteLine("Izdavac je uspesno obrisan.");
            }
            else
            {
                System.Console.WriteLine("Izdavac ne postoji.");
            }
        }

    }
}

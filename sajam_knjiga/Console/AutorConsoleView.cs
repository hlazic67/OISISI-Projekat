using Core.Models;
using Core.Services;
using Sajam.Console;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SajamKnjiga.ConsoleUI;
class AutorConsoleView
{
    private readonly AutorService _autorService;

    public AutorConsoleView(AutorService autorService)
    {
        _autorService = autorService;
    }
    //preuzeta metoda sa 3.vezbi
    public void RunMenu()
    {
        while (true)
        {
            ShowMenu();
            string userInput = System.Console.ReadLine() ?? "0";
            if (userInput == "0") break;
            HandleMenuInput(userInput);
        }
    }

    private void ShowMenu()
    {
        System.Console.WriteLine("\n===== AUTORI =====");
        System.Console.WriteLine("1: Prikaz svih autora");
        System.Console.WriteLine("2: Dodavanje autora");
        System.Console.WriteLine("3: Izmena autora");
        System.Console.WriteLine("4: Brisanje autora");
        System.Console.WriteLine("0: Nazad");
    }

    private void HandleMenuInput(string input)
    {
        switch (input)
        {
            case "1":
                PrikaziSveAutore();
                break;

            case "2":
                DodajAutora();
                break;

            case "3":
                IzmeniAutora();
                break;

            case "4":
                UkloniAutora();
                break;

            default:
                System.Console.WriteLine("Nepostojeca opcija.");
                break;
        }
    }

    private void PrikaziSveAutore()
    {
        System.Console.WriteLine("\n===== AUTORI =====");

        foreach(Autor a in _autorService.GetAllAutori())
        {
            System.Console.WriteLine(a);
            System.Console.WriteLine($"Adresa: {a.Adresa?.ToString() ?? "nepoznata"}");

            string knjige = "";
            for (int i = 0; i < a.Knjige.Count; i++)
            {
                knjige += a.Knjige[i].Naziv;
                if (i < a.Knjige.Count - 1)
                    knjige += ", ";
            }
            System.Console.WriteLine($"Knjige: {knjige}");
            System.Console.WriteLine("-------------------------");
        }
    }

    private void DodajAutora()
    {
        Autor autor = UnesiAutora();
        Adresa adresa = AdresaConsoleView.UnesiAdresu();

        _autorService.DodajAutora(autor, adresa);
        System.Console.WriteLine("Autor je uspesno dodat.");
    }

    private Autor UnesiAutora()
    {
        System.Console.WriteLine("Unesite ime:");
        string ime = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite prezime:");
        string prezime = System.Console.ReadLine() ?? string.Empty;

        DateOnly datumRodjenja = UnesiDatum("Unesite datum rodjenja (yyyy-MM-dd):");

        System.Console.WriteLine("Unesite telefon:");
        string telefon = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite e-mail:");
        string email = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite broj licne karte:");
        string brojLicneKarte = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite godine iskustva:");
        int godineIskustva = ConsoleViewUtils.SafeInputInt();

        return new Autor
        {
            Ime = ime,
            Prezime = prezime,
            DatumRodjenja = datumRodjenja,
            Telefon = telefon,
            Email = email,
            BrojLicneKarte = brojLicneKarte,
            GodineIskustva = godineIskustva,
        };
    }

    private DateOnly UnesiDatum(string poruka)
    {
        System.Console.WriteLine(poruka);
        DateOnly datum;

        //provera formata
        while (!DateOnly.TryParseExact(System.Console.ReadLine(), "yyyy-MM-dd",
                  CultureInfo.InvariantCulture, DateTimeStyles.None, out datum))
        {
            System.Console.WriteLine("Neispravan datum, pokusajte ponovo (yyyy-MM-dd):");
        }

        return datum;
    }
       
    private int UnesiIdAutora()
    {
        System.Console.WriteLine("Unesite ID autora:");
        return ConsoleViewUtils.SafeInputInt();
    }

    private void IzmeniAutora()
    {
        int id = UnesiIdAutora();
        if(_autorService.GetAutorById(id) == null)
        {
            System.Console.WriteLine("Autor nije pronadjen.");
            return;
        }

        Autor autor = UnesiAutora();
        autor.Id = id;
        Adresa adresa = AdresaConsoleView.UnesiAdresu();

        if(!_autorService.IzmeniAutora(autor, adresa))
        {
            System.Console.WriteLine("Neuspela izmena.");
            return;
        }
        System.Console.WriteLine("Autor je uspesno izmenjen.");
    }

    private void UkloniAutora()
    {
        int id = UnesiIdAutora();

        _autorService.ObrisiAutora(id, out string poruka);
        System.Console.WriteLine(poruka);

    }
}

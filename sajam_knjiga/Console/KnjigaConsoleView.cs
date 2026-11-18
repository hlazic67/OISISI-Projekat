using Core.Models;
using Core.Services;
using Sajam.Console;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SajamKnjiga.ConsoleUI;

class KnjigaConsoleView
{
    private readonly KnjigaService _knjigaService;

    public KnjigaConsoleView(KnjigaService knjigaService)
    {
        _knjigaService = knjigaService;
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
        System.Console.WriteLine("\n===== KNJIGE =====");
        System.Console.WriteLine("1: Prikaz svih knjiga");
        System.Console.WriteLine("2: Dodavanje knjige");
        System.Console.WriteLine("3: Izmena knjige");
        System.Console.WriteLine("4: Brisanje knjige");
        System.Console.WriteLine("0: Nazad");
    }

    private void HandleMenuInput(string input)
    {
        switch (input)
        {
            case "1":
                PrikaziSveKnjige();
                break;

            case "2":
                DodajKnjigu();
                break;

            case "3":
                IzmeniKnjigu();
                break;

            case "4":
                UkloniKnjigu();
                break;

            default:
                System.Console.WriteLine("Nepostojeca opcija.");
                break;
        }
    }

    private void PrikaziSveKnjige()
    {
        System.Console.WriteLine("\n===== KNJIGE =====");

        foreach (Knjiga k in _knjigaService.GetAllKnjige())
        {
            System.Console.WriteLine(k);
            System.Console.WriteLine("-------------------------");
        }
    }

    private void DodajKnjigu()
    {
        System.Console.WriteLine("Unesite ISBN:");
        string isbn = System.Console.ReadLine() ?? string.Empty;

        Knjiga knjiga = UnesiKnjigu(isbn);
        if (knjiga.IzdavacSifra == -1) return;

        _knjigaService.DodajKnjigu(knjiga, out string poruka);
        System.Console.WriteLine(poruka);
    }

    private Knjiga UnesiKnjigu(string isbn)
    {
        System.Console.WriteLine("Unesite naziv:");
        string naziv = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite zanr:");
        string zanr = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite godinu izdanja:");
        int godinaIzdanja = ConsoleViewUtils.SafeInputInt();

        double cena = UnesiCenu();

        System.Console.WriteLine("Unesite broj strana:");
        int brojStrana = ConsoleViewUtils.SafeInputInt();

        int izdavacSifra = UnesiSifruIzdavaca();

        return new Knjiga(isbn, naziv, zanr, godinaIzdanja, cena, brojStrana, izdavacSifra);
    }

    private double UnesiCenu()
    {
        System.Console.WriteLine("Unesite cenu:");
        double cena;

        //obezbedjeno da cena moze da bude i sa zarezom i sa tackom
        while (!double.TryParse((System.Console.ReadLine() ?? string.Empty).Replace(',', '.'),
                           NumberStyles.Float, CultureInfo.InvariantCulture, out cena) || cena < 0)
        {
            System.Console.WriteLine("Neispravna cena, pokusajte ponovo:");
        }

        return cena;
    }

    private int UnesiSifruIzdavaca()
    {
        if (!_knjigaService.PostojeIzdavaci())
        {
            System.Console.WriteLine("Ne postoji nijedan izdavac, prvo dodajte izdavaca.");
            return -1;
        }

        System.Console.WriteLine("Unesite sifru izdavaca:");
        int sifra = ConsoleViewUtils.SafeInputInt();

        while (!_knjigaService.PostojiIzdavac(sifra))
        {
            System.Console.WriteLine("Izdavac ne postoji, pokusajte ponovo:");
            sifra = ConsoleViewUtils.SafeInputInt();
        }

        return sifra;
    }

    private void IzmeniKnjigu()
    {
        System.Console.WriteLine("Unesite ISBN knjige:");
        string isbn = System.Console.ReadLine() ?? string.Empty;

        if (_knjigaService.GetKnjigaByIsbn(isbn) == null)
        {
            System.Console.WriteLine("Knjiga nije pronadjena.");
            return;
        }

        Knjiga knjiga = UnesiKnjigu(isbn);
        if (knjiga.IzdavacSifra == -1) return;

        _knjigaService.IzmeniKnjigu(knjiga, out string poruka);
        System.Console.WriteLine(poruka);
    }

    private void UkloniKnjigu()
    {
        System.Console.WriteLine("Unesite ISBN knjige:");
        string isbn = System.Console.ReadLine() ?? string.Empty;

        _knjigaService.ObrisiKnjigu(isbn, out string poruka);
        System.Console.WriteLine(poruka);
    }
}

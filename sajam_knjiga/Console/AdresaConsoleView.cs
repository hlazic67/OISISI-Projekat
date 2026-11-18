using System;
using System.Collections.Generic;
using System.Text;
using Core.Models;
using Core.Services;
using Sajam.Console;

namespace SajamKnjiga.ConsoleUI;

class AdresaConsoleView
{
    private readonly AdresaService _adresaService;
    
    public AdresaConsoleView(AdresaService adresaService)
    {
        _adresaService = adresaService;
    }

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
        System.Console.WriteLine("\n===== ADRESE =====");
        System.Console.WriteLine("1: Prikaz svih adresa");
        System.Console.WriteLine("2: Dodavanje adrese");
        System.Console.WriteLine("3: Izmena adrese");
        System.Console.WriteLine("4: Brisanje adrese");
        System.Console.WriteLine("0: Nazad");
    }
    
    private void HandleMenuInput(string input)
    {
        switch (input)
        {
            case "1":
                PrikaziSveAdrese();
                break;

            case "2":
                DodajAdresu();
                break;

            case "3":
                IzmeniAdresu();
                break;

            case "4":
                UkloniAdresu();
                break;

            default:
                System.Console.WriteLine("Nepostojeca opcija.");
                break;
        }
    }

    private void PrikaziSveAdrese()
    {
        System.Console.WriteLine("\n===== ADRESE =====");
        
        foreach(Adresa a in _adresaService.GetAllAdrese())
        {
            System.Console.WriteLine($"ID: {a.Id}, {a}");
            System.Console.WriteLine("-------------------------");

        }
    }

    private void DodajAdresu()
    {
        _adresaService.DodajAdresu(UnesiAdresu());
        System.Console.WriteLine("Adresa je uspesno dodata.");
    }

    public static Adresa UnesiAdresu()
    {
        System.Console.WriteLine("Unesite ulicu:");
        string ulica = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite broj:");
        string broj = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite grad:");
        string grad = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite drzavu:");
        string drzava = System.Console.ReadLine() ?? string.Empty;

        return new Adresa
        {
            Ulica = ulica,
            Broj = broj,
            Grad = grad,
            Drzava = drzava
        };
    }

    private int UnesiIdAdrese()
    {
        System.Console.WriteLine("Uneite ID adrese: ");
        return ConsoleViewUtils.SafeInputInt();
    }

    private void IzmeniAdresu()
    {
        int id = UnesiIdAdrese();
        if(_adresaService.GetAdresaById(id) == null)
        {
            System.Console.WriteLine("Adresa nije pronadjena.");
            return;
        }

        Adresa adresa = UnesiAdresu();
        adresa.Id = id;

        if (!_adresaService.IzmeniAdresu(adresa))
        {
            System.Console.WriteLine("Neuspela izmena.");
            return;
        }

        System.Console.WriteLine("Adresa je uspesno izmenjena.");
    }

    private void UkloniAdresu()
    {
        int id = UnesiIdAdrese();
        _adresaService.UkloniAdresu(id, out string poruka);
        System.Console.WriteLine(poruka);
    }
}

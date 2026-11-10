using System;
using System.Collections.Generic;
using System.Text;
using Core.DAO;
using Core.Models;
using Sajam.Console;

namespace SajamKnjiga.ConsoleUI;

class SajamConsoleView
{
    private readonly PosetilacDAO _posetilacDAO;
    private readonly KupovinaDAO _kupovinaDAO;
    private readonly IzdavacDAO _izdavacDAO;

    public SajamConsoleView(PosetilacDAO posetilacDAO,KupovinaDAO kupovinaDAO,IzdavacDAO izdavacDAO)
    {
        _posetilacDAO = posetilacDAO;
        _kupovinaDAO = kupovinaDAO;
        _izdavacDAO = izdavacDAO;
    }

    public void RunMenu() //metoda preuzeta iz materijala za Vjezbe 3
    {
        while (true)
        {
            ShowMenu();
            string userInput = Console.ReadLine() ?? "0";
            if (userInput == "0") break;
            HandleMenuInput(userInput);
        }
    }

    private void ShowMenu()
    {
        System.Console.WriteLine("\n===== SAJAM KNJIGA =====");
        System.Console.WriteLine("1: Prikaz svih posetilaca");
        System.Console.WriteLine("2: Prikaz svih kupovina");
        System.Console.WriteLine("3: Prikaz svih izdavaca");
        System.Console.WriteLine("4: Dodavanje posetioca");
        System.Console.WriteLine("5: Dodavanje kupovine");
        System.Console.WriteLine("6: Dodavanje izdavaca");
        System.Console.WriteLine("7: Izmena posetioca");
        System.Console.WriteLine("8: Izmena kupovine");
        System.Console.WriteLine("9: Izmena izdavaca");
        System.Console.WriteLine("10: Brisanje posetioca");
        System.Console.WriteLine("11: Brisanje kupovine");
        System.Console.WriteLine("12: Brisanje izdavaca");
        System.Console.WriteLine("0: Izlaz");
    }


    private void HandleMenuInput(string input)
    {
        switch (input)
        {
            case "1":
                PrikaziSvePosetioce();
                break;

            case "2":
                PrikaziSveKupovine();
                break;

            case "3":
                PrikaziSveIzdavace();
                break;

            case "4":
                DodajPosetioca();
                break;

            case "5":
                DodajKupovinu();
                break;

            case "6":
               DodajIzdavaca();
                break;

            case "7":
                IzmeniPosetioca();
                break;

            case "8":
                IzmeniKupovinu();
                break;

            case "9":
                IzmeniIzdavaca();
                break;

            case "10":
                UkloniPosetioca();
                break;

            case "11":
                UkloniKupovinu();
                break;

            case "12":
                UkloniIzdavaca();
                break;

            default:
                Console.WriteLine("Nepostojeca opcija!");
                break;
        }
    }

    private void PrikaziSvePosetioce()
    {
        Console.WriteLine("\n===== POSETIOCI =====");

        foreach (Posetilac p in _posetilacDAO.GetAllPosetioci())
        {
            Console.WriteLine(p);
            Console.WriteLine("-------------------------");
        }
    }

    private void PrikaziSveKupovine()
    {
        Console.WriteLine("\n===== KUPOVINE =====");

        foreach (Kupovina k in _kupovinaDAO.GetAllKupovine())
        {
            Console.WriteLine(k);
            Console.WriteLine("-------------------------");
        }
    }

    private void PrikaziSveIzdavace()
    {
        Console.WriteLine("\n===== IZDAVACI =====");

        foreach (Izdavac i in _izdavacDAO.GetAllIzdavaci())
        {
            Console.WriteLine(i);
            Console.WriteLine("-------------------------");
        }
    }

    //posetilac-dodavanje u listu, unos, uklanjanje i izmena
    private void DodajPosetioca()
    {
        Posetilac posetilac = UnesiPosetioca();
        _posetilacDAO.DodajPosetioca(posetilac);
        Console.WriteLine("Posetilac je uspesno dodat.");
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
        Posetilac? postojeciPosetilac = _posetilacDAO.GetAllPosetioci().Find(p => p.BrojClanskeKarte == brojClanskeKarte);
        if (postojeciPosetilac == null)
        {
            Console.WriteLine("Posetilac nije pronadjen.");
            return;
        }

        Posetilac posetilac = UnesiPosetioca();
        posetilac.BrojClanskeKarte=brojClanskeKarte;
        Posetilac? izmenjenPosetilac = _posetilacDAO.IzmeniPosetioca(posetilac);
        if (izmenjenPosetilac == null)
        {
            System.Console.WriteLine("Neuspela izmena.");
            return;
        }

        System.Console.WriteLine("Posetilac je uspesno izmenjen.");
    }

    private string UnesiBrojClanskeKarte()
    {
        System.Console.WriteLine("Unesite broj clanske karte:");
        return System.Console.ReadLine() ?? string.Empty;
    }

    private void UkloniPosetioca()
    {
        string brojClanskeKarte = UnesiBrojClanskeKarte();
        Posetilac? obrisanPosetilac = _posetilacDAO.UkloniPosetioca(brojClanskeKarte);
        if (obrisanPosetilac == null)
        {
            System.Console.WriteLine("Posetilac nije pronadjen.");
            return;
        }

        System.Console.WriteLine("Posetilac je uspesno obrisan.");
    }

    //kupovina
    private void DodajKupovinu()
    {
        Kupovina kupovina = UnesiKupovinu();
        _kupovinaDAO.DodajKupovinu(kupovina);
        System.Console.WriteLine("Kupovina je uspesno dodata.");
    }

    private Kupovina UnesiKupovinu()
    {
        System.Console.WriteLine("Unesite broj clanske karte:");
        string brojClanskeKarte = Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite ISBN knjige:");
        string isbn = Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite datum kupovine (yyyy-MM-dd):");
        DateOnly datumKupovine = DateOnly.Parse(Console.ReadLine()!);

        System.Console.WriteLine("Unesite ocenu:");
        int ocena = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Unesite komentar:");
        string komentar = Console.ReadLine() ?? string.Empty;

        return new Kupovina(brojClanskeKarte,isbn,datumKupovine,ocena,komentar);
    }

    private void IzmeniKupovinu()
    {
        Kupovina kupovina = UnesiIzmenuK();
        Kupovina? izmenjenaKupovina = _kupovinaDAO.IzmeniKupovinu(kupovina);

        if (izmenjenaKupovina == null)
        {
            Console.WriteLine("Kupovina nije pronadjena.");
            return;
        }

        Console.WriteLine("Kupovina je uspesno izmenjena.");
    }

    private Kupovina UnesiIzmenuK()
    {
        Console.WriteLine("Unesite broj clanske karte:");
        string brojClanskeKarte = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Unesite ISBN knjige:");
        string isbn = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Unesite datum kupovine (yyyy-MM-dd):");
        DateOnly datumKupovine = DateOnly.Parse(Console.ReadLine()!);

        Console.WriteLine("Unesite novu ocenu:");
        int ocena = ConsoleViewUtils.SafeInputInt();

        Console.WriteLine("Unesite novi komentar:");
        string komentar = Console.ReadLine() ?? string.Empty;

        return new Kupovina(brojClanskeKarte,isbn,datumKupovine,ocena,komentar);
    }

    private void UkloniKupovinu()
    {
        Console.WriteLine("Unesite broj clanske karte:");
        string brojClanskeKarte = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Unesite ISBN knjige:");
        string isbn = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Unesite datum kupovine (yyyy-MM-dd):");
        DateOnly datumKupovine = DateOnly.Parse(Console.ReadLine()!);

        Kupovina? obrisanaKupovina = _kupovinaDAO.UkloniKupovinu( brojClanskeKarte,isbn,datumKupovine);

        if (obrisanaKupovina == null)
        {
            Console.WriteLine("Kupovina ne postoji.");
            return;
        }

        Console.WriteLine("Kupovina je uspesno obrisana.");
    }

    //izdavac
    private void DodajIzdavaca()
    {
        Izdavac izdavac = UnesiIzdavaca();
        _izdavacDAO.DodajIzdavaca(izdavac);
        Console.WriteLine("Izdavac je uspesno dodat.");
    }

    private Izdavac UnesiIzdavaca()
    {
        Console.WriteLine("Unesite naziv izdavaca:");
        string naziv = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Unesite ID sefa izdavaca:");
        int sefID = ConsoleViewUtils.SafeInputInt();

        return new Izdavac(0, naziv, null, sefID);
    }

    private void IzmeniIzdavaca()
    {
        Console.WriteLine("Unesite sifru izdavaca:");
        int sifra = ConsoleViewUtils.SafeInputInt();

        Izdavac? postojeciIzdavac = _izdavacDAO.GetAllIzdavaci().Find(i => i.Sifra == sifra);
        if (postojeciIzdavac == null)
        {
            Console.WriteLine("Izdavac nije pronadjen.");
            return;
        }

        Izdavac izdavac = UnesiIzdavaca();
        izdavac.Sifra = sifra;

        Izdavac? izmenjenIzdavac = _izdavacDAO.IzmeniIzdavaca(izdavac);
        if (izmenjenIzdavac == null)
        {
            Console.WriteLine("Neuspela izmena.");
            return;
        }

        Console.WriteLine("Izdavac je uspesno izmenjen.");
    }

    private void UkloniIzdavaca()
    {
        Console.WriteLine("Unesite sifru izdavaca:");
        int sifra = ConsoleViewUtils.SafeInputInt();

        Izdavac? obrisanIzdavac = _izdavacDAO.UkloniIzdavaca(sifra);
        if (obrisanIzdavac == null)
        {
            Console.WriteLine("Izdavac nije pronadjen.");
            return;
        }

        Console.WriteLine("Izdavac je uspesno obrisan.");
    }
}






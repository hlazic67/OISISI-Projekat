using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Core.DAO;
using Core.Models;
using Sajam.Console;

namespace SajamKnjiga.ConsoleUI;

class AutorKnjigaConsoleView
{
    private readonly AutorDAO _autorDAO;
    private readonly AdresaDAO _adresaDAO;
    private readonly KnjigaDAO _knjigaDAO;
    private readonly AutorKnjigaDAO _autorKnjigaDAO;
    private readonly IzdavacDAO _izdavacDAO;

    public AutorKnjigaConsoleView(AutorDAO autorDao, AdresaDAO adresaDao, KnjigaDAO knjigaDao, AutorKnjigaDAO autorKnjigaDao, IzdavacDAO izdavacDao)
    {
        _autorDAO = autorDao;
        _adresaDAO = adresaDao;
        _knjigaDAO = knjigaDao;
        _autorKnjigaDAO = autorKnjigaDao;
        _izdavacDAO = izdavacDao;

        PoveziPodatke();
    }

    //Vezbe 3. - preuzeta metoda
    public void RunMenu()
    {
        while(true)
        {
            ShowMenu();
            string userInput = System.Console.ReadLine() ?? "0";
            if (userInput == "0") break;
            HandleMenuInput(userInput);
        }
    }

    private void ShowMenu()
    {
        System.Console.WriteLine("\n===== AUTORI I KNJIGE =====");
        System.Console.WriteLine("1: Prikaz svih autora");
        System.Console.WriteLine("2: Prikaz svih knjiga");
        System.Console.WriteLine("3: Prikaz svih adresa");
        System.Console.WriteLine("4: Dodavanje autora");
        System.Console.WriteLine("5: Dodavanje adrese");
        System.Console.WriteLine("6: Dodavanje knjige");
        System.Console.WriteLine("7: Izmena autora");
        System.Console.WriteLine("8: Izmena adrese");
        System.Console.WriteLine("9: Izmena knjige");
        System.Console.WriteLine("10: Brisanje autora");
        System.Console.WriteLine("11: Brisanje adrese");
        System.Console.WriteLine("12: Brisanje knjige");
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
                PrikaziSveKnjige();
                break;

            case "3":
                PrikaziSveAdrese();
                break;

            case "4":
                DodajAutora();
                break;

            case "5":
                DodajAdresu();
                break;

            case "6":
                DodajKnjigu();
                break;

            case "7":
                IzmeniAutora();
                break;

            case "8":
                IzmeniAdresu();
                break;

            case "9":
                IzmeniKnjigu();
                break;

            case "10":
                UkloniAutora();
                break;

            case "11":
                UkloniAdresu();
                break;

            case "12":
                UkloniKnjigu();
                break;

            default:
                System.Console.WriteLine("Nepostojeca opcija!");
                break;
        }
    }

    //povezivanje autora sa adresom i knjigama posle ucitavanja iz fajlova
    private void PoveziPodatke()
    {
        foreach(Autor autor in _autorDAO.GetAllAutori())
        {
            autor.Adresa = _adresaDAO.GetAdresaById(autor.AdresaId);
            autor.Knjige.Clear();
        }

        foreach(Knjiga knjiga in _knjigaDAO.GetAllKnjige())
        {
            knjiga.Autori.Clear();
        }

        foreach(AutorKnjiga veza in _autorKnjigaDAO.GetAllVeze())
        {
            Autor? autor = _autorDAO.GetAutorById(veza.AutorId);
            Knjiga? knjiga = _knjigaDAO.GetKnjigaByIsbn(veza.ISBN);

            if (autor == null || knjiga == null) continue;

            autor.Knjige.Add(knjiga);
            knjiga.Autori.Add(autor);
        }
    }

    //prikaz
    private void PrikaziSveAutore()
    {
        System.Console.WriteLine("\n===== AUTORI =====");

        foreach (Autor a in _autorDAO.GetAllAutori())
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

    private void PrikaziSveKnjige()
    {
        System.Console.WriteLine("\n===== KNJIGE =====");

        foreach (Knjiga k in _knjigaDAO.GetAllKnjige())
        {
            System.Console.WriteLine(k);
            System.Console.WriteLine("-------------------------");
        }
    }

    private void PrikaziSveAdrese()
    {
        System.Console.WriteLine("\n===== ADRESE =====");

        foreach (Adresa a in _adresaDAO.GetAllAdrese())
        {
            System.Console.WriteLine($"ID: {a.Id}, {a}");
            System.Console.WriteLine("-------------------------");
        }
    }

    //autor-dodavanje u listu, unos, izmena i uklanjanje
    private void DodajAutora()
    {
        Autor autor = UnesiAutora();
        Adresa adresa = _adresaDAO.DodajAdresu(UnesiAdresu());

        autor.AdresaId = adresa.Id;
        autor.Adresa = adresa;

        _autorDAO.DodajAutora(autor);
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
            GodineIskustva = godineIskustva
        };
    }

    private int UnesiIdAutora()
    {
        System.Console.WriteLine("Unesite ID autora:");
        return ConsoleViewUtils.SafeInputInt();
    }

    private void IzmeniAutora()
    {
        int id = UnesiIdAutora();
        Autor? postojeciAutor = _autorDAO.GetAutorById(id);
        if (postojeciAutor == null)
        {
            System.Console.WriteLine("Autor nije pronadjen.");
            return;
        }

        Autor autor = UnesiAutora();
        autor.Id = id;

        //adresa se menja zajedno sa autorom
        Adresa novaAdresa = UnesiAdresu();
        Adresa? postojecaAdresa = _adresaDAO.GetAdresaById(postojeciAutor.AdresaId);
        Adresa adresa;

        if (postojecaAdresa == null)
        {
            adresa = _adresaDAO.DodajAdresu(novaAdresa);
        }
        else
        {
            novaAdresa.Id = postojecaAdresa.Id;
            adresa = _adresaDAO.IzmeniAdresu(novaAdresa) ?? novaAdresa;
        }

        autor.AdresaId = adresa.Id;

        Autor? izmenjenAutor = _autorDAO.IzmeniAutora(autor);
        if (izmenjenAutor == null)
        {
            System.Console.WriteLine("Neuspela izmena.");
            return;
        }

        izmenjenAutor.Adresa = adresa;
        System.Console.WriteLine("Autor je uspesno izmenjen.");
    }

    private void UkloniAutora()
    {
        int id = UnesiIdAutora();

        Autor? autor = _autorDAO.GetAutorById(id);
        if (autor == null)
        {
            System.Console.WriteLine("Autor nije pronadjen.");
            return;
        }

        //sef izdavaca ne moze da se obrise
        if (_izdavacDAO.GetAllIzdavaci().Exists(i => i.SefID == id))
        {
            System.Console.WriteLine("Autor je sef izdavaca i ne moze da se obrise.");
            return;
        }

        foreach (Knjiga knjiga in autor.Knjige)
        {
            knjiga.Autori.Remove(autor);
        }

        //prvo brisanje veza iz autor_knjiga, pa autora
        _autorKnjigaDAO.UkloniVezeAutora(id);
        _autorDAO.UkloniAutora(id);
        System.Console.WriteLine("Autor je uspesno obrisan.");
    }

    //adresa-dodavanje, unos, izmena i uklanjanje
    private void DodajAdresu()
    {
        _adresaDAO.DodajAdresu(UnesiAdresu());
        System.Console.WriteLine("Adresa je uspesno dodata.");
    }

    private Adresa UnesiAdresu()
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
        System.Console.WriteLine("Unesite ID adrese:");
        return ConsoleViewUtils.SafeInputInt();
    }

    private void IzmeniAdresu()
    {
        int id = UnesiIdAdrese();
        Adresa? postojecaAdresa = _adresaDAO.GetAdresaById(id);
        if (postojecaAdresa == null)
        {
            System.Console.WriteLine("Adresa nije pronadjena.");
            return;
        }

        Adresa adresa = UnesiAdresu();
        adresa.Id = id;

        Adresa? izmenjenaAdresa = _adresaDAO.IzmeniAdresu(adresa);
        if (izmenjenaAdresa == null)
        {
            System.Console.WriteLine("Neuspela izmena.");
            return;
        }

        PoveziPodatke();
        System.Console.WriteLine("Adresa je uspesno izmenjena.");
    }

    private void UkloniAdresu()
    {
        int id = UnesiIdAdrese();

        Adresa? adresa = _adresaDAO.GetAdresaById(id);
        if (adresa == null)
        {
            System.Console.WriteLine("Adresa nije pronadjena.");
            return;
        }

        //adresa koju koristi autor ne moze da se obrise
        if (_autorDAO.GetAllAutori().Exists(a => a.AdresaId == id))
        {
            System.Console.WriteLine("Adresu koristi autor i ne moze da se obrise.");
            return;
        }

        _adresaDAO.UkloniAdresu(id);
        System.Console.WriteLine("Adresa je uspesno obrisana.");
    }

    //knjiga-dodavanje, unos, izmena i uklanjanje
    private void DodajKnjigu()
    {
        System.Console.WriteLine("Unesite ISBN:");
        string isbn = System.Console.ReadLine() ?? string.Empty;

        Knjiga knjiga = UnesiKnjigu(isbn);
        Knjiga? dodataKnjiga = _knjigaDAO.DodajKnjigu(knjiga);
        if (dodataKnjiga == null)
        {
            System.Console.WriteLine("Knjiga sa tim ISBN-om vec postoji.");
            return;
        }

        System.Console.WriteLine("Knjiga je uspesno dodata.");
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

        while (!double.TryParse((System.Console.ReadLine() ?? string.Empty).Replace(',', '.'),
                   NumberStyles.Float, CultureInfo.InvariantCulture, out cena) || cena < 0)
        {
            System.Console.WriteLine("Neispravna cena, pokusajte ponovo:");
        }

        return cena;
    }

    private int UnesiSifruIzdavaca()
    {
        System.Console.WriteLine("Unesite sifru izdavaca:");
        int sifra = ConsoleViewUtils.SafeInputInt();

        //izdavac mora da postoji
        while (!_izdavacDAO.GetAllIzdavaci().Exists(i => i.Sifra == sifra))
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

        Knjiga? postojecaKnjiga = _knjigaDAO.GetKnjigaByIsbn(isbn);
        if (postojecaKnjiga == null)
        {
            System.Console.WriteLine("Knjiga nije pronadjena.");
            return;
        }

        //ISBN je kljuc i ne menja se
        Knjiga knjiga = UnesiKnjigu(isbn);

        Knjiga? izmenjenaKnjiga = _knjigaDAO.IzmeniKnjigu(knjiga);
        if (izmenjenaKnjiga == null)
        {
            System.Console.WriteLine("Neuspela izmena.");
            return;
        }

        System.Console.WriteLine("Knjiga je uspesno izmenjena.");
    }

    private void UkloniKnjigu()
    {
        System.Console.WriteLine("Unesite ISBN knjige:");
        string isbn = System.Console.ReadLine() ?? string.Empty;

        Knjiga? knjiga = _knjigaDAO.GetKnjigaByIsbn(isbn);
        if (knjiga == null)
        {
            System.Console.WriteLine("Knjiga nije pronadjena.");
            return;
        }

        foreach (Autor autor in knjiga.Autori)
        {
            autor.Knjige.Remove(knjiga);
        }

        //prvo brisanje veza iz autor_knjiga, pa knjige
        _autorKnjigaDAO.UkloniVezeKnjige(isbn);
        _knjigaDAO.UkloniKnjigu(isbn);
        System.Console.WriteLine("Knjiga je uspesno obrisana.");
    }

    //unos datuma, ponavlja se dok format nije ispravan
    private DateOnly UnesiDatum(string poruka)
    {
        System.Console.WriteLine(poruka);
        DateOnly datum;

        while (!DateOnly.TryParseExact(System.Console.ReadLine(), "yyyy-MM-dd",
                   CultureInfo.InvariantCulture, DateTimeStyles.None, out datum))
        {
            System.Console.WriteLine("Neispravan datum, pokusajte ponovo (yyyy-MM-dd):");
        }

        return datum;
    }
}
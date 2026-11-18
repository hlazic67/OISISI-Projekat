using Core.DAO;
using Core.Models;
using Core.Services;
using SajamKnjiga.ConsoleUI;

namespace ConsoleClient;

class Program
{
    static void Main()
    {
        PosetilacDAO posetilac = new PosetilacDAO();
        KupovinaDAO kupovina = new KupovinaDAO();
      

        AdresaDAO adresa = new AdresaDAO(); 
        AutorDAO autor = new AutorDAO();
        KnjigaDAO knjiga = new KnjigaDAO();
        AutorKnjigaDAO autorKnjiga = new AutorKnjigaDAO();
        IzdavacDAO izdavac = new IzdavacDAO();

        PosetilacService posetilacService = new PosetilacService(posetilac);
        KupovinaService kupovinaService = new KupovinaService(kupovina, posetilac, knjiga);
        IzdavacService izdavacService = new IzdavacService(izdavac, autor);

        AdresaService adresaService = new AdresaService(adresa, autor);
        AutorService autorService = new AutorService(autor, adresa, knjiga, autorKnjiga, izdavac);
        KnjigaService knjigaService = new KnjigaService(knjiga, autorKnjiga, izdavac);

        autorService.PoveziPodatke();

        PosetilacConsoleView posetiociView =new PosetilacConsoleView(posetilac, kupovina, knjiga, posetilacService);
        KupovinaConsoleView kupovineView = new KupovinaConsoleView(kupovina, posetilac, knjiga, kupovinaService);
        IzdavaciConsoleView izdavaciView =new IzdavaciConsoleView(izdavac, autor, knjiga, autorKnjiga, izdavacService);

        AutorConsoleView autoriView = new AutorConsoleView(autorService);
        KnjigaConsoleView knjigeView = new KnjigaConsoleView(knjigaService);
        AdresaConsoleView adreseView = new AdresaConsoleView(adresaService);

        while (true)
        {
            System.Console.WriteLine("=====GLAVNI MENI=====");
            System.Console.WriteLine("1: Posetioci");
            System.Console.WriteLine("2: Kupovine");
            System.Console.WriteLine("3: Izdavaci");
            System.Console.WriteLine("4: Autori");
            System.Console.WriteLine("5: Knjige");
            System.Console.WriteLine("6: Adrese");
            System.Console.WriteLine("0: Izlaz");

            string userInput = System.Console.ReadLine() ?? "0";
            if (userInput == "0") break;

            switch (userInput)
            {
                case "1":
                    posetiociView.RunPosetiociMenu();
                    break;

                case "2":
                    kupovineView.RunKupovineMenu();
                    break;

                case "3":
                    izdavaciView.RunIzdavaciMenu();
                    break;

                case "4":
                    autoriView.RunMenu();
                    break;

                case "5":
                    knjigeView.RunMenu();
                    break;

                case "6":
                    adreseView.RunMenu();
                    break;

                default:
                    System.Console.WriteLine("Nepostojeca opcija.");
                    break;
            }
        }
    }
}
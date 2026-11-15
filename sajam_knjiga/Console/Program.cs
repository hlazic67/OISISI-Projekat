using Core.DAO;
using Core.Models;
using Core.Services;
using SajamKnjiga.ConsoleUI;
using System.Net.Http.Headers;

namespace ConsoleClient;

class Program
{
    static void Main()
    {
        PosetilacDAO posetilac = new PosetilacDAO();
        KupovinaDAO kupovina = new KupovinaDAO();
        IzdavacDAO izdavac = new IzdavacDAO();

        AdresaDAO adresa = new AdresaDAO(); 
        AutorDAO autor = new AutorDAO();
        KnjigaDAO knjiga = new KnjigaDAO();
        AutorKnjigaDAO autorKnjiga = new AutorKnjigaDAO();

        SajamConsoleView view = new SajamConsoleView(posetilac,kupovina,izdavac);
        AutorKnjigaConsoleView autoriView = new AutorKnjigaConsoleView(autor, adresa, knjiga, autorKnjiga, izdavac);

        while (true)
        {
            System.Console.WriteLine("=====GLAVNI MENI=====");
            System.Console.WriteLine("1: Posetioci, kupovine i izdavaci");
            System.Console.WriteLine("2: Autori, knjige i adrese");
            System.Console.WriteLine("0: Izlaz");

            string userInput = System.Console.ReadLine() ?? "0";
            if (userInput == "0") break;

            switch (userInput)
            {
                case "1":
                    view.RunMenu();
                    break;
                case "2":
                    autoriView.RunMenu();
                    break;
                default:
                    System.Console.WriteLine("Nepostojeca opcija.");
                    break;
            }
        }
    }
}
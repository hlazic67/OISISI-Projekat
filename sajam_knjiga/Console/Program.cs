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
        IzdavacDAO izdavac = new IzdavacDAO();

        SajamConsoleView view = new SajamConsoleView(posetilac,kupovina,izdavac);

        view.RunMenu();
    }
}
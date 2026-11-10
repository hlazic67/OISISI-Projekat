namespace Sajam.Console;
//klasa preuzeta iz materijala za vjezbe 3
public static class ConsoleViewUtils
{
    public static int SafeInputInt()
    {
        int input;

        string rawInput = System.Console.ReadLine() ?? string.Empty;

        while (!int.TryParse(rawInput, out input))
        {
            System.Console.WriteLine("Neispravan broj, pokusaj ponovo: ");

            rawInput = System.Console.ReadLine() ?? string.Empty;
        }

        return input;
    }
}
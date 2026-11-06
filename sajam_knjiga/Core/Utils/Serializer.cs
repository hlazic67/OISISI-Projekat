using Core.Interfaces;
using System.Text;

namespace Core.Utils;
//Preuzeto iz materijala za Vjezbe 3
class Serializer<T> where T : ISerijalizacija, new()
{
    private const char Delimiter = '|';

    public string ToCSV(List<T> objects)
    {
        StringBuilder sb = new StringBuilder();

        foreach (ISerijalizacija obj in objects)
        {
            string line = string.Join(Delimiter.ToString(), obj.ToCSV());
            sb.AppendLine(line);
        }

        return sb.ToString();
    }

    public List<T> FromCSV(IEnumerable<string> lines)
    {
        List<T> objects = new List<T>();

        foreach (string line in lines)
        {
            string[] csvValues = line.Split(Delimiter);
            T obj = new T();
            obj.FromCSV(csvValues);
            objects.Add(obj);
        }

        return objects;
    }
}
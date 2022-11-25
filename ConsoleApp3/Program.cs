using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

class Test
{
    public static void Main()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        string path = @"..\..\..\Война и мир.txt";
        File.AppendAllText(path, Environment.NewLine, Encoding.UTF8);
        string readText = File.ReadAllText(path);
        string[] separators = { ",", "—", " -", ".", "!", "?", ";", ":", " ", "", "\"", "[", "]", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "(", ")", "*", "\r\n" };
        string[] words = readText.ToLower().Split(separators, StringSplitOptions.None);

        Array.Sort(words);
        StreamWriter f = new StreamWriter(@"..\MyTest.txt", false, System.Text.Encoding.UTF8);

        var result = words.GroupBy(x => x)
                              .Where(x => x.Count() < 50 && x.Count() > 0)
                              .OrderByDescending(x => x.Count())
                            .Select(x => new { Word = x.Key, Frequency = x.Count() });
        Dictionary<string, int> resultD = new Dictionary<string, int>();

        foreach (var o in result)
        {
            resultD.Add(o.Word, o.Frequency);
           
        }
        foreach (KeyValuePair<string, int> keyValue in resultD)
            {
                f.WriteLine("Слово: {0}\tКоличество повторов: {1}", keyValue.Key, keyValue.Value);
            }
        stopwatch.Stop();

        Console.WriteLine("Файл записан. Перечень уникальных слов можно найти в файле \\bin\\Debug\\MyTest.txt. Нажмите любую клавишу..");
        Console.WriteLine("Потрачено времени на выполнение: " + stopwatch.Elapsed);
        f.Close();
        Console.ReadKey();
    }
}
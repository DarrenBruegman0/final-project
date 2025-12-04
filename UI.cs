using System.Reflection.Emit;

namespace UI;

public class Simple
{
    public static void Clear()
    {
        Console.Clear();
        Console.SetCursorPosition(0, 0);
    }
    public static void SetColor(ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black)
    {
        Console.ForegroundColor = fg;
        Console.BackgroundColor = bg;
    }
    public static ConsoleKey Keyinput()
    {
        return Console.ReadKey().Key;
    }
    public static void rainbow(string text, Random rand)
    {
        ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Yellow, ConsoleColor.Magenta, ConsoleColor.Cyan, ConsoleColor.White };
        for (int i = 0; i < text.Length; i++)
        {
            SetColor(colors[rand.Next(0,colors.Length-1)], ConsoleColor.Black);
            Console.Write(text[i]);
        }
        SetColor(ConsoleColor.White, ConsoleColor.Black);
        Console.WriteLine();
    }
    public static string Input(string label)
    {
        Console.Write(label);
        string input = Console.ReadLine();
        if (input == "" || input == null)
        {
            return Input(label);
        }
        else
        {
            return input;
        }
    }
    public static void Print(string label, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black)
    {
        SetColor(fg, bg);
        Console.WriteLine(label);
    }

}

public class Menu
{
    public static object[] Init(string label,List<string> options, string footer = "")
    {
        int curchoice = 0;
        while (true)
        {
            Console.WriteLine(label);
            Console.WriteLine("====================================================================");
            for (int i = 0; i < options.Count; i++)
            {
                if (i == curchoice)
                {
                    Simple.SetColor(ConsoleColor.Black, ConsoleColor.White);
                }  
                Console.WriteLine(options[i]);
                Simple.SetColor(ConsoleColor.White, ConsoleColor.Black);
                
            }
            Console.WriteLine("====================================================================");
            Console.WriteLine(footer);
            ConsoleKey key = Simple.Keyinput();
            if (key == ConsoleKey.UpArrow)
            {
                curchoice--;
                if (curchoice < 0)
                {
                    curchoice = options.Count - 1;
                }
            }
            else if (key == ConsoleKey.DownArrow)
            {
                curchoice++;
                if (curchoice > options.Count - 1)
                {
                    curchoice = 0;
                }
            }
            else
            {
                Simple.Clear();
                return [options[curchoice], curchoice, key];
            }
            Simple.Clear();
        }
    }
    
    public static int Basic(string label, List<string> options, string footer = "")
    {
        object[] m = Init(label, options, footer);
        if ((ConsoleKey)m[2] == ConsoleKey.Enter)
        {
            return (int)m[1];
        }
        else if ((ConsoleKey)m[2] == ConsoleKey.Escape)
        {
            return -2;
        }
        else if ((ConsoleKey)m[2] == ConsoleKey.Backspace)
        {
            return -1;
        }
        else
        {
            return Basic(label, options, footer);
        }
    }

    public static List<string> Stringify(List<List<object>> options, string[] labels)
    {
        List<string> noptions = [];
        foreach (List<object> o in options) {
            string dat = $"";
            for (int i = 0; i < o.Count; i++)
            {
                if (labels[i] == "")
                {
                    dat += $"{o[i]}";
                }
                else {
                    dat += $"{labels[i]}:  {o[i]}    ";
                }
            }
            noptions.Add(dat);
        }
        return noptions;
    }

}
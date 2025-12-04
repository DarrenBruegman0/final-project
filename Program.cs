using UI;
using DATA;
using System.Reflection.Metadata.Ecma335;
using System.Net.NetworkInformation;
using System.Xml;

public class Book
{
    public static List<List<object>> Availible()
    {
        List<List<object>> data = DATA.CSV.Read("library.csv");
        List<List<object>> ndata = [];
        foreach (List<object> d in data)
        {
            if (d[3].ToString() == "availible")
            {
                ndata.Add(d);
            }
        }
        return ndata;
    }
    public static List<List<object>> Borrowed()
    {
        List<List<object>> data = DATA.CSV.Read("library.csv");
        List<List<object>> ndata = [];
        foreach (List<object> d in data)
        {
            if (d[3].ToString() == "borrowed")
            {
                ndata.Add(d);
            }
        }
        return ndata;
    }
    public static List<List<object>> Genre(string genre = "")
    {
        List<List<object>> data = DATA.CSV.Read("library.csv");
        List<List<object>> ndata = [];
        if (genre == "")
        {
            foreach (List<object> d in data)
            {
                   ndata.Add([d[2]]);
            }
            List<object> mid = [];
            foreach (List<object> d in ndata)
            {
                foreach (object d2 in d)
                {
                    mid.Add(d2);
                }

            }
            List<object> finale = new HashSet<object>(mid).ToList();
            List<List<object>> final = [];
            foreach (object o in finale)
            {
                final.Add([o]);
            }

            
            /*foreach (List<object> d in ndata)
            {
                if (!final.Contains(d))
                {
                    final.Add(d);
                }
            }*/
            return final.ToList();
        }

        foreach (List<object> d in data)
        {
            if (d[2].ToString() == genre)
            {

                ndata.Add(d);
            }
        }
        return ndata;
    }

}

public class Library
{
    public static int ReturnBook(string title)
    {
        List<List<object>> data = DATA.CSV.Read("library.csv");
        for (int d = 0; d < data.Count; d++)
        {
            if (data[d][0].ToString() == title && data[d][3].ToString() != "availible")
            {
               DATA.CSV.ModifyCell("library.csv", d , 3, "availible");
               return 0;
            }
        }
        return -1;
    }

    public static void AddBook(string title, string author, string genre)
    {
        List<List<object>> data = DATA.CSV.Read("library.csv");
        data.Add([title,author,genre,"availible",""]);
        DATA.CSV.Write("library.csv", data);
    }
    public static void RemoveBook(string title)
    {
        List<List<object>> data = DATA.CSV.Read("library.csv");
        foreach (List<object> d in data)
        {
            if (d[0].ToString() == title)
            {
                data.Remove(d);
                break;
            }
        }
        DATA.CSV.Write("library.csv", data);
    }

    public static int BorrowBook(string title)
    {
        List<List<object>> data = DATA.CSV.Read("library.csv");
        for (int d = 0; d < data.Count; d++)
        {
            if (data[d][0].ToString() == title && data[d][3].ToString() != "borrowed")
            {
               DATA.CSV.ModifyCell("library.csv", d , 3, "borrowed");
               return 0;
            }
        }
        return -1;
    }



}


internal class Program
{
    private static void Main(string[] args)
    {
        bool playing = true;
        while (playing)
        {
            object[] menu = UI.Menu.Init("Library | Genres", UI.Menu.Stringify(Book.Genre(), [""]));
            if ((ConsoleKey)menu[2] == ConsoleKey.Enter)
            {
                while (true) {
                    object[] menu2 = UI.Menu.Init("Library | Books", UI.Menu.Stringify(Book.Genre(menu[0].ToString()), ["Title", "Author", "Genre", "Status", "", "", ""]), "Enter : check out or return | backspace : back | escape : exit | A : add book | R : remove book");
                    if ((ConsoleKey)menu2[2] == ConsoleKey.Enter)
                    {
                        if (Book.Genre(menu[0].ToString())[Convert.ToInt32(menu2[1])][3].ToString() == "availible")
                        {
                            Library.BorrowBook(Book.Genre(menu[0].ToString())[Convert.ToInt32(menu2[1])][0].ToString());
                        }
                        else
                        {
                            Library.ReturnBook(Book.Genre(menu[0].ToString())[Convert.ToInt32(menu2[1])][0].ToString());
                        }
                }
                if ((ConsoleKey)menu2[2] == ConsoleKey.Escape)
                {
                    playing = false;
                    break;
                }
                if ((ConsoleKey)menu2[2] == ConsoleKey.Backspace)
                {
                    break;
                }
                if ((ConsoleKey)menu2[2] == ConsoleKey.A)
                {
                    string title = UI.Simple.Input("Enter book title: ");
                    string author = UI.Simple.Input("Enter book author: ");
                    string genre = UI.Simple.Input("Enter book genre: ");
                    Library.AddBook(title, author, genre);
                }
                if ((ConsoleKey)menu2[2] == ConsoleKey.R)
                {
                    if (Book.Genre(menu[0].ToString()).Count >= 2)
                    {
                        Library.RemoveBook(Book.Genre(menu[0].ToString())[Convert.ToInt32(menu2[1])][0].ToString());
                    }
                    else
                    {
                        UI.Simple.Print("Must Have at least 1 book in library to remove", ConsoleColor.Red, ConsoleColor.Black);
                        UI.Simple.Keyinput();
                        UI.Simple.Clear();
                    }
                    
                }
            }
            }
            if ((ConsoleKey)menu[2] == ConsoleKey.Escape || (ConsoleKey)menu[2] == ConsoleKey.Backspace)
            {
                playing = false;
            }
        
        }
    }
}
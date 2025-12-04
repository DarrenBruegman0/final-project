namespace DATA;

public class CSV
{
    public static List<List<object>> Read(string path)
    {
        string path2 = "./../../../" + path;
        if (!File.Exists(path2))
        {
            return [["Error", "File does not exist"]];
        }
        List<List<object>> formatted = [];
        string[] raw = File.ReadAllLines(path2);
        for (int i = 0; i < raw.Length; i++)
        {
            string[] split = raw[i].Split(',');
            formatted.Add(new List<object>(split));
        }
        return formatted;
    }
    public static void Write(string path, List<List<object>> data)
    {
        string path2 = "./../../../" + path;
        File.WriteAllText(path2, "");
        string dat = "";
        foreach (List<object> d in data)
        {
            d.RemoveAt(4);
            string s = "";
            foreach (object o in d)
            {
                s += $"{o},";
            }
            dat += $"{s}\n";
        }
        File.WriteAllText(path2, dat);
    }
    public static void Append(string path, List<List<object>> data)
    {
        string path2 = "./../../../" + path;
        File.AppendAllText(path2, "");
        foreach (List<object> d in data)
        {
            string s = "";
            foreach (object o in d)
            {
                s += o + ",";
            }

            File.AppendAllText(path, $"{s}\n");
        }
    }
    public static void ModifyCell(string path, int row, int col, object value)
    {
        List<List<object>> data = Read(path);
        data[row][col] = value;
        Write(path, data);
    }
}

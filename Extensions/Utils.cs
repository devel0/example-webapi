namespace ExampleWebApi;

public static partial class Extensions
{

    //
    // Summary:
    //     Smart line splitter that split a text into lines whatever unix or windows line
    //     ending style. By default its remove empty lines.
    //
    // Parameters:
    //   removeEmptyLines:
    //     If true remove empty lines.
    //
    //   txt:
    //     string to split into lines
    public static IEnumerable<string> Lines(this string txt, bool removeEmptyLines = true)
    {
        string[] array = txt.Replace("\r\n", "\n").Split(new char[1] { '\n' });
        if (removeEmptyLines)
        {
            return array.Where((string r) => r.Trim().Length > 0);
        }

        return array;
    }

}
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

    /// <summary>
    /// Send given string to the websocket.
    /// </summary>    
    public static async Task<bool> SendStringAsync(this WebSocket ws, string str, CancellationToken cancellationToken)
    {
        var bytes = Encoding.UTF8.GetBytes(str);
        var arraySegment = new ArraySegment<byte>(bytes, 0, bytes.Length);
        if (ws.State == WebSocketState.Open)
            await ws.SendAsync(arraySegment, WebSocketMessageType.Text, true, cancellationToken);

        else if (ws.State == WebSocketState.Closed || ws.State == WebSocketState.Aborted)
            return false;

        return true;
    }

    /// <summary>
    /// Receive from websocket returning string ( max 2gb due to use of backing memorystream )
    /// </summary>    
    public static async Task<string?> ReceiveStringAsync(this WebSocket ws, CancellationToken cancellationToken)
    {
        var ms = new MemoryStream();

        WebSocketReceiveResult? wsr = null;
        var buffer = new ArraySegment<byte>(new byte[1024 * 4]);
        if (buffer.Array is null) return null;

        do
        {
            wsr = await ws.ReceiveAsync(buffer, cancellationToken);
            ms.Write(buffer.Array, buffer.Offset, wsr.Count);
        } while (!wsr.EndOfMessage);

        ms.Flush();
        ms.Seek(0, SeekOrigin.Begin);

        var res = Encoding.UTF8.GetString(ms.ToArray());

        return res;
    }

}
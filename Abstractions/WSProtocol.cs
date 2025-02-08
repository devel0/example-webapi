namespace ExampleWebApi;

public enum WSMessageType
{

    Ping,

    Pong,

    /// <summary>
    /// some event
    /// </summary>
    SomeEvent,

}

public class WSProtocol(WSMessageType messageType)
{
    public WSMessageType MessageType { get; set; } = messageType;
}

public class WSPing : WSProtocol
{

    public WSPing(string msg) : base(WSMessageType.Ping)
    {
        Msg = msg;
    }

    public string Msg { get; set; }

}

public class WSPong : WSProtocol
{

    public WSPong(string msg) : base(WSMessageType.Pong)
    {
        Msg = msg;
    }

    public string Msg { get; set; }

}

public class WSSomeEvent : WSProtocol
{

    public WSSomeEvent(string msg) : base(WSMessageType.SomeEvent)
    {
        Msg = msg;
    }

    public string Msg { get; set; }

}

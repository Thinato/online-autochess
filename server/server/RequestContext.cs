namespace server;

public class RequestContext {
    public string Method { get; private set; }
    public string Path { get; private set; }
    public string Protocol { get; private set; }
    public string Host { get; }
    public string Accept { get; }
    public string UserAgent { get; }
    public string Server { get; }

    private RequestContext(string method, string path, string protocol //,string host, string accept, string userAgent, string server
    ) {
        Method = method;
        Path = path;
        Protocol = protocol;
        // Host = host;
        // Accept = accept;
        // UserAgent = userAgent;
        // Server = server;

    }

    // Parse the HTTP request from the client stream
    public static async Task<RequestContext> ParseAsync(StreamReader reader) {
        var headers = new Dictionary<string, string>();
        // headers.GetValueOrDefault("Host", "localhost");
        // headers.GetValueOrDefault("Accept", "*/*");
        // headers.GetValueOrDefault("User-Agent", "curl/7.68.0");
        // headers.GetValueOrDefault("Server", "CustomServer");
        // Console.WriteLine($"Received request from {host}");
        // Console.WriteLine($"data: {reader.ReadToEnd()}");
        var requestLine = await reader.ReadLineAsync();
        if (requestLine == null)
            throw new InvalidOperationException("Empty request received");

        var parts = requestLine.Split(' ');
        if (parts.Length != 3)
            throw new InvalidOperationException("Invalid request line");

        Console.WriteLine($"Received request: {parts[0]} {parts[1]} {parts[2]}");

        return new RequestContext(parts[0], parts[1], parts[2]
        //host: headers["Host"], accept: headers["Accept"], userAgent: headers["User-Agent"], server: headers["Server"]
        );
    }
}

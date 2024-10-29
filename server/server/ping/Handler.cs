
using System.Text.Json;

namespace server.ping;

public class PingRequest : RequestHandler {
    public override string Handle(RequestContext context) {
        var responseBody = JsonSerializer.Serialize(new { message = "pong" });
        var response =
            "HTTP/1.1 200 OK\r\n" +
            "Date: " + DateTime.UtcNow.ToString("r") + "\r\n" +
            "Content-Type: application/json\r\n" +
            $"Content-Length: {responseBody.Length}\r\n" +
            "Connection: close\r\n" +
            "Server: CustomServer\r\n" + // Added server header
            "Access-Control-Allow-Origin: *\r\n" + // Added CORS header
            "Access-Control-Allow-Methods: GET, POST, PUT, DELETE, OPTIONS\r\n" + // Added CORS header
            "\r\n" +
            responseBody; //+
            //"\r\n"; // Ensure separation with a newline

        Console.WriteLine($"Responding with: {response}");

        return response;
    }
}
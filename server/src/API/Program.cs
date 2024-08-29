using API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var server = new Server("phildick");
        await server.StartAsync();
    }
}

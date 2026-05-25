using System.Net.Http;
using System.Net;

class CookieExample
{
    static async Task Main()
    {
       
        var cookieContainer = new CookieContainer();
        
        var handler = new HttpClientHandler { CookieContainer = cookieContainer };
        
        using var client = new HttpClient(handler);
        
        string setUrl = "https://httpbin.org/cookies/set?user=student&token=xyz789";
        var response1 = await client.GetAsync(setUrl);
        Console.WriteLine($"Статус першого запиту: {response1.StatusCode}");
        
        var baseUri = new Uri("https://httpbin.org");
        var cookies = cookieContainer.GetCookies(baseUri);
        Console.WriteLine($"\nЗбережені cookies в контейнері ({cookies.Count}):");
        foreach (Cookie cookie in cookies)
        {
            Console.WriteLine($"----------------------------------------");
            Console.WriteLine($" Name:    {cookie.Name}");
            Console.WriteLine($" Value:   {cookie.Value}");
            Console.WriteLine($" Domain:  {cookie.Domain}");
            Console.WriteLine($" Path:    {cookie.Path}");
            Console.WriteLine($" Expires: {(cookie.Expires == DateTime.MinValue ? "Session (При закриттi)" : cookie.Expires.ToString())}");
        }
        Console.WriteLine($"----------------------------------------");
        
        var response2 = await client.GetAsync("https://httpbin.org/cookies");
        var body = await response2.Content.ReadAsStringAsync();
        Console.WriteLine($"\nВiдповiдь сервера (JSON з куками, якi вiн вiд нас отримав):\n{body}");
    }
}
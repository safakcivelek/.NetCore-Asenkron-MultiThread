//// See https://aka.ms/new-console-template for more information


//void Calis(Task<string> data)
//{
//    Console.WriteLine("data uzunluk: " + data.Result.Length);
//}

//Console.WriteLine("Hello, World!");

//var myTask = new HttpClient().GetStringAsync("https://www.google.com").ContinueWith(Calis);

//Console.WriteLine("Arada yapılacak işler");

//await myTask;

public class Program
{
    public static void Calis(Task<string> data)
    {
        Console.WriteLine("data uzunluk: " + data.Result.Length);
    }

    private static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var myTask = new HttpClient().GetStringAsync("https://www.google.com").ContinueWith(Calis);

        Console.WriteLine("Arada yapılacak işler");

        await myTask;
    }
}
using Server;
using System.Net.Sockets;
using System.Net;
using System.Text;

class Program
{
    private List<Products> productList;

    public Program()
    {
        productList = new List<Products>();
        productList.Add(new Products("Product A", 10.00m, 15.00m, 3600, DateTime.Now.AddHours(1)));
        productList.Add(new Products("Product B", 20.00m, 25.00m, 7200, DateTime.Now.AddHours(2)));
    }

    public void DisplayProducts(Socket clientSocket)
    {
        Console.WriteLine("Products available on the server:");
        for (int i = 0; i < productList.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {productList[i].Name}");
        }

        Console.WriteLine("Enter the number of the product you want: ");
        string choice = Console.ReadLine();

        if (int.TryParse(choice, out int productIndex) && productIndex >= 1 && productIndex <= productList.Count)
        {
            // User chose a valid product
            int selectedProductIndex = productIndex - 1;
            string productInfo = $"Name: {productList[selectedProductIndex].Name}\nMinimum Price: {productList[selectedProductIndex].MinPrice:C}\nCurrent Price: {productList[selectedProductIndex].Price:C}\nAction Time (seconds): {productList[selectedProductIndex].ActionTimeInSeconds}\nEnd Time: {productList[selectedProductIndex].EndTime}\n";
            byte[] data = Encoding.ASCII.GetBytes(productInfo);
            clientSocket.Send(data);
        }
        else
        {
            // Invalid choice
            string errorMessage = "Invalid choice. Please enter a valid product number.";
            byte[] data = Encoding.ASCII.GetBytes(errorMessage);
            clientSocket.Send(data);
        }
        
    }

    static void Main(string[] args)
    {
        Program program = new Program();
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        serverSocket.Bind(new IPEndPoint(IPAddress.Any, 1234));
        serverSocket.Listen(10);

        Console.WriteLine("Waiting for a client to connect...");
        Socket clientSocket = serverSocket.Accept();
        Console.WriteLine("Client connected.");

        program.DisplayProducts(clientSocket);

        // Clean up and close sockets
        clientSocket.Shutdown(SocketShutdown.Both);
        clientSocket.Close();
        serverSocket.Close();
    }
}
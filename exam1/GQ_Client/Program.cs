using GQ_Client;

var client = new EntityConsoleClient();

while (true)
{
    Console.WriteLine("\nChoose an operation:");
    Console.WriteLine("1. Create Entity");
    Console.WriteLine("2. List Entities");
    Console.WriteLine("3. Update Entity");
    Console.WriteLine("4. Delete Entity");
    Console.WriteLine("5. Exit");
    Console.Write("> ");

    var choice = Console.ReadLine();

    try
    {
        switch (choice)
        {
            case "1":
                await client.CreateEntity();
                break;
            case "2":
                await client.ListEntities();
                break;
            case "3":
                await client.UpdateEntity();
                break;
            case "4":
                await client.DeleteEntity();
                break;
            case "5":
                return;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}
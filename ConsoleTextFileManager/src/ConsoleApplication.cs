namespace ConsoleTextFileManager;

public abstract class ConsoleApplication
{
    
    public static async Task Run()
    {
        var options = new List<string> { "Read", "Write", "Delete", "Create Collection" };

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to File Text Database!");
            for (var i = 0; i < options.Count; i++) Console.WriteLine($"{i + 1}. {options[i]}");
            Console.WriteLine("Select a number option (1,2,3...) or");
            Console.WriteLine("Write 'exit' to close the program.");
            Console.WriteLine("Select an option:");
            var option = Console.ReadLine();

            if (option?.Trim().ToLower() == "exit")
            {
                Console.Clear();
                Console.WriteLine("Goodbye!");
                break;
            }


            if (string.IsNullOrWhiteSpace(option) || 
                !int.TryParse(option, out var index) || 
                index < 1 || 
                index > options.Count)
            {
                Console.WriteLine("Invalid option.");
                await Task.Delay(3000);
                Console.Clear();
                continue;           
            }
            
            var optionIndex = int.Parse(option) - 1;
            Console.WriteLine($"You selected {options[optionIndex]}");

            try
            {
                SelectOption(optionIndex + 1); 
                await Task.Delay(2000);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetBaseException().Message);
                await Task.Delay(3000);
            }

        }
        
        
    }

    private static void SelectOption(int option)
    {
        TextFileManagement textFileManagement = new();
        switch (option)
        {
            case 1:
                textFileManagement.Read();
                break;
            case 2:
                textFileManagement.Write("");
                break;
            case 3:
                textFileManagement.Delete();
                break;
            case 4:
                textFileManagement.CreateCollection();
                break;
            default:
                throw new NotImplementedException("The option is not implemented.");
        }
    }
}
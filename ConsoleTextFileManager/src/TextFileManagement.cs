namespace ConsoleTextFileManager;

/// <summary>
/// Provides file management operations for a simple text-based database system.
/// Implements the IFileManagement interface to handle basic CRUD operations on text files.
/// </summary>
/// <remarks>
/// This class manages collections stored as text files in a dedicated folder.
/// Each collection is represented as a text file with semicolon-separated column names.
/// The files are stored in the "./files" directory by default.
/// 
/// Key features:
/// - Creates new collections with user-defined columns
/// - Reads content from existing collection files
/// - Supports basic file operations (create, read, write, delete)
/// - Automatically manages the storage directory
/// </remarks>
/// <example>
/// Basic usage:
/// <code>
/// var fileManager = new TextFileManagement();
/// fileManager.CreateCollection(); // Creates a new collection file
/// fileManager.Read(); // Reads and displays content from a collection
/// </code>
/// </example>
/// <seealso cref="IFileManagement"/>
public class TextFileManagement : IFileManagement
{
    private const string FolderRoute = "./files";
    private const int IdIndex = 0;
    
    /// <summary>
    /// Creates a new collection file with user-defined columns.
    /// </summary>
    /// <remarks>
    /// This method performs the following operations:
    /// 1. Initializes a list of columns with a default "id" column
    /// 2. Prompts the user to enter a collection name
    /// 3. Enters a loop to gather column names from the user until they choose to stop
    /// 4. Serializes the column names using semicolon (;) as a delimiter
    /// 5. Creates a text file with the collection name and stores the serialized column information
    /// </remarks>
    /// <exception cref="IOException">
    /// Thrown when there are issues creating or writing to the file.
    /// </exception>
    /// <example>
    /// If the user creates a collection named "users" with columns "name" and "email",
    /// the resulting file will contain: "id;name;email"
    /// </example>
    public void CreateCollection()
    {
        List<string> columns = ["id"];
        Console.WriteLine("Write the name of the collection:");
        var collectionName = Console.ReadLine();
        if(collectionName == null) return;

        while (true)
        {
            Console.WriteLine("Write the name of the column:");
            var columnName = Console.ReadLine();
            if (columnName == null) continue;
            columns.Add(columnName);
            
            Console.WriteLine("Write another column? (y/n)");
            var answer = Console.ReadLine();
            if (answer?.Trim().ToLower() == "n") break;
        }
        
        var serializedColumns = string.Join(";", columns);
        File.WriteAllText(FolderRoute + $@"/{collectionName}.txt", serializedColumns);
        Console.Clear();
        Console.WriteLine("Collection created successfully.");
    }

    /// <summary>
    /// Reads and displays the content of a text file from the files directory.
    /// </summary>
    /// <remarks>
    /// This method performs the following operations:
    /// 1. Ensures the target folder exists by calling GenerateFolder()
    /// 2. Displays a list of available files
    /// 3. Prompts the user to enter a filename
    /// 4. Reads and displays the content of the selected file
    /// 5. Provides an option to return to the main menu
    /// </remarks>
    /// <returns>
    /// Returns an empty string. The return value is not currently utilized in the implementation.
    /// </returns>
    /// <exception cref="IOException">
    /// Thrown when there are issues reading the file or if the file does not exist.
    /// </exception>
    public string Read()
    {
        GenerateFolder();
        PrintFiles();
        
        while (true)
        {
            Console.WriteLine("Write the name of the collection:");
            var collectionName = Console.ReadLine();
            if (collectionName == null) continue;
            try
            {
                var content = File.ReadAllText(FolderRoute + $@"/{collectionName}.txt");
                content = content.Replace(":", "\n");
                Console.WriteLine(content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Would you like to go back? (y/n)");
            var answer = Console.ReadLine();
            if(answer == null) continue;
            if (answer?.Trim().ToLower() != "y") continue;
            Console.Clear();
            Console.WriteLine("Back to the main menu.");
            break;
        }
        
        return "Success";
    }

    public void Write(string content)
    {
        GenerateFolder();
        PrintFiles();

        while (true)
        {
            List<string> data = [];
            Console.WriteLine("Write the name of the collection:");
            var collectionName = Console.ReadLine();
            if (collectionName == null) continue;
            var collectionRoute = FolderRoute + $@"/{collectionName}.txt";
            
            var collection = File.ReadAllText(collectionRoute);
            var firstColumn = collection.Split(":");
            var columns = firstColumn[0].Split(";");
            
            Console.WriteLine(string.Join(" ", columns));
            var transformedColumns = columns.Skip(1).ToList();
            var uuid = Guid.NewGuid();
            data.Add(uuid.ToString());
            
            foreach (var column in transformedColumns)
            {
                Console.WriteLine($"Write the value for {column}:");
                var value = Console.ReadLine();
                if (value == null)
                {
                    data.Add("null");
                    continue;
                }
                data.Add(value);
            }
            
            var columnData = string.Join(";", data);
            var collectionContent = collection + $":{columnData}";
            File.WriteAllText(collectionRoute, collectionContent);
            
            Console.WriteLine("Would you like to go back? (y/n)");
            var answer = Console.ReadLine();
            if (answer?.Trim().ToLower() != "y") continue;
            Console.Clear();
            Console.WriteLine("Back to the main menu.");
            break;   
        }

    }

    /// <summary>
    /// Deletes a specific row from a collection file based on its ID.
    /// </summary>
    /// <remarks>
    /// This method performs the following operations:
    /// 1. Prompts the user to enter a collection name
    /// 2. Reads the content of the specified collection file
    /// 3. Splits the data into rows and columns
    /// 4. Prompts the user to enter the ID of the row to delete
    /// 5. Validates if the ID exists in the collection
    /// 6. Removes the row with the matching ID
    /// 7. Reconstructs the file content without the deleted row
    /// 8. Saves the modified content back to the file
    /// 
    /// The data structure in the file is expected to be in the format:
    /// - Rows are separated by colons (:)
    /// - Columns within each row are separated by semicolons (;)
    /// - The first column of each row is always the ID
    /// </remarks>
    /// <exception cref="IOException">
    /// Thrown when there are issues reading from or writing to the file.
    /// </exception>
    /// <exception cref="FileNotFoundException">
    /// Thrown when the specified collection file does not exist.
    /// </exception>
    /// <example>
    /// For a collection file containing:
    /// "id;name;email:1;John;john@email.com:2;Jane;jane@email.com"
    /// 
    /// Deleting ID "1" would result in:
    /// "id;name;email:2;Jane;jane@email.com"
    /// </example>
    public void Delete()
    {
        // Implementation...
    }

    /// <summary>
    /// Creates the application's storage directory if it doesn't already exist.
    /// </summary>
    /// <remarks>
    /// This method checks for the existence of a directory specified by '_folderRoute'.
    /// If the directory doesn't exist, it creates it and displays a success message.
    /// If the directory already exists, the method returns without taking any action.
    /// </remarks>
    /// <exception cref="IOException">
    /// Thrown when there is an I/O error while creating the directory.
    /// </exception>
    /// <exception cref="UnauthorizedAccessException">
    /// Thrown when the application lacks the required permissions to create the directory.
    /// </exception>
    /// <example>
    /// <code>
    /// // Usage example
    /// GenerateFolder(); // Creates the directory if it doesn't exist
    /// </code>
    /// </example>
    private static void GenerateFolder()
    {
        if (Directory.Exists(FolderRoute)) return;
        Directory.CreateDirectory(FolderRoute);
        Console.WriteLine("Carpeta creada correctamente.");
    }

    /// <summary>
    /// Displays a list of all files present in the application's storage directory.
    /// </summary>
    /// <remarks>
    /// This method performs the following operations:
    /// 1. Retrieves all files from the directory specified by '_folderRoute'
    /// 2. Prints a header message indicating that files were found
    /// 3. For each file, extracts and displays only the filename without its extension
    /// </remarks>
    /// <exception cref="DirectoryNotFoundException">
    /// Thrown when the specified directory path '_folderRoute' is not found.
    /// </exception>
    /// <exception cref="IOException">
    /// Thrown when an I/O error occurs while accessing the directory.
    /// </exception>
    /// <example>
    /// <code>
    /// // Usage example
    /// PrintFiles(); // Will display something like:
    /// // Files found:
    /// // users
    /// // products
    /// // categories
    /// </code>
    /// </example>
private static void PrintFiles()
{
    var files = Directory.GetFiles(FolderRoute);

    Console.WriteLine("Files found:");
    foreach (var file in files)
    {
        Console.WriteLine(Path.GetFileName(file).Split(".").GetValue(0));
    }
}
    
}
# Console Text File Manager

A simple console application for managing data collections in text files, implementing basic CRUD operations.

## 📑 Table of Contents

- [Features](#features)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Data Structure](#data-structure)
- [Technologies](#technologies)
- [Contributing](#contributing)
- [License](#license)

## ✨ Features

- Create collections with customizable columns
- Read data from existing collections
- Write new records to collections
- Delete records by ID
- Automatic storage directory management
- Interactive console interface

## 🔧 Prerequisites #prerequisites 

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet)
- A code editor like [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio 2022+](https://visualstudio.microsoft.com/)

## 🚀 Installation

1. Clone the repository:
```bash
  cd ConsoleTextFileManager
```

2. Navigate to the project directory:
```bash
  cd ConsoleTextFileManager
```

3. Restore dependencies:
```bash
  bash dotnet restore
```

4. Build the project:
```bash
  bash dotnet build
```

## 📖 Usage

1. Run the application:
```bash
bash dotnet run
```

2. Select an option from the main menu:
   - 1: Read - View the contents of a collection
   - 2: Write - Add a new record to a collection
   - 3: Delete - Remove a record by ID
   - 4: Create Collection - Create a new collection

## 📁 Data Structure

Collections are stored as text files in the `./files` directory with the following format:

- Column names are separated by semicolons (;)
- Records are separated by colons (:)
- The first column is always 'id'

Example file:
id;name;email:1;John Doe;john@example.com:2;Jane Smith;jane@example.com

## 🛠️ Technologies

- .NET 9.0
- C# 13.0
- File system for data storage

## 🤝 Contributing

Contributions are welcome! Please:

1. Fork the repository
2. Create a new branch: `git checkout -b feature/your-feature`
3. Make your changes
4. Commit your changes: `git commit -am 'Add new feature'`
5. Push to the branch: `git push origin feature/your-feature`
6. Submit a pull request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🏗️ Project Structure
```
ConsoleTextFileManager/
├── src/
│ ├── Program.cs # Application entry point
│ ├── ConsoleApplication.cs # Main console interface logic
│ ├── TextFileManagement.cs # CRUD operations implementation
│ ├── IFileManagement.cs # File operations interface
│ └── lib/
│ └── Result.cs # Helper class for result handling
├── files/ # Collections storage directory
├── README.md # Project documentation
└── LICENSE # License file
```

## 🔍 Class Documentation

### ConsoleApplication
Abstract class that handles the main application flow and user interaction.

### TextFileManagement
Implements `IFileManagement` interface and provides the following operations:
- `CreateCollection()`: Creates a new collection with user-defined columns
- `Read()`: Displays the contents of a selected collection
- `Write()`: Adds new records to a collection
- `Delete()`: Removes records by ID

### Result<T>
Generic class for handling operation results:
- `Success`: Creates a success result with a value and message
- `Fail`: Creates a failure result with an error message

## 🔄 Data Flow

1. **Creating a Collection**
   - User provides collection name
   - User defines column names
   - System creates a text file with column structure

2. **Writing Data**
   - User selects a collection
   - System generates a unique ID
   - User provides values for each column
   - Data is appended to the collection file

3. **Reading Data**
   - User selects a collection
   - System displays formatted data from the file

4. **Deleting Data**
   - User selects a collection
   - User provides record ID
   - System removes the matching record

## 📝 Notes

- All file operations are performed synchronously
- Data is stored in plain text format
- The application automatically creates the storage directory if it doesn't exist
- Each record is assigned a unique GUID as its ID
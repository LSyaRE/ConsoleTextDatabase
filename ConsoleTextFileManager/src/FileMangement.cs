namespace ConsoleTextFileManager;

public interface IFileManagement
{
    public void CreateCollection();
    public string Read();
    public void Write(string content);
    public void Delete();
}
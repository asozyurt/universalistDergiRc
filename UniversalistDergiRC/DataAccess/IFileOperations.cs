namespace UniversalistDergiRC.DataAccess
{
    public interface IFileOperations
    {
        void SaveText(string filename, string text);
        string ReadAllText(string filename);
    }
}

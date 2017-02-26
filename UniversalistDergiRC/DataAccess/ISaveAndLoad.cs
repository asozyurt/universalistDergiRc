namespace UniversalistDergiRC.DataAccess
{
    public interface IFileOperations
    {
        void SaveText(string filename, string text);
        //string[] LoadFileLines(string filename);
        string ReadAllText(string filename);
    }
}

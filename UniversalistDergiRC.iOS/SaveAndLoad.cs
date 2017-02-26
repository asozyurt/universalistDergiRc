using System.IO;
using Xamarin.Forms;
using UniversalistDergiRC.DataAccess;
using UniversalistDergiRC.Droid;

[assembly: Dependency (typeof (SaveAndLoad))]
namespace UniversalistDergiRC.Droid
{
    public class SaveAndLoad : IFileOperations
        {
            public void SaveText(string filename, string text)
            {
                var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var filePath = Path.Combine(documentsPath, filename);
                File.WriteAllText(filePath, text);
            }
            public string[] LoadFileLines(string filename)
            {
                var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var filePath = Path.Combine(documentsPath, filename);
                return File.ReadAllLines(filePath);
            }
        }
    
}
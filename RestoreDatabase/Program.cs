using System.IO;
using System.Text.Json;
using BearBlog.Models;

namespace RestoreDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            Events.Init();
            var jsonFilePath = @"C:\Users\zhant\Documents\GitHub\penguin\migrations\typecho\dump.json";
            using var document = JsonDocument.Parse(File.ReadAllText(jsonFilePath));
            var root = document.RootElement;
            Events.OnRestore(new RestoreEventArgs {Root = root});
        }
    }
}
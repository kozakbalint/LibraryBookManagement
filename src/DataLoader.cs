using System.IO;

namespace LibraryBookManagementApp.src
{
    class DataLoader
    {
        StreamReader sr;
        public DataLoader(string path)
        {
            sr = new StreamReader(path);
        }
        public string ReadLine()
        {
            if (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                return line;
            }
            else
            {
                return null;
            }
        }
        public void Close()
        {
            sr.Close();
        }
    }
}

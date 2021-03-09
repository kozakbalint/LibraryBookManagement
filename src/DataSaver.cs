using System.IO;

namespace LibraryBookManagementApp.src
{
    class DataSaver
    {
        StreamWriter sw;
        public DataSaver(string path)
        {
            sw = new StreamWriter(path);
            sw.Flush();
        }

        public void WriteLine(string line)
        {
            sw.WriteLine(line);
        }

        public void Close()
        {
            sw.Close();
        }
    }
}

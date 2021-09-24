
using System.IO;


namespace Task2dot4
{
    class TextLogger
    {
        private object locker;

        private const string FILE_PATH = @"C:\Users\PauliusRuikis\Desktop/TextListener.txt";
        public TextLogger()
        {
            locker = new object();
            if (!File.Exists(FILE_PATH))
            {
                using (StreamWriter sw = File.CreateText(FILE_PATH))
                {

                }
            }
        }
        public void LogInfo(string message)
        {
            
            lock (locker)
            {
                using (StreamWriter sw = File.AppendText(FILE_PATH))
                {
                    sw.WriteLine(message);
                }
            }

        }

    }
}

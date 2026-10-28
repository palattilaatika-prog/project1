using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace lab3
{
    /// <summary>
    /// Scrie evenimentele aplicatiei in fisierul events.txt folosind clasa Trace.
    /// Scrierea se face asincron (Task), ca sa nu blocheze interfata.
    /// </summary>
    public static class Logger
    {
        private static bool initialized;

        public static string LogFilePath
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "events.txt"); }
        }

        public static void Init()
        {
            if (initialized)
                return;

            try
            {
                Trace.Listeners.Add(new TextWriterTraceListener(LogFilePath));
                Trace.AutoFlush = true;
                initialized = true;
            }
            catch (Exception)
            {
                // Daca fisierul nu poate fi creat, aplicatia continua fara jurnal.
            }
        }

        public static Task LogAsync(string message)
        {
            return Task.Run(() =>
            {
                try
                {
                    Trace.WriteLine(string.Format("{0:dd.MM.yyyy HH:mm:ss.fff} - {1}",
                        DateTime.Now, message));
                }
                catch (Exception)
                {
                }
            });
        }

        public static void Shutdown()
        {
            try
            {
                Trace.Flush();
                Trace.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}

using System;
using System.Windows;
using System.IO;

namespace PdfCreator.Helpers
{
    public static class LogWriter
    {
        private static string LogFile = System.AppDomain.CurrentDomain.BaseDirectory + "\\DevLog.txt";

        public static void LogToFile(string msg)
        {
            var t = DateTime.Now.ToLocalTime().ToShortTimeString();
            try
            {
                File.AppendAllText(LogFile, $"{t} >>> {msg} " + Environment.NewLine + "..." + Environment.NewLine);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Je peux pas écrire dans le log !", App.Current.MainWindow.Title, MessageBoxButton.OK, MessageBoxImage.Information);

                throw exception;
            }
        }
    }
}

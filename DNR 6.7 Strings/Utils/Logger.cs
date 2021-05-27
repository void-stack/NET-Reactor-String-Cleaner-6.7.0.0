using System.Drawing;
using Colorful;

namespace DNR.Utils
{
    internal class Logger : ILogger
    {
        private readonly StyleSheet _sheet;

        public Logger()
        {
            _sheet = new StyleSheet(Color.White);
            _sheet.AddStyle("(?<=\\[)\\-(?=\\])", Color.DarkGray);
            _sheet.AddStyle("(?<=\\[)\\*(?=\\])", Color.Cyan);
            _sheet.AddStyle("(?<=\\[)\\!(?=\\])", Color.Orange);
            _sheet.AddStyle("(?<=\\[)\\#(?=\\])", Color.Red);
            _sheet.AddStyle("(?<=\\[)\\+(?=\\])", Color.Lime);
            _sheet.AddStyle("(?<=^....)(.*)", Color.LightGray);
        }

        public void Debug(string message)
        {
            Log($"[-] {message}");
        }

        public void Info(string message)
        {
            Log($"[*] {message}");
        }

        public void Warning(string message)
        {
            Log($"[!] {message}");
        }

        public void Error(string message)
        {
            Log($"[#] {message}");
        }

        public void Success(string message)
        {
            Log($"[+] {message}");
        }

        private void Log(string message)
        {
            Console.WriteLineStyled(message, _sheet);
        }
    }
}
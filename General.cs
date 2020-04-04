using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;

namespace ADAB
{
    /// <summary>
    /// Общиие функции
    /// </summary>
    public static class General
    {
        public static string ExecutableFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "AnyDesk\\AnyDesk.exe");

        /// <summary>
        /// Имя первой книги по-умолчанию
        /// </summary>
        public const string DEFAULTBOOKNAME = "Default";
        public const string DEFAULTSTARTUPARGUMENT = "/autorun";

        /// <summary>
        /// Проверка входных данных на принадлежность к числовому типу
        /// </summary>
        /// <param name="checkData">Строка для проверки</param>
        /// <returns></returns>
        public static bool IsNumeric(string checkData)
        {
            bool returnValue = false;
            for (int i = 0; i < checkData.Length; i++)
            {
                if (char.IsNumber(checkData[i]))
                    returnValue = true;
                else
                {
                    returnValue = false;
                    break;
                }
            }
            return returnValue;
        }

        public static bool IsAutorunEnabled 
        {
            get 
            {
                bool returnValue=false;
                const string pathRegistryKeyStartup ="SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
                var all = Registry.CurrentUser.OpenSubKey(pathRegistryKeyStartup).GetValueNames();
                foreach (var programm in all)
                {
                    if (programm == Application.ProductName)
                        returnValue = true;
                }
                return returnValue ;
            }
        }
        public static void Autorun(bool isEnable)
        {
            string applicationName = Application.ProductName.ToString();
            const string pathRegistryKeyStartup =
                        "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
             
                using (RegistryKey registryKeyStartup = Registry.CurrentUser.OpenSubKey(pathRegistryKeyStartup, true))
                {
                    if (isEnable)
                    {
                        registryKeyStartup.SetValue(
                        applicationName,
                        string.Format("\"{0}\" {1}", 
                                    System.Reflection.Assembly.GetExecutingAssembly().Location, DEFAULTSTARTUPARGUMENT));
                    }
                    else
                        registryKeyStartup.DeleteValue(applicationName, false);
                    
                }
            
        }
    }
}

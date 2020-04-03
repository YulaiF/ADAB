using System;
using System.IO;

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
    }
}

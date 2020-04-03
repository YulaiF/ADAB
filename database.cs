using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using static ADAB.Logic;
using static ADAB.General;


namespace ADAB
{
    public class Database
    {
        public static String dbFileName;
        public static SQLiteConnection m_dbConn;
        public static SQLiteCommand m_sqlCmd;

        /// <summary>
        /// Создание базы и начальной книги адресов
        /// </summary>
        public static void CreateDataBase()
        {
            if (!File.Exists(dbFileName))
                SQLiteConnection.CreateFile(dbFileName);

            try
            {
                m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                m_dbConn.Open();
                m_sqlCmd.Connection = m_dbConn;

                var cmdCreateBooksTable = "CREATE TABLE IF NOT EXISTS Books (BookGUID STRING PRIMARY KEY,BookName STRING UNIQUE ON CONFLICT IGNORE NOT NULL, BookCreationDate STRING);";
                m_sqlCmd.CommandText = cmdCreateBooksTable;
                m_sqlCmd.ExecuteNonQuery();

                if (!IsRecordInBooksExists(DEFAULTBOOKNAME))
                    CreateNewBook(DEFAULTBOOKNAME);

                //lbStatusText.Text = "Connected";
            }
            catch (SQLiteException ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: \r\n" + ex.Message);
#if DEBUG
                throw new Exception(ex.Message);
#endif 
            }
        }

        /// <summary>
        /// Получение книги по её имени
        /// </summary>
        /// <param name="bookName"></param>
        /// <returns></returns>
        public static BookItem GetBookByName(string bookName)
        {
            BookItem returnValue = new BookItem("");

            DataTable dTable = new DataTable();
            String sqlQuery = "SELECT * FROM Books WHERE BookName = \"" + bookName + "\";";

            if (m_dbConn.State != ConnectionState.Open)
                m_dbConn.Open();

            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                if (dTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dTable.Rows.Count; i++)
                    {
                        var guidDefaultTable = dTable.Rows[i].ItemArray;
                        returnValue = new BookItem(Guid.Parse(guidDefaultTable[0].ToString()), guidDefaultTable[1].ToString(), guidDefaultTable[2].ToString());
                    }
                }
            }
            catch (SQLiteException ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: \r\n" + ex.Message);
#if DEBUG
                throw new Exception(ex.Message);
#endif 
            }

            return returnValue;
        }

        /// <summary>
        /// Проверка таблицы на существование 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static bool IsTableExists(string tableName)
        {
            bool returnValue = false;

            DataTable dTable = new DataTable();
            var cmdCheckTable = "SELECT name FROM sqlite_master WHERE type='table' AND name=\"" + tableName + "\";";
            if (m_dbConn.State != ConnectionState.Open)
                m_dbConn.Open();

            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmdCheckTable, m_dbConn);
                adapter.Fill(dTable);
                if (dTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dTable.Rows.Count; i++)
                    {
                        var guidDefaultTable = dTable.Rows[i].ItemArray;
                        returnValue = true;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: \r\n" + ex.Message);
#if DEBUG
                throw new Exception(ex.Message);
#endif  
            }
            return returnValue;
        }

        /// <summary>
        /// Проверка на существование записи в таблице Books
        /// </summary>
        /// <param name="recordName"></param>
        /// <returns></returns>
        public static bool IsRecordInBooksExists(string recordName)
        {
            bool returnValue = false;

            DataTable dTable = new DataTable();
            String sqlQuery = "SELECT BookGUID FROM Books WHERE BookName = \"" + recordName + "\";";

            if (m_dbConn.State != ConnectionState.Open)
                m_dbConn.Open();

            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                if (dTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dTable.Rows.Count; i++)
                    {
                        var guidTable = dTable.Rows[i].ItemArray;
                        returnValue = true;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: \r\n" + ex.Message);
#if DEBUG
                throw new Exception(ex.Message);
#endif 
            }
                return returnValue;
        }

        /// <summary>
        /// Создать запись в Books с названем книги и создать таблицу с уникальным GUID
        /// </summary>
        /// <param name="bookName"></param>
        public static void CreateNewBook(string bookName)
        {
            try
            {
                var guidBook = Guid.NewGuid().ToString();
                var cmdCreateBookTable = "CREATE TABLE IF NOT EXISTS[" + guidBook + "] (ALIAS STRING,ID INTEGER NOT NULL ON CONFLICT FAIL PRIMARY KEY ON CONFLICT FAIL, NAME STRING, COMMENT TEXT DEFAULT \"\" );";
                m_sqlCmd.CommandText = cmdCreateBookTable;
                m_sqlCmd.ExecuteNonQuery();

                var cmdInsertBook = "INSERT INTO Books (BookGUID, BookName, BookCreationDate) VALUES(\"" + guidBook + "\",\"" + bookName + "\", \"" + DateTime.Now.ToShortDateString() + "\");";
                m_sqlCmd.CommandText = cmdInsertBook;
                m_sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: \r\n" + ex.Message);
#if DEBUG
                throw new Exception(ex.Message);
#endif
            }
        }

        /// <summary>
        /// Удаление книги
        /// </summary>
        /// <param name="bookName"></param>
        public static void DeleteBook(string bookName)
        {
            try
            {
                var Book = GetBookByName(bookName);
                var cmdCreateBookTable = "DROP TABLE IF EXISTS[" + Book.BookGUID + "];";
                m_sqlCmd.CommandText = cmdCreateBookTable;
                m_sqlCmd.ExecuteNonQuery();

                var cmdInsertBook = "DELETE FROM Books WHERE BookName=\"" + bookName + "\";";
                m_sqlCmd.CommandText = cmdInsertBook;
                m_sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: \r\n" + ex.Message);
#if DEBUG
                throw new Exception(ex.Message);
#endif 
            }
        }

        /// <summary>
        /// Вставка записи в книгу
        /// </summary>
        /// <param name="book">Андресная книга для вставки записи</param>
        /// <param name="connect_Item">Запись вида <seealso cref="Connect_Item"/></param>
        public static void InsertRecordToBook(BookItem book, Connect_Item connect_Item)
        {
            try
            {
                var cmdAddOItemIntoTable2 = "INSERT INTO [" + book.BookGUID + "] (ALIAS, ID, NAME, COMMENT) SELECT '" +
                                        connect_Item.adAlias + "'," +
                                        connect_Item.ID + ",'" +
                                        connect_Item.Name + "','" +
                                        connect_Item.Comment +
                                        "' WHERE NOT EXISTS (SELECT 1 FROM [" + book.BookGUID + "] WHERE ID = " + connect_Item.ID + ");";
                m_sqlCmd.CommandText = cmdAddOItemIntoTable2;
                m_sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: \r\n" + ex.Message);
#if DEBUG
                throw new Exception(ex.Message);
#endif 
            }
        }

        public static void RenameBook(BookItem book, string newName)
        {
            try
            {
                var cmdChangeItemIntoTable = "UPDATE Books " +
                                            "SET BookName = '" + newName + "' " +
                                            "WHERE BookName = '" + book.BookName + "';";
                m_sqlCmd.CommandText = cmdChangeItemIntoTable;
                m_sqlCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: \r\n" + ex.Message);
#if DEBUG
                throw new Exception(ex.Message);
#endif 
            }
        }

        public static void RenameBook(string oldBookName, string newName)
        {
            try
            {
                var cmdChangeItemIntoTable = "UPDATE Books" +
                                            "SET BookName = '" + newName + "'" +
                                            "WHERE BookName = '" + oldBookName + "';";
                m_sqlCmd.CommandText = cmdChangeItemIntoTable;
                m_sqlCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: \r\n" + ex.Message);
#if DEBUG
                throw new Exception(ex.Message);
#endif 
            }
        }

        /// <summary>
        /// Изменить запись в книге
        /// </summary>
        /// <param name="book"></param>
        /// <param name="connect_Item"></param>
        public static void UpdateRecordInBook(BookItem book, Connect_Item connect_Item)
        {
            try
            {
                var cmdChangeItemIntoTable = "UPDATE [" + book.BookGUID + "] " +
                           "SET " +
                           "ALIAS = '" + connect_Item.adAlias + "'," +
                           "ID = '" + connect_Item.ID + "'," +
                           "NAME = '" + connect_Item.Name + "', " +
                           "COMMENT = '" + connect_Item.Comment + "' " +
                           "WHERE ID = " + connect_Item.ID + ";";

                m_sqlCmd.CommandText = cmdChangeItemIntoTable;
                m_sqlCmd.ExecuteNonQuery();

            }
            catch (SQLiteException ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: \r\n" + ex.Message);
#if DEBUG
                throw new Exception(ex.Message);
#endif 
            }

        }
    }
}

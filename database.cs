using Mono.Data.Sqlite;
using System;
using System.Data;
using System.IO;
using static ADAB.General;
using static ADAB.Logic;


namespace ADAB
{
    public static class Database
    {
        public static String dbFileName;
        public static SqliteConnection m_dbConn;
        public static SqliteCommand m_sqlCmd;

        /// <summary>
        /// Создание базы и начальной книги адресов
        /// </summary>
        public static void CreateDataBase()
        {
            if (!File.Exists(dbFileName))
                SqliteConnection.CreateFile(dbFileName);

            try
            {
                m_dbConn = new SqliteConnection("URI=file:" + dbFileName);//Data Source=file:SqliteTest.db
                                                                          //m_dbConn = new SqliteConnection("Data Source=" + dbFileName + ";Version=3;");

                m_dbConn.Open();
                m_sqlCmd = m_dbConn.CreateCommand();

                var cmdCreateBooksTable = "CREATE TABLE IF NOT EXISTS Books (BookGUID STRING PRIMARY KEY,BookName STRING UNIQUE ON CONFLICT IGNORE NOT NULL, BookCreationDate STRING);";
                m_sqlCmd.CommandText = cmdCreateBooksTable;
                m_sqlCmd.ExecuteNonQuery();

                if (!IsRecordInBooksExists(DEFAULTBOOKNAME))
                    CreateNewBook(DEFAULTBOOKNAME);

                //lbStatusText.Text = "Connected";
            }
            catch (SqliteException ex)
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
            m_sqlCmd.Parameters.Add("@bookname", DbType.StringFixedLength).Value = bookName;
            m_sqlCmd.CommandText = "SELECT * FROM Books WHERE BookName = @bookname;";

            if (m_dbConn.State != ConnectionState.Open)
                m_dbConn.Open();

            try
            {
                SqliteDataAdapter adapter = new SqliteDataAdapter(m_sqlCmd);
                adapter.Fill(dTable);

                if (dTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dTable.Rows.Count; i++)
                    {
                        var guidTable = dTable.Rows[i].ItemArray;
                        returnValue = new BookItem(Guid.Parse(guidTable[0].ToString()), guidTable[1].ToString(), guidTable[2].ToString());
                    }
                }
            }
            catch (SqliteException ex)
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
            //m_sqlCmd.Parameters.Add("@tableName", DbType.String).Value = tableName;
            //var cmdCheckTable = "SELECT name FROM sqlite_master WHERE type='table' AND name=@tableName;";
            var cmdCheckTable = "SELECT name FROM sqlite_master WHERE type='table' AND name=\"" + tableName + "\";";
            if (m_dbConn.State != ConnectionState.Open)
                m_dbConn.Open();

            try
            {
                SqliteDataAdapter adapter = new SqliteDataAdapter(cmdCheckTable, m_dbConn);
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
            catch (SqliteException ex)
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
            //var m_sqlCmd = new SqliteCommand();
            m_sqlCmd.Parameters.Add("@recordName", DbType.StringFixedLength).Value = recordName;
            m_sqlCmd.CommandText = "SELECT BookGUID FROM Books WHERE BookName = '@recordName';";

            if (m_dbConn.State != ConnectionState.Open)
                m_dbConn.Open();

            try
            {
                ////m_sqlCmd = m_dbConn.CreateCommand();
                //var reader = m_sqlCmd.ExecuteReader();
                //while (reader.Read())
                //{
                //    string firstName = reader.GetString(0);
                //    Console.WriteLine("Name: {0} {1}",
                //        firstName, lastName);
                //}
                //// clean up
                //reader.Dispose();
                //m_sqlCmd.Dispose();
                ////m_sqlCmd.Close();

                SqliteDataAdapter adapter = new SqliteDataAdapter();
                adapter.SelectCommand = m_sqlCmd;

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
            catch (SqliteException ex)
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
        /// <param name="bookName">Название книги</param>
        public static void CreateNewBook(string bookName)
        {
            try
            {
                m_sqlCmd = m_dbConn.CreateCommand();
                var guidBook = Guid.NewGuid().ToString();
                m_sqlCmd.Parameters.Add("@guidBook", DbType.StringFixedLength).Value = guidBook;
                m_sqlCmd.Parameters.Add("@bookName", DbType.StringFixedLength).Value = bookName;
                m_sqlCmd.Parameters.Add("@DateTimeNow", DbType.StringFixedLength).Value = DateTime.Now.ToShortDateString();

                var cmdCreateBookTable = "CREATE TABLE IF NOT EXISTS[" + guidBook + "] (ALIAS STRING,ID INTEGER NOT NULL ON CONFLICT FAIL PRIMARY KEY ON CONFLICT FAIL, NAME STRING, COMMENT TEXT DEFAULT \"\" );";
                m_sqlCmd.CommandText = cmdCreateBookTable;
                m_sqlCmd.ExecuteNonQuery();

                var cmdInsertBook = "INSERT INTO Books (BookGUID, BookName, BookCreationDate) VALUES(@guidBook, @bookName, @DateTimeNow);";
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
                // передавать сразу книгу
                var Book = GetBookByName(bookName);
                var cmdCreateBookTable = "DROP TABLE IF EXISTS[" + Book.BookGUID + "];";
                m_sqlCmd.CommandText = cmdCreateBookTable;
                m_sqlCmd.ExecuteNonQuery();

                m_sqlCmd.Parameters.Add("@bookName", DbType.StringFixedLength).Value = bookName;

                var cmdInsertBook = "DELETE FROM Books WHERE BookName=@bookName;";
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
                //m_sqlCmd.Parameters.Add("@bookguid", DbType.StringFixedLength).Value = book.BookGUID;
                m_sqlCmd.Parameters.Add("@alias", DbType.StringFixedLength).Value = connect_Item.adAlias;
                m_sqlCmd.Parameters.Add("@id", DbType.Int64).Value = connect_Item.ID;
                m_sqlCmd.Parameters.Add("@name", DbType.StringFixedLength).Value = connect_Item.Name;
                m_sqlCmd.Parameters.Add("@comment", DbType.StringFixedLength).Value = connect_Item.Comment;

                var cmdAddOItemIntoTable2 = "INSERT INTO [" + book.BookGUID + "] (ALIAS, ID, NAME, COMMENT) SELECT " +
                                        "@alias," +
                                        "@id, " +
                                        "@name" + "," +
                                        "@comment" +
                                        " WHERE NOT EXISTS (SELECT 1 FROM [" + book.BookGUID + "] WHERE ID=@id);";
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

        /// <summary>
        /// Переименовать книгу
        /// </summary>
        /// <param name="book">Старая книга</param>
        /// <param name="newName">Новое имя книги</param>
        public static void RenameBook(BookItem book, string newName)
        {
            try
            {
                m_sqlCmd.Parameters.Add("@newName", DbType.StringFixedLength).Value = newName;
                m_sqlCmd.Parameters.Add("@oldBookName", DbType.StringFixedLength).Value = book.BookName;
                var cmdChangeItemIntoTable = "UPDATE Books" +
                                            " SET BookName = @newName" +
                                            " WHERE BookName = @oldBookName;";
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
        /// Переименовать книгу
        /// </summary>
        /// <param name="oldBookName">Старое имя книги</param>
        /// <param name="newName">Новое имя книги</param>
        public static void RenameBook(string oldBookName, string newName)
        {
            try
            {
                m_sqlCmd.Parameters.Add("@newName", DbType.StringFixedLength).Value = newName;
                m_sqlCmd.Parameters.Add("@oldBookName", DbType.Int32).Value = oldBookName;
                var cmdChangeItemIntoTable = "UPDATE Books" +
                                            "SET BookName = @newName" +
                                            "WHERE BookName = @oldBookName;";
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
                m_sqlCmd.Parameters.Add("@alias", DbType.StringFixedLength).Value = connect_Item.adAlias;
                m_sqlCmd.Parameters.Add("@id", DbType.Int64).Value = connect_Item.ID;
                m_sqlCmd.Parameters.Add("@name", DbType.StringFixedLength).Value = connect_Item.Name;
                m_sqlCmd.Parameters.Add("@comment", DbType.StringFixedLength).Value = connect_Item.Comment;

                var cmdChangeItemIntoTable = "UPDATE [" + book.BookGUID + "] " +
                           "SET " +
                           "ALIAS = @alias," +
                           "ID = @id," +
                           "NAME = @name, " +
                           "COMMENT = @comment " +
                           "WHERE ID = @id;";
                m_sqlCmd.CommandText = cmdChangeItemIntoTable;
                m_sqlCmd.ExecuteNonQuery();

            }
            catch (SqliteException ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: \r\n" + ex.Message);
#if DEBUG
                throw new Exception(ex.Message);
#endif 
            }

        }

        /// <summary>
        /// Удалить запись из книги
        /// </summary>
        /// <param name="book"></param>
        /// <param name="connect_Item"></param>
        public static void DeleteRecordInBook(BookItem book, Connect_Item connect_Item)
        {
            try
            {
                m_sqlCmd.Parameters.Add("@id", DbType.Int64).Value = connect_Item.ID;
                var cmdDeleteItem = "DELETE FROM [" + book.BookGUID + "] WHERE ID = @id;";
                m_sqlCmd.CommandText = cmdDeleteItem;
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
    }
}

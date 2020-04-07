using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using static ADAB.Database;
using static ADAB.General;
using static ADAB.Logic;

/// <summary>
/// https://support.anydesk.com/Command_Line_Interface
/// </summary>
namespace ADAB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var cmdArgs = Environment.GetCommandLineArgs();
            if (cmdArgs.Length >=2)
            {
                if (cmdArgs[1]==DEFAULTSTARTUPARGUMENT)
                {
                    WindowState = FormWindowState.Minimized;
                    ShowInTaskbar = false;
                    Visible = false;
                }
            }
            автозагрузкаToolStripMenuItem.Checked = IsAutorunEnabled;
            Text = Application.ProductName;
            LockItemInfoOnForm(true);
            Database.dbFileName = "ADAB.db";
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            CreateDataBase();
            FillComboBox();
            lbStatusText.Text = m_dbConn.State == ConnectionState.Open ? "Connected" : "Disconnected";
            comboBox1.SelectedIndex = 0;
        }

        private void LockItemInfoOnForm(bool isLock)
        {
            textBox1.ReadOnly = isLock;
            textBox2.ReadOnly = isLock;
            textBox3.ReadOnly = isLock;
            textBox4.ReadOnly = isLock;
            SaveItemButton.Enabled = !isLock;
        }

        private void ClearItemInfoOnForm()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            SaveItemButton.Enabled = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                var connectionString = (Connect_Item)listBox1.SelectedItem;
                textBox1.Text = connectionString.adAlias;
                textBox2.Text = connectionString.ID;
                textBox3.Text = connectionString.Name;
                textBox4.Text = connectionString.Comment;
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!(listBox1.SelectedItem is null))
            {
                var conStr = (Connect_Item)listBox1.SelectedItem;
                Start(conStr.ID);
            }
        }

        private void Start(string ID)
        {
            try
            {
                string Arg = ID;
                var process = new Process();
                process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                process.StartInfo.FileName = ExecutableFileName;
                process.StartInfo.Arguments = Arg;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.UseShellExecute = false;
                //process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //process.StartInfo.CreateNoWindow = true;

                process.Start();
                //StreamReader reader = process.StandardOutput;
                //'MsgBox(process.StartInfo.StandardOutputEncoding.EncodingName.ToString & ", " & reader.CurrentEncoding.CodePage.ToString)
                //process.WaitForExit();
                //string output = reader.ReadToEnd();
                //return output;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SaveItemButton_Click(object sender, EventArgs e)
        {
            var currentBook = (BookItem)comboBox1.SelectedItem;
            var item = new Connect_Item(textBox2.Text, textBox1.Text, textBox3.Text, textBox4.Text);
            var searchClone = GetConnect_ItemFromListbox(textBox2.Text);

            if (AddItemButton.Enabled)
            {
                if (textBox2.Text != "")
                {
                    if (IsNumeric(textBox2.Text))
                    {
                        if (searchClone.ID != textBox2.Text)
                        {
                            InsertRecordToBook(currentBook, item);
                        }
                        else
                            MessageBox.Show("Такой ID уже существует: " + searchClone, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("ID должен быть числом");
                }
                else
                    MessageBox.Show("Значение поля ID не может быть пустым", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (ChangeItemButton.Enabled)
            {
                if ((searchClone.ID == zeroConnectItem.ID) || (((Connect_Item)listBox1.SelectedItem) == searchClone))
                {
                    UpdateRecordInBook(currentBook, item);
                }
                else
                    MessageBox.Show("Такой ID уже существует: " + searchClone, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (AddItemButton.Enabled)
                AddItemButton_Click(SaveItemButton, e);
            else
                ChangeItemButton_Click(SaveItemButton, e);

            comboBox1_SelectedIndexChanged(SaveItemButton, e);
        }

        private void AddItemButton_Click(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = -1;
            ClearItemInfoOnForm();
            LockItemInfoOnForm(!textBox1.ReadOnly);
            comboBox1.Enabled = textBox1.ReadOnly;
            ChangeItemButton.Enabled = textBox1.ReadOnly;
            MoveItemButton.Enabled = textBox1.ReadOnly;
            DeleteItemButton.Enabled = textBox1.ReadOnly;
            listBox1.Enabled = textBox1.ReadOnly;
            AddItemButton.Text = textBox1.ReadOnly == true ? "Добавить" : "Отмена";
        }

        private void ChangeItemButton_Click(object sender, EventArgs e)
        {
            if ((listBox1.SelectedIndex != -1))// && (listBox1.SelectedIndex!=)
            {
                SearchTextBox.ReadOnly = !SearchTextBox.ReadOnly;
                LockItemInfoOnForm(!textBox1.ReadOnly);
                comboBox1.Enabled = textBox1.ReadOnly;
                AddItemButton.Enabled = textBox1.ReadOnly;
                MoveItemButton.Enabled = textBox1.ReadOnly;
                ChangeItemButton.Text = textBox1.ReadOnly == true ? "Изменить" : "Отмена";
                DeleteItemButton.Enabled = textBox1.ReadOnly;
                listBox1.Enabled = textBox1.ReadOnly;
            }
        }

        private void DeleteItemButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                if (MessageBox.Show("Удалить " + listBox1.SelectedItem.ToString() + "?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    var currentBook = (BookItem)comboBox1.SelectedItem;
                    DeleteRecordInBook(currentBook, (Connect_Item)listBox1.SelectedItem);
                    ClearItemInfoOnForm();
                    comboBox1_SelectedIndexChanged(SaveItemButton, e);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LockItemInfoOnForm(true);
            ClearItemInfoOnForm();
            BookItem cur = (BookItem)comboBox1.SelectedItem;
            var q = GetListOfAdressFromBook(cur.BookGUID);
            listBox1.Items.Clear();
            foreach (var item in q)
            {
                listBox1.Items.Add(item);
            }
            label5.Text = "Адресная книга от " + cur.BookCreationDate.ToString()+ ":";
        }

        public void FillComboBox()
        {
            DataTable dTable = new DataTable();
            string sqlQuery;

            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show("Open connection with database");
                return;
            }

            try
            {
                sqlQuery = "SELECT * FROM Books;";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);
                var ListBook = new List<Logic.BookItem>();

                if (dTable.Rows.Count > 0)
                {
                    comboBox1.Items.Clear();
                    for (int i = 0; i < dTable.Rows.Count; i++)
                    {
                        var item = dTable.Rows[i].ItemArray;
                        ListBook.Add(new Logic.BookItem(new Guid(item[0].ToString()), item[1].ToString(), item[2].ToString()));
                    }
                    foreach (var bookitem in ListBook)
                    {
                        comboBox1.Items.Add(bookitem);
                    }
                }
                else
                    CreateNewBook(DEFAULTBOOKNAME);
                comboBox1.SelectedIndex = 0;
            }
            catch (SQLiteException ex)
            {
#if DEBUG
                MessageBox.Show("Error: " + ex.Message);
#endif
            }
        }

        public List<Connect_Item> GetListOfAdressFromBook(Guid bookguid)
        {
            List<Connect_Item> returnValue = new List<Connect_Item>();

            DataTable dTable = new DataTable();
            String sqlQuery;

            if (m_dbConn.State != ConnectionState.Open)
            {
                m_dbConn.Open(); //MessageBox.Show("Open connection with database");
            }

            try
            {
                sqlQuery = "SELECT * FROM [" + bookguid + "];";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                if (dTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dTable.Rows.Count; i++)
                    {
                        var item = dTable.Rows[i].ItemArray;
                        Connect_Item roster_Item = new Connect_Item(item[1].ToString(), item[0].ToString(), item[2].ToString(), item[3].ToString());
                        returnValue.Add(roster_Item);
                    }
                }
                //else
                //MessageBox.Show("Database is empty");
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return returnValue;
        }

        public Connect_Item GetConnect_ItemFromListbox(string ID)
        {
            var returnValue = zeroConnectItem;
            foreach (Connect_Item item in listBox1.Items)
            {
                if (item.ID == ID)
                    returnValue = item;
            }
            return returnValue;
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var About = new AboutBox1();
            About.ShowDialog();
        }

        private void импортИзНедавнихСеансовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(UserConfig.ConfigUserFile))
            {
                var suggestedBookName = System.Net.Dns.GetHostName() + " (" + Environment.UserName.ToString() + ")";
                var selectForm = new frmSelectBook
                {
                    Owner = this,
                    SUGGESTEDBOOKNAME = suggestedBookName
                };

                selectForm.ShowDialog();
                FillComboBox();
                var selectBook = selectForm.SELECTEDBOOK;
                if (selectBook.BookName != "")
                {
                    var q = UserConfig.GetLastConnections();
                    foreach (var item in q)
                    {
                        InsertRecordToBook(selectBook, new Connect_Item(item.ID , item.adAlias, item.Name,"Добавлен через \"Импорт недавних сеансов\" \r\n "  + DateTime.Now.ToLongDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString()));
                    }
                    var findIndex = comboBox1.FindStringExact(selectBook.BookName);
                    comboBox1.SelectedIndex = findIndex != -1 ? findIndex : 0;
                    comboBox1_SelectedIndexChanged(this, e);
                }
            }
            else
                MessageBox.Show("Недавние сеансы не найдены");
        }

        private void создатьНовуюКнигуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var BookName = Interaction.InputBox("Введите название новой адресной книги", "Создать новую книгу");
            if (BookName != "")
            {
                if (!IsRecordInBooksExists(BookName))
                {
                    CreateNewBook(BookName);
                    FillComboBox();
                    try { comboBox1.SelectedIndex = comboBox1.Items.Count - 1; }
                    catch (Exception) { }
                }
                else
                    MessageBox.Show("Книга \"" + BookName + "\" уже существует", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void переименоватьКнигуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var currentBook = (BookItem)comboBox1.SelectedItem;
            if (currentBook.BookName != DEFAULTBOOKNAME)
            {
                var newBookName = Interaction.InputBox("Введите новое название адресной книги", "Переименовать книгу \"" + currentBook.BookName + "\"");
                if (newBookName != "")
                {
                    if (!IsRecordInBooksExists(newBookName))
                    {
                        RenameBook(currentBook, newBookName);
                        FillComboBox();
                    }
                    else
                        MessageBox.Show("Книга \"" + newBookName + "\" уже существует", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
                MessageBox.Show("Книга по-умолчанию не может быть переименована", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void удалитьКнигуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                var currentBook = (BookItem)comboBox1.SelectedItem;
                if (currentBook.BookName != DEFAULTBOOKNAME)
                {
                    if (MessageBox.Show("Удалить безвозвратно книгу \r\n\r\n" + currentBook.BookName + " от " + currentBook.BookCreationDate + "\r\n\r\n со всеми записями?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        DeleteBook(currentBook.BookName);
                        FillComboBox();
                        //comboBox1.SelectedIndex = comboBox1.Items.Count != 0 ? 0 : -1;
                    }
                }
                else
                    MessageBox.Show("Книга по-умолчанию не может быть удалена", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MoveItemButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                var currentItemID = textBox2.Text;                          //обнуляется 
                var item = GetConnect_ItemFromListbox(currentItemID);       //после FillComboBox, 
                var currentBook = (BookItem)comboBox1.SelectedItem;         //поэтому определяем в начале
                var currentComboBoxIndexBook = comboBox1.SelectedIndex;
                var selectForm = new frmSelectBook();
                selectForm.ShowDialog();
                FillComboBox();
                var selectBook = selectForm.SELECTEDBOOK;
                if (currentBook!=selectBook)
                {
                    if (selectBook.BookName != "")
                    {
                        InsertRecordToBook(selectBook, item);
                        DeleteRecordInBook(currentBook, item);

                        ClearItemInfoOnForm();
                        var findIndex = comboBox1.FindStringExact(selectBook.BookName);
                        comboBox1.SelectedIndex = findIndex != -1 ? findIndex : 0;
                        comboBox1_SelectedIndexChanged(this, e);
                    }
                    else
                        comboBox1.SelectedIndex = currentComboBoxIndexBook; //возвращаем просматриваемую книгу после обновления списка книг
                }
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = true;
                this.Activate();
                ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon1.Visible = false;
            notifyIcon1.Dispose();
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = !this.Visible;
            }
        }

        private void tmrNotify_Tick(object sender, EventArgs e)
        {
            try
            {
                notifyIcon1.Icon = Icon;
                notifyIcon1.Text = Application.ProductName;
                notifyIcon1.Visible = true;
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void добавитьТекущийАдресToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(UserConfig.ConfigUserFile))
            {
                var suggestedIDName = System.Net.Dns.GetHostName();
                var selectForm = new frmSelectBook();

                selectForm.ShowDialog();
                FillComboBox();
                var selectBook = selectForm.SELECTEDBOOK;
                if (selectBook.BookName != "")
                {
                    var thisID = UserConfig.GetThisID();
                    
                    if (thisID.ID != zeroConnectItem.ID)
                    {
                        var item = new Connect_Item(thisID.adAlias , thisID.ID ,suggestedIDName, "Добавлен через \"Текущий адрес\" \r\n" + DateTime.Now.ToLongDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString());
                        InsertRecordToBook(selectBook, item);
                    }
                    else
                        MessageBox.Show("Текущий адрес не найден");

                    var findIndex = comboBox1.FindStringExact(selectBook.BookName);
                    comboBox1.SelectedIndex = findIndex != -1 ? findIndex : 0;
                    comboBox1_SelectedIndexChanged(this, e);
                }
            }
            else
                MessageBox.Show("Текущий адрес не найден");
        }

        private void автозагрузкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Autorun(автозагрузкаToolStripMenuItem.Checked);
        }
    }
}

using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using static ADAB.Database;
using static ADAB.General;
using static ADAB.Logic;

namespace ADAB
{
    public partial class frmSelectBook : Form
    {
        public BookItem SELECTEDBOOK = new BookItem("");
        public string SUGGESTEDBOOKNAME = "";
        public frmSelectBook()
        {
            InitializeComponent();
        }

        private void frmSelectBook_Load(object sender, EventArgs e)
        {
            FillComboBox();
            comboBox1.Items.Add("<<Создать новую книгу>>");
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (true)
            {
                SELECTEDBOOK = (BookItem)comboBox1.SelectedItem;
                Close();
            }
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "<<Создать новую книгу>>")
            {
                var BookName = Interaction.InputBox("Введите название новой адресной книги", "Создать новую книгу", SUGGESTEDBOOKNAME);
                if (BookName != "")
                {
                    if (!IsRecordInBooksExists(BookName))
                    {
                        CreateNewBook(BookName);
                        FillComboBox();
                        comboBox1.Items.Add("<<Создать новую книгу>>");
                        try { comboBox1.SelectedIndex = comboBox1.Items.Count - 2; }
                        catch (Exception) { }
                    }
                    else
                    {
                        MessageBox.Show("Книга \"" + BookName + "\" уже существует", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        var findIndex = comboBox1.FindStringExact(BookName);
                        comboBox1.SelectedIndex = findIndex != -1 ? findIndex : 0;
                    }
                }
                else
                    comboBox1.SelectedIndex = 0;
            }
        }
    }
}

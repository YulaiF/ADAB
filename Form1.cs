using System;
using System.IO;
using System.Windows.Forms;
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
            Text = Application.ProductName;
            if (!(UserConfig.Roster_Items is null))
            {
                foreach (var item in UserConfig.Roster_Items)
                {
                    var displayName = item.Name == "" ? item.adAlias : item.Name; 
                    listBox1.Items.Add(displayName);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
                    
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserConfig.Roster_Item conStr = UserConfig.GetItemFromRoster_ItemsByAnyData(listBox1.SelectedItem.ToString());

            textBox1.Text = conStr.adAlias;
            textBox2.Text = conStr.ID;
            textBox3.Text = conStr.Name;
            checkBox1.Checked = conStr.Favorite == "fav" ? true : false;
            checkBox1.Text  = conStr.Favorite == "fav" ? "Да" : "Нет";
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //run ID
        }

        //private string Start(string ID)
        //{
        //    try
        //    {
        //        //    var process = new System.Diagnostics.Process()
        //        //    string Arg = ID
        //        //process.StartInfo
        //        //    .StandardOutputEncoding = System.Text.Encoding.UTF8
        //        //    .FileName = "curl.exe"
        //        //    .Arguments = Arg
        //        //    .RedirectStandardOutput = True
        //        //    .RedirectStandardError = True
        //        //    .RedirectStandardInput = True
        //        //    .UseShellExecute = False
        //        //    .WindowStyle = ProcessWindowStyle.Hidden
        //        //    .CreateNoWindow = True
        //        //End With
        //        //process.Start()
        //        //Dim reader As System.IO.StreamReader = process.StandardOutput
        //        //'MsgBox(process.StartInfo.StandardOutputEncoding.EncodingName.ToString & ", " & reader.CurrentEncoding.CodePage.ToString)
        //        //process.WaitForExit()
        //        //Dim output As String
        //        //output = reader.ReadToEnd
        //        //Return output
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}

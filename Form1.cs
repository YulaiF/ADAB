using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var AnyDeskConfigFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+ "\\AnyDesk\\" ;
            var AnyDeskConfigUserFile = "user.conf";
            var configStringLastConnection = "ad.roster.items=";
            string[] LastConnection= {""};

            var pathToConfigUserFile = AnyDeskConfigFolder + AnyDeskConfigUserFile;

            if (File.Exists(pathToConfigUserFile))
            {
                var allLinesFromFile=File.ReadAllLines(pathToConfigUserFile);
                foreach (var searchString in allLinesFromFile)
                {
                    if (searchString.Contains(configStringLastConnection))
                    {
                        LastConnection = searchString.Replace(configStringLastConnection, "").Split(new char[] {';'});
                        break;
                    }
                }

                foreach (var connection in LastConnection)
                {
                    listBox1.Items.Add(connection);
                }
            }

            

        }

        private string ConstructorConnectionString(string ID, string adAlias= "", string Name="", bool isFavorite=false )
        {
            string retval="";

            if (string.IsNullOrEmpty(adAlias))
            {
                if (isFavorite) retval = ID + "," + ID + "," + Name + "," + "fav"; else retval = ID + "," + ID + "," + Name + "," + "";
            }
            else
                if (isFavorite) retval = adAlias + "," + ID + "," + Name + "," + "fav"; else retval = adAlias + "," + ID + "," + Name + "," + "";

            return retval;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ConstructorConnectionString("1111111111", "", "", true));
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private string Start(string ID)
        {
            try
            {
                var process = new System.Diagnostics.Process()
                string Arg = "-F ""xml_file=@" & filename & """ " & URL
            process.StartInfo
                .StandardOutputEncoding = System.Text.Encoding.UTF8
                .FileName = "curl.exe"
                .Arguments = Arg
                .RedirectStandardOutput = True
                .RedirectStandardError = True
                .RedirectStandardInput = True
                .UseShellExecute = False
                .WindowStyle = ProcessWindowStyle.Hidden
                .CreateNoWindow = True
            End With
            process.Start()
            Dim reader As System.IO.StreamReader = process.StandardOutput
            'MsgBox(process.StartInfo.StandardOutputEncoding.EncodingName.ToString & ", " & reader.CurrentEncoding.CodePage.ToString)
            process.WaitForExit()
            Dim output As String
            output = reader.ReadToEnd
            Return output
            }
            catch (Exception)
            {

                throw;
            }
            
    
    }
    }
}

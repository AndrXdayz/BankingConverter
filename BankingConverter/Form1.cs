using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace BankingConverter
{
    public partial class Form1 : Form
    {
        public string[] DCBankingJSONFiles;
        public List<string> DCBankingFiles = new List<string>();
        public string ServerProfilesFolderDir { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Path",

              //  CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "json files (*.json)|*.json",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int fileCount = 0;
           using(var fdg = new FolderBrowserDialog()) 
           {
                DialogResult result = fdg.ShowDialog();

                if(result == DialogResult.OK && !string.IsNullOrWhiteSpace(fdg.SelectedPath)) 
                {
                    string[] filenames = Directory.GetFiles(fdg.SelectedPath + @"\DC_BANKING\PlayerDatabase\");
                    ServerProfilesFolderDir = fdg.SelectedPath;
                    foreach (string filename in filenames) 
                    {
                       
                        if (filename.Contains(".json")) 
                        {
                            fileCount++;
                            DCBankingFiles.Add(filename);
                        }
                    }
                    MessageBox.Show("found " + fileCount + "files \nIn folder please convert it now!");
                    button3.Enabled = true;
                }
           }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ServerProfilesFolderDir)) 
            {
                for (int i = 0; i < DCBankingFiles.Count; i++)
                {
                    var DCBankingJson = File.ReadAllText(DCBankingFiles[i]);
                    var JsonMeta = new DCBankingObject();
                    JsonConvert.PopulateObject(DCBankingJson, JsonMeta);

                    var AdvJsonObject = new AdvancedBankingObject();

                    AdvJsonObject.m_BonusCurrency = JsonMeta.m_MaxOwnedCurrencyBonus;
                    AdvJsonObject.m_PayCheckBonus = 0;
                    AdvJsonObject.m_PlayerName = JsonMeta.m_Username;
                    AdvJsonObject.m_Steam64ID = JsonMeta.m_PlainID;
                    AdvJsonObject.m_OwnedCurrency = JsonMeta.m_OwnedCurrency;
                    AdvJsonObject.m_ClanID = "NONE";
                    if (!Directory.Exists(ServerProfilesFolderDir + @"\KR_BANKING\PlayerDataBase\"))
                        Directory.CreateDirectory(ServerProfilesFolderDir + @"\KR_BANKING\PlayerDataBase\");
                    using(StreamWriter file = File.CreateText(ServerProfilesFolderDir + @"\KR_BANKING\PlayerDataBase\" + JsonMeta.m_PlainID + ".json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, AdvJsonObject);
                        serializer.Formatting = Formatting.Indented;
                    }
                }

                MessageBox.Show("DONE!");


            }
            else 
            {
                MessageBox.Show("ERROR!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*   void FilePicker()
           {
               var fileContent = string.Empty;
               var filePath = string.Empty;

               using (OpenFileDialog openFileDialog = new OpenFileDialog())
               {
                   openFileDialog.InitialDirectory = "c:\\";
                   openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                   openFileDialog.FilterIndex = 2;
                   openFileDialog.RestoreDirectory = true;

                   if (openFileDialog.ShowDialog() == DialogResult.OK)
                   {
                       //Get the path of specified file
                       filePath = openFileDialog.FileName;

                       //Read the contents of the file into a stream
                       var fileStream = openFileDialog.OpenFile();

                       using (StreamReader reader = new StreamReader(fileStream))
                       {
                           fileContent = reader.ReadToEnd();
                       }
                   }

               }
        */

    }
}

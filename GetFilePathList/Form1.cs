using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLib;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using ClosedXML.Excel;
using Microsoft.VisualBasic.FileIO;

namespace GetFilePathList
{
    public partial class Form1 : Form
    {

        List<string> _throughElements = new List<string>() { "YukkuriMovieMakerProject", "TimelineItems", "CommonItem" };
        List<string> _targetElements = new List<string>() { "FilePath" };
        string user = Environment.UserName;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Ramda();
            if (!File.Exists("default.txt")) 
            {
                FileStream str = File.Create("default.txt");
                str.Dispose();
            }
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void Ramda() 
        {

            button1.Click += (_sender, _e) => 
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "YMMPファイル(*.ymmp)|*.ymmp";

                using (StreamReader sr = new StreamReader("default.txt", Encoding.GetEncoding("Shift_JIS")))
                {
                    string defaultPath = sr.ReadLine();
                    if (defaultPath == "")
                    {
                        fileDialog.InitialDirectory = @"C:\";
                    }
                    else
                    {
                        fileDialog.InitialDirectory = defaultPath;

                    }
                    fileDialog.Title = "YMMPファイルを指定してください";

                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        textBox1.Text = fileDialog.FileName;
                        int LastMoneyMark = textBox1.Text.LastIndexOf("\\");
                        if (LastMoneyMark > 0)
                        {
                            string fileName = textBox1.Text.Substring(LastMoneyMark + 1);
                            label1.Text = "ファイル名：" + fileName;
                        }
                    }
                }
            };

            button4.Click += (_sender, _e) => 
            {
                //DataTable xmlTable = XMLConvert.GetXMLTable(textBox1.Text, _throughElements, _targetElements);
                // xmlTable;
                if (textBox1.Text == "")
                {
                    MessageBox.Show("YMMPファイルを指定してください");
                }
                else
                {
                    if (checkBox2.Checked) 
                    {
                        dataGridView1.DataSource = sortByKakutyosi(getYMMPInner(textBox1.Text));
                    }
                    else 
                    {
                        dataGridView1.DataSource = getYMMPInner(textBox1.Text);
                    }

                }
            };

            button5.Click += (_sender, _e) => 
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("YMMPファイルを指定してください");
                }
                else
                {
                    if (checkBox2.Checked)
                    {
                        dataGridView1.DataSource = sortByKakutyosi(getYMMPInner(textBox1.Text,true));
                    }
                    else
                    {
                        dataGridView1.DataSource = getYMMPInner(textBox1.Text, true);
                    }
                }
            };

            dataGridView1.CellDoubleClick += (_sender, _e) => 
            {

                if (_e.RowIndex>-1&&dataGridView1[_e.ColumnIndex, _e.RowIndex].Value!=null)
                {
                    OpenFile(_e.ColumnIndex,_e.RowIndex);
                }
            
            };


            button2.Click += (_sender, _e) =>
            {

                //CSV

                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "Forder|.";
                fileDialog.Title = "CSVファイルの出力先を選択";
                fileDialog.FileName = "SelectFolder";
                string csvFileName = "";
                fileDialog.CheckFileExists = false;
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {

                    csvFileName = Path.GetDirectoryName(fileDialog.FileName);
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Number");
                    dt.Columns.Add("FileName");
                    dt.Columns.Add("FullPath");
                    int count = 0;
                        for (int row = 0; row < dataGridView1.Rows.Count - 1; row++)
                        {
                            DataRow dr = dt.NewRow();
                            for (int col = 0; col < dataGridView1.Columns.Count; col++)
                            {
                                dr[col] = dataGridView1.Rows[row].Cells[col].Value;
                            }
                            dt.Rows.Add(dr);
                            count++;
                        }

                    dt.Columns["Number"].ColumnName = "ID";
                    dt.Columns["FileName"].ColumnName = "ファイル名";
                    dt.Columns["FullPath"].ColumnName = "ファイルパス";

                    string filename = "";
                    if (checkBox3.Checked)
                    {
                        filename = textBox2.Text+".csv";
                    }
                    else
                    {
                        int lastMoneyIndex = textBox1.Text.LastIndexOf("\\");
                        filename = textBox1.Text.Substring(lastMoneyIndex + 1) + ".csv";
                        filename = Regex.Replace(filename, ".ymmp", DateTime.Now.ToString("yyyyMMdd"));
                    }
                    MyLib.CSV.CSVWrite(dt, csvFileName + "\\" + filename, true);
                    MessageBox.Show("出力完了");

                }

                //string ymmpFileName = label1.Text.Substring(7,label1.Text.Length-7);

                //OpenFileDialog fileDialog = new OpenFileDialog();
                //fileDialog.Filter = "CSVファイル(*.csv)|*.csv";
                //using (StreamReader sr = new StreamReader(ymmpFileName+".csv", Encoding.GetEncoding("Shift_JIS")))
                //{
                //    string defaultPath = sr.ReadLine();

                //    fileDialog.InitialDirectory = @"C:\";
                //    fileDialog.Title = "CSVファイルの情報を設定してください";

                //    if (fileDialog.ShowDialog() == DialogResult.OK)
                //    {
                //        CSV.CreateCSVFile()
                //        MyLib.CSV.CSVWrite(dt, fileDialog.FileName);
                //    }
                //}


            };

            checkBox3.CheckedChanged += (_sender, _e) =>
            {
                if (checkBox3.Checked)
                {
                    panel1.Enabled = true;
                }
                else 
                {
                    panel1.Enabled = false;
                }
            };

            textBox1.TextChanged += (_sender, _e) => 
            {
                if (textBox1.Text == "") 
                {
                    button2.Enabled = false;
                    button3.Enabled = false;
                }
                else 
                {
                    button2.Enabled = true;
                    button3.Enabled = true;
                }
            };

            button3.Click += (_sender, _e) =>
             {
                  OpenFileDialog fileDialog = new OpenFileDialog();
                  fileDialog.Filter = "Forder|.";
                  fileDialog.Title = "Excelファイルの出力先を選択";
                  fileDialog.FileName = "SelectFolder";
                  string excelFileName = "";
                  fileDialog.CheckFileExists = false;
                  if (fileDialog.ShowDialog() == DialogResult.OK)
                  {
                    excelFileName= Path.GetDirectoryName(fileDialog.FileName);
                     DataTable dt = new DataTable();
                     dt.Columns.Add("Number");
                     dt.Columns.Add("FileName");
                     dt.Columns.Add("FullPath");
                     int count = 0;
                     for (int row = 0; row < dataGridView1.Rows.Count - 1; row++)
                     {
                         DataRow dr = dt.NewRow();
                         for (int col = 0; col < dataGridView1.Columns.Count; col++)
                         {
                             dr[col] = dataGridView1.Rows[row].Cells[col].Value;
                         }
                         dt.Rows.Add(dr);
                         count++;
                     }

                     dt.Columns["Number"].ColumnName = "ID";
                     dt.Columns["FileName"].ColumnName = "ファイル名";
                     dt.Columns["FullPath"].ColumnName = "ファイルパス";

                     string filename = "";
                     if (checkBox3.Checked)
                     {
                         filename = textBox2.Text + ".xlsx";
                     }
                     else
                     {
                         int lastMoneyIndex = textBox1.Text.LastIndexOf("\\");
                         filename = textBox1.Text.Substring(lastMoneyIndex + 1) + ".xlsx";
                         filename = Regex.Replace(filename, ".ymmp", DateTime.Now.ToString("yyyyMMdd"));
                     }
                     var book = new XLWorkbook();
                     var sheet = book.Worksheets.Add("Sheet1");
                     sheet.Cell(2, 2).Value = "id";
                     sheet.Cell(2, 3).Value = "ファイル名";
                     sheet.Cell(2, 4).Value = "ファイルパス";
                     sheet.Range("B2:D2").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                     sheet.Range("B2:D2").Style.Border.InsideBorder= XLBorderStyleValues.Thick;
                     sheet.Range("B2:D2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                     sheet.Column(3).Width = 50;
                     sheet.Column(4).Width = 100;


                     DialogResult isKotei = MessageBox.Show("エクセルシートの、「ウインドウの固定」を行いますか?", "ウインドウ固定", MessageBoxButtons.YesNo);
                     if (isKotei == DialogResult.Yes) 
                     {
                         sheet.SheetView.FreezeRows(2);
                     }

                     int rowcount = 3;
                     foreach(DataRow dr in dt.Rows) 
                     {
                         sheet.Cell(rowcount, 2).Value = dr["ID"].ToString();
                         sheet.Cell(rowcount, 3).Value = dr["ファイル名"].ToString();
                         sheet.Cell(rowcount, 4).Value = dr["ファイルパス"].ToString();
                         rowcount++;
                     }

                     if (File.Exists(excelFileName + "\\" + filename))
                     {
                         DialogResult goDelete = MessageBox.Show("同名のファイルが存在します。ごみ箱に移動してよろしいですか？", "既存のファイル名", MessageBoxButtons.YesNo);
                        if(goDelete == DialogResult.Yes)
                         {
                             string filePath = @"c:\tmp\gomi.bin";
                             FileSystem.DeleteFile(
                             excelFileName + "\\" + filename,
                             UIOption.OnlyErrorDialogs,
                             RecycleOption.SendToRecycleBin);
                             book.SaveAs(excelFileName + "\\" + filename);
                         }
                     }
                     else 
                     {
                         book.SaveAs(excelFileName + "\\" + filename);
                     }
                 }
             };
        }

        private DataTable getYMMPInner(string FilePath,bool isAdd=false) 
        {
            DataTable answerDt = new DataTable();
            answerDt.Columns.Add("Number");
            answerDt.Columns.Add("FileName");
            answerDt.Columns.Add("FullPath");
            int count = 0;
            if (isAdd)
            {
               for(int row = 0; row < dataGridView1.Rows.Count - 1; row++) 
                {
                    DataRow dr = answerDt.NewRow();
                    for(int col = 0; col < dataGridView1.Columns.Count; col++) 
                    {
                        dr[col] = dataGridView1.Rows[row].Cells[col].Value;
                    }
                    answerDt.Rows.Add(dr);
                    count++;
                }
            }
            File.Copy(FilePath, "target.xml", true);

            XDocument doc = XDocument.Load("target.xml");
            doc.Element("YukkuriMovieMakerProject").Attributes().Where(e=>e.IsNamespaceDeclaration).Remove();
            doc.Save("target.xml");
            doc = XDocument.Load("target.xml");
            XElement YMMP = doc.Element("YukkuriMovieMakerProject");
            XElement TimeLine = YMMP.Element("TimelineItems");
            IEnumerable<XElement> Commons = TimeLine.Elements("CommonItem");

            int gotItemsCount = count;
            if (checkBox1.Checked)
            {
                foreach (XElement Common in Commons)
                {
                    XElement PathEle = Common.Element("FilePath");
                    if (PathEle != null)
                    {

                        if (!answerDt.AsEnumerable().Any(row => PathEle.Value.ToString() == row.Field<string>("FullPath").ToString()))
                        {

                            gotItemsCount++;
                            string FullPath = PathEle.Value.ToString();
                            int finalMoneyMark = FullPath.LastIndexOf("\\");
                            string FileName = FullPath.Substring(finalMoneyMark + 1);
                            var newRow = answerDt.NewRow();
                            newRow["Number"] = gotItemsCount;
                            newRow["FileName"] = FileName;
                            newRow["FullPath"] = FullPath;
                            answerDt.Rows.Add(newRow);
                        }
                    }
                }
            }
            else
            {
                foreach (XElement Common in Commons)
                {
                    XElement PathEle = Common.Element("FilePath");

                    if (PathEle != null)
                    {
                        gotItemsCount++;
                        string FullPath = PathEle.Value.ToString();
                        int finalMoneyMark = FullPath.LastIndexOf("\\");
                        string FileName = FullPath.Substring(finalMoneyMark + 1);
                        var newRow = answerDt.NewRow();
                        newRow["Number"] = gotItemsCount;
                        newRow["FileName"] = FileName;
                        newRow["FullPath"] = FullPath;
                        answerDt.Rows.Add(newRow);
                    }
                }
            }
            return answerDt;

        }

        private DataTable sortByKakutyosi(DataTable dt) 
        {
            DataTable answerDT = new DataTable();
            var kakutyosiList = new List<string>() { "png", "jpeg", "jpg", "mp3", "wav", "mp4" };
            var encountKakutyosiList = new List<string>();
            foreach(DataRow dr in dt.Rows) 
            {
                string fileName = dr["FileName"].ToString();
                int dotIndex = fileName.LastIndexOf('.');
                string kakutyosi = fileName.Substring(dotIndex + 1);
                if (!encountKakutyosiList.Any(a => a == kakutyosi)) 
                {
                    encountKakutyosiList.Add(kakutyosi);
                }
            }

            var jagRows = new List<DataTable>();
            foreach(string oneKakutyosi in encountKakutyosiList) 
            {
                var oneRows = dt.AsEnumerable().Where(row => row["FileName"].ToString().Contains(oneKakutyosi));
                jagRows.Add(oneRows.CopyToDataTable());
            }

            foreach(string oneKakutyosi in kakutyosiList) 
            {
                DataTable target=new DataTable();
                bool isDiscovery = false;
                foreach(DataTable onedt in jagRows) 
                {
                    if (onedt.AsEnumerable().Where(row => row["FileName"].ToString().Contains(oneKakutyosi)).Count()>0) 
                    {
                        target = onedt;
                        isDiscovery = true;
                    }
                }
                if (isDiscovery)
                {
                    target.AcceptChanges();
                    answerDT.Merge(target);
                }
            }

            var madaList = new List<string>();
            foreach(string oneKakutyosi in kakutyosiList) 
            {
                if (kakutyosiList.Where(s => s == oneKakutyosi).Count() == 0) 
                {
                    madaList.Add(oneKakutyosi);
                }
            }

            foreach (string oneKakutyosi in madaList)
            {
                DataTable target = new DataTable();
                bool isDiscovery = false;
                foreach (DataTable onedt in jagRows)
                {
                    if (onedt.AsEnumerable().Where(row => row["FileName"].ToString().Contains(oneKakutyosi)).Count() > 0)
                    {
                        target = onedt;
                        isDiscovery = true;
                    }
                }
                if (isDiscovery)
                {
                    target.AcceptChanges();
                    answerDT.Merge(target);
                }
            }


            return answerDT;
        }

        private void OpenFile(int columnIndex, int rowindex) 
        {

            switch (columnIndex) 
            {
                case 1:
                    //ファイル名をダブルクリック

                    string fullpath = dataGridView1[columnIndex + 1, rowindex].Value.ToString();
                    if (File.Exists(fullpath)) 
                    {
                        System.Diagnostics.Process.Start(fullpath);
                    }
                    else 
                    {
                        MessageBox.Show("error:ファイルが削除されたか、移動されました");
                    }


                    break;

                case 2:
                    //フォルダパスをダブルクリック
                    fullpath=dataGridView1[columnIndex , rowindex].Value.ToString();
                    int nameCharCount = dataGridView1[columnIndex - 1, rowindex].Value.ToString().Length;
                    string folderPath = fullpath.Remove(fullpath.Length - nameCharCount, nameCharCount);
                    System.Diagnostics.Process.Start(folderPath.TrimEnd('\\'));
                    break;

                default: break;
            }
      
        }

        private void 既定のフォルダを指定ToolStripMenuItem_Click(object sender, EventArgs e)
        {

                OpenFileDialog select = new OpenFileDialog();
                select.Title = "既定のフォルダを選択";
                select.InitialDirectory = @"C:\";
                select.FileName = "--入力不可--";
                select.Filter = "Folder|.";
                select.CheckFileExists = false;
                if (select.ShowDialog() == DialogResult.OK)
                {
                
                    var Jis = Encoding.GetEncoding("Shift_JIS");
                    using (StreamWriter writer = new StreamWriter("default.txt", false, Jis))
                    {
                      writer.WriteLine(Path.GetDirectoryName(select.FileName));
                    }
                }
                select.Dispose();

        }

      
    }
}

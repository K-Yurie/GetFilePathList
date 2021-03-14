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
        #region グローバル変数
        /// <summary>
        /// YMMPファイル：素材ファイルをあらわさないエレメントの名前です
        /// </summary>
        List<string> _throughElements = new List<string>() { "YukkuriMovieMakerProject", "TimelineItems", "CommonItem" };

        /// <summary>
        /// YMMPファイル：素材ファイルをあらわすエレメントの名前です
        /// </summary>
        List<string> _targetElements = new List<string>() { "FilePath" };

        /// <summary>
        /// プログラム利用中PCのユーザー名です
        /// </summary>
        string user = Environment.UserName;

        /// <summary>
        /// プログラム用データフォルダのパスです
        /// </summary>
        string appDataPath = "";

        /// <summary>
        /// OpenFileDialog起動時に開くフォルダのパスです
        /// </summary>
        string defaultTextPath = "";
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタです
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }
        #endregion

        #region ロードイベント
        /// <summary>
        /// ロードイベントです
        /// </summary>
        /// <param name="sender">イベント発生オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            FormEvents();
            btnOutputCSVFile.Enabled = false;
            btnOutputExcelFile.Enabled = false;

            //データフォルダ・default.txtのパスを設定
            appDataPath = "C:\\Users\\" + user + "\\AppData\\Local\\GetFilePathList";
            defaultTextPath = appDataPath + "\\default.txt";

            //プログラム用データフォルダを作成
            if (!Directory.Exists(appDataPath)) 
            {
                Directory.CreateDirectory(appDataPath);
            }

            //以下のファイルをデータフォルダに作成
            //①素材情報取得用xmlファイル(指定YMMPファイルのコピー先)
            //②OpenFileDialog起動時に開く、フォルダの情報を記述したtxtファイル
            appDataPath = "C:\\Users\\" + user + "\\AppData\\Local\\GetFilePathList"+"\\target.xml";
            if (!File.Exists(appDataPath)) 
            {
                System.IO.File.Copy(@"target.xml", appDataPath);
            }
            if (!File.Exists(defaultTextPath)) 
            {
                System.IO.File.Copy(@"default.txt", defaultTextPath);
            }
        }
        #endregion

        #region イベント
        /// <summary>
        /// イベント定義・実装をします
        /// </summary>
        private void FormEvents() 
        {
            #region ファイル選択ボタン押下時
            ///<summary>
            ///btnSortFileのクリックイベントです
            ///YMMPまたはexoファイルを選択するイベントです
            ///</summary>
            btnSortFile.Click += (_sender, _e) => 
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "対応ファイル(*.ymmp;*.exo)|*.ymmp;*.exo";
                using (StreamReader sr = new StreamReader(defaultTextPath, Encoding.GetEncoding("Shift_JIS")))
                {
                    //default.txtにパス情報の記述があれば、そこを開く
                    string defaultPath = sr.ReadLine();
                    if (defaultPath == "")
                    {
                        fileDialog.InitialDirectory = @"C:\";
                    }
                    else
                    {
                        fileDialog.InitialDirectory = defaultPath;

                    }
                    fileDialog.Title = "ファイルを指定してください";

                    //選択されたファイルのパスをtbSelectedPathに格納
                    //また、ファイルのパスからファイル名部分をとりだして、lblSelectedFolderNamemに表示
                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        tbSelectedPath.Text = fileDialog.FileName;
                        int LastMoneyMark = tbSelectedPath.Text.LastIndexOf("\\");
                        if (LastMoneyMark > 0)
                        {
                            string fileName = tbSelectedPath.Text.Substring(LastMoneyMark + 1);
                            lblSelectedFolderName.Text = "ファイル名：" + fileName;
                        }
                    }
                }
            };
            #endregion

            #region 新規抽出ボタン押下時
            ///<summary>
            ///btnSelectNewFileのクリックイベントです
            ///指定されたファイルから、素材情報取得して、dataGridViewに表示します
            ///</summary>
            btnSelectNewFile.Click += (_sender, _e) => 
            {
                if (string.IsNullOrEmpty(tbSelectedPath.Text))
                {
                    MessageBox.Show("ファイルを指定してください");
                }
                else
                {
                    if (chkIsSortByExtension.Checked) 
                    {
                        if (tbSelectedPath.Text.Substring(tbSelectedPath.Text.IndexOf('.')) == ".exo")
                        {
                            dataGridView1.DataSource = SortByKakutyosi(GetExoInnner(tbSelectedPath.Text));
                        }
                        else
                        {
                            dataGridView1.DataSource = SortByKakutyosi(GetYMMPInner(tbSelectedPath.Text));
                        }
                    }
                    else 
                    {
                        if (tbSelectedPath.Text.Substring(tbSelectedPath.Text.IndexOf('.')) == ".exo")
                        {
                            dataGridView1.DataSource = GetExoInnner(tbSelectedPath.Text);
                        }
                        else
                        {
                            dataGridView1.DataSource = GetYMMPInner(tbSelectedPath.Text);

                        }
                    }
                }
            };
            #endregion

            #region 追加で抽出ボタン押下時
            ///<summary>
            ///btnSelectAddFileのクリックイベントです
            ///指定されたファイルから、素材情報を取得して、現在ataGridViewに表示されているデータに追記表示します
            ///</summary>
            btnSelectAddFile.Click += (_sender, _e) => 
            {
                if (string.IsNullOrEmpty(tbSelectedPath.Text))
                {
                    MessageBox.Show("ファイルを指定してください");
                }
                else
                {
                    if (chkIsSortByExtension.Checked)
                    {
                        if (tbSelectedPath.Text.Substring(tbSelectedPath.Text.IndexOf('.')) == ".exo")
                        {
                            dataGridView1.DataSource = SortByKakutyosi(GetExoInnner(tbSelectedPath.Text, true));
                        }
                        else
                        {
                            dataGridView1.DataSource = SortByKakutyosi(GetYMMPInner(tbSelectedPath.Text, true));
                        }
                    }
                    else
                    {
                        if (tbSelectedPath.Text.Substring(tbSelectedPath.Text.IndexOf('.')) == ".exo")
                        {
                            dataGridView1.DataSource = GetExoInnner(tbSelectedPath.Text, true);
                        }
                        else
                        {
                            dataGridView1.DataSource = GetYMMPInner(tbSelectedPath.Text, true);
                        }
                    }
                }
            };
            #endregion

            #region DataGridViewダブルクリック時
            ///<summary>
            ///DatagridView1ダブルクリック時の処理です
            ///素材パス部分をダブルクリック：素材の存在するフォルダを開きます
            ///素材ファイル名部分をダブルクリック：素材ファイルをそれぞれの既定のアプリで開きます
            ///</summary>
            dataGridView1.CellDoubleClick += (_sender, _e) => 
            {

                if (_e.RowIndex>-1&&dataGridView1[_e.ColumnIndex, _e.RowIndex].Value!=null)
                {
                    OpenFile(_e.ColumnIndex,_e.RowIndex);
                }
            
            };
            #endregion

            #region CSV出力ボタン押下時
            ///<summary>
            ///btnOutputCSVFileのクリックイベントです
            ///DataGridViewに表示されている情報を、CSVファイルとして出力します
            /// </summary>
            btnOutputCSVFile.Click += (_sender, _e) =>
            {
                //CSVファイル出力先を指定
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "Forder|.";
                fileDialog.Title = "CSVファイルの出力先を選択";
                fileDialog.FileName = "SelectFolder";
                string csvFileName = "";
                fileDialog.CheckFileExists = false;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    csvFileName = Path.GetDirectoryName(fileDialog.FileName);
                    //DataGridViewの表示内容をDataTableに変換
                    DataTable dt = CreateGridDataTable(dataGridView1);

                    //出力ファイルの名前を設定
                    string filename = "";
                    if (chkIsNamedByUser.Checked)
                    {
                        filename = tbOutputFileName.Text+".csv";
                    }
                    else
                    {
                        int lastMoneyIndex = tbSelectedPath.Text.LastIndexOf("\\");
                        filename = tbSelectedPath.Text.Substring(lastMoneyIndex + 1) + ".csv";
                        filename = Regex.Replace(filename, ".ymmp", DateTime.Now.ToString("yyyyMMdd"));
                    }

                    //指定の場所にCSVファイルを出力
                    MyLib.CSV.CSVWrite(dt, csvFileName + "\\" + filename, true);
                    MessageBox.Show("出力完了");
                }
            };
            #endregion

            #region ファイル名指定用チェックボックス チェック変更時
            ///<summary>
            ///chkIsNamedByUserのCheckedChangedイベントです
            ///出力ファイル名指定用パネルの操作を可能/不可能にします
            /// </summary>
            chkIsNamedByUser.CheckedChanged += (_sender, _e) =>
            {
                if (chkIsNamedByUser.Checked)
                {
                    pnlNamedByUser.Enabled = true;
                }
                else 
                {
                    pnlNamedByUser.Enabled = false;
                }
            };
            #endregion

            #region 指定YMMP・EXOファイルパス表示用テキストボックス 入力変更時
            ///<summary>
            ///tbSelectedPathのTextChangedイベントです
            /// テキストボックスのテキストがブランクなら、出力用ボタンを操作不能にします
            /// </summary>
            tbSelectedPath.TextChanged += (_sender, _e) => 
            {
                if (tbSelectedPath.Text == "") 
                {
                    btnOutputCSVFile.Enabled = false;
                    btnOutputExcelFile.Enabled = false;
                }
                else 
                {
                    btnOutputCSVFile.Enabled = true;
                    btnOutputExcelFile.Enabled = true;
                }
            };
            #endregion

            #region Excelファイル出力ボタン押下時
            ///<summary>
            ///btnOutputExcelFileのクリックイベントです 
            ///DataGridViewに表示されている情報を、EXCELファイル(.xlsx)として出力します
            /// </summary>
            btnOutputExcelFile.Click += (_sender, _e) =>
             {
                 //Excelファイルの出力先を指定
                  OpenFileDialog fileDialog = new OpenFileDialog();
                  fileDialog.Filter = "Forder|.";
                  fileDialog.Title = "Excelファイルの出力先を選択";
                  fileDialog.FileName = "SelectFolder";
                  string excelFilePath = "";
                  fileDialog.CheckFileExists = false;

                 if (fileDialog.ShowDialog() == DialogResult.OK)
                 {
                     excelFilePath = Path.GetDirectoryName(fileDialog.FileName);
                     //DataGridViewの表示内容をDataTableに変換
                     DataTable dt = CreateGridDataTable(dataGridView1);

                     //出力ファイルの名前を設定
                     string filename = "";
                     if (chkIsNamedByUser.Checked)
                     {
                         filename = tbOutputFileName.Text + ".xlsx";
                     }
                     else
                     {
                         int lastMoneyIndex = tbSelectedPath.Text.LastIndexOf("\\");
                         filename = tbSelectedPath.Text.Substring(lastMoneyIndex + 1) + ".xlsx";
                         filename = Regex.Replace(filename, ".ymmp", DateTime.Now.ToString("yyyyMMdd"));
                     }
                     //指定の場所にExcelファイルを出力
                     OutputExcelFile(dt, excelFilePath, filename);
                 }
             };
        }
        #endregion

        #endregion

        #region ローカルメソッド

        #region YMMPファイル内素材情報の取得
        /// <summary>
        /// 指定のYMMPファイルから、素材情報を取得して、DataTableとして返します
        /// </summary>
        /// <param name="FilePath">指定のYMMPファイルのパス</param>
        /// <param name="isAdd">true:現在DataGridViewに表示されているデータに追記します /
        /// 　　　　　　　　　　false:指定のYMMPファイルの情報だけを表示します</param>
        /// <returns>DataGridView表示データを格納したDataTable</returns>
        private DataTable GetYMMPInner(string FilePath,bool isAdd=false) 
        {
            //戻り値用のDataTableを定義する
            DataTable answerDt = new DataTable();
            answerDt.Columns.Add("Number");
            answerDt.Columns.Add("FileName");
            answerDt.Columns.Add("FullPath");
            int count = 0;

            //追記モードなら、DataGridView表示中のデータを、戻り値用DataTableにあらかじめ格納する
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

            //YMMPファイルをtarget.xmlにコピーして、素材情報を取得する
            File.Copy(FilePath, appDataPath, true);
            XDocument doc = XDocument.Load(appDataPath);
            doc.Element("YukkuriMovieMakerProject").Attributes().Where(e=>e.IsNamespaceDeclaration).Remove();
            doc.Save(appDataPath);
            doc = XDocument.Load(appDataPath);
            XElement YMMP = doc.Element("YukkuriMovieMakerProject");
            XElement TimeLine = YMMP.Element("TimelineItems");
            IEnumerable<XElement> Commons = TimeLine.Elements("CommonItem");

            int gotItemsCount = count;
            if (chkFirstEncountOnly.Checked)
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
        #endregion

        #region EXOファイル内素材情報の取得
        /// <summary>
        /// 指定のEXOファイルから、素材情報を取得して、DataTableとして返します
        /// </summary>
        /// <param name="FilePath">指定のEXOファイルのパス</param>
        /// <param name="isAdd">true:現在DataGridViewに表示されているデータに追記します /
        /// 　　　　　　　　　　false:指定のYMMPファイルの情報だけを表示します</param>
        /// <returns>DataGridView表示データを格納したDataTable</returns>
        private DataTable GetExoInnner(string FilePath, bool isAdd = false)
        {
            //戻り値用DataTableを定義する
            DataTable answerDt = new DataTable();
            answerDt.Columns.Add("Number");
            answerDt.Columns.Add("FileName");
            answerDt.Columns.Add("FullPath");

            //追記モードなら、DataGridView表示中のデータを、戻り値用DataTableにあらかじめ格納する
            int count = 0;
            if (isAdd)
            {
                for (int row = 0; row < dataGridView1.Rows.Count - 1; row++)
                {
                    DataRow dr = answerDt.NewRow();
                    for (int col = 0; col < dataGridView1.Columns.Count; col++)
                    {
                        dr[col] = dataGridView1.Rows[row].Cells[col].Value;
                    }
                    answerDt.Rows.Add(dr);
                    count++;
                }
            }

            //指定EXOファイル内のテキストを、1行ずつ、List<string>として格納
            var fileTexts = new List<string>();
            string line = "";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(FilePath, Encoding.GetEncoding("SHIFT-JIS")))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    fileTexts.Add(line);
                }
            }

            //上のListから、ファイルパスを表している行だけを抽出し整形
            var sozaiPathes = fileTexts.Where(x => x.Length > 5).Where(x => x.Substring(0, 5) == "file=").ToList();
            int lastMoney = FilePath.LastIndexOf('\\');
            lastMoney = lastMoney + 1;
            string exoName = FilePath.Substring(lastMoney, FilePath.Length - lastMoney);
            string filePath = FilePath.Substring(0, lastMoney);
            exoName.Replace("\\", "");
            exoName = exoName.Replace(".exo", "");
            exoName = exoName.Replace(".EXO", "");
            sozaiPathes = sozaiPathes.Where(x => !x.Contains(filePath + exoName)).ToList();
            sozaiPathes = sozaiPathes.Select(x => x.Replace("file=", "")).ToList();

            //抽出した素材情報を戻り値用Dtに追加
            int gotItemsCount = 1;
            if (chkFirstEncountOnly.Checked)
            {
                foreach (string target in sozaiPathes)
                {
                    if (!string.IsNullOrEmpty(target))
                    {
                        if (!answerDt.AsEnumerable().Any(row => target == row.Field<string>("FullPath").ToString()))
                        {
                            var newRow = answerDt.NewRow();
                            newRow["Number"] = gotItemsCount;
                            int lastMoney1 = target.LastIndexOf('\\');
                            lastMoney1 = lastMoney1 + 1;
                            newRow["FileName"] = target.Substring(lastMoney1, target.Length - lastMoney1);
                            newRow["FullPath"] = target;
                            answerDt.Rows.Add(newRow);
                            gotItemsCount++;
                        }
                    }
                }
            }
            else
            {
                foreach (string target in sozaiPathes)
                {
                    if (!string.IsNullOrEmpty(target))
                    {
                        var newRow = answerDt.NewRow();
                        newRow["Number"] = gotItemsCount;
                        int lastMoney1 = target.LastIndexOf('\\');
                        lastMoney1 = lastMoney1 + 1;
                        newRow["FileName"] = target.Substring(lastMoney1, target.Length - lastMoney1);
                        newRow["FullPath"] = target;
                        answerDt.Rows.Add(newRow);
                    }
                    gotItemsCount++;
                }
            }
            return answerDt;
        }
        #endregion

        #region ファイルを開く
        /// <summary>
        /// DataGridView1の列index,行indexをもとに、フォルダまたはファイルを開く
        /// </summary>
        /// <param name="columnIndex">DataGridView1の列index</param>
        /// <param name="rowindex">DataGridView1の行index</param>
        private void OpenFile(int columnIndex, int rowindex)
        {

            switch (columnIndex)
            {
                case 1:
                    //ファイル名をダブルクリックした場合
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
                    //フォルダパスをダブルクリックした場合
                    fullpath = dataGridView1[columnIndex, rowindex].Value.ToString();
                    int nameCharCount = dataGridView1[columnIndex - 1, rowindex].Value.ToString().Length;
                    string folderPath = fullpath.Remove(fullpath.Length - nameCharCount, nameCharCount);
                    if (Directory.Exists(folderPath.TrimEnd('\\')))
                    {
                        System.Diagnostics.Process.Start(folderPath.TrimEnd('\\'));
                    }
                    else
                    {
                        MessageBox.Show("error:フォルダが削除されたか、移動されました");
                    }
                    break;

                default: break;
            }

        }
        #endregion

        #region 拡張子順に並べ替え
        /// <summary>
        /// 素材情報を素材のタイプ・拡張子にしたがって並べ替えます
        /// </summary>
        /// <param name="dt">素材情報を格納したDataTable</param>
        /// <returns>並べ替え結果を格納したDataTable</returns>
        private DataTable SortByKakutyosi(DataTable dt) 
        {
            DataTable answerDT = new DataTable();
            var kakutyosiList = new List<string>() { "png", "jpeg", "jpg", "mp3", "wav", "mp4" };
            var encountKakutyosiList = new List<string>();

            //各行の拡張子をkakutyosiListに取得
            foreach (DataRow dr in dt.Rows) 
            {
                string fileName = dr["FileName"].ToString();
                int dotIndex = fileName.LastIndexOf('.');
                string kakutyosi = fileName.Substring(dotIndex + 1);
                if (!encountKakutyosiList.Any(a => a == kakutyosi)) 
                {
                    encountKakutyosiList.Add(kakutyosi);
                }
            }

            //拡張子ごとにDataTableを分割
            var tableList = new List<DataTable>();
            foreach(string oneKakutyosi in encountKakutyosiList) 
            {
                var oneRows = dt.AsEnumerable().Where(row => row["FileName"].ToString().Contains(oneKakutyosi));
                tableList.Add(oneRows.CopyToDataTable());
            }

            //拡張子別のDataTableを、kakutyosiListへの出現順に並べ替え結合する
            foreach (string oneKakutyosi in kakutyosiList) 
            {
                DataTable target=new DataTable();
                bool isDiscovery = false;
                foreach(DataTable onedt in tableList) 
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

            //kakutyosiListに存在しなかった拡張子をList<string>として取得
            var madaList = new List<string>();
            foreach(string oneKakutyosi in kakutyosiList) 
            {
                if (kakutyosiList.Where(s => s == oneKakutyosi).Count() == 0) 
                {
                    madaList.Add(oneKakutyosi);
                }
            }

            //kakutyosiListに存在しなかった拡張子のDataTableを、末尾に結合
            foreach (string oneKakutyosi in madaList)
            {
                DataTable target = new DataTable();
                bool isDiscovery = false;
                foreach (DataTable onedt in tableList)
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

            //DataTableの列 Numberに値を、並べ替えた順番に対応するように修正する
            int count = 1;
            foreach(DataRow dr in answerDT.Rows) 
            {
                dr["Number"] = count.ToString();
                count++;
            }
            return answerDT;
        }
        #endregion

        #region DataGridViewのDataTable変換
        /// <summary>
        /// DataGridViewの表示内容をDataTableに変換
        /// </summary>
        /// <param name="targetDataGridView">指定のDataGridView</param>
        /// <returns>変換結果のDataTable</returns>
        private DataTable CreateGridDataTable(DataGridView targetDataGridView) 
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Number");
            dt.Columns.Add("FileName");
            dt.Columns.Add("FullPath");
            int count = 0;
            for (int row = 0; row < dataGridView1.Rows.Count - 1; row++)
            {
                DataRow dr = dt.NewRow();
                for (int col = 0; col < targetDataGridView.Columns.Count; col++)
                {
                    dr[col] = targetDataGridView.Rows[row].Cells[col].Value;
                }
                dt.Rows.Add(dr);
                count++;
            }
            dt.Columns["Number"].ColumnName = "ID";
            dt.Columns["FileName"].ColumnName = "ファイル名";
            dt.Columns["FullPath"].ColumnName = "ファイルパス";
            return dt;
        }
        #endregion]

        #region EXCELファイルを出力
        /// <summary>
        /// 指定の条件でExcelファイルを出力します
        /// </summary>
        /// <param name="gridTable">DataGridView表示情報を格納したDataTable</param>
        /// <param name="excelFilePath">Excelファイル出力先のディレクトリパス</param>
        /// <param name="filename">Excelファイルの名前</param>
        private void OutputExcelFile(DataTable gridTable,string excelFilePath,string filename) 
        {
            try
            {
                //Excelブック・シートを定義
                var book = new XLWorkbook();
                var sheet = book.Worksheets.Add("Sheet1");
                sheet.Cell(2, 2).Value = "id";
                sheet.Cell(2, 3).Value = "ファイル名";
                sheet.Cell(2, 4).Value = "ファイルパス";
                sheet.Range("B2:D2").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                sheet.Range("B2:D2").Style.Border.InsideBorder = XLBorderStyleValues.Thick;
                sheet.Range("B2:D2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Column(3).Width = 50;
                sheet.Column(4).Width = 100;
                DialogResult isKotei = MessageBox.Show("エクセルシートの、「ウインドウの固定」を行いますか?", "ウインドウ固定", MessageBoxButtons.YesNo);
                if (isKotei == DialogResult.Yes)
                {
                    sheet.SheetView.FreezeRows(2);
                }

                //定義したExcelブック・シートに、gridTableの情報を記述
                int rowcount = 3;
                foreach (DataRow dr in gridTable.Rows)
                {
                    sheet.Cell(rowcount, 2).Value = dr["ID"].ToString();
                    sheet.Cell(rowcount, 3).Value = dr["ファイル名"].ToString();
                    sheet.Cell(rowcount, 4).Value = dr["ファイルパス"].ToString();
                    rowcount++;
                }

                //データ記述済みのExcelブックを、指定の場所に保存
                if (File.Exists(excelFilePath + "\\" + filename))
                {
                    DialogResult goDelete = MessageBox.Show("同名のファイルが存在します。ごみ箱に移動してよろしいですか？", "既存のファイル名", MessageBoxButtons.YesNo);
                    if (goDelete == DialogResult.Yes)
                    {
                        FileSystem.DeleteFile(
                        excelFilePath + "\\" + filename,
                        UIOption.OnlyErrorDialogs,
                        RecycleOption.SendToRecycleBin);
                        book.SaveAs(excelFilePath + "\\" + filename);
                        MessageBox.Show("出力完了");
                    }
                }
                else
                {
                    book.SaveAs(excelFilePath + "\\" + filename);
                    MessageBox.Show("出力完了");
                }
            }
            catch (Exception) 
            {
                MessageBox.Show("申し訳ございません。出力エラーが発生しました");
            }
        }
        #endregion

        #endregion

        #region 上部メニューの操作
        /// <summary>
        /// ToolStripMenu "既定のフォルダを指定"を選択時
        /// </summary>
        /// <param name="sender">イベントを発生させたオブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void 既定のフォルダを指定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
                //OpenFileDialogの既定フォルダを設定
                OpenFileDialog select = new OpenFileDialog();
                select.Title = "既定のフォルダを選択";
                select.InitialDirectory = @"C:\";
                select.FileName = "--入力不可--";
                select.Filter = "Folder|.";
                select.CheckFileExists = false;
                if (select.ShowDialog() == DialogResult.OK)
                {
                
                    var Jis = Encoding.GetEncoding("Shift_JIS");
                    using (StreamWriter writer = new StreamWriter(defaultTextPath, false, Jis))
                    {
                      writer.WriteLine(Path.GetDirectoryName(select.FileName));
                    }
                }
                select.Dispose();

        }
        #endregion
    }
}

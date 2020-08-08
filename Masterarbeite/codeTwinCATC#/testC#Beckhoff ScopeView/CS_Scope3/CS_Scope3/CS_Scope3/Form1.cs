using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TwinCAT.Ads;
using TwinCAT.Scope2.Communications;
using TwinCAT.Scope2.View.ScopeViewControlLib;
using System.IO;
using System.Collections;

namespace CS_Scope3
{
    public partial class Form1 : Form
    {
        //创建流量计句柄
        public TcAdsClient _client = null;
        public int _handBOOl = 0;




        //当前工程打开的路径，主要用于打开对话框和保存对话框
        private string ApplicationPath;
        //1打开文件对话框打开Project，2打开文件对话框打开Record
        private int iOpenDialogFrom = 0;
        private string OpenScopeTemplateName;
        private string OpenSaveDataFileName;
        //1保存文件对话框Project，2保存文件对话框Record
        private int iSaveDialogFrom = 0;
        private string SaveScopeFileName;

        //实例化三个子界面
        private TreeViewForm TreeViewFrm1 = new TreeViewForm();
        private PropertyForm PropertyFrm1 = new PropertyForm();
        private DataGridForm DataGridFrm1 = new DataGridForm();
        //如果是从Treeview选择切换的Chart，则不再反馈与ItemTreeView
        private bool bChartChangeFromSelect = false;

        public Form1()
        {
            InitializeComponent();
            //全局Scope控件实例
            GlobalVar.scopeViewControl1 = new TwinCAT.Scope2.View.ScopeViewControlLib.ScopeViewControl();
            GlobalVar.scopeViewControl1.ActiveChartChanged += new System.EventHandler<TwinCAT.Scope2.View.ScopeViewControlLib.
                                                        ActiveChartChangedEventArgs>(this.scopeViewControl1_ActiveChartChanged);
            //树形结构界面托管的节点选中消息
            TreeViewFrm1.TreeViewSelectSendTick += new System.EventHandler(this.TreeViewFrm1TreeViewSelect);
            //树形结构界面托管的Channel及Cursor改变消息
            TreeViewFrm1.TreeViewChannelCrusorChangeSendTick+= new System.EventHandler(this.TreeViewFrm1ChannelCursorChange);
        }



















        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                _client = new TcAdsClient();


                _client.Connect(851);
                _handBOOl = _client.CreateVariableHandle("MAIN.Temperatur");               //根据PLC中的名字读取值 

               


            }
            catch (Exception ex) { }





            GlobalVar.eventList_12 = new TwinCAT.Scope2.Tools.EventList<TwinCAT.Scope2.View.ScopeViewControlLib.ScopeViewControlChannel>();
            //设置Scope的基本属性
            scopeViewControl1Set();
            //添加Scope到Panel中
            this.panel6.Controls.Add(GlobalVar.scopeViewControl1);
            //初始化文件路径
            ApplicationPath = Application.StartupPath;
            //初始化节点选中数组
            for (int i=0;i<4;i++)
            {
                GlobalVar.TreeViewSelectIndexArray[i] = -1;
            }

            //========导入Cursor DataGridview界面
            RefreshDataGrid();


            }
       
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        //设置Scope的基本属性
        private void scopeViewControl1Set()
        {
            GlobalVar.scopeViewControl1.AllowDrop = true;
            GlobalVar.scopeViewControl1.BackColor = System.Drawing.SystemColors.ControlDark;
            GlobalVar.scopeViewControl1.ConnectedChannels = GlobalVar.eventList_12;
            GlobalVar.scopeViewControl1.IsWizardTemplate = false;
            GlobalVar.scopeViewControl1.Name = "scopeViewControl1";
            GlobalVar.scopeViewControl1.Size = new System.Drawing.Size(294, 217);
            GlobalVar.scopeViewControl1.SortPriority = -1;
            GlobalVar.scopeViewControl1.SuppressMessages = false;
            GlobalVar.scopeViewControl1.Title = "Scope 1";
            GlobalVar.scopeViewControl1.UnsafedChanges = true;
            GlobalVar.scopeViewControl1.UseGraphic = TwinCAT.Scope2.View.ScopeViewControlLib.GraphicLibrary.GDI_Plus;
            GlobalVar.scopeViewControl1.ViewDetailLevel = ScopeViewDetailLevel.Default;
            GlobalVar.scopeViewControl1.Dock = DockStyle.Fill;
        }

        //scopeViewControl1中 Chart改变时对应的树形表中节点选中，刷新datagrid数据
        private void scopeViewControl1_ActiveChartChanged(object sender, ActiveChartChangedEventArgs e)
        {
            if (e.Chart != null)
            {
                string str = e.Chart.Name;
                int index = 0;
                //判断当前选中的是哪一个Chart及Chart的类型
                for (int i = 0; i < GlobalVar.scopeViewControl1.Charts.Count; i++)
                {
                    if (str == GlobalVar.scopeViewControl1.Charts[i].Name)
                    {
                        index = i;
                        GlobalVar.ChartMode = GlobalVar.scopeViewControl1.Charts[i].ChartType;
                        break;
                    }
                }

                GlobalVar.TreeViewSelectIndexArray[1] = index;
                //刷新Datagrid的架构
                if (DataGridFrm1 != null && DataGridFrm1.Parent != null)
                {
                    DataGridFrm1.DataGridViewXYFrameFresh(index);
                }
                //选中对应的树形节点
                if (!bChartChangeFromSelect)
                {
                    TreeViewFrm1.TreeViewChartSelect(index);
                }
                bChartChangeFromSelect = false;
            }
        }

        // Menu Load Project
        private void loadProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iOpenDialogFrom = 1;
            openFileDialog1.Filter = "Measurement files (*.tcscope)|*.tcscope";
            openFileDialog1.FilterIndex = 0; //Index                  
            openFileDialog1.RestoreDirectory = true;//获取或设置一个值，该值使文件对话框将其当前目录还原为用户更改目录以搜索文件之前的初始值  
            openFileDialog1.Title = "Open File"; //获取或设置在文件对话框的标题栏中显示的文本   

            openFileDialog1.InitialDirectory = ApplicationPath + "\\TemplateFiles";
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
        }
        //Menu Load Record
        private void loadRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iOpenDialogFrom = 2;
            openFileDialog1.Filter = "Measurement files (*.svd)|*.svd";
            openFileDialog1.FilterIndex = 0; //Index                  
            openFileDialog1.RestoreDirectory = true;//获取或设置一个值，该值使文件对话框将其当前目录还原为用户更改目录以搜索文件之前的初始值  
            openFileDialog1.Title = "Open File"; //获取或设置在文件对话框的标题栏中显示的文本   

            openFileDialog1.InitialDirectory = ApplicationPath + "\\SaveDataFiles";
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
        }
        private bool FunLoadTemplate(ref ScopeViewControl SVC1, string FilePath)
        {
            bool returnVar = false;
            try
            {
                if (SVC1.State != ScopeViewControlStates.CONFIG)
                    SVC1.Operating.DisConnect(false);
                //删除当前所有得Chart
                while (SVC1.Charts.Count > 0)
                {
                    SVC1.DeleteChart(SVC1.Charts[0]);
                }
                //load configuration
                SVC1.LoadScopeConfig(FilePath);
                foreach (ScopeViewControlChannel channel in SVC1.ConnectedChannels)
                {
                    channel.Acquisition.AmsNetId = TwinCAT.Ads.AmsNetId.Local;

                }
                returnVar = true;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            return returnVar;
        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            //切换Scope运行状态
            if (GlobalVar.scopeViewControl1.State == ScopeViewControlStates.RECORD)
            {
                GlobalVar.scopeViewControl1.Operating.StopRecord();
            }
            if (GlobalVar.scopeViewControl1.State == ScopeViewControlStates.REPLY)
            {
                GlobalVar.scopeViewControl1.Operating.DisConnect(false);
            }

            bool LoadComplete = false;
            //删除所有得Chart
            while (GlobalVar.scopeViewControl1.Charts.Count > 0)
            {
                GlobalVar.scopeViewControl1.DeleteChart(GlobalVar.scopeViewControl1.Charts[0]);
            }
            //导入Project
            if (iOpenDialogFrom == 1)
            {
                OpenScopeTemplateName = openFileDialog1.FileName;
                LoadComplete = FunLoadTemplate(ref GlobalVar.scopeViewControl1, OpenScopeTemplateName);
            }
            //导入Record
            if (iOpenDialogFrom == 2)
            {
                OpenSaveDataFileName = openFileDialog1.FileName;
                LoadComplete = FunLoadTemplate(ref GlobalVar.scopeViewControl1, OpenSaveDataFileName);
            }
            if (LoadComplete)
            {
                //导入成功，刷新树形表，刷新DataGrid及部分参数
                GlobalVar.TreeViewSelectIndexArray[1] = GlobalVar.scopeViewControl1.Charts.Count - 1;
                 RefreshTreeView();
                TreeViewFrm1.TreeViewChartSelect(GlobalVar.TreeViewSelectIndexArray[1]);
                RefreshDataGrid();
                MessageBox.Show("Load Completed!");
            }
            else
            { MessageBox.Show("Load Failed!"); }
        }
       
        //Save Project
        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iSaveDialogFrom = 1;
            saveFileDialog1.Filter = "Measurement files (*.tcscope)|*.tcscope";
            saveFileDialog1.FilterIndex = 0; //索引                  
            saveFileDialog1.RestoreDirectory = true;//获取或设置一个值，该值使文件对话框将其当前目录还原为用户更改目录以搜索文件之前的初始值  
            saveFileDialog1.CreatePrompt = true;//获取或设置一个值，该值指示如果用户指定一个不存在的文件，SaveFileDialog 是否提示用户以允许创建文件  
            saveFileDialog1.Title = "Save  Config File"; //获取或设置在文件对话框的标题栏中显示的文本   

            saveFileDialog1.InitialDirectory = ApplicationPath + "\\TemplateFiles";
            saveFileDialog1.FileName = GlobalVar.scopeViewControl1.Title;

            saveFileDialog1.ShowDialog();
        }
        //Save Record
        private void saveRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iSaveDialogFrom = 2;
            saveFileDialog1.Filter = "Measurement files (*.svd)|*.svd";
            saveFileDialog1.FilterIndex = 0; //索引                  
            saveFileDialog1.RestoreDirectory = true;//获取或设置一个值，该值使文件对话框将其当前目录还原为用户更改目录以搜索文件之前的初始值  
            saveFileDialog1.CreatePrompt = true;//获取或设置一个值，该值指示如果用户指定一个不存在的文件，SaveFileDialog 是否提示用户以允许创建文件  
            saveFileDialog1.Title = "Save Record File"; //获取或设置在文件对话框的标题栏中显示的文本   

            saveFileDialog1.InitialDirectory = ApplicationPath + "\\SaveDataFiles";
            saveFileDialog1.FileName = GlobalVar.scopeViewControl1 .Title ;

            saveFileDialog1.ShowDialog();
        }
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            SaveScopeFileName = saveFileDialog1.FileName;
            try
            {
                stopToolStripMenuItem_Click(sender, e);
               
                if (iSaveDialogFrom == 1)
                {
                    //检查名称后缀
                    if (SaveScopeFileName.IndexOf(".tcscope") == -1)
                    {
                        SaveScopeFileName = SaveScopeFileName + ".tcscope";
                    }
                    //创建文件
                    File.Create(SaveScopeFileName).Close();
                    GlobalVar.scopeViewControl1.SaveScopeConfig(SaveScopeFileName);
                }
                if (iSaveDialogFrom == 2)
                {
                    if (SaveScopeFileName.IndexOf(".svd") == -1)
                    {
                        SaveScopeFileName = SaveScopeFileName + ".svd";
                    }
                    File.Create(SaveScopeFileName).Close();
                    GlobalVar.scopeViewControl1.Operating.SaveData(SaveScopeFileName);
                }

                if (iSaveDialogFrom == 3)
                {
                    if (SaveScopeFileName.IndexOf(".CSV") == -1 && SaveScopeFileName.IndexOf(".csv") == -1)
                    {
                        SaveScopeFileName = SaveScopeFileName + ".csv";
                    }
                   
                    GlobalVar.scopeViewControl1.Operating.ExportData(SaveScopeFileName);
                }

                if (iSaveDialogFrom ==4)
                {
                    if (SaveScopeFileName.IndexOf(".txt") == -1 && SaveScopeFileName.IndexOf(".TXT") == -1)
                    {
                        SaveScopeFileName = SaveScopeFileName + ".txt";
                    }
                    GlobalVar.scopeViewControl1.Operating.ExportData(SaveScopeFileName);
                }
                if (iSaveDialogFrom == 5)
                {
                    if (SaveScopeFileName.IndexOf(".dat") == -1 && SaveScopeFileName.IndexOf(".DAT") == -1)
                    {
                        SaveScopeFileName = SaveScopeFileName + ".dat";
                    }
                    GlobalVar.scopeViewControl1.Operating.ExportDataToDat(SaveScopeFileName);
                }
                if (iSaveDialogFrom == 6)
                {
                    if (SaveScopeFileName.IndexOf(".tdms") == -1 && SaveScopeFileName.IndexOf(".TDMS") == -1)
                    {
                        SaveScopeFileName = SaveScopeFileName + ".tdms";
                    }
                    GlobalVar.scopeViewControl1.Operating.ExportDataToTDMS(SaveScopeFileName);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        //Menu New
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //确认是否需要创建新项目，因为会删除当前正在打开项目中的Chart
            if (DialogResult.Yes == MessageBox.Show("Create A New Project？", "Confirm", MessageBoxButtons.YesNo))
            {

                if (GlobalVar.scopeViewControl1.State == ScopeViewControlStates.RECORD)
                {
                    GlobalVar.scopeViewControl1.Operating.StopRecord();
                }
                if (GlobalVar.scopeViewControl1.State == ScopeViewControlStates.REPLY)
                {
                    GlobalVar.scopeViewControl1.Operating.DisConnect(false);
                }


                while (GlobalVar.scopeViewControl1.Charts.Count > 0)
                {
                    GlobalVar.scopeViewControl1.DeleteChart(GlobalVar.scopeViewControl1.Charts[0]);
                }

                GlobalVar.scopeViewControl1.Title = "Twincat Scope Project";
                //刷新树形列表
                RefreshTreeView();
            }
        }
        // Menu Start 
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //discard old data
                if (GlobalVar.scopeViewControl1.State == ScopeViewControlStates.REPLY)
                    GlobalVar.scopeViewControl1.Operating.DisConnect(false);

                //start record
                if (GlobalVar.scopeViewControl1.State == ScopeViewControlStates.CONFIG)
                    GlobalVar.scopeViewControl1.Operating.StartRecord();

                //start all charts
                if (GlobalVar.scopeViewControl1.State == ScopeViewControlStates.CONNECTED)
                    GlobalVar.scopeViewControl1.Operating.StartAllDisplays();
                
                //显示Cursor
                if (DataGridFrm1 != null && DataGridFrm1.Parent != null)
                {
                    DataGridFrm1.DataGridViewXYFrameFresh(GlobalVar.TreeViewSelectIndexArray[1]);
                }

                panel3.Visible = false;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        //Menu Stop
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GlobalVar.scopeViewControl1.State == ScopeViewControlStates.RECORD)
                {
                    GlobalVar.scopeViewControl1.Operating.StopRecord();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
              
        //刷新树形表
        private void RefreshTreeView()
        {
            try
            {
                panel1.Visible = true;
                if (TreeViewFrm1.Parent == null)
                {
                    panel1.Controls.Clear();
                    panel1.Controls.Add(TreeViewFrm1);
                }
                TreeViewFrm1.TreeViewRefresh(ref  GlobalVar.scopeViewControl1);
                TreeViewFrm1.Show();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        //刷新树形表界面的相关控件大小
        private void ResizeTreeViewFrm()
        {
            TreeViewFrm1.PrtWidth = panel1.Width;
            TreeViewFrm1.PrtHeight = panel1.Height;
            TreeViewFrm1.TreeViewFrmSizeChange();

        }
        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            ResizeTreeViewFrm();
        }

        private void RefreshProperty()
        {
            try
            {
                panel3.Visible = true;
                if(PropertyFrm1.Parent==null )
                {
                    panel3.Controls.Clear();
                    panel3.Controls.Add(PropertyFrm1);
                };
                PropertyFrm1.RefreshCombobox(GlobalVar.TreeViewSelectLevel);
                PropertyFrm1.Show();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        private void panel3_SizeChanged(object sender, EventArgs e)
        {
            if (PropertyFrm1 != null)
            {
                PropertyFrm1.PrtWidth = panel3.Width;
                PropertyFrm1.PrtHeight = panel3.Height;
                PropertyFrm1.PropertyFormSizeChange();
            }
        }

        private void RefreshDataGrid()
        {
            try
            {
                panel4.Visible = true;
                if (DataGridFrm1.Parent == null)
                {
                    panel4.Controls.Clear();
                    panel4.Controls.Add(DataGridFrm1);
                };
                DataGridFrm1.DataGridViewXYFrameFresh(GlobalVar.TreeViewSelectIndexArray[1]);
                DataGridFrm1.Show();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        private void panel4_SizeChanged(object sender, EventArgs e)
        {
            if (DataGridFrm1 != null)
            {
                DataGridFrm1.PrtWidth = panel4.Width;
                DataGridFrm1.PrtHeight = panel4.Height;
                DataGridFrm1.DataGridFrmSizeChange();
            }
        }

        //树形表节点选中，切换Scope中响应的Chart，刷新属性界面
        private void TreeViewFrm1TreeViewSelect(object sender, EventArgs e)
        {
            if (GlobalVar.TreeViewSelectIndexArray[1] > -1)
            {
                bChartChangeFromSelect = true;
                GlobalVar.scopeViewControl1.Charts[GlobalVar.TreeViewSelectIndexArray[1]].Show();
            }
            RefreshProperty();
        }
        //树形表中channel及Cursor增删，刷新DataGrid界面的框架
        private void  TreeViewFrm1ChannelCursorChange(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }


        //导出数据CSV
        private void cSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iSaveDialogFrom = 3;
            saveFileDialog1.Filter = "Scope data files (*.csv)|*.csv";
            saveFileDialog1.FilterIndex = 0; //索引                  
            saveFileDialog1.RestoreDirectory = true;//获取或设置一个值，该值使文件对话框将其当前目录还原为用户更改目录以搜索文件之前的初始值  
            saveFileDialog1.CreatePrompt = true;//获取或设置一个值，该值指示如果用户指定一个不存在的文件，SaveFileDialog 是否提示用户以允许创建文件  
            saveFileDialog1.Title = "Save data to file"; //获取或设置在文件对话框的标题栏中显示的文本   

            saveFileDialog1.InitialDirectory = ApplicationPath + "\\ExportDataFiles";
            saveFileDialog1.FileName = GlobalVar.scopeViewControl1.Title;

            saveFileDialog1.ShowDialog();
        }
        //导出数据txt
        private void binaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iSaveDialogFrom = 4;
            saveFileDialog1.Filter = "Scope data files (*.txt)|*.txt";
            saveFileDialog1.FilterIndex = 0; //索引                  
            saveFileDialog1.RestoreDirectory = true;//获取或设置一个值，该值使文件对话框将其当前目录还原为用户更改目录以搜索文件之前的初始值  
            saveFileDialog1.CreatePrompt = true;//获取或设置一个值，该值指示如果用户指定一个不存在的文件，SaveFileDialog 是否提示用户以允许创建文件  
            saveFileDialog1.Title = "Save data to file"; //获取或设置在文件对话框的标题栏中显示的文本   

            saveFileDialog1.InitialDirectory = ApplicationPath + "\\ExportDataFiles";
            saveFileDialog1.FileName = GlobalVar.scopeViewControl1.Title;

            saveFileDialog1.ShowDialog();
        }
        //导出数据Dat 该功能只有professional才可用
        private void dATToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iSaveDialogFrom = 5;
            saveFileDialog1.Filter = "Scope data files (*.dat)|*.dat";
            saveFileDialog1.FilterIndex = 0; //索引                  
            saveFileDialog1.RestoreDirectory = true;//获取或设置一个值，该值使文件对话框将其当前目录还原为用户更改目录以搜索文件之前的初始值  
            saveFileDialog1.CreatePrompt = true;//获取或设置一个值，该值指示如果用户指定一个不存在的文件，SaveFileDialog 是否提示用户以允许创建文件  
            saveFileDialog1.Title = "Save data to file"; //获取或设置在文件对话框的标题栏中显示的文本   

            saveFileDialog1.InitialDirectory = ApplicationPath + "\\ExportDataFiles";
            saveFileDialog1.FileName = GlobalVar.scopeViewControl1.Title;

            saveFileDialog1.ShowDialog();
        }
        //导出数据TDMS 该功能只有professional才可用
        private void tDmSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iSaveDialogFrom = 6;
            saveFileDialog1.Filter = "Scope data files (*.tdms)|*.tdms";
            saveFileDialog1.FilterIndex = 0; //索引                  
            saveFileDialog1.RestoreDirectory = true;//获取或设置一个值，该值使文件对话框将其当前目录还原为用户更改目录以搜索文件之前的初始值  
            saveFileDialog1.CreatePrompt = true;//获取或设置一个值，该值指示如果用户指定一个不存在的文件，SaveFileDialog 是否提示用户以允许创建文件  
            saveFileDialog1.Title = "Save data to file"; //获取或设置在文件对话框的标题栏中显示的文本   

            saveFileDialog1.InitialDirectory = ApplicationPath + "\\ExportDataFiles";
            saveFileDialog1.FileName = GlobalVar.scopeViewControl1.Title;

            saveFileDialog1.ShowDialog();
        }




        //自有模块
             private void button1_Click(object sender, EventArgs e)
           {
               AdsStream stream = new AdsStream(100);
               AdsBinaryReader reader = new AdsBinaryReader(stream);
               _client.Read(_handBOOl, stream);
               aquaGauge1.Value = reader.ReadInt16();
            // stream.Seek(0, System.IO.SeekOrigin.Begin);

            // System.Timers.Timer t = new System.Timers.Timer(Convert.ToDouble(1000));          //每一秒刷新
            //  t.Elapsed += new System.Timers.ElapsedEventHandler(button1_Click);
            // t.SynchronizingObject = button1;//加这句就好了
            // t.AutoReset = true;
            //  t.Enabled = true;


        }







        private void textBox1_TextChanged_1(object sender, EventArgs e)
           {


           }

           private void aquaGauge1_Load(object sender, EventArgs e)
           {

           }

           private void panel3_Paint(object sender, PaintEventArgs e)
           {

           }

           private void timer1_Tick(object sender, EventArgs e)
           {
            button1_Click(button1, null);
        }
       }
   }

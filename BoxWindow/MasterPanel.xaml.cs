using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RecordPeriphelTechniс.BoxWindow;
using RecordPeriphelTechniс.Connection;
using RecordPeriphelTechniс.Windows;

namespace RecordPeriphelTechniс.BoxWindow
{
    /// <summary>
    /// Логика взаимодействия для MasterPanel.xaml
    /// </summary>
    public partial class MasterPanel : Window
    {
        public MasterPanel()
        {
            InitializeComponent();
            LoadDB_Application();
        }

        public void LoadDB_Application()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    connection.Open();
                    string query = $@"SELECT RepairDevice.ID, RepairDevice.IDMaster, RepairDevice.IDDevice, StatusApplications.NameStatus as Status , Users.Surname as Master,
                    RepairDevice.DateAppeals, RepairDevice.Comment  FROM RepairDevice
                    Left JOIN StatusApplications on RepairDevice.IDStatus  =  StatusApplications.ID
                    left JOIN Users on RepairDevice.IDMaster = Users.ID";                    
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    DataTable DT = new DataTable("RepairDevice");
                    SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    SDA.Fill(DT);
                    ListApplications.ItemsSource = DT.DefaultView;
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnResize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void BtnSazeMax_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
            }
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch
            {

            }
        }

        private void ListTechn_Click(object sender, RoutedEventArgs e)
        {
            MenuInformation menuinfor = new MenuInformation();
            this.Close();
            menuinfor.ShowDialog();
        }

        //private void ListService_Click(object sender, RoutedEventArgs e)
        //{
        //    MasterPanel master = new MasterPanel();
        //    this.Close();
        //    master.ShowDialog();
        //}

        private void ListUsers_Click(object sender, RoutedEventArgs e)
        {
            AdminPanel admpnl = new AdminPanel();
            this.Close();
            admpnl.ShowDialog();
        }
        private void ExportToExcel()
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

            for (int j = 0; j < ListApplications.Columns.Count; j++)
            {
                try
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[1, j + 1];
                    sheet1.Cells[1, j + 1].Font.Bold = true;
                    sheet1.Columns[j + 1].ColumnWidth = 20;
                    myRange.Value2 = ListApplications.Columns[j].Header;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


            for (int i = 0; i < ListApplications.Columns.Count; i++)
            {
                for (int j = 0; j < ListApplications.Items.Count; j++)
                {
                    TextBlock b = ListApplications.Columns[i].GetCellContent(ListApplications.Items[j]) as TextBlock;

                    if (b == null)
                        continue;

                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;

                }
            }
        }
        private void ExsportExcel_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel();
        }
    }
}

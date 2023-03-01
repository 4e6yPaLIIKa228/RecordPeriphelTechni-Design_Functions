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
                    string query = $@"SELECT RepairDevice.ID, RepairDevice.IDMaster, RepairDevice.IDDevice,MenuPerTech.Name as NameDevice, MenuPerTech.IDTypeTech, TypeTechs.NameType as NameType, MenuPerTech.Number as NumberDevice, StatusApplications.NameStatus as Status , 
                    Users.Surname as Surname, Users.Name as Name, Users.MiddleName as MiddleName,RepairDevice.DateAppeals, RepairDevice.Comment FROM RepairDevice
					LEFT JOIN MenuPerTech on  RepairDevice.IDDevice = MenuPerTech.ID
					Left Join TypeTechs on MenuPerTech.IDTypeTech = TypeTechs.ID
                    Left JOIN StatusApplications on RepairDevice.IDStatus  =  StatusApplications.ID
                    Left JOIN Users on RepairDevice.IDMaster = Users.ID";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    DataTable DT = new DataTable("RepairDevice");
                    SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    SDA.Fill(DT);
                    DataGridApplications.ItemsSource = DT.DefaultView;
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

            for (int j = 0; j < DataGridApplications.Columns.Count; j++)
            {
                try
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[1, j + 1];
                    sheet1.Cells[1, j + 1].Font.Bold = true;
                    sheet1.Columns[j + 1].ColumnWidth = 20;
                    myRange.Value2 = DataGridApplications.Columns[j].Header;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


            for (int i = 0; i < DataGridApplications.Columns.Count; i++)
            {
                for (int j = 0; j < DataGridApplications.Items.Count; j++)
                {
                    TextBlock b = DataGridApplications.Columns[i].GetCellContent(DataGridApplications.Items[j]) as TextBlock;

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

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchInfo(sender, e);
        }

        private void CombSearchInfo_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void ItemInfoTech_Click(object sender, RoutedEventArgs e)
        {
            Eddit_InforPcTex();
        }

        public void SearchInfo(object sender, TextChangedEventArgs e)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    connection.Open();
                    string DBSearch = $@"SELECT RepairDevice.ID, RepairDevice.IDMaster, RepairDevice.IDDevice,MenuPerTech.Name as NameDevice, MenuPerTech.IDTypeTech, TypeTechs.NameType as NameType, MenuPerTech.Number as NumberDevice, StatusApplications.NameStatus as Status , 
                    Users.Surname as Surname, Users.Name as Name, Users.MiddleName as MiddleName,RepairDevice.DateAppeals, RepairDevice.Comment FROM RepairDevice
					LEFT JOIN MenuPerTech on  RepairDevice.IDDevice = MenuPerTech.ID
					Left Join TypeTechs on MenuPerTech.IDTypeTech = TypeTechs.ID
                    Left JOIN StatusApplications on RepairDevice.IDStatus  =  StatusApplications.ID
                    Left JOIN Users on RepairDevice.IDMaster = Users.ID";
                    String combtext = CombSearchInfo.Text;
                    if (combtext == "ID")
                    {
                        DataGridApplications.ItemsSource = null;
                        string query = $@"{DBSearch}   WHERE RepairDevice.ID like '%{TxtSearch.Text.ToLower()}%' or RepairDevice.ID like '%{TxtSearch.Text.ToUpper()}%' or RepairDevice.ID like '%{TxtSearch.Text}%'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("RepairDevice");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        DataGridApplications.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Тип устройства")
                    {
                        DataGridApplications.ItemsSource = null;
                        string query = $@"{DBSearch}  WHERE TypeTechs.NameType like '%{TxtSearch.Text.ToLower()}%' or TypeTechs.NameType like '%{TxtSearch.Text.ToUpper()}%' or TypeTechs.NameType like '%{TxtSearch.Text}%'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("RepairDevice");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        DataGridApplications.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Номер")
                    {
                        DataGridApplications.ItemsSource = null;
                        string query = $@"{DBSearch}  WHERE MenuPerTech.Number like '%{TxtSearch.Text.ToLower()}%' or MenuPerTech.Number like '%{TxtSearch.Text.ToUpper()}%' or MenuPerTech.Number like '%{TxtSearch.Text}%'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("RepairDevice");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        DataGridApplications.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Статус")
                    {
                        DataGridApplications.ItemsSource = null;
                        string query = $@"{DBSearch}  WHERE StatusApplications.NameStatus like '%{TxtSearch.Text.ToLower()}%' or StatusApplications.NameStatus like '%{TxtSearch.Text.ToUpper()}%' or StatusApplications.NameStatus like '%{TxtSearch.Text}%'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("RepairDevice");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        DataGridApplications.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Мастер")
                    {
                        DataGridApplications.ItemsSource = null;
                        string query = $@"{DBSearch}  ??????";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("RepairDevice");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        DataGridApplications.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Дата обращения")
                    {
                        DataGridApplications.ItemsSource = null;
                        string query = $@"{DBSearch}  WHERE RepairDevice.DateAppeals like '%{TxtSearch.Text.ToLower()}%' or RepairDevice.DateAppeals like '%{TxtSearch.Text.ToUpper()}%' or RepairDevice.DateAppeals like '%{TxtSearch.Text}%'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("RepairDevice");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        DataGridApplications.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Коментарий")
                    {
                        DataGridApplications.ItemsSource = null;
                        string query = $@"{DBSearch}  WHERE RepairDevice.Comment like '%{TxtSearch.Text.ToLower()}%' or RepairDevice.Comment like '%{TxtSearch.Text.ToUpper()}%' or RepairDevice.Comment like '%{TxtSearch.Text}%'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("RepairDevice");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        DataGridApplications.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }







        private void Eddit_InforPcTex()
        {
            if (DataGridApplications.SelectedIndex != -1)
            {
                EdditMessageReport tech = new EdditMessageReport((DataRowView)DataGridApplications.SelectedItem);
                tech.Owner = this;
                bool? result = tech.ShowDialog();
                switch (result)
                {
                    default:
                        LoadDB_Application();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Выберите строку с данными,чтобы ее изменить");
            }
        }

        private void ListApplications_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Eddit_InforPcTex();
        }
    } 
}

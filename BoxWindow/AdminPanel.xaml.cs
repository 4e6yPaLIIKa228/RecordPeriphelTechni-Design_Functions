using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        public AdminPanel()
        {
            InitializeComponent();
            LoadDB_Users();
        }

        public void LoadDB_Users()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    connection.Open();
                    string query = $@"SELECT  Users.ID,Users.Login, Users.Password,Users.Surname,Users.Name,Users.MiddleName,Users.DataRegist, StatusUsers.StatusUser, AllowanceUsers.Allowance  FROM Users
                                JOIN StatusUsers on Users.IDStatus = StatusUsers.ID
								JOIN AllowanceUsers on Users.IDAllowance = AllowanceUsers.ID";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    DataTable DT = new DataTable("MenuPerTech");
                    SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    SDA.Fill(DT);
                    DataGridUsers.ItemsSource = DT.DefaultView;
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void InHome_Click(object sender, RoutedEventArgs e)
        {
            MenuInformation addtech = new MenuInformation();
            this.Close();
            addtech.ShowDialog();

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

        private void ListService_Click(object sender, RoutedEventArgs e)
        {
            MasterPanel addtech = new MasterPanel();
            this.Close();
            addtech.ShowDialog();
        }

        private void AddTec_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReportMessgae_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EdiitTec_Click(object sender, RoutedEventArgs e)
        {
            Eddit_User();
        }

        private void ExcelPc_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExcelPer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddComponet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DellComponet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CombSearchInfo_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void ExsportExcel_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel();
        }

        private void ListTechnic_Click(object sender, RoutedEventArgs e)
        {
            MenuInformation addtech = new MenuInformation();
            this.Close();
            addtech.ShowDialog();
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchInfo(sender, e);
        }
        public void SearchInfo(object sender, TextChangedEventArgs e)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    connection.Open();
                    string DBSearch = $@"SELECT  Users.ID,Users.Login, Users.Password,Users.Surname,Users.Name,Users.MiddleName,Users.DataRegist, StatusUsers.StatusUser, AllowanceUsers.Allowance  FROM Users
                                JOIN StatusUsers on Users.IDStatus = StatusUsers.ID
								JOIN AllowanceUsers on Users.IDAllowance = AllowanceUsers.ID";
                    String combtext = CombSearchInfo.Text;
                    if (combtext == "ID")
                    {
                        DataGridUsers.ItemsSource = null;
                        string query = $@"{DBSearch} WHERE  Users.ID like '%{TxtSearch.Text}%'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("Users");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        DataGridUsers.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Логин")
                    {
                        DataGridUsers.ItemsSource = null;
                        string query = $@"{DBSearch}  WHERE Users.Login like '%{TxtSearch.Text.ToLower()}%' or Users.Login like '%{TxtSearch.Text.ToUpper()}%' or Users.Login like '%{TxtSearch.Text}%'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("Users");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        DataGridUsers.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Фамилия")
                    {
                        DataGridUsers.ItemsSource = null;
                        string query = $@"{DBSearch}  WHERE Users.Surname like '%{TxtSearch.Text.ToLower()}%' or Users.Surname like '%{TxtSearch.Text.ToUpper()}%' or Users.Surname like '%{TxtSearch.Text}%'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("Users");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        DataGridUsers.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Имя")
                    {
                        DataGridUsers.ItemsSource = null;
                        string query = $@"{DBSearch}  WHERE Users.Name like '%{TxtSearch.Text.ToLower()}%' or Users.Name like '%{TxtSearch.Text.ToUpper()}%' or Users.Name like '%{TxtSearch.Text}%'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("Users");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        DataGridUsers.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Отчество")
                    {
                        DataGridUsers.ItemsSource = null;
                        string query = $@"{DBSearch}  WHERE Users.MiddleName like '%{TxtSearch.Text.ToLower()}%' or Users.MiddleName like '%{TxtSearch.Text.ToUpper()}%' or Users.MiddleName like '%{TxtSearch.Text}%'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("Users");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        DataGridUsers.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Дата регистрации")
                    {
                        DataGridUsers.ItemsSource = null;
                        string query = $@"{DBSearch}  WHERE Users.DataRegist like '%{TxtSearch.Text.ToLower()}%' or Users.DataRegist like '%{TxtSearch.Text.ToUpper()}%' or Users.MiddleName like '%{TxtSearch.Text}%'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("Users");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        DataGridUsers.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Статус")
                    {
                        DataGridUsers.ItemsSource = null;
                        string query = $@"{DBSearch}  WHERE StatusUsers.StatusUser like '%{TxtSearch.Text.ToLower()}%' or StatusUsers.StatusUser like '%{TxtSearch.Text.ToUpper()}%' or StatusUsers.StatusUser like '%{TxtSearch.Text}%'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("Users");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        DataGridUsers.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Доступ")
                    {
                        DataGridUsers.ItemsSource = null;
                        string query = $@"{DBSearch}  WHERE AllowanceUsers.Allowance like '%{TxtSearch.Text.ToLower()}%' or AllowanceUsers.Allowance like '%{TxtSearch.Text.ToUpper()}%' or AllowanceUsers.Allowance like '%{TxtSearch.Text}%'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("Users");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        DataGridUsers.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExportToExcel()
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

            for (int j = 0; j < DataGridUsers.Columns.Count; j++)
            {
                try
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[1, j + 1];
                    sheet1.Cells[1, j + 1].Font.Bold = true;
                    sheet1.Columns[j + 1].ColumnWidth = 20;
                    sheet1.Columns[j + 1].NumberFormat = "@";
                    myRange.Value2 = DataGridUsers.Columns[j].Header;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


            for (int i = 0; i < DataGridUsers.Columns.Count; i++)
            {
                for (int j = 0; j < DataGridUsers.Items.Count; j++)
                {
                    TextBlock b = DataGridUsers.Columns[i].GetCellContent(DataGridUsers.Items[j]) as TextBlock;

                    if (b == null)
                        continue;

                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;

                }
            }
        }

        private void AddUsers_Click(object sender, RoutedEventArgs e)
        {
            Registration addtech = new Registration();
            bool? result = addtech.ShowDialog();
            switch (result)
            {
                default:
                    LoadDB_Users();
                    break;
            }
        }



        private void DataGridUsers_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Eddit_User();
        }
        private void Eddit_User()
        {
            if (DataGridUsers.SelectedIndex != -1)
            {
                EdditUsers tech = new EdditUsers((DataRowView)DataGridUsers.SelectedItem);
                tech.Owner = this;
                bool? result = tech.ShowDialog();
                switch (result)
                {
                    default:
                        LoadDB_Users();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Выберите строку с данными,чтобы ее изменить");
            }

        }

        private void ExitAcc_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите выйти из аккаунта?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Saver.IDUser = null;
                Saver.LoginUser = null;
                Authorization admpnl = new Authorization();
                this.Close();
                admpnl.ShowDialog();
            }
        }

        private void ScrollAdminPanel_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
        private void ScrollPCLineUp(object sender, RoutedEventArgs e)
        {
            ((IScrollInfo)ScrollAdminPanel).LineUp();
        }
        private void ScrollPCLineDown(object sender, RoutedEventArgs e)
        {
            ((IScrollInfo)ScrollAdminPanel).LineDown();
        }       
    }
}

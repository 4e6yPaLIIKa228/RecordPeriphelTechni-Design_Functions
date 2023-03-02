using Microsoft.Office.Interop.Excel;
using RecordPeriphelTechniс.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Net.NetworkInformation;
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
using DataTable = System.Data.DataTable;
using Window = System.Windows.Window;

namespace RecordPeriphelTechniс.BoxWindow
{
    /// <summary>
    /// Логика взаимодействия для EdditUsers.xaml
    /// </summary>
    public partial class EdditUsers : Window
    {
        string IDUser = null,OldLogin=null;
        public EdditUsers(DataRowView drv)
        {
            InitializeComponent();
            CombBoxDowmload();
            IDUser = drv["ID"].ToString();
            OldLogin = drv["Login"].ToString();
            TextBoxLogin.Text = drv["Login"].ToString();
            PassBox.Password = drv["Password"].ToString();
            //  var Pass = SimpleComand.GetHash(PassBox.Password);
            //  PassBox.Password = Convert.ToString(Pass);
            CombAllowance.Text = drv["Allowance"].ToString();
            CombStatus.Text = drv["StatusUser"].ToString();
            TextFamili.Text = drv["Surname"].ToString();
            TextName.Text = drv["Name"].ToString();
            TextOthectbo.Text = drv["MiddleName"].ToString();


        }

        public void CombBoxDowmload()  //Данные для комбобоксов 
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                try
                {
                    connection.Open();
                    string query = $@"SELECT * FROM AllowanceUsers";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    DataTable dt = new DataTable("AllowanceUsers");
                    SDA.Fill(dt);
                    CombAllowance.ItemsSource = dt.DefaultView;
                    CombAllowance.DisplayMemberPath = "Allowance";
                    CombAllowance.SelectedValuePath = "ID";
                    string query1 = $@"SELECT * FROM StatusUsers";
                    SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                    SQLiteDataAdapter SDA1 = new SQLiteDataAdapter(cmd1);
                    DataTable dt1 = new DataTable("StatusUsers");
                    SDA1.Fill(dt1);
                    CombStatus.ItemsSource = dt1.DefaultView;
                    CombStatus.DisplayMemberPath = "StatusUser";
                    CombStatus.SelectedValuePath = "ID";

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void UpdateUser()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    if (String.IsNullOrEmpty(TextBoxLogin.Text) || String.IsNullOrEmpty(PassBox.Password) || String.IsNullOrEmpty(CombAllowance.Text) || String.IsNullOrEmpty(TextFamili.Text) || String.IsNullOrEmpty(TextName.Text) || String.IsNullOrEmpty(CombStatus.Text))
                    {
                        //CheckerText();
                        MessageBox.Show("Заполните обязательные поля ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        if (MessageBox.Show("Вы уверены что хотите изменить данные?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            connection.Open();
                            int ProverkaLogin = 0;
                            if (OldLogin != TextBoxLogin.Text)
                            {
                                string query = $@"SELECT count (Login) FROM Users WHERE Login = '{TextBoxLogin.Text}'";
                                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                                ProverkaLogin = Convert.ToInt32(cmd.ExecuteScalar());
                                MessageBox.Show("Данный логин занят, выберите другой ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                if (ProverkaLogin == 0)
                                {
                                    if (String.IsNullOrEmpty(PassBoxNew1.Password) || String.IsNullOrEmpty(PassBoxNew1.Password))
                                    {
                                        bool resultClass = int.TryParse(CombAllowance.SelectedValue.ToString(), out int idAllow);
                                        bool resultKab = int.TryParse(CombStatus.SelectedValue.ToString(), out int idStatus);
                                        string query = $@"UPDATE Users SET Login='{TextBoxLogin.Text.ToLower()}',Surname='{TextFamili.Text}', Name='{TextName.Text}', MiddleName='{TextOthectbo.Text}', 
                                            IDStatus='{idStatus}' ,IDAllowance='{idAllow}'  WHERE ID='{IDUser}';";
                                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                                        cmd.ExecuteNonQuery();
                                        MessageBox.Show("Данные обновленны!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                                    }
                                    else if (PassBoxNew1.Password != "" && PassBoxNew2.Password != "" && PassBoxNew1.Password == PassBoxNew2.Password)
                                    {                                       
                                        
                                        bool resultClass = int.TryParse(CombAllowance.SelectedValue.ToString(), out int idAllow);
                                        bool resultKab = int.TryParse(CombStatus.SelectedValue.ToString(), out int idStatus);
                                        var Pass = SimpleComand.GetHash(PassBoxNew2.Password);
                                        string query = $@"UPDATE Users SET Login='{TextBoxLogin.Text.ToLower()}', Password=@Password, Surname='{TextFamili.Text}',Name='{TextName.Text}', MiddleName='{TextOthectbo.Text}', 
                                        IDStatus='{idStatus}' ,IDAllowance='{idAllow}'  WHERE ID='{IDUser}';";
                                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                                        cmd.Parameters.AddWithValue("@Password", Pass);
                                        cmd.ExecuteNonQuery();
                                        MessageBox.Show("Данные обновленны", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                                        PassBoxNew1.Password = string.Empty;
                                        PassBoxNew2.Password = string.Empty;
                                        PassBox.Password = "111111111";
                                    }
                                    else if (PassBoxNew1.Password != PassBoxNew2.Password)
                                    {
                                        MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                    }                                    
                                }
                            } 
                        }
                    }
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

        private void EdditUser_Click(object sender, RoutedEventArgs e)
        {
            UpdateUser();
        }
    }
}

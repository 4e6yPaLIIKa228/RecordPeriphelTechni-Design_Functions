using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
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
using RecordPeriphelTechniс.Connection;
using RecordPeriphelTechniс.Windows;
using RecordPeriphelTechniс.BoxWindow;

namespace RecordPeriphelTechniс.BoxWindow
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

       public void CheckerText()
        {
            SimpleComand.CheckTextBox(TextBoxLogin);
            SimpleComand.CheckPassBox(PassBox);
        } //Проверка пустых строк(подсветка)

        public void AuthorizationUser() 
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {

                    if (String.IsNullOrEmpty(TextBoxLogin.Text) || String.IsNullOrEmpty(PassBox.Password))
                    {
                        CheckerText();
                        MessageBox.Show("Заполните обязательные поля ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {                       
                        connection.Open();
                        var Pass = SimpleComand.GetHash(PassBox.Password);
                        string LoginLower = TextBoxLogin.Text.ToLower();
                        string query = $@"SELECT  COUNT(1) FROM Users WHERE Login='{LoginLower}' AND Password = '{Pass}' and IDStatus != 3";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        int UsersFound = Convert.ToInt32(cmd.ExecuteScalar());
                        if (UsersFound == 1)
                        {
                            query = $@"SELECT ID,IDAllowance FROM Users WHERE Login= '{LoginLower}'";
                            Saver.LoginUser = LoginLower;
                            SQLiteDataReader dr = null;
                            SQLiteCommand cmd1 = new SQLiteCommand(query, connection);
                            int IDAllowance = 0;
                            dr = cmd1.ExecuteReader();
                            while (dr.Read())
                            {
                                Saver.IDUser = dr["ID"].ToString();
                                IDAllowance  = Convert.ToInt32(dr["IDAllowance"].ToString());
                                //  Saver.IDAcc = countID;
                            }
                            if (IDAllowance == 1)
                            {
                                MessageBox.Show("Добро пожаловать!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                                AdminPanel admpnl = new AdminPanel();
                                this.Close();
                                admpnl.ShowDialog();
                                connection.Close();
                            }
                            else
                            {
                                MessageBox.Show("Добро пожаловать!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                                MenuInformation menuinfor = new MenuInformation();
                                this.Close();
                                menuinfor.ShowDialog();
                                connection.Close();
                            }

                            connection.Close();
                            //MessageBox.Show("Добро пожаловать! " + $@"{TextBoxLogin.Text}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex);
            }
        } //Функция авторизации пользователя 


        private void TextBoxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            SimpleComand.CheckTextBox(TextBoxLogin);
        }

        private void PassBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            SimpleComand.CheckPassBox(PassBox);
        }

        private void BtnAvtoriz_Click(object sender, RoutedEventArgs e)
        {           
            AuthorizationUser();
        }
    }
}

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

namespace RecordPeriphelTechniс.BoxWindow
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        int CheckeLogin = 0;
        public Registration()
        {
            InitializeComponent();
            CombBoxDowmload();
        }

        public void CombBoxDowmload()  //Данные для комбобоксов 
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                try
                {
                    connection.Open();
                    string query = $@"SELECT * FROM StatusUsers";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    DataTable dt = new DataTable("StatusUsers");
                    SDA.Fill(dt);
                    CombStatus.ItemsSource = dt.DefaultView;
                    CombStatus.DisplayMemberPath = "StatusUser";
                    CombStatus.SelectedValuePath = "ID";

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUsers();
        }

        public void CheckerText()
        {
            SimpleComand.CheckTextBox(TextBoxLogin);
            SimpleComand.CheckPassBox(PassBox);
            SimpleComand.CheckTextBox(TextFamili);
            SimpleComand.CheckTextBox(TextName);
            SimpleComand.CheckComboBox(CombStatus); 
        }

        private void TextBoxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            SimpleComand.CheckTextBox(TextBoxLogin);
        }

        private void PassBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            SimpleComand.CheckPassBox(PassBox);
        }

        private void TextFamili_TextChanged(object sender, TextChangedEventArgs e)
        {
            SimpleComand.CheckTextBox(TextFamili);
        }

        private void TextName_TextChanged(object sender, TextChangedEventArgs e)
        {
            SimpleComand.CheckTextBox(TextName);
        }

        private void CombStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SimpleComand.CheckComboBox(CombStatus);
        }

        public void AddUsers()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {

                    if (String.IsNullOrEmpty(TextBoxLogin.Text) || String.IsNullOrEmpty(PassBox.Password) || String.IsNullOrEmpty(TextFamili.Text) || String.IsNullOrEmpty(TextName.Text) || String.IsNullOrEmpty(CombStatus.Text))
                    {
                        CheckerText();
                        MessageBox.Show("Заполните обязательные поля ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        CheckerLogin();
                        if (CheckeLogin == 0)
                        {
                            connection.Open();
                            var Login = TextBoxLogin.Text.ToLower();
                            var Pass = SimpleComand.GetHash(PassBox.Password);
                            var DateNow = DateTime.Now.ToString("dd/MM/yyyy");
                            bool resultType = int.TryParse(CombStatus.SelectedValue.ToString(), out int  IDCombStatus);
                            string query = $@"INSERT INTO Users ('Login','Passoword','Surname',Name,MiddleName,IDStasus,DataRegist) VALUES ('{Login}','{Pass}','{TextFamili.Text}','{TextName.Text}','{TextOthectbo.Text}','{IDCombStatus}','{DateNow}')";
                            SQLiteCommand cmd = new SQLiteCommand(query, connection);
                            cmd.ExecuteScalar();
                            connection.Close();
                            MessageBox.Show("Пользователь добавлен ", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);                            
                        }
                        else
                        {
                            MessageBox.Show("Такой логин занят ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex);
            }
        }

        public void CheckerLogin()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    connection.Open();
                    var Login = TextBoxLogin.Text.ToLower();
                    string query = $@"SELECT  COUNT(1) FROM Users WHERE Login='{Login}'";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    CheckeLogin = Convert.ToInt32(cmd.ExecuteScalar());
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex);
            }
        }

        private void InHome_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
         
 

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
                        //string query = $@"SELECT  COUNT(1) FROM Users WHERE Login='{LoginLower}' AND Password = @Password";
                        string query = $@"SELECT  COUNT(1) FROM Users WHERE Login='{LoginLower}'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        // cmd.Parameters.AddWithValue("@Password", Pass);
                        int UsersSearch = Convert.ToInt32(cmd.ExecuteScalar());
                        if (UsersSearch == 1)
                        {
                            query = $@"SELECT  COUNT(1) FROM Users WHERE Login='{LoginLower}' AND Password = @Password and IDStatus != 3";
                            cmd = new SQLiteCommand(query, connection);
                            cmd.Parameters.AddWithValue("@Password", Pass);
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
                                    IDAllowance = Convert.ToInt32(dr["IDAllowance"].ToString());
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
                            else
                            {
                                NotInvalidPass();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неверный пароль или логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex);
            }
        } //Функция авторизации пользователя 

        public void NotInvalidPass()
        {

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    connection.Open();
                    var Pass = SimpleComand.GetHash(PassBox.Password);
                    string LoginLower = TextBoxLogin.Text.ToLower();
                    string query = $@"SELECT  COUNT(1) FROM Users WHERE Login = '{LoginLower}' and Password != @Password";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Password", Pass);
                    int proverkaInvalidPass = Convert.ToInt32(cmd.ExecuteScalar());
                    query = $@"SELECT  COUNT(1) FROM Users WHERE Login='{LoginLower}' AND Password = @Password and IDStatus = 3";
                    cmd = new SQLiteCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Password", Pass);
                    int UsersUnBlock = Convert.ToInt32(cmd.ExecuteScalar());
                    if (proverkaInvalidPass == 1)
                    {
                        query = $@"SELECT IDAllowance FROM Users WHERE Login= '{LoginLower}'";
                        Saver.LoginUser = LoginLower;
                        SQLiteDataReader dr = null;
                        SQLiteCommand cmd1 = new SQLiteCommand(query, connection);
                        int IDProverka = 0;
                        dr = cmd1.ExecuteReader();
                        while (dr.Read())
                        {
                            //Saver.IDUser = dr["ID"].ToString();
                            IDProverka = Convert.ToInt32(dr["IDAllowance"].ToString());
                            //  Saver.IDAcc = countID;
                        }
                        query = $@"SELECT AttemptNumber,TimeEnd,TimeBegin FROM Proverka WHERE ID = '{IDProverka}';";
                        cmd = new SQLiteCommand(query, connection);
                        string dateOpen = DateTime.Now.ToString("t");
                        TimeSpan s2 = TimeSpan.Parse(dateOpen);
                        dr = null;
                        string AttemptNumber = "";
                        string dateban = "00:00";
                        string dateBegin = "00:00";
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            AttemptNumber = dr["AttemptNumber"].ToString();
                            dateban = dr["TimeEnd"].ToString();
                            dateBegin = dr["TimeBegin"].ToString();

                        }
                        TimeSpan sban = TimeSpan.Parse(dateban);
                        TimeSpan bban = TimeSpan.Parse(dateBegin);
                        if (Convert.ToInt32(AttemptNumber) < 3)
                        {

                            int kolint = Convert.ToInt32(AttemptNumber);
                            kolint++;
                            //string timeban = "0:02";
                            TimeSpan s1 = TimeSpan.Parse("0:02");
                            TimeSpan s3 = s1 + s2;
                            string times3 = s3.ToString("hh':'mm");
                            query = $@"UPDATE Proverka SET AttemptNumber='{kolint}',TimeBegin='{dateOpen}',TimeEnd ='{times3}' WHERE ID ='{IDProverka}';";
                            cmd = new SQLiteCommand(query, connection);
                            cmd.ExecuteReader();
                            MessageBox.Show("Неверный пароль или логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                        }
                        else if (Convert.ToInt32(AttemptNumber) == 3)
                        {
                            if (Convert.ToInt32(AttemptNumber) == 3 && (TimeSpan.Parse(dateOpen) < TimeSpan.Parse(dateban)))
                            {
                                query = $@"UPDATE Users SET IDStatus='{3}' WHERE Login='{LoginLower}';";
                                cmd = new SQLiteCommand(query, connection);
                                cmd.ExecuteReader();
                                MessageBox.Show("Ваша учетная запись временно заблокированна,попробуйте позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else if (Convert.ToInt32(AttemptNumber) == 3 && (TimeSpan.Parse(dateOpen) >= TimeSpan.Parse(dateban)))
                            {
                                query = $@"UPDATE Proverka SET AttemptNumber = '{0}',TimeBegin = '00:00',TimeEnd = '00:00' WHERE ID ='{IDProverka}';";
                                cmd = new SQLiteCommand(query, connection);
                                cmd.ExecuteReader();
                                query = $@"UPDATE Users SET IDStatus='{1}' WHERE Login='{LoginLower}';";
                                cmd = new SQLiteCommand(query, connection);
                                cmd.ExecuteReader();
                                MessageBox.Show("Неверный пароль или логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {


                            }
                        }
                    }
                    if (UsersUnBlock == 1)// снятие времменгой блокировки при правельном пароле,при котором время блокировки еще не прошло
                    {
                        query = $@"SELECT IDAllowance,IDProverka FROM Users WHERE Login= '{LoginLower}'";
                        Saver.LoginUser = LoginLower;
                        SQLiteDataReader dr = null;
                        SQLiteCommand cmd1 = new SQLiteCommand(query, connection);
                        int IDAllowance = 0;
                        int IDProverka = 0;
                        dr = cmd1.ExecuteReader();
                        while (dr.Read())
                        {
                            // Saver.IDUser = dr["ID"].ToString();
                            IDAllowance = Convert.ToInt32(dr["IDAllowance"].ToString());
                            IDProverka = Convert.ToInt32(dr["IDAllowance"].ToString());
                            //  Saver.IDAcc = countID;
                        }
                        query = $@"SELECT AttemptNumber,TimeEnd,TimeBegin FROM Proverka WHERE ID = '{IDProverka}';";
                        cmd = new SQLiteCommand(query, connection);
                        string dateOpen = DateTime.Now.ToString("t");
                        TimeSpan s2 = TimeSpan.Parse(dateOpen);
                        dr = null;
                        string AttemptNumber = "";
                        string dateban = "00:00";
                        string dateBegin = "00:00";
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            AttemptNumber = dr["AttemptNumber"].ToString();
                            dateban = dr["TimeEnd"].ToString();
                            dateBegin = dr["TimeBegin"].ToString();

                        }
                        if (Convert.ToInt32(AttemptNumber) == 3 && (TimeSpan.Parse(dateOpen) > TimeSpan.Parse(dateban)))
                        {
                            query = $@"UPDATE Proverka SET AttemptNumber = '{0}',TimeBegin = '00:00',TimeEnd = '00:00' WHERE ID ='{IDProverka}';";
                            cmd = new SQLiteCommand(query, connection);
                            cmd.ExecuteReader();
                            query = $@"UPDATE Users SET IDStatus='{1}' WHERE Login='{LoginLower}';";
                            cmd = new SQLiteCommand(query, connection);
                            cmd.ExecuteReader();
                            query = $@"SELECT ID FROM Users WHERE Login= '{LoginLower}'";
                            Saver.LoginUser = LoginLower;
                            dr = null;
                            cmd1 = new SQLiteCommand(query, connection);
                            dr = cmd1.ExecuteReader();
                            while (dr.Read())
                            {
                                Saver.IDUser = dr["ID"].ToString();

                            }
                            if (IDAllowance == 1)
                            {
                                MessageBox.Show("Добро пожаловать!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                                AdminPanel admpnl = new AdminPanel();
                                this.Close();
                                admpnl.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("Добро пожаловать!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                                MenuInformation menuinfor = new MenuInformation();
                                this.Close();
                                menuinfor.ShowDialog();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ваша учетная запись временно заблокированна,попробуйте позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }


                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex);
            }
        } //Если пароль не правельный

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

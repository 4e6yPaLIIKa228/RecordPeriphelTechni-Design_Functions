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
using Microsoft.Office.Interop.Excel;
using RecordPeriphelTechniс.BoxWindow;
using RecordPeriphelTechniс.Connection;
using Window = System.Windows.Window;

namespace RecordPeriphelTechniс.BoxWindow
{
    /// <summary>
    /// Логика взаимодействия для ReportMessage.xaml
    /// </summary>
    public partial class ReportMessage : Window
    {
        public ReportMessage(string numbertech)
        {
            InitializeComponent();
            TextNumberTech.Text = numbertech;
        }

        public void CheckerText() // подцветка при пустоте
        {
            SimpleComand.CheckTextBox(TextNumberTech);
            SimpleComand.CheckTextBox(TextComments);
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
            this.Close();
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

        public void MessageReport()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {

                    if (String.IsNullOrEmpty(TextNumberTech.Text) || String.IsNullOrEmpty(TextComments.Text))
                    {
                        CheckerText();
                        MessageBox.Show("Заполните обязательные поля ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else // поиск устройства
                    {
                        connection.Open();
                        string NumberLower = TextNumberTech.Text.ToLower();
                        string query = $@"SELECT  COUNT(1) FROM MenuPerTech WHERE Number = '{NumberLower}'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        int NumberSearch = Convert.ToInt32(cmd.ExecuteScalar());
                        if (NumberSearch == 1)
                        {
                            query = $@"SELECT ID FROM MenuPerTech WHERE Number = '{NumberLower}'";
                            cmd = new SQLiteCommand(query, connection);
                            SQLiteDataReader dr = null;                            
                            string IDTechnic = null;
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                IDTechnic = dr["ID"].ToString();
                                //  Saver.IDAcc = countID;
                            }
                            query = $@"SELECT  COUNT(1) FROM  RepairDevice where IDDevice =  '{IDTechnic}'";
                            cmd = new SQLiteCommand(query, connection);
                            int IDDeviceSearch = Convert.ToInt32(cmd.ExecuteScalar());
                            if (IDDeviceSearch == 0)
                            {//заявка создается
                                string DateNow =  DateTime.Now.ToString("d/MM//yyyy");
                                query = $@"INSERT INTO RepairDevice('IDDevice','IDStatus',IDMaster,'DateAppeals','Comment')
                                values ('{IDTechnic}','{1}', @IDMaster,'{DateNow}' , '{TextComments.Text}' )";
                                cmd = new SQLiteCommand(query, connection);
                                cmd.Parameters.AddWithValue("@IDMaster", null);
                                cmd.ExecuteNonQuery();
                                query = $@"UPDATE MenuPerTech SET IDWorks='{2}' where ID = '{IDTechnic}'";
                                cmd = new SQLiteCommand(query, connection);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Заявка отправленна!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);

                            }
                            else // уже была отправлена заявка
                            {
                                MessageBox.Show($@"Устройстов с номером: " + NumberLower + " уже была отправлена заявка на ремонт ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else //нте такого устройства
                        {
                            MessageBox.Show($@"Устройстов с номером: " + NumberLower + " не найдено", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex);
            }
        }

        private void BtnMessage_Click(object sender, RoutedEventArgs e)
        {
            MessageReport();
        }
               
    }
}

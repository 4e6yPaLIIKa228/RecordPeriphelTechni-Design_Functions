using Microsoft.Office.Interop.Excel;
using RecordPeriphelTechniс.Connection;
using System;
using System.Collections.Generic;
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
using Window = System.Windows.Window;

namespace RecordPeriphelTechniс.BoxWindow
{
    /// <summary>
    /// Логика взаимодействия для AddComponets.xaml
    /// </summary>
    public partial class AddComponets : Window
    {
        public AddComponets()
        {
            InitializeComponent();
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

        private void CombSearchInfo_DropDownClosed(object sender, EventArgs e)
        {
           // String combtext = CombKruterui.Text;
           // MessageBox.Show(combtext);
        }

        public void CheckerText()
        {
            SimpleComand.CheckComboBox(CombKruterui);
            SimpleComand.CheckTextBox(TextComponet);
        } //Проверка пустых строк(подсветка)

        public void AddComponet()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                connection.Open();
                try
                {
                    if (String.IsNullOrEmpty(CombKruterui.Text) || String.IsNullOrEmpty(TextComponet.Text))
                    {
                        CheckerText();
                        MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                        if (MessageBox.Show("Вы уверены что хотите добавить этот компонет?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            String combtext = CombKruterui.Text;
                            if (combtext == "Организация")
                            {
                                string query = $@"SELECT count(NameOrg) FROM Organiz where Organiz.NameOrg = '{TextComponet.Text.ToUpper()}'";
                                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                                int ProverkaComponet = Convert.ToInt32(cmd.ExecuteScalar());
                                if (ProverkaComponet == 0)
                                {
                                    query = $@"INSERT INTO Organiz('NameOrg') values ('{TextComponet.Text.ToUpper()}')";
                                    cmd = new SQLiteCommand(query, connection);
                                    cmd.ExecuteScalar();
                                    MessageBox.Show("Компонет добавлен в базу", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Такой компонет уже внесет в базу", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                            else if (combtext == "Производитель процессора")
                            {
                                string query = $@"SELECT count() FROM MakersProcc where MakersProcc.Name = '{TextComponet.Text.ToUpper()}'";
                                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                                int ProverkaComponet = Convert.ToInt32(cmd.ExecuteScalar());
                                if (ProverkaComponet == 0)
                                {
                                    query = $@"INSERT INTO MakersProcc('Name') values ('{TextComponet.Text.ToUpper()}')";
                                    cmd = new SQLiteCommand(query, connection);
                                    cmd.ExecuteScalar();
                                    MessageBox.Show("Компонет добавлен в базу", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Такой компонет уже внесет в базу", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                            else if (combtext == "Производитель материнской платы")
                            {
                                string query = $@"SELECT count() FROM MakersMaterPlat where MakersMaterPlat.Name ='{TextComponet.Text.ToUpper()}'";
                                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                                int ProverkaComponet = Convert.ToInt32(cmd.ExecuteScalar());
                                if (ProverkaComponet == 0)
                                {
                                    query = $@"INSERT INTO MakersMaterPlat('Name') values ('{TextComponet.Text.ToUpper()}')";
                                    cmd = new SQLiteCommand(query, connection);
                                    cmd.ExecuteScalar();
                                    MessageBox.Show("Компонет добавлен в базу", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Такой компонет уже внесет в базу", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                            else if (combtext == "Производитель видеокарты")
                            {
                                string query = $@"SELECT count() FROM MakersVideoCard where MakersVideoCard.Name = '{TextComponet.Text.ToUpper()}'";
                                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                                int ProverkaComponet = Convert.ToInt32(cmd.ExecuteScalar());
                                if (ProverkaComponet == 0)
                                {
                                    query = $@"INSERT INTO MakersVideoCard('Name') values ('{TextComponet.Text.ToUpper()}')";
                                    cmd = new SQLiteCommand(query, connection);
                                    cmd.ExecuteScalar();
                                    MessageBox.Show("Компонет добавлен в базу", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Такой компонет уже внесет в базу", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                    }
                }catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void ClearData()
        {
            CombKruterui.Text = string.Empty;
            TextComponet.Text = string.Empty;
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAddcomponet_Click(object sender, RoutedEventArgs e)
        {
            AddComponet();
        }

        private void TextComponet_TextChanged(object sender, TextChangedEventArgs e)
        {
            //CheckerText();
        }

        private void CombKruterui_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //CheckerText();
        }
        
    }
}

    

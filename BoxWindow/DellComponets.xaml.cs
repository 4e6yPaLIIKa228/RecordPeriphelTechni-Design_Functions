using RecordPeriphelTechniс.Connection;
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

namespace RecordPeriphelTechniс.BoxWindow
{
    /// <summary>
    /// Логика взаимодействия для DellComponets.xaml
    /// </summary>
    public partial class DellComponets : Window
    {
        public DellComponets()
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
        
        public void LoadComboBox()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                connection.Open();
                try
                {
                    String combtext = CombKruterui.Text;                   
                    if (combtext == "Организация")
                    {
                        string query = $@"SELECT * FROM Organiz";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        DataTable dt = new DataTable("Organiz");
                        SDA.Fill(dt);
                        CombNazvanue.ItemsSource = dt.DefaultView;
                        CombNazvanue.DisplayMemberPath = "NameOrg";
                        CombNazvanue.SelectedValuePath = "ID";
                    }
                    else if (combtext == "Производитель процессора")
                    {
                        string query = $@"SELECT * FROM MakersProcc";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        DataTable dt = new DataTable("MakersProcc");
                        SDA.Fill(dt);
                        CombNazvanue.ItemsSource = dt.DefaultView;
                        CombNazvanue.DisplayMemberPath = "Name";
                        CombNazvanue.SelectedValuePath = "ID";
                    }
                    else if (combtext == "Производитель материнской платы")
                    {
                        string query = $@"SELECT * FROM MakersMaterPlat";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        DataTable dt = new DataTable("MakersMaterPlat");
                        SDA.Fill(dt);
                        CombNazvanue.ItemsSource = dt.DefaultView;
                        CombNazvanue.DisplayMemberPath = "Name";
                        CombNazvanue.SelectedValuePath = "ID";
                    }
                    else if (combtext == "Производитель видеокарты")
                    {
                        string query = $@"SELECT * FROM MakersVideoCard";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        DataTable dt = new DataTable("MakersVideoCard");
                        SDA.Fill(dt);
                        CombNazvanue.ItemsSource = dt.DefaultView;
                        CombNazvanue.DisplayMemberPath = "Name";
                        CombNazvanue.SelectedValuePath = "ID";
                    }                   

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void DellComponet()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                connection.Open();
                try
                {
                    if (String.IsNullOrEmpty(CombKruterui.Text) || String.IsNullOrEmpty(CombNazvanue.Text))
                    {
                        CheckerText();
                        MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                        if (MessageBox.Show("Вы уверены что хотите удалить этот компонет?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            String combtext = CombKruterui.Text;
                            if (combtext == "Организация")
                            {
                                bool NumKab = int.TryParse(CombNazvanue.SelectedValue.ToString(), out int IDOrg);
                                string query = $@"SELECT COUNT() FROM MenuPerTech WHERE IDOrganiz= '{IDOrg}'";
                                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                                int ProverkaComponet = Convert.ToInt32(cmd.ExecuteScalar());
                                if (ProverkaComponet == 0)
                                {                                    
                                    query = $@"Delete from Organiz WHERE ID = '{IDOrg}'";
                                    cmd = new SQLiteCommand(query, connection);
                                    cmd.ExecuteScalar();
                                    MessageBox.Show("Название удалено из базы", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                                    ClearData();
                                }
                                else
                                {
                                    MessageBox.Show("Название используется в данных", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                            else if (combtext == "Производитель процессора")
                            {
                                bool NumKab = int.TryParse(CombNazvanue.SelectedValue.ToString(), out int IDMaker);
                                string query = $@"SELECT COUNT() FROM Procces WHERE IDMaker= '{IDMaker}'";
                                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                                int ProverkaComponet = Convert.ToInt32(cmd.ExecuteScalar());
                                if (ProverkaComponet == 0)
                                {
                                    query = $@"Delete from MakersProcc WHERE ID = '{IDMaker}'";
                                    cmd = new SQLiteCommand(query, connection);
                                    cmd.ExecuteScalar();
                                    MessageBox.Show("Название удалено из базы", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                                    ClearData();
                                }
                                else
                                {
                                    MessageBox.Show("Название используется в данных", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                            else if (combtext == "Производитель материнской платы")
                            {
                                bool NumKab = int.TryParse(CombNazvanue.SelectedValue.ToString(), out int IDMaker);
                                string query = $@"SELECT COUNT() FROM MaterPlatas WHERE IDMaker= '{IDMaker}'";
                                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                                int ProverkaComponet = Convert.ToInt32(cmd.ExecuteScalar());
                                if (ProverkaComponet == 0)
                                {
                                    query = $@"Delete from MakersMaterPlat WHERE ID = '{IDMaker}'";
                                    cmd = new SQLiteCommand(query, connection);
                                    cmd.ExecuteScalar();
                                    MessageBox.Show("Название удалено из базы", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                                    ClearData();
                                }
                                else
                                {
                                    MessageBox.Show("Название используется в данных", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                            else if (combtext == "Производитель видеокарты")
                            {
                                bool NumKab = int.TryParse(CombNazvanue.SelectedValue.ToString(), out int IDMaker);
                                string query = $@"SELECT COUNT() FROM VideoCards  WHERE IDMaker= '{IDMaker}'";
                                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                                int ProverkaComponet = Convert.ToInt32(cmd.ExecuteScalar());
                                if (ProverkaComponet == 0)
                                {
                                    query = $@"Delete from MakersVideoCard WHERE ID = '{IDMaker}'";
                                    cmd = new SQLiteCommand(query, connection);
                                    cmd.ExecuteScalar();
                                    MessageBox.Show("Название удалено из базы", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                                    ClearData();
                                }
                                else
                                {
                                    MessageBox.Show("Название используется в данных", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
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
        }

        private void CombSearchInfo_DropDownClosed(object sender, EventArgs e)
        {
            String combtext = CombKruterui.Text;
           // MessageBox.Show(combtext);
         //   LoadComboBox();
        }

        public void CheckerText()
        {
            SimpleComand.CheckComboBox(CombKruterui);
            SimpleComand.CheckComboBox(CombNazvanue);
        } //Проверка пустых строк(подсветка)

        public void ClearData()
        {
            CombKruterui.Text = string.Empty;
            CombNazvanue.Text = string.Empty;
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnDellcomponet_Click(object sender, RoutedEventArgs e)
        {
            DellComponet();
        }

        private void TextComponet_TextChanged(object sender, TextChangedEventArgs e)
        {
          //  CheckerText();
        }

        private void CombKruterui_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // CheckerText();
        }
    }
}

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
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        public AdminPanel()
        {
            InitializeComponent();
            LoadDB_Users();
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

        public void LoadDB_Users()
        {
            try
            {
               using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
               
                    connection.Open();
                    string query = $@"SELECT  Users.ID,Users.Login, Users.Passoword,Users.Surname,Users.Name,Users.MiddleName,Users.DataRegist, StatusUsers.StatusUser FROM Users
                                JOIN StatusUsers on Users.IDStasus = StatusUsers.ID";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    DataTable DT = new DataTable("MenuPerTech");
                    SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    SDA.Fill(DT);
                    ListUsers.ItemsSource = DT.DefaultView;
                    cmd.ExecuteNonQuery();                             


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
    }
}

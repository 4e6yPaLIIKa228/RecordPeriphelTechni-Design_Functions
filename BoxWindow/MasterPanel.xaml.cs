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
                    string query = $@"SELECT RepairDevice.ID, RepairDevice.IDMaster, RepairDevice.IDDevice, StatusApplications.NameStatus as Status , Users.Surname as Master,
 RepairDevice.DateAppeals, RepairDevice.Comment  FROM RepairDevice
 JOIN StatusApplications on RepairDevice.IDStatus  =  StatusApplications.ID
 JOIN Users on RepairDevice.IDMaster = Users.ID";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    DataTable DT = new DataTable("RepairDevice");
                    SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    SDA.Fill(DT);
                    ListApplications.ItemsSource = DT.DefaultView;
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}

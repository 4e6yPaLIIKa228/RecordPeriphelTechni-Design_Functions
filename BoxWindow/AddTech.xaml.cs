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
using RecordPeriphelTechniс.Connection;


namespace RecordPeriphelTechniс.BoxWindow
{
    /// <summary>
    /// Логика взаимодействия для AddTech.xaml
    /// </summary>
    public partial class AddTech : Window
    {
        int IDMenuPerTech = 0, IDTypeTech=0, Proverka=0, Proverka2=0,IDComponets = 0, IDProcces=0, IDMaterPlat=0, IDVideoCard=0,IDRams=0, IDSlot1=0;
        public AddTech()
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
                    string query1 = $@"SELECT * FROM TypeTechs"; // 
                    string query2 = $@"SELECT * FROM Organiz"; // 
                                                               // 
                    string query4 = $@"SELECT * FROM Status"; // 
                    string query5 = $@"SELECT * FROM Works"; // 
                    string query6 = $@"SELECT * FROM MakersProcc"; // 
                    string query7 = $@"SELECT * FROM MakersMaterPlat"; // 
                    string query8 = $@"SELECT * FROM MakersVideoCard"; // 


                    //----------------------------------------------
                    SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                    SQLiteCommand cmd2 = new SQLiteCommand(query2, connection);
                    //SQLiteCommand cmd3 = new SQLiteCommand(query3, connection);
                    SQLiteCommand cmd4 = new SQLiteCommand(query4, connection);
                    SQLiteCommand cmd5 = new SQLiteCommand(query5, connection);
                    SQLiteCommand cmd6 = new SQLiteCommand(query6, connection);
                    SQLiteCommand cmd7 = new SQLiteCommand(query7, connection);
                    SQLiteCommand cmd8 = new SQLiteCommand(query8, connection);

                    //----------------------------------------------
                    SQLiteDataAdapter SDA1 = new SQLiteDataAdapter(cmd1);
                    SQLiteDataAdapter SDA2 = new SQLiteDataAdapter(cmd2);
                    //SQLiteDataAdapter SDA3 = new SQLiteDataAdapter(cmd3);
                    SQLiteDataAdapter SDA4 = new SQLiteDataAdapter(cmd4);
                    SQLiteDataAdapter SDA5 = new SQLiteDataAdapter(cmd5);
                    SQLiteDataAdapter SDA6 = new SQLiteDataAdapter(cmd6);
                    SQLiteDataAdapter SDA7 = new SQLiteDataAdapter(cmd7);
                    SQLiteDataAdapter SDA8 = new SQLiteDataAdapter(cmd8);
                    //----------------------------------------------
                    DataTable dt1 = new DataTable("TypeTechs");
                    DataTable dt2 = new DataTable("Organiz");
                    // DataTable dt3 = new DataTable("NumberKabs");
                    DataTable dt4 = new DataTable("Status");
                    DataTable dt5 = new DataTable("Works");
                    DataTable dt6 = new DataTable("MakersProcc");
                    DataTable dt7 = new DataTable("MakersMaterPlat");
                    DataTable dt8 = new DataTable("MakersVideoCard");
                    //----------------------------------------------
                    SDA1.Fill(dt1);
                    SDA2.Fill(dt2);
                    //SDA3.Fill(dt3);
                    SDA4.Fill(dt4);
                    SDA5.Fill(dt5);
                    SDA6.Fill(dt6);
                    SDA7.Fill(dt7);
                    SDA8.Fill(dt8);
                    //----------------------------------------------
                    CombTypeTech.ItemsSource = dt1.DefaultView;
                    CombTypeTech.DisplayMemberPath = "NameType";
                    CombTypeTech.SelectedValuePath = "ID";
                    //----------------------------------------------
                    CombIDOrgamniz.ItemsSource = dt2.DefaultView;
                    CombIDOrgamniz.DisplayMemberPath = "NameOrg";
                    CombIDOrgamniz.SelectedValuePath = "ID";
                    //----------------------------------------------
                    //----------------------------------------------
                    CombIDStatus.ItemsSource = dt4.DefaultView;
                    CombIDStatus.DisplayMemberPath = "NameStatus";
                    CombIDStatus.SelectedValuePath = "ID";
                    //----------------------------------------------
                    CombIDWorks.ItemsSource = dt5.DefaultView;
                    CombIDWorks.DisplayMemberPath = "NameWorks";
                    CombIDWorks.SelectedValuePath = "ID";
                    //----------------------------------------------
                    CombProccMaker.ItemsSource = dt6.DefaultView;
                    CombProccMaker.DisplayMemberPath = "Name";
                    CombProccMaker.SelectedValuePath = "ID";
                    //----------------------------------------------
                    CombMatePlatMaker.ItemsSource = dt7.DefaultView;
                    CombMatePlatMaker.DisplayMemberPath = "Name";
                    CombMatePlatMaker.SelectedValuePath = "ID";
                    //----------------------------------------------
                    CombVidieoMaker.ItemsSource = dt8.DefaultView;
                    CombVidieoMaker.DisplayMemberPath = "Name";
                    CombVidieoMaker.SelectedValuePath = "ID";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            AddOsnova();
            if (IDTypeTech == 1 && Proverka == 0)
            {
                
                AddPcCompet();
                if (Proverka2 != 0)
                {
                    AddOsnova();
                    AddRams();
                    AddComponets();
                    UpdateMenuPer();
                    MessageBox.Show("Данные добавлены");
                }
                
            }
        }         
        
        public void AddOsnova()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                connection.Open();
                if (String.IsNullOrEmpty(CombIDOrgamniz.Text) || String.IsNullOrEmpty(TextIDKabuneta.Text) || String.IsNullOrEmpty(TextNumber.Text) || String.IsNullOrEmpty(CombIDStatus.Text) || String.IsNullOrEmpty(TextName.Text) ||
                    String.IsNullOrEmpty(CombIDWorks.Text))
                {
                    MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    bool resultType = int.TryParse(CombTypeTech.SelectedValue.ToString(), out  IDTypeTech);
                    bool resultClass = int.TryParse(CombIDOrgamniz.SelectedValue.ToString(), out int  id);
                    bool resultKab = int.TryParse(CombIDStatus.SelectedValue.ToString(), out int  id2);
                    bool resultCon = int.TryParse(CombIDWorks.SelectedValue.ToString(), out int  id3);
                    string query = $@"SELECT count (Number) FROM MenuPerTech WHERE Number = '{TextNumber.Text}'";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    Proverka = Convert.ToInt32(cmd.ExecuteScalar());

                    if (Proverka != 1 && IDTypeTech != 1)
                    {
                        if (Proverka != 1)
                        {

                            query = $@"INSERT INTO MenuPerTech('IDTypeTech','IDOrganiz','Kabunet','Number','IDComponets','Name','StartWork','EndWork','IDStatus','IDWorks','Comments')
                                        values (@IDTypeTech,@IDOrganiz,@Kabunet,@Number,@IDComponets,@Name,@StartWork,@EndWork,@IDStatus,@IDWorks,@Comments)";
                            cmd = new SQLiteCommand(query, connection);
                            try
                            {
                                cmd.Parameters.AddWithValue("@IDTypeTech", IDTypeTech);
                                cmd.Parameters.AddWithValue("@IDOrganiz", id);
                                cmd.Parameters.AddWithValue("@Kabunet", TextIDKabuneta.Text);
                                cmd.Parameters.AddWithValue("@Number", TextNumber.Text);
                                cmd.Parameters.AddWithValue("@IDComponets", null);
                                cmd.Parameters.AddWithValue("@Name", TextName.Text);
                                cmd.Parameters.AddWithValue("@StartWork", TextDataStart.Text);
                                cmd.Parameters.AddWithValue("@EndWork", TextDataEnd.Text);
                                cmd.Parameters.AddWithValue("@IDStatus", id2);
                                cmd.Parameters.AddWithValue("@IDWorks", id3);
                                cmd.Parameters.AddWithValue("@Comments", TextComments.Text);
                                cmd.ExecuteNonQuery();
                            }
                            catch (SQLiteException ex)
                            {
                                MessageBox.Show("Error: " + ex.Message);
                            }
                            query = $@"SELECT ID FROM MenuPerTech WHERE IDTypeTech = '{IDTypeTech}' and IDOrganiz = '{id}' and Kabunet = '{TextIDKabuneta.Text}' and  Number = '{TextNumber.Text}' and Name = '{TextName.Text}' and StartWork = '{TextDataStart.Text}' and EndWork = '{TextDataEnd.Text}' and  IDStatus = '{id2}' and  IDWorks ='{id3}' and Comments = '{TextComments.Text}' ";
                            cmd = new SQLiteCommand(query, connection);
                            IDMenuPerTech = Convert.ToInt32(cmd.ExecuteScalar());
                            MessageBox.Show("Данные добавлены");
                        }
                        else
                        {
                            MessageBox.Show("Измените номер техники");
                        }

                    }
                    else
                    {
                       // MessageBox.Show("Измените номер техники");
                    }
                    if (Proverka2 != 0)
                    {
                        query = $@"INSERT INTO MenuPerTech('IDTypeTech','IDOrganiz','Kabunet','Number','IDComponets','Name','StartWork','EndWork','IDStatus','IDWorks','Comments')
                                        values (@IDTypeTech,@IDOrganiz,@Kabunet,@Number,@IDComponets,@Name,@StartWork,@EndWork,@IDStatus,@IDWorks,@Comments)";
                        cmd = new SQLiteCommand(query, connection);
                        try
                        {
                            cmd.Parameters.AddWithValue("@IDTypeTech", IDTypeTech);
                            cmd.Parameters.AddWithValue("@IDOrganiz", id);
                            cmd.Parameters.AddWithValue("@Kabunet", TextIDKabuneta.Text);
                            cmd.Parameters.AddWithValue("@Number", TextNumber.Text);
                            cmd.Parameters.AddWithValue("@IDComponets", 1);
                            cmd.Parameters.AddWithValue("@Name", TextName.Text);
                            cmd.Parameters.AddWithValue("@StartWork", TextDataStart.Text);
                            cmd.Parameters.AddWithValue("@EndWork", TextDataEnd.Text);
                            cmd.Parameters.AddWithValue("@IDStatus", id2);
                            cmd.Parameters.AddWithValue("@IDWorks", id3);
                            cmd.Parameters.AddWithValue("@Comments", TextComments.Text);
                            cmd.ExecuteNonQuery();
                        }
                        catch (SQLiteException ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                        query = $@"SELECT ID FROM MenuPerTech WHERE IDTypeTech = '{IDTypeTech}' and IDOrganiz ='{id}' and Kabunet = '{TextIDKabuneta.Text}' and  Number = '{TextNumber.Text}' and Name = '{TextName.Text}' and StartWork = '{TextDataStart.Text}' and EndWork = '{TextDataEnd.Text}' and  IDStatus = '{id2}' and  IDWorks = '{id3}' and Comments = '{TextComments.Text}' ";
                        cmd = new SQLiteCommand(query, connection);
                        IDMenuPerTech = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
        }

        
        public void AddPcCompet()
        {
            if (String.IsNullOrEmpty(TextProccModel.Text) || String.IsNullOrEmpty(TextSpeed.Text) || String.IsNullOrEmpty(CombProccMaker.Text) || String.IsNullOrEmpty(TextMatePlatModel.Text) || String.IsNullOrEmpty(CombMatePlatMaker.Text) ||
                       String.IsNullOrEmpty(CombMatePlatMaker.Text) || String.IsNullOrEmpty(TextRAMModel1.Text) || String.IsNullOrEmpty(TextVmemory1.Text) || String.IsNullOrEmpty(TextTypeMemory1.Text) ||
                       String.IsNullOrEmpty(TextMaker1.Text) || String.IsNullOrEmpty(TextVideoModel.Text) || String.IsNullOrEmpty(TextVideoMemory.Text) || String.IsNullOrEmpty(CombVidieoMaker.Text))
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Proverka2 = 0;
            }
            else
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    connection.Open();
                    bool resultTitl = int.TryParse(CombProccMaker.SelectedValue.ToString(), out int id4);
                    bool resultBrand = int.TryParse(CombMatePlatMaker.SelectedValue.ToString(), out int id5);
                    bool resultModel = int.TryParse(CombVidieoMaker.SelectedValue.ToString(), out int id6);

                    string query = $@"INSERT INTO Procces ('Model', 'Speed','IDMaker') VALUES ('{TextProccModel.Text}','{TextSpeed.Text}','{id4}');";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    query = $@"SELECT ID FROM Procces WHERE Model = '{TextProccModel.Text}' and Speed = '{TextSpeed.Text}' and IDMaker = '{id4}' ";
                    cmd = new SQLiteCommand(query, connection);
                    IDProcces = Convert.ToInt32(cmd.ExecuteScalar());


                    query = $@"INSERT INTO MaterPlatas ('Model','IDMaker') VALUES ('{TextMatePlatModel.Text}','{id5}');";
                    cmd = new SQLiteCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    query = $@"SELECT ID FROM MaterPlatas  WHERE Model = '{TextMatePlatModel.Text}' and IDMaker = '{id5}' ";
                    cmd = new SQLiteCommand(query, connection);
                    IDMaterPlat = Convert.ToInt32(cmd.ExecuteScalar());

                    query = $@"INSERT INTO VideoCards ('Model','VVideoMemory','IDMaker') VALUES ('{TextVideoModel.Text}','{TextVideoMemory.Text}','{id6}');";
                    cmd = new SQLiteCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    query = $@"SELECT ID FROM VideoCards  WHERE Model = '{TextVideoModel.Text}' and VVideoMemory = '{TextVideoMemory.Text}' and IDMaker = '{id6}' ";
                    cmd = new SQLiteCommand(query, connection);
                    IDVideoCard = Convert.ToInt32(cmd.ExecuteScalar());

                    query = $@"INSERT INTO RAMs  ('Model1','Vmemory1','TypeMemory1','Maker1') VALUES ('{TextRAMModel1.Text}','{TextVmemory1.Text}','{TextTypeMemory1.Text}','{TextMaker1.Text}');";
                    cmd = new SQLiteCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    query = $@"SELECT ID FROM RAMs WHERE Model1 = '{TextRAMModel1.Text}' and Vmemory1 = '{TextVmemory1.Text}' and TypeMemory1 = '{TextTypeMemory1.Text}' and Maker1 = '{TextMaker1.Text}'";
                    cmd = new SQLiteCommand(query, connection);
                    IDRams = Convert.ToInt32(cmd.ExecuteScalar());                    
                    Proverka2 = 1;
                }
            }
        }

       
        public void AddRams()
        {
            try 
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    connection.Open();                    
                    if (TextRAMModel2.Text != "" && TextVmemory2.Text != "" && TextTypeMemory2.Text != "" && TextMaker2.Text != "")
                    {
                        MessageBox.Show("заполнено все2");
                        string query1 = $@"UPDATE RAMs SET Model2='{TextRAMModel2.Text}', Vmemory2='{TextVmemory2.Text}',TypeMemory2='{TextTypeMemory2.Text}',Maker2='{TextMaker2.Text}' WHERE ID='{IDRams}'";
                        SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                        cmd1.ExecuteNonQuery();
                    }
                    else if (TextRAMModel2.Text != "" && (TextVmemory2.Text == "" || TextTypeMemory2.Text == "" || TextMaker2.Text == ""))
                    {
                        //  MessageBox.Show("Дозаполните");
                    }
                    else if (TextRAMModel2.Text == "" && (TextVmemory2.Text == "" && TextTypeMemory2.Text == "" && TextMaker2.Text == ""))
                    {
                        MessageBox.Show("Все пусто");
                        string query1 = $@"UPDATE RAMs SET  Model2='Нет', Vmemory2='Нет',TypeMemory2='Нет',Maker2='Нет' WHERE ID='{IDRams}'";
                        SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                        cmd1.ExecuteNonQuery();
                    }
                    else
                    {
                        // MessageBox.Show("Дозаполните");
                    }
                    if (TextRAMModel3.Text != "" && TextVmemory3.Text != "" && TextTypeMemory3.Text != "" && TextMaker3.Text != "")
                    {
                        MessageBox.Show("заполнено все3");
                        string query1 = $@"UPDATE RAMs SET Model3 ='{TextRAMModel3.Text}', Vmemory3='{TextVmemory3.Text}',TypeMemory3='{TextTypeMemory3.Text}',Maker3='{TextMaker3.Text}' WHERE ID='{IDRams}'";
                        SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                        cmd1.ExecuteNonQuery();
                    }
                    else if (TextRAMModel3.Text != "" && (TextVmemory3.Text == "" || TextTypeMemory3.Text == "" || TextMaker3.Text == ""))
                    {
                        //  MessageBox.Show("Дозаполните");
                    }
                    else if (TextRAMModel3.Text == "" && (TextVmemory3.Text == "" && TextTypeMemory3.Text == "" && TextMaker3.Text == ""))
                    {
                        //  MessageBox.Show("Все пусто");
                        string query1 = $@"UPDATE RAMs SET  Model3='Нет', Vmemory3='Нет',TypeMemory3='Нет',Maker3='Нет' WHERE ID='{IDRams}'";
                        SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                        cmd1.ExecuteNonQuery();
                    }
                    else
                    {
                        // MessageBox.Show("Дозаполните");
                    }
                    if (TextRAMModel4.Text != "" && TextVmemory4.Text != "" && TextTypeMemory4.Text != "" && TextMaker4.Text != "")
                    {
                        MessageBox.Show("заполнено все4");
                        string query1 = $@"UPDATE RAMs SET Model4='{TextRAMModel4.Text}', Vmemory4='{TextVmemory4.Text}',TypeMemory4='{TextTypeMemory4.Text}',Maker4='{TextMaker4.Text}' WHERE ID='{IDRams}'";
                        SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                        cmd1.ExecuteNonQuery();
                    }
                    else if (TextRAMModel4.Text != "" && (TextVmemory4.Text == "" || TextTypeMemory4.Text == "" || TextMaker4.Text == ""))
                    {
                        //  MessageBox.Show("Дозаполните");
                    }
                    else if (TextRAMModel4.Text == "" && (TextVmemory4.Text == "" && TextTypeMemory4.Text == "" && TextMaker4.Text == ""))
                    {
                        //  MessageBox.Show("Все пусто");
                        string query1 = $@"UPDATE RAMs SET  Model4='Нет', Vmemory4='Нет',TypeMemory4='Нет',Maker4='Нет' WHERE ID='{IDRams}'";
                        SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                        cmd1.ExecuteNonQuery();
                    }
                    else
                    {
                        // MessageBox.Show("Дозаполните");
                    }
                   
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        public void AddComponets()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    connection.Open();
                    string query = $@"INSERT INTO Components  ('IDProcces', 'IDMaterPlata','IDRAM','IDVideo') VALUES ('{IDProcces}','{IDMaterPlat}','{IDRams}','{IDVideoCard}') ";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    query = $@"SELECT ID FROM Components WHERE IDProcces = '{IDProcces}' and IDMaterPlata = '{IDMaterPlat}' and IDRAM = '{IDRams}' and IDVideo = '{IDVideoCard}'";
                    cmd = new SQLiteCommand(query, connection);
                    IDComponets = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
        public void UpdateMenuPer()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    connection.Open();
                    string query = $@"UPDATE MenuPerTech SET IDComponets ='{IDComponets}' WHERE ID ='{IDMenuPerTech}' ";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Expander1_Expanded(object sender, RoutedEventArgs e)
        {
            Expander2.IsExpanded = false;
            Expander3.IsExpanded = false;
            Expander4.IsExpanded = false;
        }
        private void Expander2_Expanded(object sender, RoutedEventArgs e)
        {
            Expander1.IsExpanded = false;
            Expander3.IsExpanded = false;
            Expander4.IsExpanded = false;
        }
        private void Expander3_Expanded(object sender, RoutedEventArgs e)
        {
            Expander1.IsExpanded = false;
            Expander2.IsExpanded = false;
            Expander4.IsExpanded = false;
        }
        private void Expander4_Expanded(object sender, RoutedEventArgs e)
        {
            Expander1.IsExpanded = false;
            Expander2.IsExpanded = false;
            Expander3.IsExpanded = false;
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
            //Environment.Exit(0);
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

    }
}

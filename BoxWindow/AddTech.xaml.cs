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
        int IDMenuPerTech = 0, IDTypeTech = 0, IDTypeTechCombBox = 0, ProverkaOsnova = 0, ProverkaNumber = 0, ProverkaPcCompont=0, ProverkaRams = 0, IDComponets = 0, IDProcces=0, IDMaterPlat=0, IDVideoCard=0,IDRams=0, IDSlot1=0;
        string TextRamInfo2 = "", TextRamInfo3 = "", TextRamInfo4 = "";
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

            if (IDTypeTechCombBox == 0)
            {
                AddOsnovaPC();               
            }
            else
            {
                AddOsnovaPerTech();
            }
           
        }
        public void AddOsnovaPerTech()
        {
            try
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
                        bool resultType = int.TryParse(CombTypeTech.SelectedValue.ToString(), out IDTypeTech);
                        bool resultClass = int.TryParse(CombIDOrgamniz.SelectedValue.ToString(), out int id);
                        bool resultKab = int.TryParse(CombIDStatus.SelectedValue.ToString(), out int id2);
                        bool resultCon = int.TryParse(CombIDWorks.SelectedValue.ToString(), out int id3);
                        string query = $@"SELECT count (Number) FROM MenuPerTech WHERE Number = '{TextNumber.Text}'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        ProverkaNumber = Convert.ToInt32(cmd.ExecuteScalar());
                        if (ProverkaNumber != 1)
                        {
                            
                            query = $@"INSERT INTO MenuPerTech('IDTypeTech','IDOrganiz','Kabunet','Number','IDComponets','Name','StartWork','EndWork','IDStatus','IDWorks','Comments')
                            values ('{IDTypeTech}','{id}','{TextIDKabuneta.Text}','{TextNumber.Text}','','{TextName.Text}','{TextDataStart.Text}','{TextDataEnd.Text}','{id2}','{id3}','{TextComments.Text}')";
                            cmd = new SQLiteCommand(query, connection);
                            cmd.ExecuteNonQuery();
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
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
        public void AddOsnovaPC()
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
                    bool resultType = int.TryParse(CombTypeTech.SelectedValue.ToString(), out IDTypeTech);
                    bool resultClass = int.TryParse(CombIDOrgamniz.SelectedValue.ToString(), out int id);
                    bool resultKab = int.TryParse(CombIDStatus.SelectedValue.ToString(), out int id2);
                    bool resultCon = int.TryParse(CombIDWorks.SelectedValue.ToString(), out int id3);
                    string query = $@"SELECT count (Number) FROM MenuPerTech WHERE Number = '{TextNumber.Text}'";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    ProverkaNumber = Convert.ToInt32(cmd.ExecuteScalar());
                    if (ProverkaNumber != 1)
                    {
                        CheckRams();
                        if (ProverkaRams == 0)
                        {
                            AddPcCompet();
                            if (ProverkaPcCompont == 1)
                            {
                                query = $@"INSERT INTO MenuPerTech('IDTypeTech','IDOrganiz','Kabunet','Number','IDComponets','Name','StartWork','EndWork','IDStatus','IDWorks','Comments')
                                values ('{IDTypeTech}','{id}','{TextIDKabuneta.Text}','{TextNumber.Text}','{null}','{TextName.Text}','{TextDataStart.Text}','{TextDataEnd.Text}','{id2}','{id3}','{TextComments.Text}')";
                                cmd = new SQLiteCommand(query, connection);
                                cmd.ExecuteNonQuery();
                                query = $@"SELECT ID FROM MenuPerTech WHERE IDTypeTech = '{IDTypeTech}' and IDOrganiz = '{id}' and Kabunet = '{TextIDKabuneta.Text}' and  Number = '{TextNumber.Text}' and Name = '{TextName.Text}' and StartWork = '{TextDataStart.Text}' and EndWork = '{TextDataEnd.Text}' and  IDStatus = '{id2}' and  IDWorks ='{id3}' and Comments = '{TextComments.Text}' ";
                                cmd = new SQLiteCommand(query, connection);
                                IDMenuPerTech = Convert.ToInt32(cmd.ExecuteScalar());
                                ProverkaOsnova = 0;
                                AddRams();
                                AddComponets();
                                UpdateMenuPer();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Измените номер техники");
                        ProverkaOsnova = 1;
                    }
                }
            }
        }

        private void CombTypeTech_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IDTypeTechCombBox = CombTypeTech.SelectedIndex;
        }

        public void AddPcCompet()
        {
            if (String.IsNullOrEmpty(TextProccModel.Text) || String.IsNullOrEmpty(TextSpeed.Text) || String.IsNullOrEmpty(CombProccMaker.Text) || String.IsNullOrEmpty(TextMatePlatModel.Text) || String.IsNullOrEmpty(CombMatePlatMaker.Text) ||
                       String.IsNullOrEmpty(CombMatePlatMaker.Text) || String.IsNullOrEmpty(TextRAMModel1.Text) || String.IsNullOrEmpty(TextVmemory1.Text) || String.IsNullOrEmpty(TextTypeMemory1.Text) ||
                       String.IsNullOrEmpty(TextMaker1.Text) || String.IsNullOrEmpty(TextVideoModel.Text) || String.IsNullOrEmpty(TextVideoMemory.Text) || String.IsNullOrEmpty(CombVidieoMaker.Text))
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                ProverkaPcCompont = 0;
            }
            else
            {
                try
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
                        query = $@"SELECT count (ID) FROM Procces WHERE Model = '{TextProccModel.Text}' and Speed = '{TextSpeed.Text}' and IDMaker = '{id4}'";
                        cmd = new SQLiteCommand(query, connection);
                        int CheckIDProcces = Convert.ToInt32(cmd.ExecuteScalar());
                        int IDProccesCheck = 0;
                        if (CheckIDProcces != 1)
                        {
                            while (CheckIDProcces >= 1)
                            {
                                query = $@"SELECT ID FROM Procces WHERE Model = '{TextProccModel.Text}' and Speed = '{TextSpeed.Text}' and IDMaker = '{id4}' and ID > '{IDProccesCheck}' ";
                                cmd = new SQLiteCommand(query, connection);
                                IDProccesCheck = Convert.ToInt32(cmd.ExecuteScalar());
                                query = $@"SELECT count (ID) FROM Procces WHERE Model = '{TextProccModel.Text}' and Speed = '{TextSpeed.Text}' and IDMaker = '{id4}' and ID > '{IDProccesCheck}'";
                                cmd = new SQLiteCommand(query, connection);
                                CheckIDProcces = Convert.ToInt32(cmd.ExecuteScalar());
                            }
                            IDProcces = IDProccesCheck;
                        }
                        else
                        {
                            query = $@"SELECT ID FROM Procces WHERE Model = '{TextProccModel.Text}' and Speed = '{TextSpeed.Text}' and IDMaker = '{id4}' ";
                            cmd = new SQLiteCommand(query, connection);
                            IDProcces = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        query = $@"INSERT INTO MaterPlatas ('Model','IDMaker') VALUES ('{TextMatePlatModel.Text}','{id5}');";
                        cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        query = $@"SELECT count (ID) FROM  MaterPlatas WHERE Model = '{TextMatePlatModel.Text}' and IDMaker = '{id5}'";
                        cmd = new SQLiteCommand(query, connection);
                        int CheckIDMaterPlat = Convert.ToInt32(cmd.ExecuteScalar());
                        int IDMaterPlatCheck = 0;
                        if (CheckIDMaterPlat != 0)
                        {
                            while (CheckIDMaterPlat >= 1)
                            {
                                query = $@"SELECT ID FROM MaterPlatas  WHERE Model = '{TextMatePlatModel.Text}' and IDMaker = '{id5}' and ID > '{IDMaterPlatCheck}' ";
                                cmd = new SQLiteCommand(query, connection);
                                IDMaterPlatCheck = Convert.ToInt32(cmd.ExecuteScalar());
                                query = $@"SELECT count (ID) FROM  MaterPlatas  WHERE Model = '{TextMatePlatModel.Text}' and IDMaker = '{id5}' and ID > '{IDMaterPlatCheck}'";
                                cmd = new SQLiteCommand(query, connection);
                                CheckIDMaterPlat = Convert.ToInt32(cmd.ExecuteScalar());
                            }
                            IDMaterPlat = IDMaterPlatCheck;
                        }
                        else
                        {
                            query = $@"SELECT ID FROM MaterPlatas  WHERE Model = '{TextMatePlatModel.Text}' and IDMaker = '{id5}' ";
                            cmd = new SQLiteCommand(query, connection);
                            IDMaterPlat = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        query = $@"INSERT INTO VideoCards ('Model','VVideoMemory','IDMaker') VALUES ('{TextVideoModel.Text}','{TextVideoMemory.Text}','{id6}');";
                        cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        query = $@"SELECT count (ID) FROM  VideoCards  WHERE Model = '{TextVideoModel.Text}' and VVideoMemory = '{TextVideoMemory.Text}' and IDMaker = '{id6}'";
                        cmd = new SQLiteCommand(query, connection);
                        int CheckIDVideCards = Convert.ToInt32(cmd.ExecuteScalar());
                        int IDVideoCardsCheck = 0;
                        if (CheckIDVideCards != 0)
                        {
                            while (CheckIDVideCards >= 1)
                            {
                                query = $@"SELECT ID FROM VideoCards  WHERE Model = '{TextVideoModel.Text}' and VVideoMemory = '{TextVideoMemory.Text}' and IDMaker = '{id6}' and ID > '{IDVideoCardsCheck}' ";
                                cmd = new SQLiteCommand(query, connection);
                                IDVideoCardsCheck = Convert.ToInt32(cmd.ExecuteScalar());
                                query = $@"SELECT count (ID) FROM  VideoCards  WHERE Model = '{TextVideoModel.Text}' and VVideoMemory = '{TextVideoMemory.Text}' and IDMaker = '{id6}' and ID > '{IDVideoCardsCheck}'";
                                cmd = new SQLiteCommand(query, connection);
                                CheckIDVideCards = Convert.ToInt32(cmd.ExecuteScalar());
                            }
                            IDVideoCard = IDVideoCardsCheck;
                        }
                        else
                        {
                            query = $@"SELECT ID FROM VideoCards  WHERE Model = '{TextVideoModel.Text}' and VVideoMemory = '{TextVideoMemory.Text}' and IDMaker = '{id6}' ";
                            cmd = new SQLiteCommand(query, connection);
                            IDVideoCard = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        query = $@"INSERT INTO RAMs  ('Model1','Vmemory1','TypeMemory1','Maker1') VALUES ('{TextRAMModel1.Text}','{TextVmemory1.Text}','{TextTypeMemory1.Text}','{TextMaker1.Text}');";
                        cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        query = $@"SELECT count (ID) FROM  RAMs WHERE Model1 = '{TextRAMModel1.Text}' and Vmemory1 = '{TextVmemory1.Text}' and TypeMemory1 = '{TextTypeMemory1.Text}' and Maker1 = '{TextMaker1.Text}'";
                        cmd = new SQLiteCommand(query, connection);
                        int CheckIDRams = Convert.ToInt32(cmd.ExecuteScalar());
                        int IDRamsCheck = 0;
                        if (CheckIDRams >= 1)
                        {
                            while (CheckIDRams != 0)
                            {
                                query = $@"SELECT ID FROM  RAMs WHERE Model1 = '{TextRAMModel1.Text}' and Vmemory1 = '{TextVmemory1.Text}' and TypeMemory1 = '{TextTypeMemory1.Text}' and Maker1 = '{TextMaker1.Text}' and ID > '{IDRamsCheck}' ";
                                cmd = new SQLiteCommand(query, connection);
                                IDRamsCheck = Convert.ToInt32(cmd.ExecuteScalar());
                                query = $@"SELECT count (ID) FROM  RAMs WHERE Model1 = '{TextRAMModel1.Text}' and Vmemory1 = '{TextVmemory1.Text}' and TypeMemory1 = '{TextTypeMemory1.Text}' and Maker1 = '{TextMaker1.Text}' and ID > '{IDRamsCheck}'";
                                cmd = new SQLiteCommand(query, connection);
                                CheckIDRams = Convert.ToInt32(cmd.ExecuteScalar());
                            }
                            IDRams = IDRamsCheck;
                        }
                        else
                        {
                            query = $@"SELECT ID FROM RAMs WHERE Model1 = '{TextRAMModel1.Text}' and Vmemory1 = '{TextVmemory1.Text}' and TypeMemory1 = '{TextTypeMemory1.Text}' and Maker1 = '{TextMaker1.Text}'";
                            cmd = new SQLiteCommand(query, connection);
                            IDRams = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        ProverkaPcCompont = 1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void CheckRams()
        {
            ProverkaRams = 0;
            TextRamInfo2 = "Заполните во втором слоте : ";
            TextRamInfo3 = "Заполните в третьем слоте: " ;
            TextRamInfo4 = "Заполните в четвертом слоте: ";
            if (TextRAMModel2.Text != "" && TextVmemory2.Text != "" && TextTypeMemory2.Text != "" && TextMaker2.Text != "")
            {
                // MessageBox.Show("заполнено все2");               
                TextRamInfo2 = "";
            }
            else if (TextRAMModel2.Text == "" && (TextVmemory2.Text == "" && TextTypeMemory2.Text == "" && TextMaker2.Text == ""))
            {
                MessageBox.Show("Все пусто");
                string query1 = $@"UPDATE RAMs SET  Model2='Нет', Vmemory2='Нет',TypeMemory2='Нет',Maker2='Нет' WHERE ID='{IDRams}'";               
                TextRamInfo2 = "";
            }
            else
            {
                if (TextRAMModel2.Text == "")
                {
                    TextRamInfo2 = TextRamInfo2 + "Модель, ";
                }
                if (TextVmemory2.Text == "")
                {
                    TextRamInfo2 = TextRamInfo2 + "Объем памяти, ";
                }
                if (TextTypeMemory2.Text == "")
                {
                    TextRamInfo2 = TextRamInfo2 + "Тип памяти, ";
                }
                if (TextMaker2.Text == "")
                {
                    TextRamInfo2 = TextRamInfo2 + "Производитель ";
                }
                ProverkaRams = 1;
            }
            if (TextRAMModel3.Text != "" && TextVmemory3.Text != "" && TextTypeMemory3.Text != "" && TextMaker3.Text != "")
            {                           
                TextRamInfo3 = "";
            }
            
            else if (TextRAMModel3.Text == "" && (TextVmemory3.Text == "" && TextTypeMemory3.Text == "" && TextMaker3.Text == ""))
            {
                //  MessageBox.Show("Все пусто");
                string query1 = $@"UPDATE RAMs SET  Model3='Нет', Vmemory3='Нет',TypeMemory3='Нет',Maker3='Нет' WHERE ID='{IDRams}'";               
                TextRamInfo3 = "";
            }
            else
            {
                if (TextRAMModel3.Text == "")
                {
                    TextRamInfo3 = TextRamInfo3 + "Модель, ";
                }
                if (TextVmemory3.Text == "")
                {
                    TextRamInfo3 = TextRamInfo3 + "Объем памяти, ";
                }
                if (TextTypeMemory3.Text == "")
                {
                    TextRamInfo3 = TextRamInfo3 + "Тип памяти, ";
                }
                if (TextMaker3.Text == "")
                {
                    TextRamInfo3 = TextRamInfo3 + "Производитель ";
                }
                ProverkaRams = 1;
            }
            if (TextRAMModel4.Text != "" && TextVmemory4.Text != "" && TextTypeMemory4.Text != "" && TextMaker4.Text != "")
            {
               // MessageBox.Show("заполнено все4");               
                TextRamInfo4 = "";
            }            
            else if (TextRAMModel4.Text == "" && (TextVmemory4.Text == "" && TextTypeMemory4.Text == "" && TextMaker4.Text == ""))
            {
                //  MessageBox.Show("Все пусто");               
                TextRamInfo4 = "";
            }
            else
            {
                if (TextRAMModel4.Text == "")
                {
                    TextRamInfo4 = TextRamInfo4 + "Модель, ";
                }
                if (TextVmemory4.Text == "")
                {
                    TextRamInfo4 = TextRamInfo4 + "Объем памяти, ";
                }
                if (TextTypeMemory4.Text == "")
                {
                    TextRamInfo4 = TextRamInfo4 + "Тип памяти, ";
                }
                if (TextMaker4.Text == "")
                {
                    TextRamInfo4 = TextRamInfo4 + "Производитель ";
                }
                ProverkaRams = 1;
            }
            if (ProverkaRams == 1)
            {
                MessageBox.Show(TextRamInfo2 + '\n' + TextRamInfo3 + '\n' + TextRamInfo4);
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
                       // MessageBox.Show("заполнено все2");
                        string query1 = $@"UPDATE RAMs SET Model2='{TextRAMModel2.Text}', Vmemory2='{TextVmemory2.Text}',TypeMemory2='{TextTypeMemory2.Text}',Maker2='{TextMaker2.Text}' WHERE ID='{IDRams}'";
                        SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                        cmd1.ExecuteNonQuery();
                    }                    
                    else if (TextRAMModel2.Text == "" && (TextVmemory2.Text == "" && TextTypeMemory2.Text == "" && TextMaker2.Text == ""))
                    {
                       // MessageBox.Show("Все пусто");
                        string query1 = $@"UPDATE RAMs SET  Model2='Нет', Vmemory2='Нет',TypeMemory2='Нет',Maker2='Нет' WHERE ID='{IDRams}'";
                        SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                        cmd1.ExecuteNonQuery();

                    }                   
                    if (TextRAMModel3.Text != "" && TextVmemory3.Text != "" && TextTypeMemory3.Text != "" && TextMaker3.Text != "")
                    {
                        //MessageBox.Show("заполнено все3");
                        string query1 = $@"UPDATE RAMs SET Model3 ='{TextRAMModel3.Text}', Vmemory3='{TextVmemory3.Text}',TypeMemory3='{TextTypeMemory3.Text}',Maker3='{TextMaker3.Text}' WHERE ID='{IDRams}'";
                        SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                        cmd1.ExecuteNonQuery();

                    }                    
                    else if (TextRAMModel3.Text == "" && (TextVmemory3.Text == "" && TextTypeMemory3.Text == "" && TextMaker3.Text == ""))
                    {
                        //  MessageBox.Show("Все пусто");
                        string query1 = $@"UPDATE RAMs SET  Model3='Нет', Vmemory3='Нет',TypeMemory3='Нет',Maker3='Нет' WHERE ID='{IDRams}'";
                        SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                        cmd1.ExecuteNonQuery();
                 
                    }                   
                    if (TextRAMModel4.Text != "" && TextVmemory4.Text != "" && TextTypeMemory4.Text != "" && TextMaker4.Text != "")
                    {
                        //MessageBox.Show("заполнено все4");
                        string query1 = $@"UPDATE RAMs SET Model4='{TextRAMModel4.Text}', Vmemory4='{TextVmemory4.Text}',TypeMemory4='{TextTypeMemory4.Text}',Maker4='{TextMaker4.Text}' WHERE ID='{IDRams}'";
                        SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                        cmd1.ExecuteNonQuery();
                      
                    }
                    else if (TextRAMModel4.Text == "" && (TextVmemory4.Text == "" && TextTypeMemory4.Text == "" && TextMaker4.Text == ""))
                    {
                        //  MessageBox.Show("Все пусто");
                        string query1 = $@"UPDATE RAMs SET  Model4='Нет', Vmemory4='Нет',TypeMemory4='Нет',Maker4='Нет' WHERE ID='{IDRams}'";
                        SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                        cmd1.ExecuteNonQuery();                       
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
                    MessageBox.Show("Данные добавлены");
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

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
using System.Xml.Linq;
using RecordPeriphelTechniс.BoxWindow;
using RecordPeriphelTechniс.Connection;

namespace RecordPeriphelTechniс.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditTech.xaml
    /// </summary>
    public partial class EditTech : Window
    {
        int TypeEdit, ProverkaPC = 0,  /*ProverkaOsnova=0,*/ ProverkaRams = 0;
        string IDMenuPerPC=null, IDComponets=null, ProccesID=null, MaterPlatID=null, VideCardID=null, IDRAM=null, IDDisk = null, IDSoundCard = null, IDBlockPower = null, IDCorpus = null, IDMenuPerTech = null, NameTypeTech = null, OldNumber=null, TextRamInfo2 = "",  TextRamInfo3 = "", TextRamInfo4 = "";

        public EditTech(DataRowView drv, int Type,string NameType)
        {

            InitializeComponent();
            NameTypeTech = NameType;
            TypeEdit = Type;
            CombBoxDowmload();            
            IDMenuPerTech = drv["IDMenuPer"].ToString();
            //IDMenuOboryd = drv["IDMenuPer"].ToString();
            CombTypeTech.Text = drv["NameType"].ToString();
            CombIDOrgamniz.Text = drv["NameOrg"].ToString();
            TextIDKabuneta.Text = drv["Kabunet"].ToString();
            TextName.Text = drv["NameYstr"].ToString();
            TextNumber.Text = drv["Number"].ToString();
            OldNumber = drv["Number"].ToString();
            TextDataStart.Text = drv["StartWork"].ToString();
            TextDataEnd.Text = drv["EndWork"].ToString();
            CombIDStatus.Text = drv["NameStatus"].ToString();
            CombIDWorks.Text = drv["NameWorks"].ToString();
            TextComments.Text = drv["Comments"].ToString();

            if (NameType == "Компьютерная техника")
            {
                IDMenuPerPC = drv["IDMenuPer"].ToString();
                IDComponets = drv["IDComponets"].ToString();
                ProccesID = drv["ProccesID"].ToString();
                MaterPlatID = drv["MaterPlatID"].ToString();
                VideCardID = drv["VideoCardID"].ToString();
                IDRAM = drv["IDRAM"].ToString();
                IDDisk = drv["DiskID"].ToString();
                IDSoundCard = drv["SoundCardsID"].ToString();
                IDBlockPower = drv["PowerBlocksID"].ToString();
                IDCorpus = drv["CorpusID"].ToString();
                IDMenuPerTech = drv["IDMenuPer"].ToString();

                TextProccModel.Text = drv["NameProcces"].ToString();
                TextSpeed.Text = drv["SpeedProcces"].ToString();
                CombProccMaker.Text = drv["MakerProcc"].ToString();

                TextMatePlatModel.Text = drv["ModelMatePlat"].ToString();
                CombMatePlatMaker.Text = drv["MakerMaterPlat"].ToString();

                TextDiskModel.Text = drv["ModelDisk"].ToString();
                TextSizeDisk.Text = drv["SizeDisk"].ToString();
                CombDiskType.Text = drv["TypeDisk"].ToString();

                TextSoundCardModel.Text = drv["ModelSoundCard"].ToString();
                CombSoundCardVud.Text = drv["TypeSoundCard"].ToString();

                TextPowerBlockName.Text = drv["ModelPowerBlocks"].ToString();
                TextPowerBlockEnergy.Text = drv["EnergyPowerBlock"].ToString();  
                
                TextCorpusModel.Text = drv["ModelCorpus"].ToString();


                TextRAMModel1.Text = drv["Model1"].ToString();
                TextVmemory1.Text = drv["V1"].ToString();
                TextTypeMemory1.Text = drv["TypeMemory1"].ToString();
                TextMaker1.Text = drv["Maker1"].ToString();

                TextRAMModel2.Text = drv["Model2"].ToString();
                TextVmemory2.Text = drv["V2"].ToString();
                TextTypeMemory2.Text = drv["TypeMemory2"].ToString();
                TextMaker2.Text = drv["Maker2"].ToString();

                TextRAMModel3.Text = drv["Model3"].ToString();
                TextVmemory3.Text = drv["V3"].ToString();
                TextTypeMemory3.Text = drv["TypeMemory3"].ToString();
                TextMaker3.Text = drv["Maker3"].ToString();

                TextRAMModel4.Text = drv["Model4"].ToString();
                TextVmemory4.Text = drv["V4"].ToString();
                TextTypeMemory4.Text = drv["TypeMemory4"].ToString();
                TextMaker4.Text = drv["Maker4"].ToString();

                TextVideoModel.Text = drv["ModelVideos"].ToString();
                TextVideoMemory.Text = drv["VVideoMemory"].ToString();
                CombVidieoMaker.Text = drv["MakerVideoCard"].ToString();
                //idi = drv["Components"].ToString();
            }
            else

            {
                IsEnabledData();
            }            
        }

        public void CheckerTextOsnova() // подцветка при пустоте
        {
            SimpleComand.CheckComboBox(CombTypeTech);
            SimpleComand.CheckComboBox(CombIDOrgamniz);
            SimpleComand.CheckTextBox(TextIDKabuneta);
            SimpleComand.CheckTextBox(TextName);
            SimpleComand.CheckTextBox(TextNumber);
            SimpleComand.CheckComboBox(CombIDStatus);
            SimpleComand.CheckTextBox(TextName);
            SimpleComand.CheckComboBox(CombIDWorks);
            SimpleComand.CheckDatePicker(TextDataStart);
        }

        public void CheckerTextComponets()
        {

            SimpleComand.CheckTextBox(TextProccModel);
            SimpleComand.CheckTextBox(TextSpeed);
            SimpleComand.CheckComboBox(CombProccMaker);
            SimpleComand.CheckTextBox(TextMatePlatModel);
            SimpleComand.CheckComboBox(CombMatePlatMaker);
            SimpleComand.CheckTextBox(TextRAMModel1);
            SimpleComand.CheckTextBox(TextVmemory1);
            SimpleComand.CheckTextBox(TextTypeMemory1);
            SimpleComand.CheckTextBox(TextMaker1);
            SimpleComand.CheckTextBox(TextVideoModel);
            SimpleComand.CheckTextBox(TextVideoMemory);
            SimpleComand.CheckComboBox(CombVidieoMaker);
            SimpleComand.CheckTextBox(TextDiskModel);
            SimpleComand.CheckTextBox(TextSizeDisk);
            SimpleComand.CheckComboBox(CombDiskType);
            SimpleComand.CheckTextBox(TextSoundCardModel);
            SimpleComand.CheckComboBox(CombSoundCardVud);
            SimpleComand.CheckTextBox(TextPowerBlockEnergy);
            SimpleComand.CheckTextBox(TextPowerBlockName);
            SimpleComand.CheckTextBox(TextCorpusModel);
        }

        public void CombBoxDowmload()  //Данные для комбобоксов 
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                try
                {
                    connection.Open();
                    string query1 = $@"SELECT * FROM TypeTechs where ID = 1"; // 
                    string query2 = $@"SELECT * FROM Organiz"; // 
                    string query1_3 = $@"SELECT * FROM TypeTechs where ID > 1"; // 
                    string query4 = $@"SELECT * FROM Status"; // 
                    string query5 = $@"SELECT * FROM Works"; // 
                    string query6 = $@"SELECT * FROM MakersProcc"; // 
                    string query7 = $@"SELECT * FROM MakersMaterPlat"; // 
                    string query8 = $@"SELECT * FROM MakersVideoCard"; // 
                    string query9 = $@"SELECT * FROM DiskType"; // 
                    string query10 = $@"SELECT * FROM SoundCardsType"; // 


                    //----------------------------------------------
                    SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                    SQLiteCommand cmd2 = new SQLiteCommand(query2, connection);
                    SQLiteCommand cmd3 = new SQLiteCommand(query1_3, connection);
                    SQLiteCommand cmd4 = new SQLiteCommand(query4, connection);
                    SQLiteCommand cmd5 = new SQLiteCommand(query5, connection);
                    SQLiteCommand cmd6 = new SQLiteCommand(query6, connection);
                    SQLiteCommand cmd7 = new SQLiteCommand(query7, connection);
                    SQLiteCommand cmd8 = new SQLiteCommand(query8, connection);
                    SQLiteCommand cmd9 = new SQLiteCommand(query9, connection);
                    SQLiteCommand cmd10 = new SQLiteCommand(query10, connection);

                    //----------------------------------------------
                    SQLiteDataAdapter SDA1 = new SQLiteDataAdapter(cmd1);
                    SQLiteDataAdapter SDA2 = new SQLiteDataAdapter(cmd2);
                    SQLiteDataAdapter SDA3 = new SQLiteDataAdapter(cmd3);
                    SQLiteDataAdapter SDA4 = new SQLiteDataAdapter(cmd4);
                    SQLiteDataAdapter SDA5 = new SQLiteDataAdapter(cmd5);
                    SQLiteDataAdapter SDA6 = new SQLiteDataAdapter(cmd6);
                    SQLiteDataAdapter SDA7 = new SQLiteDataAdapter(cmd7);
                    SQLiteDataAdapter SDA8 = new SQLiteDataAdapter(cmd8);
                    SQLiteDataAdapter SDA9 = new SQLiteDataAdapter(cmd9);
                    SQLiteDataAdapter SDA10 = new SQLiteDataAdapter(cmd10);
                    //----------------------------------------------
                    DataTable dt1 = new DataTable("TypeTechs");
                    DataTable dt2 = new DataTable("Organiz");
                    DataTable dt3 = new DataTable("TypeTechs");
                    DataTable dt4 = new DataTable("Status");
                    DataTable dt5 = new DataTable("Works");
                    DataTable dt6 = new DataTable("MakersProcc");
                    DataTable dt7 = new DataTable("MakersMaterPlat");
                    DataTable dt8 = new DataTable("MakersVideoCard");
                    DataTable dt9 = new DataTable("DiskType");
                    DataTable dt10 = new DataTable("SoundCardsType");
                    //----------------------------------------------
                    SDA1.Fill(dt1);
                    SDA2.Fill(dt2);
                    SDA3.Fill(dt3);
                    SDA4.Fill(dt4);
                    SDA5.Fill(dt5);
                    SDA6.Fill(dt6);
                    SDA7.Fill(dt7);
                    SDA8.Fill(dt8);
                    SDA9.Fill(dt9);
                    SDA10.Fill(dt10);
                    //----------------------------------------------
                    if (TypeEdit != 1)
                    {
                        CombTypeTech.ItemsSource = dt3.DefaultView;
                        CombTypeTech.DisplayMemberPath = "NameType";
                        CombTypeTech.SelectedValuePath = "ID";
                    }
                    else
                    {
                    CombTypeTech.ItemsSource = dt1.DefaultView;
                    CombTypeTech.DisplayMemberPath = "NameType";
                    CombTypeTech.SelectedValuePath = "ID";
                    }                   
                    //----------------------------------------------
                    CombIDOrgamniz.ItemsSource = dt2.DefaultView;
                    CombIDOrgamniz.DisplayMemberPath = "NameOrg";
                    CombIDOrgamniz.SelectedValuePath = "ID"; 
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
                    //----------------------------------------------
                    CombDiskType.ItemsSource = dt9.DefaultView;
                    CombDiskType.DisplayMemberPath = "TypeName";
                    CombDiskType.SelectedValuePath = "ID";
                    //----------------------------------------------
                    CombSoundCardVud.ItemsSource = dt10.DefaultView;
                    CombSoundCardVud.DisplayMemberPath = "TypeName";
                    CombSoundCardVud.SelectedValuePath = "ID";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void IsEnabledData()
        {          

            TextProccModel.IsEnabled = false;
            TextSpeed.IsEnabled = false;
            CombProccMaker.IsEnabled = false;
            TextMatePlatModel.IsEnabled = false;
            CombMatePlatMaker.IsEnabled = false;

            TextRAMModel1.IsEnabled = false;
            TextVmemory1.IsEnabled = false;
            TextTypeMemory1.IsEnabled = false;
            TextMaker1.IsEnabled = false;

            TextRAMModel2.IsEnabled = false;
            TextVmemory2.IsEnabled = false;
            TextTypeMemory2.IsEnabled = false;
            TextMaker2.IsEnabled = false;

            TextRAMModel3.IsEnabled = false; ;
            TextVmemory3.IsEnabled = false;
            TextTypeMemory3.IsEnabled = false;
            TextMaker3.IsEnabled = false;

            TextRAMModel4.IsEnabled = false;
            TextVmemory4.IsEnabled = false;
            TextTypeMemory4.IsEnabled = false;
            TextMaker4.IsEnabled = false;

            TextVideoModel.IsEnabled = false; ;
            TextVideoMemory.IsEnabled = false;
            CombVidieoMaker.IsEnabled = false;

            CicBlock.Visibility = Visibility.Collapsed;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
          EdiitOcnovaInfo();
        }


        public void  EdiitOcnovaInfo()
        {
            try
            {            
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                //ProverkaOsnova = 0;
                connection.Open();
                if (String.IsNullOrEmpty(CombIDOrgamniz.Text) || String.IsNullOrEmpty(TextIDKabuneta.Text) || String.IsNullOrEmpty(TextNumber.Text) || String.IsNullOrEmpty(CombIDStatus.Text) || String.IsNullOrEmpty(TextDataStart.Text)  || String.IsNullOrEmpty(TextName.Text) ||
                    String.IsNullOrEmpty(CombIDWorks.Text))
                {
                    MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    CheckerTextOsnova();
                    //ProverkaOsnova = 1;
                }
                else
                {
                    if (MessageBox.Show("Вы уверены что хотите изменить данные?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        bool resultClass = int.TryParse(CombIDOrgamniz.SelectedValue.ToString(), out int idOrg);
                        bool resultKab = int.TryParse(CombIDStatus.SelectedValue.ToString(), out int idStatus);
                        bool resultCon = int.TryParse(CombIDWorks.SelectedValue.ToString(), out int  idWork);
                            if (NameTypeTech == "Компьютерная техника")
                            {
                                int ProverkaNumber = 0;
                                if (OldNumber != TextNumber.Text)
                                {
                                    string query = $@"SELECT count (Number) FROM MenuPerTech WHERE Number = '{TextNumber.Text}'";
                                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                                    ProverkaNumber = Convert.ToInt32(cmd.ExecuteScalar());
                                }
                                else
                                {
                                    ProverkaNumber = 0;
                                }
                                if (ProverkaNumber == 0)
                                {
                                    CheckRams();
                                    if (ProverkaRams == 0)
                                    {
                                        EdiitPC();
                                        if (ProverkaPC == 0)
                                        {
                                            string query = $@"UPDATE MenuPerTech SET IDOrganiz='{idOrg}', Kabunet='{TextIDKabuneta.Text.ToLower()}',Number='{TextNumber.Text.ToLower()}',Name='{TextName.Text.ToLower()}', StartWork='{TextDataStart.Text}', 
                                            EndWork=@EndWork ,IDStatus='{idStatus}',IDWorks='{idWork}',Comments='{TextComments.Text.ToLower()}' WHERE ID='{IDMenuPerTech}';";
                                            SQLiteCommand cmd = new SQLiteCommand(query, connection);
                                            if (String.IsNullOrEmpty(TextDataEnd.Text))
                                            {
                                                cmd.Parameters.AddWithValue("@EndWork", null);
                                            }
                                            else
                                            {
                                                cmd.Parameters.AddWithValue("@EndWork", TextDataEnd.Text);
                                            }
                                            cmd.ExecuteNonQuery();
                                            MessageBox.Show("Информация обновленна", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show($@"Номер '{TextNumber.Text.ToLower()}' занят, выберите другой", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                    TextNumber.Text = OldNumber;
                                }
                            }
                            else
                            {
                                int ProverkaNumber = 0;
                                if (OldNumber != TextNumber.Text)
                                {
                                   string query = $@"SELECT count (Number) FROM MenuPerTech WHERE Number = '{TextNumber.Text.ToLower()}'";
                                   SQLiteCommand cmd = new SQLiteCommand(query, connection);
                                   ProverkaNumber = Convert.ToInt32(cmd.ExecuteScalar());
                                }
                                else
                                {
                                    ProverkaNumber = 0;
                                }
                                if (ProverkaNumber == 0)
                                {
                                    try
                                    {

                                        string query = $@"UPDATE MenuPerTech SET IDOrganiz='{idOrg}', Kabunet='{TextIDKabuneta.Text.ToLower()}',Number='{TextNumber.Text.ToLower()}',Name='{TextName.Text.ToLower()}', 
                                            Number='{TextNumber.Text.ToLower()}', StartWork='{TextDataStart.Text}', 
                                            EndWork=@EndWork ,IDStatus='{idStatus}',IDWorks='{idWork}',Comments='{TextComments.Text}' WHERE ID='{IDMenuPerTech}';";
                                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                                        if (String.IsNullOrEmpty(TextDataEnd.Text))
                                        {
                                            cmd.Parameters.AddWithValue("@EndWork", null);
                                        }
                                        else
                                        {
                                            cmd.Parameters.AddWithValue("@EndWork", TextDataEnd.Text);
                                        }                                       
                                        cmd.ExecuteNonQuery();
                                        MessageBox.Show("Информация обновленна", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    
                                }
                                else
                                {
                                    MessageBox.Show($@"Номер '{TextNumber.Text}' занят, выберите другой", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                    TextNumber.Text = OldNumber;
                                }
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

        public void EdiitPC()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    connection.Open();
                    ProverkaPC = 0;
                    if (String.IsNullOrEmpty(TextProccModel.Text) || String.IsNullOrEmpty(TextSpeed.Text) || String.IsNullOrEmpty(CombProccMaker.Text) || String.IsNullOrEmpty(TextMatePlatModel.Text) || String.IsNullOrEmpty(CombMatePlatMaker.Text)
                       || String.IsNullOrEmpty(TextRAMModel1.Text) || String.IsNullOrEmpty(TextVmemory1.Text) || String.IsNullOrEmpty(TextTypeMemory1.Text) ||
                       String.IsNullOrEmpty(TextMaker1.Text) || String.IsNullOrEmpty(TextVideoModel.Text) || String.IsNullOrEmpty(TextVideoMemory.Text) ||
                       String.IsNullOrEmpty(CombVidieoMaker.Text) || String.IsNullOrEmpty(TextDiskModel.Text) || String.IsNullOrEmpty(TextSizeDisk.Text) || String.IsNullOrEmpty(CombDiskType.Text)
                       || String.IsNullOrEmpty(TextSoundCardModel.Text) || String.IsNullOrEmpty(CombSoundCardVud.Text) || String.IsNullOrEmpty(TextPowerBlockName.Text) || String.IsNullOrEmpty(TextPowerBlockEnergy.Text)
                       || String.IsNullOrEmpty(TextCorpusModel.Text))
                    {
                        MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        CheckerTextComponets();
                        ProverkaPC = 1;
                    }
                    else
                    {
                        int id4, id5, id6;
                        bool resultTitl = int.TryParse(CombProccMaker.SelectedValue.ToString(), out id4);
                        bool resultBrand = int.TryParse(CombMatePlatMaker.SelectedValue.ToString(), out id5);
                        bool resultModel = int.TryParse(CombVidieoMaker.SelectedValue.ToString(), out id6);
                        bool resultDisk = int.TryParse(CombDiskType.SelectedValue.ToString(), out int id7);
                        bool resultSounCard = int.TryParse(CombSoundCardVud.SelectedValue.ToString(), out int id8);
                        string query = $@"UPDATE Procces SET Model='{TextProccModel.Text.ToLower()}', Speed='{TextSpeed.Text.ToLower()}',IDMaker='{id4}' WHERE ID='{ProccesID}';";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        query = $@"UPDATE MaterPlatas SET Model='{TextMatePlatModel.Text.ToLower()}', IDMaker='{id5}' WHERE ID='{MaterPlatID}';";
                        cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        query = $@"UPDATE VideoCards SET Model='{TextVideoModel.Text.ToLower()}', VVideoMemory='{TextVideoMemory.Text.ToLower()}', IDMaker='{id6}' WHERE ID='{VideCardID}';";
                        cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        query = $@"UPDATE Disks SET Model='{TextDiskModel.Text.ToLower()}', Size='{TextSizeDisk.Text.ToLower()}', IDTypeDisk='{id7}' WHERE ID='{IDDisk}';";
                        cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        query = $@"UPDATE SoundCards SET Model='{TextSoundCardModel.Text.ToLower()}', IDTypeCards='{id8}' WHERE ID='{IDSoundCard}';";
                        cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        query = $@"UPDATE PowerBlocks SET Model='{TextPowerBlockName.Text.ToLower()}', Energy='{TextPowerBlockEnergy.Text.ToLower()}' WHERE ID='{IDBlockPower}';";
                        cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        query = $@"UPDATE Corpus SET Model='{TextCorpusModel.Text.ToLower()}' WHERE ID='{IDCorpus}';";
                        cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        query = $@"UPDATE RAMs SET Model1='{TextRAMModel1.Text.ToLower()}', Vmemory1='{TextVmemory1.Text.ToLower()}',TypeMemory1='{TextTypeMemory1.Text.ToLower()}',Maker1='{TextMaker1.Text.ToLower()}' WHERE ID='{IDRAM}'";
                        cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();

                        if (TextRAMModel2.Text != "" && TextVmemory2.Text != "" && TextTypeMemory2.Text != "" && TextMaker2.Text != "")
                        {
                           // MessageBox.Show("заполнено все2");
                            string query1 = $@"UPDATE RAMs SET Model2='{TextRAMModel2.Text.ToLower()}', Vmemory2='{TextVmemory2.Text.ToLower()}',TypeMemory2='{TextTypeMemory2.Text.ToLower()}',Maker2='{TextMaker2.Text.ToLower()}' WHERE ID='{IDRAM}'";
                            SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                            cmd1.ExecuteNonQuery();

                        }                      
                        else if (TextRAMModel2.Text == "" && (TextVmemory2.Text == "" && TextTypeMemory2.Text == "" && TextMaker2.Text == ""))
                        {
                           // MessageBox.Show("Все пусто");
                            string query1 = $@"UPDATE RAMs SET  Model2='нет', Vmemory2='нет',TypeMemory2='нет',Maker2='нет' WHERE ID='{IDRAM}'";
                            SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                            cmd1.ExecuteNonQuery();

                        }                       
                        if (TextRAMModel3.Text != "" && TextVmemory3.Text != "" && TextTypeMemory3.Text != "" && TextMaker3.Text != "")
                        {
                            string query1 = $@"UPDATE RAMs SET Model3 ='{TextRAMModel3.Text.ToLower()}', Vmemory3='{TextVmemory3.Text.ToLower()}',TypeMemory3='{TextTypeMemory3.Text.ToLower()}',Maker3='{TextMaker3.Text.ToLower()}' WHERE ID='{IDRAM}'";
                            SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                            cmd1.ExecuteNonQuery();

                        }                       
                        else if (TextRAMModel3.Text == "" && (TextVmemory3.Text == "" && TextTypeMemory3.Text == "" && TextMaker3.Text == ""))
                        {
                            //  MessageBox.Show("Все пусто");
                            string query1 = $@"UPDATE RAMs SET  Model3='нет', Vmemory3='Нет',TypeMemory3='нет',Maker3='нет' WHERE ID='{IDRAM}'";
                            SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                            cmd1.ExecuteNonQuery();
                        }                       
                        if (TextRAMModel4.Text != "" && TextVmemory4.Text != "" && TextTypeMemory4.Text != "" && TextMaker4.Text != "")
                        {
                           // MessageBox.Show("заполнено все4");
                            string query1 = $@"UPDATE RAMs SET Model4='{TextRAMModel4.Text.ToLower()}', Vmemory4='{TextVmemory4.Text.ToLower()}',TypeMemory4='{TextTypeMemory4.Text.ToLower()}',Maker4='{TextMaker4.Text.ToLower()}' WHERE ID='{IDRAM}'";
                            SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                            cmd1.ExecuteNonQuery();

                        }                        
                        else if (TextRAMModel4.Text == "" && (TextVmemory4.Text == "" && TextTypeMemory4.Text == "" && TextMaker4.Text == ""))
                        {
                            //  MessageBox.Show("Все пусто");
                            string query1 = $@"UPDATE RAMs SET  Model4='нет', Vmemory4='нет',TypeMemory4='нет',Maker4='Нет' WHERE ID='{IDRAM}'";
                            SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                            cmd1.ExecuteNonQuery();
                        }                        
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CheckRams()
        {
            ProverkaRams = 0;
            TextRamInfo2 = "Заполните во втором слоте : ";
            TextRamInfo3 = "Заполните в третьем слоте: ";
            TextRamInfo4 = "Заполните в четвертом слоте: ";
            if (TextRAMModel2.Text != "" && TextVmemory2.Text != "" && TextTypeMemory2.Text != "" && TextMaker2.Text != "")
            {
                // MessageBox.Show("заполнено все2");               
                TextRamInfo2 = "";
            }
            else if (TextRAMModel2.Text == "" && (TextVmemory2.Text == "" && TextTypeMemory2.Text == "" && TextMaker2.Text == ""))
            {
               // MessageBox.Show("Все пусто");               
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

        private void BtnMessage_Click(object sender, RoutedEventArgs e)
        {
            string numbertech = TextNumber.Text;
            ReportMessage report = new ReportMessage(numbertech);
            bool? result = report.ShowDialog();
            switch (result)
            {
                default:                   
                    break;
            }
        }

        public void EditPCRams()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    connection.Open();
                    ProverkaPC = 0;
                    if (String.IsNullOrEmpty(TextProccModel.Text) || String.IsNullOrEmpty(TextSpeed.Text) || String.IsNullOrEmpty(CombProccMaker.Text) || String.IsNullOrEmpty(TextMatePlatModel.Text) ||
                        String.IsNullOrEmpty(CombMatePlatMaker.Text) || String.IsNullOrEmpty(TextRAMModel1.Text) || String.IsNullOrEmpty(TextVmemory1.Text) || String.IsNullOrEmpty(TextTypeMemory1.Text) ||
                        String.IsNullOrEmpty(TextMaker1.Text) || String.IsNullOrEmpty(TextVideoModel.Text) || String.IsNullOrEmpty(TextVideoMemory.Text) || String.IsNullOrEmpty(CombVidieoMaker.Text))
                    {
                        MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        ProverkaPC = 1;
                    }
                    else
                    {
                        int id4, id5, id6;
                        bool resultTitl = int.TryParse(CombProccMaker.SelectedValue.ToString(), out id4);
                        bool resultBrand = int.TryParse(CombMatePlatMaker.SelectedValue.ToString(), out id5);
                        bool resultModel = int.TryParse(CombVidieoMaker.SelectedValue.ToString(), out id6);
                        string query = $@"UPDATE Procces SET Model='{TextProccModel.Text.ToLower()}', Speed='{TextSpeed.Text.ToLower()}',IDMaker='{id4}' WHERE ID='{ProccesID}';";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        query = $@"UPDATE MaterPlatas SET Model='{TextMatePlatModel.Text.ToLower()}', IDMaker='{id5}' WHERE ID='{MaterPlatID}';";
                        cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        query = $@"UPDATE VideoCards SET Model='{TextVideoModel.Text.ToLower()}', VVideoMemory='{TextVideoMemory.Text.ToLower()}', IDMaker='{id6}' WHERE ID='{VideCardID}';";
                        cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        query = $@"UPDATE RAMs SET Model1='{TextRAMModel1.Text}', Vmemory1='{TextVmemory1.Text.ToLower()}',TypeMemory1='{TextTypeMemory1.Text.ToLower()}',Maker1='{TextMaker1.Text}' WHERE ID='{IDRAM}'";
                        cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();

                        if (TextRAMModel2.Text != "" && TextVmemory2.Text != "" && TextTypeMemory2.Text != "" && TextMaker2.Text != "")
                        {
                           // MessageBox.Show("заполнено все2");
                            string query1 = $@"UPDATE RAMs SET Model2='{TextRAMModel2.Text.ToLower()}', Vmemory2='{TextVmemory2.Text.ToLower()}',TypeMemory2='{TextTypeMemory2.Text.ToLower()}',Maker2='{TextMaker2.Text.ToLower()}' WHERE ID='{IDRAM}'";
                            SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                            cmd1.ExecuteNonQuery();

                        }
                        else if (TextRAMModel2.Text == "" && (TextVmemory2.Text == "" && TextTypeMemory2.Text == "" && TextMaker2.Text == ""))
                        {
                            //MessageBox.Show("Все пусто");
                            string query1 = $@"UPDATE RAMs SET  Model2='нет', Vmemory2='нет',TypeMemory2='нет',Maker2='нет' WHERE ID='{IDRAM}'";
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
                            string query1 = $@"UPDATE RAMs SET Model3 ='{TextRAMModel3.Text.ToLower()}', Vmemory3='{TextVmemory3.Text.ToLower()}',TypeMemory3='{TextTypeMemory3.Text.ToLower()}',Maker3='{TextMaker3.Text.ToLower()}' WHERE ID='{IDRAM}'";
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
                            string query1 = $@"UPDATE RAMs SET  Model3='нет', Vmemory3='нет',TypeMemory3='нет',Maker3='нет' WHERE ID='{IDRAM}'";
                            SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                            cmd1.ExecuteNonQuery();
                        }
                        else
                        {
                            // MessageBox.Show("Дозаполните");
                        }
                        if (TextRAMModel4.Text != "" && TextVmemory4.Text != "" && TextTypeMemory4.Text != "" && TextMaker4.Text != "")
                        {
                            //MessageBox.Show("заполнено все4");
                            string query1 = $@"UPDATE RAMs SET Model4='{TextRAMModel4.Text.ToLower()}', Vmemory4='{TextVmemory4.Text.ToLower()}',TypeMemory4='{TextTypeMemory4.Text.ToLower()}',Maker4='{TextMaker4.Text.ToLower()}' WHERE ID='{IDRAM}'";
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
                            string query1 = $@"UPDATE RAMs SET  Model4='нет', Vmemory4='нет',TypeMemory4='нет',Maker4='нет' WHERE ID='{IDRAM}'";
                            SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                            cmd1.ExecuteNonQuery();
                        }
                        else
                        {
                            // MessageBox.Show("Дозаполните");
                        }
                        //ProverkaOsnova = 1;
                    }

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

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
      
        
    


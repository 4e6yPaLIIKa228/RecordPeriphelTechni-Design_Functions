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
    /// Логика взаимодействия для EdditMessageReport.xaml
    /// </summary>
    public partial class EdditMessageReport : Window
    {
        string IDMenuPerTech = null, TypeTech= null,IDRepairDevice = null;
        public EdditMessageReport(DataRowView drv)
        {
            InitializeComponent();
            CombBoxDowmload();

            IDRepairDevice = drv["ID"].ToString();
            IDMenuPerTech = drv["IDDevice"].ToString();
            TypeTech = drv["NameType"].ToString();
            CombStatus.Text = drv["Status"].ToString();
            TextNameTech.Text = drv["NameDevice"].ToString();
            TextNameMaster.Text = drv["Surname"].ToString();
            if (TextNameMaster.Text == "")
            {
                TextNameMaster.Text = "Отуствует";
            }
            TextCommentsProblems.Text = drv["Comment"].ToString();
            LoadInfo();
            EnableInfp();
            


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

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            AcceptReport();
        }

        public void AcceptReport()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    String combtext = CombStatus.Text;
                    if (combtext == "Принята" || combtext == "принята")
                    {
                        if (MessageBox.Show("Вы уверены что хотите принять эту заявку?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            connection.Open();
                            string query = $@"SELECT count() from RepairDevice 
                                       JOIN StatusApplications on RepairDevice.IDStatus = StatusApplications.ID
                                       WHERE StatusApplications.NameStatus = 'Выполнина' or StatusApplications.NameStatus = 'Принята' and  RepairDevice.ID = '{IDRepairDevice}';";
                            SQLiteCommand cmd = new SQLiteCommand(query, connection);
                            int CheckDevice = Convert.ToInt32(cmd.ExecuteScalar());
                            if (CheckDevice == 0)
                            {
                                bool resultCon = int.TryParse(CombStatus.SelectedValue.ToString(), out int id3);
                                query = $@"UPDATE RepairDevice SET IDStatus='{id3}', IDMaster='{Saver.IDUser}' WHERE ID='{IDRepairDevice}';";
                                cmd = new SQLiteCommand(query, connection);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Зявка принята!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Отказано в изменении!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }

                        }
                    }
                    else if (combtext == "Выполнина" || combtext == "выполнина")
                    {
                        if (MessageBox.Show("Вы уверены что заявка выполнена?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            connection.Open();
                            bool resultCon = int.TryParse(CombStatus.SelectedValue.ToString(), out int id3);
                            string query = $@"UPDATE RepairDevice SET IDStatus='{id3}' WHERE ID='{IDRepairDevice}' and  IDMaster = '{Saver.IDUser}';";
                            SQLiteCommand cmd = new SQLiteCommand(query, connection);
                            cmd.ExecuteNonQuery();
                            query = $@"SELECT count() from RepairDevice 
                                       JOIN StatusApplications on RepairDevice.IDStatus = StatusApplications.ID
                                       WHERE StatusApplications.NameStatus = 'Выполнина' and RepairDevice.ID = '{IDRepairDevice}';";
                            cmd = new SQLiteCommand(query, connection);
                            int  CheckDevice = Convert.ToInt32(cmd.ExecuteScalar());
                            if (CheckDevice == 1)
                            {
                                MessageBox.Show("Зявка выполнена!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Вы не принимали эту заявку. Отказано в изменении", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

        public void EnableInfp()
        {
           // CombTypeTech.IsEnabled= false;
            //CombTypeTech.IsHitTestVisible = false; 
            //CombIDOrgamniz.IsEnabled = false;
            //TextIDKabuneta.IsReadOnly = true; 
            //TextName.IsReadOnly = true;
            //TextNumber.IsReadOnly = true;
            //TextDataStart.IsEnabled = false;        
            //TextDataEnd.IsEnabled = false;
            //CombIDStatus.IsEnabled = false;
            //CombIDWorks.IsEnabled = false;
            //TextComments.IsReadOnly = true;           
            //TextProccModel.IsReadOnly = true;
            //TextSpeed.IsReadOnly = true;
            //CombProccMaker.IsEnabled = false;
            //TextMatePlatModel.IsReadOnly = true;
            //CombMatePlatMaker.IsEnabled = false;
            //TextRAMModel1.IsReadOnly = true;
            //TextVmemory1.IsReadOnly = true;
            //TextTypeMemory1.IsReadOnly = true;
            //TextMaker1.IsReadOnly = true;
            //TextRAMModel2.IsReadOnly = true;
            //TextVmemory2.IsReadOnly = true;
            //TextTypeMemory2.IsReadOnly = true;
            //TextMaker2.IsReadOnly = true;
            //TextRAMModel3.IsReadOnly = true;
            //TextVmemory3.IsReadOnly = true;
            //TextTypeMemory3.IsReadOnly = true;
            //TextMaker3.IsReadOnly = true;
            //TextRAMModel4.IsReadOnly = true;
            //TextVmemory4.IsReadOnly = true;
            //TextTypeMemory4.IsReadOnly = true;
            //TextMaker4.IsReadOnly = true;
            //TextVideoModel.IsReadOnly = true;
            //TextVideoMemory.IsReadOnly = true;
            //CombVidieoMaker.IsReadOnly = true;
            }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void LoadInfo()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    connection.Open();
                    string DBSearchPC = $@"SELECT MenuPerTech.ID as IDMenuPer, MenuPerTech.Name as NameYstr, TypeTechs.NameType,MenuPerTech.Kabunet ,Organiz.NameOrg, MenuPerTech.Number,
        MenuPerTech.IDComponets as IDComponets, MenuPerTech.StartWork, MenuPerTech.EndWork,
        MenuPerTech.Comments, Status.NameStatus, Works.NameWorks,
        Procces.ID as ProccesID, Procces.Model as NameProcces ,Procces.Speed as SpeedProcces, MakersProcc.Name as MakerProcc,
        MaterPlatas.ID as MaterPlatID, MaterPlatas.Model as ModelMatePlat, MakersMaterPlat.Name as MakerMaterPlat,
        VideoCards.ID as VideoCardID, VideoCards.Model as ModelVideos, VideoCards.VVideoMemory , MakersVideoCard.Name as MakerVideoCard,
        Disks.ID as DiskID, Disks.Model as ModelDisk, Disks.Size as SizeDisk, DiskType.TypeName as TypeDisk,
        SoundCards.ID as SoundCardsID, SoundCards.Model as ModelSoundCard, SoundCardsType.TypeName as TypeSoundCard,
        PowerBlocks.ID as PowerBlocksID, PowerBlocks.Model as ModelPowerBlocks, PowerBlocks.Energy as EnergyPowerBlock,
        Corpus.ID as CorpusID, Corpus.Model as ModelCorpus,
        RAMs.ID as IDRAM,
        RAMs.Model1 as Model1, RAMs.Vmemory1 as V1, RAMs.TypeMemory1 as TypeMemory1 , RAMs.Maker1 as Maker1, 
        RAMs.Model2 as Model2, RAMs.Vmemory2 as V2, RAMs.TypeMemory2 as TypeMemory2 , RAMs.Maker2 as Maker2,
        RAMs.Model3 as Model3, RAMs.Vmemory3 as V3, RAMs.TypeMemory3 as TypeMemory3 , RAMs.Maker3 as Maker3,
        RAMs.Model4 as Model4, RAMs.Vmemory4 as V4, RAMs.TypeMemory4 as TypeMemory4 , RAMs.Maker4 as Maker4    

        FROM MenuPerTech
        LEFT JOIN TypeTechs on MenuPerTech.IDTypeTech = TypeTechs.ID
        LEFT JOIN Organiz on MenuPerTech.IDOrganiz = Organiz.ID
        LEFT JOIN Components on MenuPerTech.IDComponets = Components.ID
        LEFT JOIN Status on MenuPerTech.IDStatus = Status.ID
        LEFT JOIN Works on MenuPerTech.IDWorks = Works.ID

        LEFT JOIN Procces on Components.IDProcces = Procces.ID
        LEFT JOIN MakersProcc ON Procces.IDMaker = MakersProcc.ID

        LEFT JOIN MaterPlatas on Components.IDMaterPlata = MaterPlatas.ID
        LEFT JOIN MakersMaterPlat on MaterPlatas.IDMaker = MakersMaterPlat.ID

        LEFT JOIN VideoCards on Components.IDVideo = VideoCards.ID
        LEFT JOIN MakersVideoCard on VideoCards.IDMaker = MakersVideoCard.ID

        LEFT JOIN Disks on Components.IDDisk = Disks.ID
        LEFT JOIN DiskType on Disks.IDTypeDisk = DiskType.ID

        LEFT JOIN SoundCards on Components.IDSoundCard = SoundCards.ID
        LEFT JOIN SoundCardsType on SoundCards.IDTypeCards = SoundCardsType.ID

        LEFT JOIN PowerBlocks on Components.IDPowerBlock = PowerBlocks.ID

        LEFT JOIN Corpus on Components.IDCorpus = Corpus.ID

        LEFT JOIN RAMs on Components.ID = RAMs.ID
        where IDMenuPer = '{IDMenuPerTech}'";
                    SQLiteCommand cmd = new SQLiteCommand(DBSearchPC, connection);                    
                    cmd.ExecuteNonQuery();
                    SQLiteDataReader dr = null;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {  
                        CombTypeTech.Text = dr["NameType"].ToString();
                        CombIDOrgamniz.Text = dr["NameOrg"].ToString();
                        TextIDKabuneta.Text = dr["Kabunet"].ToString();
                        TextName.Text = dr["NameYstr"].ToString();                        
                        TextNumber.Text = dr["Number"].ToString();
                        TextDataStart.Text = dr["StartWork"].ToString();
                        TextDataEnd.Text = dr["EndWork"].ToString();
                        CombIDStatus.Text = dr["NameStatus"].ToString();
                        CombIDWorks.Text = dr["NameWorks"].ToString();
                        TextComments.Text = dr["Comments"].ToString();
                        if (TypeTech == "Компьютерная техника" | TypeTech == "компьютерная техника")
                        {
                            IDMenuPerTech = dr["IDMenuPer"].ToString();
                            TextProccModel.Text = dr["NameProcces"].ToString();
                            TextSpeed.Text = dr["SpeedProcces"].ToString();
                            CombProccMaker.Text = dr["MakerProcc"].ToString();

                            TextMatePlatModel.Text = dr["ModelMatePlat"].ToString();
                            CombMatePlatMaker.Text = dr["MakerMaterPlat"].ToString();

                            TextDiskModel.Text = dr["ModelDisk"].ToString();
                            TextSizeDisk.Text = dr["SizeDisk"].ToString();
                            CombDiskType.Text = dr["TypeDisk"].ToString();

                            TextSoundCardModel.Text = dr["ModelSoundCard"].ToString();
                            CombSoundCardVud.Text = dr["TypeSoundCard"].ToString();

                            TextPowerBlockName.Text = dr["ModelPowerBlocks"].ToString();
                            TextPowerBlockEnergy.Text = dr["EnergyPowerBlock"].ToString();

                            TextCorpusModel.Text = dr["ModelCorpus"].ToString();

                            TextRAMModel1.Text = dr["Model1"].ToString();
                            TextVmemory1.Text = dr["V1"].ToString();
                            TextTypeMemory1.Text = dr["TypeMemory1"].ToString();
                            TextMaker1.Text = dr["Maker1"].ToString();
                            TextRAMModel2.Text = dr["Model2"].ToString();
                            TextVmemory2.Text = dr["V2"].ToString();
                            TextTypeMemory2.Text = dr["TypeMemory2"].ToString();
                            TextMaker2.Text = dr["Maker2"].ToString();
                            TextRAMModel3.Text = dr["Model3"].ToString();
                            TextVmemory3.Text = dr["V3"].ToString();
                            TextTypeMemory3.Text = dr["TypeMemory3"].ToString();
                            TextMaker3.Text = dr["Maker3"].ToString();
                            TextRAMModel4.Text = dr["Model4"].ToString();
                            TextVmemory4.Text = dr["V4"].ToString();
                            TextTypeMemory4.Text = dr["TypeMemory4"].ToString();
                            TextMaker4.Text = dr["Maker4"].ToString();
                            TextVideoModel.Text = dr["ModelVideos"].ToString();
                            TextVideoMemory.Text = dr["VVideoMemory"].ToString();
                            CombVidieoMaker.Text = dr["MakerVideoCard"].ToString();
                        }
                        else
                        {
                            CuctemBlock.Visibility = Visibility.Collapsed;
                        }
                    }
                   
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
                    string query4 = $@"SELECT * FROM Status"; // 
                    string query5 = $@"SELECT * FROM Works"; // 
                    string query6 = $@"SELECT * FROM MakersProcc"; // 
                    string query7 = $@"SELECT * FROM MakersMaterPlat"; // 
                    string query8 = $@"SELECT * FROM MakersVideoCard"; // 
                    string query9 = $@"SELECT * FROM StatusApplications";
                    string query10 = $@"SELECT * FROM DiskType"; // 
                    string query11 = $@"SELECT * FROM SoundCardsType"; // 

                    //----------------------------------------------
                    SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                    SQLiteCommand cmd2 = new SQLiteCommand(query2, connection);
                    SQLiteCommand cmd4 = new SQLiteCommand(query4, connection);
                    SQLiteCommand cmd5 = new SQLiteCommand(query5, connection);
                    SQLiteCommand cmd6 = new SQLiteCommand(query6, connection);
                    SQLiteCommand cmd7 = new SQLiteCommand(query7, connection);
                    SQLiteCommand cmd8 = new SQLiteCommand(query8, connection);
                    SQLiteCommand cmd9 = new SQLiteCommand(query9, connection);
                    SQLiteCommand cmd10 = new SQLiteCommand(query10, connection);
                    SQLiteCommand cmd11 = new SQLiteCommand(query11, connection);

                    //----------------------------------------------
                    SQLiteDataAdapter SDA1 = new SQLiteDataAdapter(cmd1);
                    SQLiteDataAdapter SDA2 = new SQLiteDataAdapter(cmd2);
                    SQLiteDataAdapter SDA4 = new SQLiteDataAdapter(cmd4);
                    SQLiteDataAdapter SDA5 = new SQLiteDataAdapter(cmd5);
                    SQLiteDataAdapter SDA6 = new SQLiteDataAdapter(cmd6);
                    SQLiteDataAdapter SDA7 = new SQLiteDataAdapter(cmd7);
                    SQLiteDataAdapter SDA8 = new SQLiteDataAdapter(cmd8);
                    SQLiteDataAdapter SDA9 = new SQLiteDataAdapter(cmd9);
                    SQLiteDataAdapter SDA10 = new SQLiteDataAdapter(cmd10);
                    SQLiteDataAdapter SDA11 = new SQLiteDataAdapter(cmd11);
                    //----------------------------------------------
                    DataTable dt1 = new DataTable("TypeTechs");
                    DataTable dt2 = new DataTable("Organiz");
                    DataTable dt4 = new DataTable("Status");
                    DataTable dt5 = new DataTable("Works");
                    DataTable dt6 = new DataTable("MakersProcc");
                    DataTable dt7 = new DataTable("MakersMaterPlat");
                    DataTable dt8 = new DataTable("MakersVideoCard");
                    DataTable dt9 = new DataTable("StatusApplications");
                    DataTable dt10 = new DataTable("DiskType");
                    DataTable dt11 = new DataTable("SoundCardsType");
                    //----------------------------------------------
                    SDA1.Fill(dt1);
                    SDA2.Fill(dt2);
                    SDA4.Fill(dt4);
                    SDA5.Fill(dt5);
                    SDA6.Fill(dt6);
                    SDA7.Fill(dt7);
                    SDA8.Fill(dt8);
                    SDA9.Fill(dt9);
                    SDA10.Fill(dt10);
                    SDA11.Fill(dt11);
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
                    //----------------------------------------------
                    CombStatus.ItemsSource = dt9.DefaultView;
                    CombStatus.DisplayMemberPath = "NameStatus";
                    CombStatus.SelectedValuePath = "ID";
                    //----------------------------------------------
                    CombDiskType.ItemsSource = dt10.DefaultView;
                    CombDiskType.DisplayMemberPath = "TypeName";
                    CombDiskType.SelectedValuePath = "ID";
                    //----------------------------------------------
                    CombSoundCardVud.ItemsSource = dt11.DefaultView;
                    CombSoundCardVud.DisplayMemberPath = "TypeName";
                    CombSoundCardVud.SelectedValuePath = "ID";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using RecordPeriphelTechniс.Connection;
using DataTable = System.Data.DataTable;
using Window = System.Windows.Window;

namespace RecordPeriphelTechniс.BoxWindow
{
    /// <summary>
    /// Логика взаимодействия для AddTech.xaml
    /// </summary>
    public partial class AddTech : Window
    {
        int IDMenuPerTech = 0, IDTypeTech = 0, ProverkaNumber = 0, ProverkaPcCompont = 0, ProverkaRams = 0,
            IDComponets = 0, IDProcces = 0, IDMaterPlat = 0, IDVideoCard = 0,
            IDRams = 0, IDDisk = 0, IDSoundCard = 0, IDBlockPower = 0, IDCorpus = 0,
            IDClone = 0, TextNumberInt = 0;
        string TextRamInfo2 = "", TextRamInfo3 = "", TextRamInfo4 = "";

        public AddTech()
        {
            InitializeComponent();
            CombBoxDowmload();
            CuctemBlock.Visibility = Visibility.Collapsed;
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
                    string query9 = $@"SELECT * FROM DiskType"; // 
                    string query10 = $@"SELECT * FROM SoundCardsType"; // 


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
                    //----------------------------------------------
                    DataTable dt1 = new DataTable("TypeTechs");
                    DataTable dt2 = new DataTable("Organiz");
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
                    SDA4.Fill(dt4);
                    SDA5.Fill(dt5);
                    SDA6.Fill(dt6);
                    SDA7.Fill(dt7);
                    SDA8.Fill(dt8);
                    SDA9.Fill(dt9);
                    SDA10.Fill(dt10);
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
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            String textcomb = CombTypeTech.Text;
            if (MessageBox.Show("Вы уверены,что хотите добавить новое устройство?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (textcomb == "компьютерная техника" || textcomb == "Компьютерная техника")
                {
                    AddOsnovaPC();
                }
                else
                {
                    AddOsnovaPerTech();
                }
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

        public void AddOsnovaPerTech()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    connection.Open();
                    if (String.IsNullOrEmpty(CombIDOrgamniz.Text) || String.IsNullOrEmpty(TextIDKabuneta.Text) || String.IsNullOrEmpty(TextNumber.Text) || String.IsNullOrEmpty(CombIDStatus.Text) || String.IsNullOrEmpty(TextName.Text) || String.IsNullOrEmpty(TextDataStart.Text)
                        || String.IsNullOrEmpty(CombIDWorks.Text))
                    {
                        MessageBox.Show("Заполните обязательные поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        CheckerTextOsnova();
                    }
                    else
                    {
                        bool resultType = int.TryParse(CombTypeTech.SelectedValue.ToString(), out IDTypeTech);
                        bool resultClass = int.TryParse(CombIDOrgamniz.SelectedValue.ToString(), out int id);
                        bool resultKab = int.TryParse(CombIDStatus.SelectedValue.ToString(), out int id2);
                        bool resultCon = int.TryParse(CombIDWorks.SelectedValue.ToString(), out int id3);
                        int TxtNumberString = Convert.ToInt32(TextNumber.Text);
                        if (IDClone == 0)
                        {
                            TxtNumberString = Convert.ToInt32(TextNumber.Text);
                        }
                        else
                        {
                            TxtNumberString = TextNumberInt;
                        }

                        string query = $@"SELECT count (Number) FROM MenuPerTech WHERE Number = '{TxtNumberString}'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        ProverkaNumber = Convert.ToInt32(cmd.ExecuteScalar());
                        if (ProverkaNumber != 1)
                        {

                            query = $@"INSERT INTO MenuPerTech('IDTypeTech','IDOrganiz','Kabunet','Number','IDComponets','Name','StartWork','EndWork','IDStatus','IDWorks','Comments')
                            values ('{IDTypeTech}','{id}','{TextIDKabuneta.Text.ToLower()}','{TxtNumberString}',@IDComponets,'{TextName.Text.ToLower()}','{TextDataStart.Text.ToLower()}',@EndWork,'{id2}','{id3}','{TextComments.Text.ToLower()}')";
                            cmd = new SQLiteCommand(query, connection);
                            if (String.IsNullOrEmpty(TextDataEnd.Text))
                            {
                                cmd.Parameters.AddWithValue("@EndWork", null);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@EndWork", TextDataEnd.Text);
                            }
                            cmd.Parameters.AddWithValue("@IDComponets", null);
                            cmd.ExecuteNonQuery();
                            query = $@"SELECT ID FROM MenuPerTech WHERE IDTypeTech = '{IDTypeTech}' and IDOrganiz = '{id}' and Kabunet = '{TextIDKabuneta.Text.ToLower()}' and  Number = '{TxtNumberString}' and Name = '{TextName.Text.ToLower()}' and StartWork = '{TextDataStart.Text.ToLower()}' and EndWork = '{TextDataEnd.Text}' and  IDStatus = '{id2}' and  IDWorks ='{id3}' and Comments = '{TextComments.Text.ToLower()}' ";
                            cmd = new SQLiteCommand(query, connection);
                            IDMenuPerTech = Convert.ToInt32(cmd.ExecuteScalar());
                            MessageBox.Show("Устройство добавлено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                            BtnCloun.Visibility = Visibility.Visible;
                            TxtKoll.Visibility = Visibility.Visible;
                            TextNumberClone.Visibility = Visibility.Visible;

                        }
                        else
                        {
                            MessageBox.Show("Измените номер техники", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    String.IsNullOrEmpty(CombIDWorks.Text) || String.IsNullOrEmpty(TextDataStart.Text))
                {
                    CheckerTextOsnova();
                    CheckerTextComponets();
                    MessageBox.Show("Заполните обязательные поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    bool resultType = int.TryParse(CombTypeTech.SelectedValue.ToString(), out IDTypeTech);
                    bool resultClass = int.TryParse(CombIDOrgamniz.SelectedValue.ToString(), out int id);
                    bool resultKab = int.TryParse(CombIDStatus.SelectedValue.ToString(), out int id2);
                    bool resultCon = int.TryParse(CombIDWorks.SelectedValue.ToString(), out int id3);
                    int TxtNumberString = Convert.ToInt32(TextNumber.Text);
                    if (IDClone == 0)
                    {
                        TxtNumberString = Convert.ToInt32(TextNumber.Text);
                    }
                    else
                    {
                        TxtNumberString = TextNumberInt;
                    }
                    string query = $@"SELECT count (Number) FROM MenuPerTech WHERE Number = '{TxtNumberString}'";
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
                                values ('{IDTypeTech}','{id}','{TextIDKabuneta.Text.ToLower()}','{TxtNumberString}','{null}','{TextName.Text.ToLower()}','{TextDataStart.Text.ToLower()}',@EndWork,'{id2}','{id3}','{TextComments.Text.ToLower()}')";
                                cmd = new SQLiteCommand(query, connection);
                                if (String.IsNullOrEmpty(TextDataEnd.Text))
                                {
                                    cmd.Parameters.AddWithValue("@EndWork", null);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@EndWork", TextDataEnd.Text);
                                }
                                cmd.ExecuteNonQuery();
                                query = $@"SELECT ID FROM MenuPerTech WHERE IDTypeTech = '{IDTypeTech}' and IDOrganiz = '{id}' and Kabunet = '{TextIDKabuneta.Text.ToLower()}' and  Number = '{TxtNumberString}' and Name = '{TextName.Text.ToLower()}'  and  IDStatus = '{id2}' and  IDWorks ='{id3}' and Comments = '{TextComments.Text.ToLower()}' ";
                                cmd = new SQLiteCommand(query, connection);
                                IDMenuPerTech = Convert.ToInt32(cmd.ExecuteScalar());
                                AddRams();
                                AddComponets();
                                UpdateMenuPer();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Измените номер техники", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
        }
        public void AddPcCompet()
        {
            if (String.IsNullOrEmpty(TextProccModel.Text) || String.IsNullOrEmpty(TextSpeed.Text) || String.IsNullOrEmpty(CombProccMaker.Text) || String.IsNullOrEmpty(TextMatePlatModel.Text) || String.IsNullOrEmpty(CombMatePlatMaker.Text)
                       || String.IsNullOrEmpty(TextRAMModel1.Text) || String.IsNullOrEmpty(TextVmemory1.Text) || String.IsNullOrEmpty(TextTypeMemory1.Text) ||
                       String.IsNullOrEmpty(TextMaker1.Text) || String.IsNullOrEmpty(TextVideoModel.Text) || String.IsNullOrEmpty(TextVideoMemory.Text) ||
                       String.IsNullOrEmpty(CombVidieoMaker.Text) || String.IsNullOrEmpty(TextDiskModel.Text) || String.IsNullOrEmpty(TextSizeDisk.Text) || String.IsNullOrEmpty(CombDiskType.Text)
                       || String.IsNullOrEmpty(TextSoundCardModel.Text) || String.IsNullOrEmpty(CombSoundCardVud.Text) || String.IsNullOrEmpty(TextPowerBlockName.Text) || String.IsNullOrEmpty(TextPowerBlockEnergy.Text)
                       || String.IsNullOrEmpty(TextCorpusModel.Text))
            {
                CheckerTextComponets();
                MessageBox.Show("Заполните обязательные поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        bool resultDisk = int.TryParse(CombDiskType.SelectedValue.ToString(), out int id7);
                        bool resultSounCard = int.TryParse(CombSoundCardVud.SelectedValue.ToString(), out int id8);

                        string query = $@"INSERT INTO Procces ('Model', 'Speed','IDMaker') VALUES ('{TextProccModel.Text.ToLower()}','{TextSpeed.Text.ToLower()}','{id4}');";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        query = $@"SELECT count (ID) FROM Procces WHERE Model = '{TextProccModel.Text.ToLower()}' and Speed = '{TextSpeed.Text.ToLower()}' and IDMaker = '{id4}'";
                        cmd = new SQLiteCommand(query, connection);
                        int CheckIDProcces = Convert.ToInt32(cmd.ExecuteScalar());
                        int IDProccesCheck = 0;
                        if (CheckIDProcces != 1)
                        {
                            while (CheckIDProcces >= 1)
                            {
                                query = $@"SELECT ID FROM Procces WHERE Model = '{TextProccModel.Text.ToLower()}' and Speed = '{TextSpeed.Text.ToLower()}' and IDMaker = '{id4}' and ID > '{IDProccesCheck}' ";
                                cmd = new SQLiteCommand(query, connection);
                                IDProccesCheck = Convert.ToInt32(cmd.ExecuteScalar());
                                query = $@"SELECT count (ID) FROM Procces WHERE Model = '{TextProccModel.Text.ToLower()}' and Speed = '{TextSpeed.Text.ToLower()}' and IDMaker = '{id4}' and ID > '{IDProccesCheck}'";
                                cmd = new SQLiteCommand(query, connection);
                                CheckIDProcces = Convert.ToInt32(cmd.ExecuteScalar());
                            }
                            IDProcces = IDProccesCheck;
                        }
                        else
                        {
                            query = $@"SELECT ID FROM Procces WHERE Model = '{TextProccModel.Text.ToLower()}' and Speed = '{TextSpeed.Text.ToLower()}' and IDMaker = '{id4}' ";
                            cmd = new SQLiteCommand(query, connection);
                            IDProcces = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        query = $@"INSERT INTO MaterPlatas ('Model','IDMaker') VALUES ('{TextMatePlatModel.Text.ToLower()}','{id5}');";
                        cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        query = $@"SELECT count (ID) FROM  MaterPlatas WHERE Model = '{TextMatePlatModel.Text.ToLower()}' and IDMaker = '{id5}'";
                        cmd = new SQLiteCommand(query, connection);
                        int CheckIDMaterPlat = Convert.ToInt32(cmd.ExecuteScalar());
                        int IDMaterPlatCheck = 0;
                        if (CheckIDMaterPlat != 0)
                        {
                            while (CheckIDMaterPlat >= 1)
                            {
                                query = $@"SELECT ID FROM MaterPlatas  WHERE Model = '{TextMatePlatModel.Text.ToLower()}' and IDMaker = '{id5}' and ID > '{IDMaterPlatCheck}' ";
                                cmd = new SQLiteCommand(query, connection);
                                IDMaterPlatCheck = Convert.ToInt32(cmd.ExecuteScalar());
                                query = $@"SELECT count (ID) FROM  MaterPlatas  WHERE Model = '{TextMatePlatModel.Text.ToLower()}' and IDMaker = '{id5}' and ID > '{IDMaterPlatCheck}'";
                                cmd = new SQLiteCommand(query, connection);
                                CheckIDMaterPlat = Convert.ToInt32(cmd.ExecuteScalar());
                            }
                            IDMaterPlat = IDMaterPlatCheck;
                        }
                        else
                        {
                            query = $@"SELECT ID FROM MaterPlatas  WHERE Model = '{TextMatePlatModel.Text.ToLower()}' and IDMaker = '{id5}' ";
                            cmd = new SQLiteCommand(query, connection);
                            IDMaterPlat = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        query = $@"INSERT INTO VideoCards ('Model','VVideoMemory','IDMaker') VALUES ('{TextVideoModel.Text.ToLower()}','{TextVideoMemory.Text.ToLower()}','{id6}');";
                        cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        query = $@"SELECT count (ID) FROM  VideoCards  WHERE Model = '{TextVideoModel.Text.ToLower()}' and VVideoMemory = '{TextVideoMemory.Text.ToLower()}' and IDMaker = '{id6}'";
                        cmd = new SQLiteCommand(query, connection);
                        int CheckIDVideCards = Convert.ToInt32(cmd.ExecuteScalar());
                        int IDVideoCardsCheck = 0;
                        if (CheckIDVideCards != 0)
                        {
                            while (CheckIDVideCards >= 1)
                            {
                                query = $@"SELECT ID FROM VideoCards  WHERE Model = '{TextVideoModel.Text.ToLower()}' and VVideoMemory = '{TextVideoMemory.Text.ToLower()}' and IDMaker = '{id6}' and ID > '{IDVideoCardsCheck}' ";
                                cmd = new SQLiteCommand(query, connection);
                                IDVideoCardsCheck = Convert.ToInt32(cmd.ExecuteScalar());
                                query = $@"SELECT count (ID) FROM  VideoCards  WHERE Model = '{TextVideoModel.Text.ToLower()}' and VVideoMemory = '{TextVideoMemory.Text.ToLower()}' and IDMaker = '{id6}' and ID > '{IDVideoCardsCheck}'";
                                cmd = new SQLiteCommand(query, connection);
                                CheckIDVideCards = Convert.ToInt32(cmd.ExecuteScalar());
                            }
                            IDVideoCard = IDVideoCardsCheck;
                        }
                        else
                        {
                            query = $@"SELECT ID FROM VideoCards  WHERE Model = '{TextVideoModel.Text.ToLower()}' and VVideoMemory = '{TextVideoMemory.Text.ToLower()}' and IDMaker = '{id6}' ";
                            cmd = new SQLiteCommand(query, connection);
                            IDVideoCard = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        query = $@"INSERT INTO RAMs  ('Model1','Vmemory1','TypeMemory1','Maker1') VALUES ('{TextRAMModel1.Text.ToLower()}','{TextVmemory1.Text.ToLower()}','{TextTypeMemory1.Text.ToLower()}','{TextMaker1.Text.ToLower()}');";
                        cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        query = $@"SELECT count (ID) FROM  RAMs WHERE Model1 = '{TextRAMModel1.Text.ToLower()}' and Vmemory1 = '{TextVmemory1.Text.ToLower()}' and TypeMemory1 = '{TextTypeMemory1.Text.ToLower()}' and Maker1 = '{TextMaker1.Text.ToLower()}'";
                        cmd = new SQLiteCommand(query, connection);
                        int CheckIDRams = Convert.ToInt32(cmd.ExecuteScalar());
                        int IDRamsCheck = 0;
                        if (CheckIDRams >= 1)
                        {
                            while (CheckIDRams != 0)
                            {
                                query = $@"SELECT ID FROM  RAMs WHERE Model1 = '{TextRAMModel1.Text.ToLower()}' and Vmemory1 = '{TextVmemory1.Text.ToLower()}' and TypeMemory1 = '{TextTypeMemory1.Text.ToLower()}' and Maker1 = '{TextMaker1.Text.ToLower()}' and ID > '{IDRamsCheck}' ";
                                cmd = new SQLiteCommand(query, connection);
                                IDRamsCheck = Convert.ToInt32(cmd.ExecuteScalar());
                                query = $@"SELECT count (ID) FROM  RAMs WHERE Model1 = '{TextRAMModel1.Text.ToLower()}' and Vmemory1 = '{TextVmemory1.Text.ToLower()}' and TypeMemory1 = '{TextTypeMemory1.Text.ToLower()}' and Maker1 = '{TextMaker1.Text.ToLower()}' and ID > '{IDRamsCheck}'";
                                cmd = new SQLiteCommand(query, connection);
                                CheckIDRams = Convert.ToInt32(cmd.ExecuteScalar());
                            }
                            IDRams = IDRamsCheck;
                        }
                        else
                        {
                            query = $@"SELECT ID FROM RAMs WHERE Model1 = '{TextRAMModel1.Text.ToLower()}' and Vmemory1 = '{TextVmemory1.Text.ToLower()}' and TypeMemory1 = '{TextTypeMemory1.Text.ToLower()}' and Maker1 = '{TextMaker1.Text.ToLower()}'";
                            cmd = new SQLiteCommand(query, connection);
                            IDRams = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        query = $@"INSERT INTO Disks ('Model','Size','IDTypeDisk') VALUES ('{TextDiskModel.Text.ToLower()}','{TextSizeDisk.Text.ToLower()}','{id7}');";
                        cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        query = $@"SELECT count (ID) FROM  Disks  WHERE Model = '{TextDiskModel.Text.ToLower()}' and Size = '{TextSizeDisk.Text.ToLower()}' and IDTypeDisk = '{id7}'";
                        cmd = new SQLiteCommand(query, connection);
                        int CheckIDDisk = Convert.ToInt32(cmd.ExecuteScalar());
                        int IDDiskCheck = 0;
                        if (CheckIDDisk != 0)
                        {
                            while (CheckIDDisk >= 1)
                            {
                                query = $@"SELECT ID FROM Disks  WHERE Model = '{TextDiskModel.Text.ToLower()}' and Size = '{TextSizeDisk.Text.ToLower()}' and IDTypeDisk = '{id7}' and ID > '{IDDiskCheck}' ";
                                cmd = new SQLiteCommand(query, connection);
                                IDDiskCheck = Convert.ToInt32(cmd.ExecuteScalar());
                                query = $@"SELECT count (ID) FROM  Disks  WHERE Model = '{TextDiskModel.Text.ToLower()}' and Size = '{TextSizeDisk.Text.ToLower()}' and IDTypeDisk = '{id7}' and ID > '{IDDiskCheck}'";
                                cmd = new SQLiteCommand(query, connection);
                                CheckIDDisk = Convert.ToInt32(cmd.ExecuteScalar());
                            }
                            IDDisk = IDDiskCheck;
                        }
                        else
                        {
                            query = $@"SELECT ID FROM Disks  WHERE Model = '{TextDiskModel.Text.ToLower()}' and Size = '{TextSizeDisk.Text.ToLower()}' and IDTypeDisk = '{id7}' ";
                            cmd = new SQLiteCommand(query, connection);
                            IDDisk = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        query = $@"INSERT INTO SoundCards ('Model','IDTypeCards') VALUES ('{TextSoundCardModel.Text.ToLower()}','{id8}');";
                        cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        query = $@"SELECT count (ID) FROM  SoundCards  WHERE Model = '{TextSoundCardModel.Text.ToLower()}' and IDTypeCards = '{id8}'";
                        cmd = new SQLiteCommand(query, connection);
                        int CheckIDSoundCards = Convert.ToInt32(cmd.ExecuteScalar());
                        int IDSoundCardsCheck = 0;
                        if (CheckIDSoundCards != 0)
                        {
                            while (CheckIDSoundCards >= 1)
                            {
                                query = $@"SELECT ID FROM SoundCards  WHERE Model = '{TextSoundCardModel.Text.ToLower()}' and IDTypeCards = '{id8}' and ID > '{IDSoundCardsCheck}'";
                                cmd = new SQLiteCommand(query, connection);
                                IDSoundCardsCheck = Convert.ToInt32(cmd.ExecuteScalar());
                                query = $@"SELECT count (ID) FROM  SoundCards  WHERE Model = '{TextSoundCardModel.Text.ToLower()}' and IDTypeCards = '{id8}' and ID > '{IDSoundCardsCheck}'";
                                cmd = new SQLiteCommand(query, connection);
                                CheckIDSoundCards = Convert.ToInt32(cmd.ExecuteScalar());
                            }
                            IDSoundCard = IDSoundCardsCheck;
                        }
                        else
                        {
                            query = $@"SELECT ID FROM SoundCards  WHERE Model = '{TextSoundCardModel.Text.ToLower()}' and IDTypeCards = '{id8}' ";
                            cmd = new SQLiteCommand(query, connection);
                            IDSoundCard = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        query = $@"INSERT INTO PowerBlocks ('Model','Energy') VALUES ('{TextPowerBlockName.Text.ToLower()}','{TextPowerBlockEnergy.Text.ToLower()}');";
                        cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        query = $@"SELECT count (ID) FROM  PowerBlocks  WHERE Model = '{TextPowerBlockName.Text.ToLower()}' and Energy = '{TextPowerBlockEnergy.Text.ToLower()}'";
                        cmd = new SQLiteCommand(query, connection);
                        int CheckIDBlockPower = Convert.ToInt32(cmd.ExecuteScalar());
                        int IDBlockPowerCheck = 0;
                        if (CheckIDBlockPower != 0)
                        {
                            while (CheckIDBlockPower >= 1)
                            {
                                query = $@"SELECT ID FROM PowerBlocks  WHERE Model = '{TextPowerBlockName.Text.ToLower()}' and Energy = '{TextPowerBlockEnergy.Text.ToLower()}' and ID > '{IDBlockPowerCheck}'";
                                cmd = new SQLiteCommand(query, connection);
                                IDBlockPowerCheck = Convert.ToInt32(cmd.ExecuteScalar());
                                query = $@"SELECT count (ID) FROM  PowerBlocks  WHERE Model = '{TextPowerBlockName.Text.ToLower()}' and Energy = '{TextPowerBlockEnergy.Text.ToLower()}' and ID > '{IDBlockPowerCheck}'";
                                cmd = new SQLiteCommand(query, connection);
                                CheckIDBlockPower = Convert.ToInt32(cmd.ExecuteScalar());
                            }
                            IDBlockPower = IDBlockPowerCheck;
                        }
                        else
                        {
                            query = $@"SELECT ID FROM PowerBlocks  WHERE Model = '{TextPowerBlockName.Text.ToLower()}' and Energy = '{TextPowerBlockEnergy.Text.ToLower()}'";
                            cmd = new SQLiteCommand(query, connection);
                            IDBlockPower = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        query = $@"INSERT INTO Corpus ('Model') VALUES ('{TextCorpusModel.Text.ToLower()}');";
                        cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        query = $@"SELECT count (ID) FROM  Corpus  WHERE Model = '{TextCorpusModel.Text.ToLower()}'";
                        cmd = new SQLiteCommand(query, connection);
                        int CheckIDCorpus = Convert.ToInt32(cmd.ExecuteScalar());
                        int IDCorpusCheck = 0;
                        if (CheckIDCorpus != 0)
                        {
                            while (CheckIDCorpus >= 1)
                            {
                                query = $@"SELECT ID FROM Corpus  WHERE Model = '{TextCorpusModel.Text.ToLower()}' and ID > '{IDCorpusCheck}'";
                                cmd = new SQLiteCommand(query, connection);
                                IDCorpusCheck = Convert.ToInt32(cmd.ExecuteScalar());
                                query = $@"SELECT count (ID) FROM  Corpus  WHERE Model = '{TextCorpusModel.Text.ToLower()}' and ID > '{IDCorpusCheck}'";
                                cmd = new SQLiteCommand(query, connection);
                                CheckIDCorpus = Convert.ToInt32(cmd.ExecuteScalar());
                            }
                            IDCorpus = IDCorpusCheck;
                        }
                        else
                        {
                            query = $@"SELECT ID FROM PoweBlocks  WHERE Model = '{TextCorpusModel.Text.ToLower()}'";
                            cmd = new SQLiteCommand(query, connection);
                            IDCorpus = Convert.ToInt32(cmd.ExecuteScalar());
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

        private void CombTypeTech_DropDownClosed(object sender, EventArgs e)
        {
            //ComboBox cbx = (ComboBox)sender;
            //string s = ((DataRowView)cbx.Items.GetItemAt(cbx.SelectedIndex)).Row.ItemArray[0].ToString();
            // String a = CombTypeTech.Text;
            String a = CombTypeTech.Text;
            if (a == "Компьютерная техника" || a == "компьютерная техника")
            {
                TextName.Text = "пк";
                TextName.IsReadOnly = true;
                CuctemBlock.Visibility = Visibility.Visible;
            }
            else
            {
                CuctemBlock.Visibility = Visibility.Collapsed;
                CuctemBlock.Visibility = Visibility.Collapsed;
                TextName.Text = string.Empty;
                TextName.IsReadOnly = false;
            }
        }

        private void CuctemBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            String a = CombTypeTech.Text;
            if (a == "Компьютерная техника" || a == "компьютерная техника")
            {

                TabProcces.IsEnabled = true;
                TextProccModel.IsEnabled = true;
                TextSpeed.IsEnabled = true;
                CombProccMaker.IsEnabled = true;
                TabItemMaterPlat.IsEnabled = true;
                TabItemVideoCarta.IsEnabled = true;
                TabItemRAMS.IsEnabled = true;
                TabItemDisk.IsEnabled = true;
                TabItemSoundCard.IsEnabled = true;
                TabItemPoweBlock.IsEnabled = true;
                TabItemCorpus.IsEnabled = true;

            }
            else
            {

                TabProcces.IsSelected = true;
                TabProcces.IsEnabled = false;
                TextProccModel.IsEnabled = false;
                TextSpeed.IsEnabled = false;
                CombProccMaker.IsEnabled = false;
                TabItemMaterPlat.IsEnabled = false;
                TabItemVideoCarta.IsEnabled = false;
                TabItemRAMS.IsEnabled = false;
                TabItemDisk.IsEnabled = false;
                TabItemSoundCard.IsEnabled = false;
                TabItemPoweBlock.IsEnabled = false;
                TabItemCorpus.IsEnabled = false;

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
                //MessageBox.Show("Все пусто");
                string query1 = $@"UPDATE RAMs SET  Model2='нет', Vmemory2='нет',TypeMemory2='нет',Maker2='нет' WHERE ID='{IDRams}'";
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
                string query1 = $@"UPDATE RAMs SET  Model3='нет', Vmemory3='нет',TypeMemory3='нет',Maker3='нет' WHERE ID='{IDRams}'";
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

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void ClearData()
        {
            if (MessageBox.Show("Вы уверены, что хотите очистить данные?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                CombTypeTech.Text = null;
                CombIDOrgamniz.Text = null;
                TextIDKabuneta.Text = null;
                TextName.Text = null;
                TextNumber.Text = null;
                CombIDStatus.Text = null;
                TextName.Text = null;
                CombIDWorks.Text = null;
                TextDataStart.Text = null;
                TextProccModel.Text = null;
                TextSpeed.Text = null;
                CombProccMaker.Text = null;
                TextMatePlatModel.Text = null;
                CombMatePlatMaker.Text = null;
                TextRAMModel1.Text = null;
                TextVmemory1.Text = null;
                TextTypeMemory1.Text = null;
                TextMaker1.Text = null;
                TextVideoModel.Text = null;
                TextVideoMemory.Text = null;
                CombVidieoMaker.Text = null;
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {

            ClearData();
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
                        string query1 = $@"UPDATE RAMs SET Model2='{TextRAMModel2.Text.ToLower()}', Vmemory2='{TextVmemory2.Text.ToLower()}',TypeMemory2='{TextTypeMemory2.Text.ToLower()}',Maker2='{TextMaker2.Text.ToLower()}' WHERE ID='{IDRams}'";
                        SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                        cmd1.ExecuteNonQuery();
                    }
                    else if (TextRAMModel2.Text == "" && (TextVmemory2.Text == "" && TextTypeMemory2.Text == "" && TextMaker2.Text == ""))
                    {
                        // MessageBox.Show("Все пусто");
                        string query1 = $@"UPDATE RAMs SET  Model2='нет', Vmemory2='нет',TypeMemory2='нет',Maker2='нет' WHERE ID='{IDRams}'";
                        SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                        cmd1.ExecuteNonQuery();

                    }
                    if (TextRAMModel3.Text != "" && TextVmemory3.Text != "" && TextTypeMemory3.Text != "" && TextMaker3.Text != "")
                    {
                        //MessageBox.Show("заполнено все3");
                        string query1 = $@"UPDATE RAMs SET Model3 ='{TextRAMModel3.Text.ToLower()}', Vmemory3='{TextVmemory3.Text.ToLower()}',TypeMemory3='{TextTypeMemory3.Text.ToLower()}',Maker3='{TextMaker3.Text.ToLower()}' WHERE ID='{IDRams}'";
                        SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                        cmd1.ExecuteNonQuery();

                    }
                    else if (TextRAMModel3.Text == "" && (TextVmemory3.Text == "" && TextTypeMemory3.Text == "" && TextMaker3.Text == ""))
                    {
                        //  MessageBox.Show("Все пусто");
                        string query1 = $@"UPDATE RAMs SET  Model3='нет', Vmemory3='нет',TypeMemory3='нет',Maker3='нет' WHERE ID='{IDRams}'";
                        SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                        cmd1.ExecuteNonQuery();

                    }
                    if (TextRAMModel4.Text != "" && TextVmemory4.Text != "" && TextTypeMemory4.Text != "" && TextMaker4.Text != "")
                    {
                        //MessageBox.Show("заполнено все4");
                        string query1 = $@"UPDATE RAMs SET Model4='{TextRAMModel4.Text.ToLower()}', Vmemory4='{TextVmemory4.Text.ToLower()}',TypeMemory4='{TextTypeMemory4.Text.ToLower()}',Maker4='{TextMaker4.Text}' WHERE ID='{IDRams}'";
                        SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                        cmd1.ExecuteNonQuery();

                    }
                    else if (TextRAMModel4.Text == "" && (TextVmemory4.Text == "" && TextTypeMemory4.Text == "" && TextMaker4.Text == ""))
                    {
                        //  MessageBox.Show("Все пусто");
                        string query1 = $@"UPDATE RAMs SET  Model4='нет', Vmemory4='нет',TypeMemory4='нет',Maker4='нет' WHERE ID='{IDRams}'";
                        SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                        cmd1.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
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
                    string query = $@"INSERT INTO Components  ('IDProcces', 'IDMaterPlata','IDRAM','IDVideo','IDDisk','IDSoundCard','IDPowerBlock','IDCorpus' ) VALUES ('{IDProcces}','{IDMaterPlat}','{IDRams}','{IDVideoCard}','{IDDisk}','{IDSoundCard}','{IDBlockPower}','{IDCorpus}') ";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    query = $@"SELECT ID FROM Components WHERE IDProcces = '{IDProcces}' and IDMaterPlata = '{IDMaterPlat}' and IDRAM = '{IDRams}' and IDVideo = '{IDVideoCard}' and IDDisk = '{IDDisk}' and IDSoundCard = '{IDSoundCard}' and IDPowerBlock = '{IDBlockPower}' and IDCorpus= '{IDCorpus}'";
                    cmd = new SQLiteCommand(query, connection);
                    IDComponets = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
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
                    MessageBox.Show("Устройство добавлено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    BtnCloun.Visibility = Visibility.Visible;
                    TxtKoll.Visibility = Visibility.Visible;
                    TextNumberClone.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ClonePc()
        {
            String textcomb = CombTypeTech.Text;

            if (String.IsNullOrEmpty(TextNumberClone.Text))
            {
                MessageBox.Show("Заполните обязательные поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                CheckerTextOsnova();
            }
            else
            {
                string NumberClone = TextNumberClone.Text;
                int NumberCloneint = Convert.ToInt32(NumberClone);
                IDClone = 1;
                for (int i = 1; NumberCloneint >= i; i++)
                {
                    TextNumberInt = Convert.ToInt32(TextNumber.Text);
                    TextNumberInt = TextNumberInt + 1;
                    TextNumber.Text = Convert.ToString(TextNumberInt);

                    if (textcomb == "компьютерная техника" || textcomb == "Компьютерная техника")
                    {
                        AddOsnovaPC();

                    }
                    else
                    {
                        AddOsnovaPerTech();
                    }
                }
                IDClone = 0;
            }
        }

        private void BtnCloun_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите клонировать технику?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                ClonePc();
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

        public void TextValidationTextBox(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9$]");
            e.Handled = regex.IsMatch(e.Text); 
        }
        private void NumberValidationTextBoxZapud(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,$]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void TextValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-ZА-яА-я ,]+");
            e.Handled = regex.IsMatch(e.Text);

        }
    }
}

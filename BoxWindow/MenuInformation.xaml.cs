﻿using System;
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
using RecordPeriphelTechniс.BoxWindow;
using RecordPeriphelTechniс.Connection;

namespace RecordPeriphelTechniс.Windows
{
    /// <summary>
    /// Логика взаимодействия для MenuInformation.xaml
    /// </summary>
    public partial class MenuInformation : Window
    {
        int IndexTabCont;
        string DBSearchPC = $@"SELECT MenuPerTech.ID as IDMenuPer, MenuPerTech.Name as NameYstr, TypeTechs.NameType,MenuPerTech.Kabunet ,Organiz.NameOrg, MenuPerTech.Number,
MenuPerTech.IDComponets as IDComponets, MenuPerTech.StartWork, MenuPerTech.EndWork,
MenuPerTech.Comments, Status.NameStatus, Works.NameWorks,
Procces.ID as ProccesID, Procces.Model as NameProcces ,Procces.Speed as SpeedProcces, MakersProcc.Name as MakerProcc,
MaterPlatas.ID as MaterPlatID, MaterPlatas.Model as ModelMatePlat, MakersMaterPlat.Name as MakerMaterPlat,
VideoCards.ID as VideoCardID, VideoCards.Model as ModelVideos, VideoCards.VVideoMemory , MakersVideoCard.Name as MakerVideoCard,
RAMs.ID as IDRAM,
RAMs.Model1 as Model1, RAMs.Vmemory1 as V1, RAMs.TypeMemory1 as TypeMemory1 , RAMs.Maker1 as Maker1, 
RAMs.Model2 as Model2, RAMs.Vmemory2 as V2, RAMs.TypeMemory2 as TypeMemory2 , RAMs.Maker2 as Maker2,
RAMs.Model3 as Model3, RAMs.Vmemory3 as V3, RAMs.TypeMemory3 as TypeMemory3 , RAMs.Maker3 as Maker3,
RAMs.Model4 as Model4, RAMs.Vmemory4 as V4, RAMs.TypeMemory4 as TypeMemory4 , RAMs.Maker4 as Maker4    

FROM MenuPerTech
JOIN TypeTechs on MenuPerTech.IDTypeTech = TypeTechs.ID
JOIN Organiz on MenuPerTech.IDOrganiz = Organiz.ID
JOIN Components on MenuPerTech.IDComponets = Components.ID
JOIN Status on MenuPerTech.IDStatus = Status.ID
JOIN Works on MenuPerTech.IDWorks = Works.ID

JOIN Procces on Components.IDProcces = Procces.ID
LEFT JOIN MakersProcc ON Procces.IDMaker = MakersProcc.ID

JOIN MaterPlatas on Components.IDMaterPlata = MaterPlatas.ID
JOIN MakersMaterPlat on MaterPlatas.IDMaker = MakersMaterPlat.ID

JOIN VideoCards on Components.IDVideo = VideoCards.ID
JOIN MakersVideoCard on VideoCards.IDMaker = MakersVideoCard.ID

LEFT JOIN RAMs on Components.ID = RAMs.ID";
        public MenuInformation()
        {
            //VisitorCheck = s;
            InitializeComponent();
            
            if (Saver.IDAllowanceString == "Гость")
            {
                Visitor();
            }else if (Saver.IDAllowanceString == "Пользователь")
            {
                Users();
            }else if (Saver.IDAllowanceString == "Мастер")
            {
                Master();
            }
            LoadDB_InforPcTex();
            LoadDB_InforPerTech();
            LoadDB_InforDopOboryd();
        }

        public void Visitor()
        {
            BtnEddit.IsEnabled = false;
            BtnAdd.IsEnabled = false;
            ListWind.IsEnabled = false;
            AddMenu.IsEnabled = false;
            EdiitTec.IsEnabled = false;

        }

        public void Users()
        {
            ListWind.Visibility = Visibility.Collapsed;
        }

        public void Master()
        {
            ListUsers.Visibility = Visibility.Collapsed;
            AddMenu.Visibility = Visibility.Collapsed;
            EdiitTec.Visibility = Visibility.Collapsed;
        }

        public void LoadDB_InforPcTex()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                try
                {
                    connection.Open();
                    string query = $@"SELECT MenuPerTech.ID as IDMenuPer, MenuPerTech.Name as NameYstr, TypeTechs.NameType,MenuPerTech.Kabunet ,Organiz.NameOrg, MenuPerTech.Number,
MenuPerTech.IDComponets as IDComponets, MenuPerTech.StartWork, MenuPerTech.EndWork,
MenuPerTech.Comments, Status.NameStatus, Works.NameWorks,
Procces.ID as ProccesID, Procces.Model as NameProcces ,Procces.Speed as SpeedProcces, MakersProcc.Name as MakerProcc,
MaterPlatas.ID as MaterPlatID, MaterPlatas.Model as ModelMatePlat, MakersMaterPlat.Name as MakerMaterPlat,
VideoCards.ID as VideoCardID, VideoCards.Model as ModelVideos, VideoCards.VVideoMemory , MakersVideoCard.Name as MakerVideoCard,
RAMs.ID as IDRAM,
RAMs.Model1 as Model1, RAMs.Vmemory1 as V1, RAMs.TypeMemory1 as TypeMemory1 , RAMs.Maker1 as Maker1, 
RAMs.Model2 as Model2, RAMs.Vmemory2 as V2, RAMs.TypeMemory2 as TypeMemory2 , RAMs.Maker2 as Maker2,
RAMs.Model3 as Model3, RAMs.Vmemory3 as V3, RAMs.TypeMemory3 as TypeMemory3 , RAMs.Maker3 as Maker3,
RAMs.Model4 as Model4, RAMs.Vmemory4 as V4, RAMs.TypeMemory4 as TypeMemory4 , RAMs.Maker4 as Maker4    

FROM MenuPerTech
JOIN TypeTechs on MenuPerTech.IDTypeTech = TypeTechs.ID
JOIN Organiz on MenuPerTech.IDOrganiz = Organiz.ID
JOIN Components on MenuPerTech.IDComponets = Components.ID
JOIN Status on MenuPerTech.IDStatus = Status.ID
JOIN Works on MenuPerTech.IDWorks = Works.ID

JOIN Procces on Components.IDProcces = Procces.ID
LEFT JOIN MakersProcc ON Procces.IDMaker = MakersProcc.ID

JOIN MaterPlatas on Components.IDMaterPlata = MaterPlatas.ID
JOIN MakersMaterPlat on MaterPlatas.IDMaker = MakersMaterPlat.ID

JOIN VideoCards on Components.IDVideo = VideoCards.ID
JOIN MakersVideoCard on VideoCards.IDMaker = MakersVideoCard.ID

LEFT JOIN RAMs on Components.ID = RAMs.ID

WHERE  MenuPerTech.IDTypeTech = '1'
                          ";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    DataTable DT = new DataTable("MenuPerTech");
                    SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    SDA.Fill(DT);
                    InforPcTex.ItemsSource = DT.DefaultView;
                    cmd.ExecuteNonQuery();
                    SQLiteDataReader dr = null;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        //Componets, ProccesID, MaterPlatID, VideCardID, IDRAM, Slot1ID1, Slot1ID2, Slot1ID3, Slot1ID4;
                        //Saver.IDMenuPerPC = dr["IDMenuPer"].ToString();
                        //Saver.IDComponets = dr["IDComponets"].ToString();
                        //Saver.ProccesID = dr["ProccesID"].ToString();
                        //Saver.MaterPlatID = dr["MaterPlatID"].ToString();
                        //Saver.VideCardID = dr["VideoCardID"].ToString();
                        //Saver.IDRAM = dr["IDRAM"].ToString();
                        //Saver.SlotID1 = dr["SlotID1"].ToString();
                        //Saver.SlotID2 = dr["SlotID2"].ToString();
                        //Saver.SlotID3 = dr["SlotID3"].ToString();
                        //Saver.SlotID4 = dr["SlotID4"].ToString();

                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void LoadDB_InforPerTech()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                try
                {
                    connection.Open();
                    string query = $@"SELECT MenuPerTech.ID as IDMenuPer, MenuPerTech.Name as NameYstr, TypeTechs.NameType, Organiz.NameOrg, MenuPerTech.Kabunet, MenuPerTech.Number,
MenuPerTech.IDComponets, MenuPerTech.StartWork, MenuPerTech.EndWork,
MenuPerTech.Comments, Status.NameStatus, Works.NameWorks

FROM MenuPerTech
LEFT JOIN TypeTechs on MenuPerTech.IDTypeTech = TypeTechs.ID
LEFT JOIN Organiz on MenuPerTech.IDOrganiz = Organiz.ID
LEFT JOIN Components on MenuPerTech.IDComponets = Components.ID
LEFT JOIN Status on MenuPerTech.IDStatus = Status.ID
LEFT JOIN Works on MenuPerTech.IDWorks = Works.ID

WHERE  MenuPerTech.IDTypeTech = '2'
                          ";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    DataTable DT = new DataTable("MenuPerTech");
                    SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    SDA.Fill(DT);
                    InforPerTech.ItemsSource = DT.DefaultView;
                    cmd.ExecuteNonQuery();
                    SQLiteDataReader dr = null;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        //Saver.IDMenuPerTech = dr["IDMenuPer"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void LoadDB_InforDopOboryd()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                try
                {
                    connection.Open();
                    string query = $@"SELECT MenuPerTech.ID as IDMenuPer, MenuPerTech.Name as NameYstr, TypeTechs.NameType, Organiz.NameOrg,MenuPerTech.Kabunet, MenuPerTech.Number,
MenuPerTech.IDComponets, MenuPerTech.StartWork, MenuPerTech.EndWork,
MenuPerTech.Comments, Status.NameStatus, Works.NameWorks

FROM MenuPerTech
LEFT JOIN TypeTechs on MenuPerTech.IDTypeTech = TypeTechs.ID
LEFT JOIN Organiz on MenuPerTech.IDOrganiz = Organiz.ID
LEFT JOIN Components on MenuPerTech.IDComponets = Components.ID
LEFT JOIN Status on MenuPerTech.IDStatus = Status.ID
LEFT JOIN Works on MenuPerTech.IDWorks = Works.ID
WHERE  MenuPerTech.IDTypeTech = '3'
                          ";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    DataTable DT = new DataTable("MenuPerTech");
                    SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    SDA.Fill(DT);
                    InforDopOboryd.ItemsSource = DT.DefaultView;
                    cmd.ExecuteNonQuery();
                    SQLiteDataReader dr = null;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        //Saver.IDMenuOboryd = dr["IDMenuPer"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void Eddit_InforPcTex()
        {
            if (Saver.Visitor != 1)
            {
                if (InforPcTex.SelectedIndex != -1)
                {
                    int Type = 1;
                    EditTech tech = new EditTech((DataRowView)InforPcTex.SelectedItem, Type);
                    tech.Owner = this;
                    bool? result = tech.ShowDialog();
                    switch (result)
                    {
                        default:
                            LoadDB_InforPcTex();
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Выберите строку с данными,чтобы ее изменить");
                }
            }
        }
        private void Eddit_InforPerTech()
        {
            if (Saver.Visitor != 1)
            {
                if (InforPerTech.SelectedIndex != -1)
                {
                    int Type = 2;
                    EditTech tech = new EditTech((DataRowView)InforPerTech.SelectedItem, Type);
                    tech.Owner = this;
                    bool? result = tech.ShowDialog();
                    switch (result)
                    {
                        default:
                            LoadDB_InforPerTech();
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Выберите строку с данными,чтобы ее изменить");
                }
            }
        }
        private void Eddit_InforDopOboryd()
        {
            if (Saver.Visitor != 1)
            {
                if (InforDopOboryd.SelectedIndex != -1)
                {
                    int Type = 3;
                    EditTech tech = new EditTech((DataRowView)InforDopOboryd.SelectedItem, Type);
                    tech.Owner = this;
                    bool? result = tech.ShowDialog();
                    switch (result)
                    {
                        default:
                            LoadDB_InforDopOboryd();
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Выберите строку с данными,чтобы ее изменить");
                }
            }
        }
        private void InforPcTex_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Eddit_InforPcTex();
        }
        private void InforPerTech_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Eddit_InforPerTech();
        }
        private void InforDopOboryd_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Eddit_InforDopOboryd();
        }
        private void BtnEddit_Click(object sender, RoutedEventArgs e)
        {
            if (IndexTabCont == 0)
            {
                Eddit_InforPcTex();
            }
            else if (IndexTabCont == 1)
            {
                Eddit_InforPerTech();
            }
            else if (IndexTabCont == 2)
            {
                Eddit_InforDopOboryd();
            }
        }
        private void TabConrlMenuPer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IndexTabCont = TabConrlMenuPer.SelectedIndex;
            if (IndexTabCont == 1 || IndexTabCont == 2)
            {
                TreeSisBlock.Visibility = Visibility.Hidden;
            }
            else
            {
                TreeSisBlock.Visibility = Visibility.Visible;
            }
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            AddTech addtech = new AddTech();
            bool? result = addtech.ShowDialog();
            switch (result)
            {
                default:
                    LoadDB_InforPcTex();
                    LoadDB_InforPerTech();
                    LoadDB_InforDopOboryd();
                    break;
            }
        }
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {

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
            Environment.Exit(0);
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

        private void TreeInfoGlav_Expanded(object sender, RoutedEventArgs e)
        {
            TreeSisBlock.IsExpanded = false;
        }

        private void TreeSisBlock_Expanded(object sender, RoutedEventArgs e)
        {
            TreeInfoGlav.IsExpanded = false;
        }

        public void SelectTree(object sender, RoutedEventArgs e)
        {
            //if  (Organiz.IsSelected == true)
            //{
            //    SearchInfo();
            //}else if(Kabunet.IsSelected== true)
            //{
            //    SearchInfo();
            //}

            SearchInfo();

        }

        public void SearchInfo()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                connection.Open();
                try
                {
                    String combtext = CombSearchInfo.Text;
                    if (combtext == "Организация" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC}   WHERE (Organiz.NameOrg like '%{TxtSearch.Text.ToLower()}%' or Organiz.NameOrg like '%{TxtSearch.Text.ToUpper()}%')  and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    else if (Organiz.IsSelected == true && IndexTabCont == 1)
                    {
                        // InforPcTex.ItemsSource = null;
                    }
                    else if (Organiz.IsSelected == true && IndexTabCont == 2)
                    {
                        // InforPcTex.ItemsSource = null;
                    }

                    if (combtext == "Кабинет" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC}  WHERE (MenuPerTech.Kabunet like '%{TxtSearch.Text.ToLower()}%' or MenuPerTech.Kabunet like '%{TxtSearch.Text.ToUpper()}%') and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    else if (Kabunet.IsSelected == true && IndexTabCont == 1)
                    {

                    }
                    else if (Kabunet.IsSelected == true && IndexTabCont == 2)
                    {

                    }

                    if (combtext == "Номер" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC}   WHERE MenuPerTech.Number like '%{TxtSearch.Text}%' and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    else if (Number.IsSelected == true && IndexTabCont == 1)
                    {

                    }
                    else if (Number.IsSelected == true && IndexTabCont == 2)
                    {

                    }
                    if (combtext == "Дата начала" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC}   WHERE MenuPerTech.StartWork like '%{TxtSearch.Text}%' and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    else if (DataStart.IsSelected == true && IndexTabCont == 1)
                    {

                    }
                    else if (DataStart.IsSelected == true && IndexTabCont == 2)
                    {

                    }
                    if (combtext == "Дата окончания" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC}   WHERE MenuPerTech.EndWork like '%{TxtSearch.Text}%' and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    else if (DataStart.IsSelected == true && IndexTabCont == 1)
                    {

                    }
                    else if (DataStart.IsSelected == true && IndexTabCont == 2)
                    {

                    }
                    if (combtext == "Статус" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (Status.NameStatus like '{TxtSearch.Text.ToLower()}%' or  Status.NameStatus like '{TxtSearch.Text.ToUpper()}%' or  Status.NameStatus like '{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    else if (Status.IsSelected == true && IndexTabCont == 1)
                    {

                    }
                    else if (Status.IsSelected == true && IndexTabCont == 2)
                    {

                    }
                    if (combtext == "Работоспособность" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (Works.NameWorks like '{TxtSearch.Text.ToLower()}%' or  Works.NameWorks like '{TxtSearch.Text.ToUpper()}%' or  Works.NameWorks like '{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    else if (Status.IsSelected == true && IndexTabCont == 1)
                    {

                    }
                    else if (Status.IsSelected == true && IndexTabCont == 2)
                    {

                    }
                    if (combtext == "Коментарий" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (MenuPerTech.Comments like '{TxtSearch.Text.ToLower()}%' or MenuPerTech.Comments like '{TxtSearch.Text.ToUpper()}%' or MenuPerTech.Comments like '{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    else if (Comment.IsSelected == true && IndexTabCont == 1)
                    {

                    }
                    else if (Comment.IsSelected == true && IndexTabCont == 2)
                    {

                    }
                    if (combtext == "Модель процессора" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (NameProcces like '%{TxtSearch.Text.ToLower()}%' or NameProcces like '%{TxtSearch.Text.ToUpper()}%' or NameProcces like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Скорость процессора" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (SpeedProcces like '%{TxtSearch.Text.ToLower()}%' or SpeedProcces like '%{TxtSearch.Text.ToUpper()}%' or SpeedProcces like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Производитель процессора" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (MakerProcc like '%{TxtSearch.Text.ToLower()}%' or MakerProcc like '%{TxtSearch.Text.ToUpper()}%' or MakerProcc like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Модель материнской платы" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (ModelMatePlat like '%{TxtSearch.Text.ToLower()}%' or ModelMatePlat like '%{TxtSearch.Text.ToUpper()}%' or ModelMatePlat like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Производитель материнской платы" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (MakerMaterPlat like '%{TxtSearch.Text.ToLower()}%' or MakerMaterPlat like '%{TxtSearch.Text.ToUpper()}%' or MakerMaterPlat like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Модель видеокарты" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (ModelVideos like '%{TxtSearch.Text.ToLower()}%' or ModelVideos like '%{TxtSearch.Text.ToUpper()}%' or ModelVideos like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Объем памяти видеокарты" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (VVideoMemory like '%{TxtSearch.Text.ToLower()}%' or VVideoMemory like '%{TxtSearch.Text.ToUpper()}%' or VVideoMemory like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Производитель видеокарты" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (MakerVideoCard like '%{TxtSearch.Text.ToLower()}%' or MakerVideoCard like '%{TxtSearch.Text.ToUpper()}%' or MakerVideoCard like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Модель RAM1" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (Model1 like '%{TxtSearch.Text.ToLower()}%' or Model1 like '%{TxtSearch.Text.ToUpper()}%' or Model1 like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Объем RAM1" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (V1 like '%{TxtSearch.Text.ToLower()}%' or V1 like '%{TxtSearch.Text.ToUpper()}%' or V1 like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Тип памяти RAM1" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (TypeMemory1 like '%{TxtSearch.Text.ToLower()}%' or TypeMemory1 like '%{TxtSearch.Text.ToUpper()}%' or TypeMemory1 like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Производитель RAM1" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (Maker1 like '%{TxtSearch.Text.ToLower()}%' or Maker1 like '%{TxtSearch.Text.ToUpper()}%' or Maker1 like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Модель RAM2"  && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (Model2 like '%{TxtSearch.Text.ToLower()}%' or Model2 like '%{TxtSearch.Text.ToUpper()}%' or Model2 like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Объем RAM2" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (V2 like '%{TxtSearch.Text.ToLower()}%' or V2 like '%{TxtSearch.Text.ToUpper()}%' or V2 like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Тип памяти RAM2" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (TypeMemory2 like '%{TxtSearch.Text.ToLower()}%' or TypeMemory2 like '%{TxtSearch.Text.ToUpper()}%' or TypeMemory2 like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Производитель RAM2" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (Maker2 like '%{TxtSearch.Text.ToLower()}%' or Maker2 like '%{TxtSearch.Text.ToUpper()}%' or Maker2 like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Модель RAM3" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (Model3 like '%{TxtSearch.Text.ToLower()}%' or Model3 like '%{TxtSearch.Text.ToUpper()}%' or Model3 like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Объем RAM3" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (V3 like '%{TxtSearch.Text.ToLower()}%' or V3 like '%{TxtSearch.Text.ToUpper()}%' or V3 like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Тип памяти RAM3" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (TypeMemory3 like '%{TxtSearch.Text.ToLower()}%' or TypeMemory3 like '%{TxtSearch.Text.ToUpper()}%' or TypeMemory3 like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Производитель RAM3" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (Maker3 like '%{TxtSearch.Text.ToLower()}%' or Maker3 like '%{TxtSearch.Text.ToUpper()}%' or Maker3 like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Модель RAM4" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (Model4 like '%{TxtSearch.Text.ToLower()}%' or Model4 like '%{TxtSearch.Text.ToUpper()}%' or Model4 like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Объем RAM4" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (V4 like '%{TxtSearch.Text.ToLower()}%' or V4 like '%{TxtSearch.Text.ToUpper()}%' or V4 like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Тип памяти RAM4" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (TypeMemory4 like '%{TxtSearch.Text.ToLower()}%' or TypeMemory4 like '%{TxtSearch.Text.ToUpper()}%' or TypeMemory4 like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (combtext == "Производитель RAM4" && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (Maker4 like '%{TxtSearch.Text.ToLower()}%' or Maker4 like '%{TxtSearch.Text.ToUpper()}%' or Maker4 like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("MenuPerTech");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        InforPcTex.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void TxtSearch_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // SelectTree(sender,e);
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SelectTree(sender, e);
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            //ExportToExcelPc
        }

        private void ListService_Click(object sender, RoutedEventArgs e)
        {
            MasterPanel master = new MasterPanel();
            this.Close();
            master.ShowDialog();
        }
        private void ListUsers_Click(object sender, RoutedEventArgs e)
        {
            AdminPanel admpnl = new AdminPanel();
            this.Close();
            admpnl.ShowDialog();
        }

        private void AddTec_Click(object sender, RoutedEventArgs e)
        {
            AddTech addtech = new AddTech();
            bool? result = addtech.ShowDialog();
            switch (result)
            {
                default:
                    LoadDB_InforPcTex();
                    LoadDB_InforPerTech();
                    LoadDB_InforDopOboryd();
                    break;
            }
        }

        private void EdiitTec_Click(object sender, RoutedEventArgs e)
        {
            if (IndexTabCont == 0)
            {
                Eddit_InforPcTex();
            }
            else if (IndexTabCont == 1)
            {
                Eddit_InforPerTech();
            }
            else if (IndexTabCont == 2)
            {
                Eddit_InforDopOboryd();
            }
        }

        private void ExcelPc_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcelPc();
        }

        private void ExportToExcelPc()
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

            for (int j = 0; j < InforPcTex.Columns.Count; j++)
            {
                try
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[1, j + 1];
                    sheet1.Cells[1, j + 1].Font.Bold = true;
                    sheet1.Columns[j + 1].ColumnWidth = 20;
                    myRange.Value2 = InforPcTex.Columns[j].Header;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


            for (int i = 0; i < InforPcTex.Columns.Count; i++)
            {
                for (int j = 0; j < InforPcTex.Items.Count; j++)
                {
                    TextBlock b = InforPcTex.Columns[i].GetCellContent(InforPcTex.Items[j]) as TextBlock;

                    if (b == null)
                        continue;

                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;

                }
            }
        }

        private void ExcelPer_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcelPerTech();
        }

        private void ReportMessgae_Click(object sender, RoutedEventArgs e)
        {
            string numbertech = null;
            ReportMessage report = new ReportMessage(numbertech);
            bool? result = report.ShowDialog();
            switch (result)
            {
                default:
                    LoadDB_InforPcTex();
                    LoadDB_InforPerTech();
                    LoadDB_InforDopOboryd();
                    break;
            }
        }

        private void CombSearchInfo_DropDownClosed(object sender, EventArgs e)
        {
            String combtext = CombSearchInfo.Text;
            MessageBox.Show(combtext);
        }
        private void ExportToExcelPerTech()
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

            for (int j = 0; j < InforPerTech.Columns.Count; j++)
            {
                try
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[1, j + 1];
                    sheet1.Cells[1, j + 1].Font.Bold = true;
                    sheet1.Columns[j + 1].ColumnWidth = 20;
                    myRange.Value2 = InforPerTech.Columns[j].Header;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


            for (int i = 0; i < InforPerTech.Columns.Count; i++)
            {
                for (int j = 0; j < InforPerTech.Items.Count; j++)
                {
                    TextBlock b = InforPerTech.Columns[i].GetCellContent(InforPerTech.Items[j]) as TextBlock;

                    if (b == null)
                        continue;

                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;

                }
            }
        }
    }
}

﻿using System;
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
VideoCards.ID as VideoCardID, VideoCards.Model as ModeVideos, VideoCards.VVideoMemory , MakersVideoCard.Name as MakerVideoCard,
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
            InitializeComponent();
            LoadDB_InforPcTex();
            LoadDB_InforPerTech();
            LoadDB_InforDopOboryd();
        }

        public void LoadDB_InforPcTex()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                try
                {
                connection.Open();
                    string query  = $@"SELECT MenuPerTech.ID as IDMenuPer, MenuPerTech.Name as NameYstr, TypeTechs.NameType,MenuPerTech.Kabunet ,Organiz.NameOrg, MenuPerTech.Number,
MenuPerTech.IDComponets as IDComponets, MenuPerTech.StartWork, MenuPerTech.EndWork,
MenuPerTech.Comments, Status.NameStatus, Works.NameWorks,
Procces.ID as ProccesID, Procces.Model as NameProcces ,Procces.Speed as SpeedProcces, MakersProcc.Name as MakerProcc,
MaterPlatas.ID as MaterPlatID, MaterPlatas.Model as ModelMatePlat, MakersMaterPlat.Name as MakerMaterPlat,
VideoCards.ID as VideoCardID, VideoCards.Model as ModeVideos, VideoCards.VVideoMemory , MakersVideoCard.Name as MakerVideoCard,
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
        private void Eddit_InforPerTech()
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
        private void Eddit_InforDopOboryd()
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
            //MessageBox.Show(Convert.ToString(IndexTabCont));
            if (IndexTabCont == 0)
            {
                Eddit_InforPcTex();
            }else if(IndexTabCont == 1)
            {
               
                Eddit_InforPerTech();
            }else if (IndexTabCont == 2)
            {
                Eddit_InforDopOboryd();
            }            
        }
        private void TabConrlMenuPer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          IndexTabCont =  TabConrlMenuPer.SelectedIndex;
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
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {

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
                    if (Organiz.IsSelected == true && IndexTabCont == 0)
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

                    if (Kabunet.IsSelected == true && IndexTabCont == 0)
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
                    
                    if (Number.IsSelected == true && IndexTabCont == 0)
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
                    if (DataStart.IsSelected == true && IndexTabCont == 0)
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
                    if (Status.IsSelected == true && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (Status.NameStatus like '%{TxtSearch.Text.ToLower()}%' or  Status.NameStatus like '%{TxtSearch.Text.ToUpper()}%' or  Status.NameStatus like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
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
                    if (Comment.IsSelected == true && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
                        string query = $@"{DBSearchPC} WHERE (MenuPerTech.Comments like '%{TxtSearch.Text.ToLower()}%' or MenuPerTech.Comments like '%{TxtSearch.Text.ToUpper()}%' or MenuPerTech.Comments like '%{TxtSearch.Text}%' ) and  MenuPerTech.IDTypeTech = '1'";
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
                    if (ModelProcces.IsSelected == true && IndexTabCont == 0)
                    {
                        InforPcTex.ItemsSource = null;
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
    }
}

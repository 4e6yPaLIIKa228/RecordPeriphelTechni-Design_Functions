using System;
using System.Collections.Generic;
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
        }

        private void AddUsers_Click(object sender, RoutedEventArgs e)
        {
            Registration addtech = new Registration();
            bool? result = addtech.ShowDialog();
            switch (result)
            {
                default:
                   // LoadDB_InforPcTex();
                  //  LoadDB_InforPerTech();
                  //  LoadDB_InforDopOboryd();
                    break;
            }
        }
    }
}

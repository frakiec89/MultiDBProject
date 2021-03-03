using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MultiDBProject
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            dtMain.SelectionChanged += DtMain_SelectionChanged; ;
            Run();
        }

        private void DtMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtMain.SelectedIndex >= 0)
            {
                try
                {
                    var s = dtMain.SelectedItem;
                    var f = new FileInfo(s.ToString());
                    lb0.Content = "Название файла " + f.Name;
                    lb1.Content = "Полный путь " + f.DirectoryName;
                    lb2.Content = "Размер файла " + f.Length / 1024 / 1024 + " mb";
                    lb3.Content = "Время создания " + f.CreationTime;
                    lb4.Content = "Последнее редактирование " + f.LastAccessTime;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        public  void Run ()
        {
            var backupFolder = @"\\192.168.10.160\задание\";

            List<string> fileEntries = Directory.GetFiles(backupFolder).Where(x=>x.EndsWith(@".bak")).ToList();
            dtMain.ItemsSource = fileEntries;
        }

        private void btGo_Click(object sender, RoutedEventArgs e)
        {
            if (dtMain.SelectedIndex >= 0)
            {
                try
                {
                    var s = dtMain.SelectedItem;
                    Administrator.RecoveryDatabase(s.ToString());
                    MessageBox.Show("Восстановление прошло успешно");
                    DialogResult = true;
                }catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }


}

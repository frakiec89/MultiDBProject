using Bogus;
using MultiDBProject.DB;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MultiDBProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            btPg.Click += BtPg_Click;
            btSql.Click += BtSql_Click;
            btAddGender.Click += BtAddGender_Click;
        }

        private void BtAddGender_Click(object sender, RoutedEventArgs e)
        {
            ApDBContext sqlContext = new MSSQLDBContext();
            ApDBContext pgContext = new PostGreSQLDBContext();

            if (sqlContext.Genders.Count() == 0 && pgContext.Genders.Count() == 0)
            {
                List<Gender> genders = new List<Gender>()
                { new Gender { Name = "Мужской" }, new Gender { Name = "Женский" } };

                sqlContext.Genders.AddRange(genders);
                sqlContext.SaveChanges();
                pgContext.Genders.AddRange(genders);
                pgContext.SaveChanges();
            }
            else
            {
                MessageBox.Show("Уже добавлены в бд");
            }




        }

        private void BtSql_Click(object sender, RoutedEventArgs e)
        {
            ApDBContext sqlContext = new MSSQLDBContext();
            sqlContext.Users.Add(NewUserGenerator());
            sqlContext.SaveChanges();
            dgSql.ItemsSource = sqlContext.Users.ToList();
        }

        private void BtPg_Click(object sender, RoutedEventArgs e)
        {
            ApDBContext pgContext = new PostGreSQLDBContext();
            pgContext.Users.Add(NewUserGenerator());
            pgContext.SaveChanges();
            dgPostgreSql.ItemsSource = pgContext.Users.ToList();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ApDBContext sqlContext = new MSSQLDBContext();
            ApDBContext pgContext = new PostGreSQLDBContext();
            dgSql.ItemsSource = sqlContext.Users.ToList();
            dgPostgreSql.ItemsSource = pgContext.Users.ToList();
        }

        private User NewUserGenerator()
        {

            //
            var generator = new Faker<User>("ru").RuleFor(x => x.Name, f => f.Name.LastName())
                .RuleFor(x => x.GenderId, f => f.Random.Number(1, 2));
                ;

            return generator.Generate();
        }

    }
}

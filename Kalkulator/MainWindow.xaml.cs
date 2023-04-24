using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
namespace Kalkulator
{
    public partial class MainWindow : Window
    {
        private string connectionString;
        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            LoadZgloszenia();
        }
        private void btnDodajZgloszenie_Click(object sender, RoutedEventArgs e)
        {
            string nazwaUzytkownika = tbNazwaUzytkownika.Text;
            string opisAwarii = tbOpisAwarii.Text;
            if (string.IsNullOrEmpty(nazwaUzytkownika) || string.IsNullOrEmpty(opisAwarii))
            {
                MessageBox.Show("Nazwa użytkownika i opis awarii nie mogą być puste.");
                return;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO zgloszenia (nazwa_uzytkownika, opis) VALUES(@nazwaUzytkownika, @opisAwarii)", connection);
               
                command.Parameters.AddWithValue("@nazwaUzytkownika", nazwaUzytkownika);
                command.Parameters.AddWithValue("@opisAwarii", opisAwarii);
                command.ExecuteNonQuery();
            }
            LoadZgloszenia();
        }
        private void btnUsunZgloszenie_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)((Button)e.Source).DataContext;
            int id = (int)row["Id"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM zgloszenia WHERE id = @id", connection);
               
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
            LoadZgloszenia();
        }
        private void LoadZgloszenia()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM zgloszenia",connection);
                DataTable zgloszeniaTable = new DataTable();
                adapter.Fill(zgloszeniaTable);
                dgZgloszenia.ItemsSource = zgloszeniaTable.DefaultView;
            }
        }
    }
}

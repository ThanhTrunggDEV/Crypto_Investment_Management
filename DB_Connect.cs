using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using CryptoManagement;
namespace Crypto_Investment_Management
{
    public class DB_Connect
    {
       private static string server = Environment.GetEnvironmentVariable("DB_SERVER");
       private static string userName = Environment.GetEnvironmentVariable("DB_USER");
       private static string passWord = Environment.GetEnvironmentVariable("DB_PASSWORD");
       private static string databaseName = Environment.GetEnvironmentVariable("DB_NAME");
       private string connectionString = $"server={server};port=3306;user={userName};password={passWord};database={databaseName};";

        public async void Get_Data()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM coin_info";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    Dictionary<string, double> coinPrice = new Dictionary<string, double>();
                    while (reader.Read())
                    {
                        string name = reader["coin_name"].ToString();
                        double buyPrice = double.Parse(reader["buying_price"].ToString());
                        double currentPrice = 0;
                        if (coinPrice.ContainsKey(name))
                        {
                            currentPrice = coinPrice[name];
                        }
                        else
                        {
                            var price = await Crypto.GetPrice(name.ToUpper());
                            coinPrice.Add(name, double.Parse(price.ToString()));
                            currentPrice = coinPrice[name];
                        }
                         
                        double owningCoin = double.Parse(reader["owned_coin"].ToString());
                        frmMain.coinList.Add(new Coin(name, buyPrice, currentPrice, owningCoin));
                    }
                    
                
                    reader.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void Delete_Data(string id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = $"DELETE FROM coin_info WHERE id = {id}";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteScalar();
                    

                    conn.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void Add_Invest()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptoManagement;

namespace Crypto_Investment_Management
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        public static List<Coin> coinList = new List<Coin>();
        private DB_Connect dataBase = new DB_Connect();

        private void Load_List_View()
        {
            int count = 1;
            foreach (Coin coin in coinList)
            {
                ListViewItem item = new ListViewItem(count++ + "");
                item.SubItems.Add(coin.CoinName);
                item.SubItems.Add(coin.BuyingPrice.ToString() + " VND");
                item.SubItems.Add(coin.CurrentPrice.ToString() + " VND");
                item.SubItems.Add(coin.OwnedCoin.ToString());
                item.SubItems.Add(Math.Ceiling(coin.Profit).ToString() + " VND");
                lvCoin.Items.Add(item);
            }
        }

        private void Cal_Total_Investment()
        {
            double sum = 0;
            foreach (Coin coin in coinList)
            {
                sum += coin.OwnedCoin * coin.BuyingPrice;
            }
            txtTotal.Text = Math.Ceiling(sum).ToString() + " VND";
        }
        private void Cal_Profit()
        {
            double sum = 0;
            foreach(Coin coin in coinList)
            {
                sum += coin.Profit;
            }
            txtProfit.Text = Math.Ceiling(sum).ToString() + " VND";
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            dataBase.Get_Data();
        }

        
        private void tmLoad_Tick(object sender, EventArgs e)
        {
            lvCoin.Items.Clear();
            Load_List_View();
            Cal_Total_Investment();
            Cal_Profit();
        }

        private void tmReload_Tick(object sender, EventArgs e)
        {
            coinList.Clear();
            dataBase.Get_Data();
        }
    }
}

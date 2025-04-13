using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Investment_Management
{
    public class Coin
    {
        public string CoinName {  get; set; }
        public double BuyingPrice { get; set; }
        public double CurrentPrice {  get; set; }
        public double OwnedCoin { get; set; }
        public double Profit {  get; private set; }
        public Coin() { }
        private void Cal_Profit()
        {
            Profit = (OwnedCoin * CurrentPrice) - (OwnedCoin * BuyingPrice);
        }
        public Coin(string coinName, double buyingPrice, double currentPrice, double ownedCoin)
        {
            CoinName = coinName;
            BuyingPrice = buyingPrice;
            CurrentPrice = currentPrice;
            OwnedCoin = ownedCoin;
            Cal_Profit();
        }
        public override string ToString()
        {
            return $"Coin Name: {CoinName}\n" +
                   $"Buying Price: {BuyingPrice}\n" +
                   $"Current Price: {CurrentPrice}\n" +
                   $"Owned Coin: {OwnedCoin}" +
                   $"Profit: {Profit}";
        }
    }
}

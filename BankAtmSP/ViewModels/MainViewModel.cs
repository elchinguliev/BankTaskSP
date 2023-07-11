using BankAtmSP.Commands;
using BankAtmSP.Models;
using BankAtmSP.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace BankAtmSP.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        public RelayCommand EnterCardBtn { get; set; }
        public RelayCommand LoadCommand { get; set; }
        public RelayCommand TransferMoneyCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }

        public CardRepo cardRepo { get; set; }
        public List<Card> Cards { get; set; }
        public Card Card { get; set; }
        public MainWindow MainWindow { get; set; }
        public Mutex Mutex { get; }
        private static Mutex _mutex = null;
        bool createdNew;




        public static object obj = new object();

        public MainViewModel(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            Cards = new List<Card>();
            cardRepo = new CardRepo();
            Cards = cardRepo.GetAll();
            EnterCardBtn = new RelayCommand((objj) =>
            {
                mainWindow.cardNumberTxt.Visibility = Visibility.Visible;
                mainWindow.cardNumberLbl.Visibility = Visibility.Visible;
                mainWindow.loadBtn.Visibility = Visibility.Visible;
            });
            LoadCommand = new RelayCommand((objj) =>
            {
                foreach (var item in Cards)
                {
                    if (item.CardNumber == mainWindow.cardNumberTxt.Text)
                    {
                        mainWindow.cardHolderLbl.Visibility = Visibility.Visible;   
                        mainWindow.balanceLbl.Visibility = Visibility.Visible;
                        mainWindow.cardHolderNameTxt.Visibility = Visibility.Visible;
                        mainWindow.balanceTxtBox.Visibility = Visibility.Visible;
                        mainWindow.transferredMoneyLbl.Visibility=Visibility.Visible;
                        mainWindow.transferredMoneyTxtBox.Visibility=Visibility.Visible;
                        mainWindow.transferBtn.Visibility=Visibility.Visible;
                        mainWindow.cardHolderNameTxt.Text = item.CardHolderName;
                        mainWindow.balanceTxtBox.Text = item.Balance.ToString();
                        Card = item;
                    }
                }
            });

            TransferMoneyCommand = new RelayCommand((objj) =>
            {      
                lock (obj)
                {
                    if (Card.Balance >= decimal.Parse(mainWindow.transferredMoneyTxtBox.Text))
                    {
                        Thread.Sleep(5000);
                        var neww = int.Parse(mainWindow.transferredMoneyTxtBox.Text);
                        var neww1=Card.Balance-neww;
                        mainWindow.balanceTxtBox.Text=neww1.ToString();
                      
                    }
                    else
                    {
                        MessageBox.Show(@"Not enough balance!
                                        Try again with possible cash out!");
                        mainWindow.transferredMoneyTxtBox.Text="";
                    }
                }
            });

            CloseCommand = new RelayCommand((obj) =>
            {
                const string appName = "MyAppName";

                _mutex = new Mutex(true, appName, out createdNew);

                if (!createdNew)
                {
                    //app is already running! Exiting the application  
                    MessageBox.Show("App is already running");
                    //Application.Current.Shutdown();
                }
            });




        }

    }
}

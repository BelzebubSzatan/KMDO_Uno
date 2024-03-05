using KMDO_Uno.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KMDO_Uno
{
    public partial class MainPage : ContentPage
    {
        Deck deck = new Deck();
        List<Card> playerCards= new List<Card>();
        List<Card> computerCards= new List<Card>();
        Card middleCard= null;
        bool win=false;
        bool playerAction = true;
        public MainPage()
        {
            InitializeComponent();
            playerCards = deck.GetPlayerCards(3);
            computerCards = deck.GetPlayerCards(3);
            RenderPlayerCards();
            SetMiddleCard(null);
        }
        void SetMiddleCard(Card c)
        {

        }
        void RenderPlayerCards()
        {

        }
        private void PlayerCardClick(object sender, EventArgs e)
        {

        }
        void WinCheck()
        {

        }
        void SpecialCard()
        {

        }
        void ComputerMove() { }
        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}

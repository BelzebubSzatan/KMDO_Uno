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
            if (win) return;
            PlayersCards.Children.Clear();
            playerCards=playerCards.OrderBy(z=>z.Color.ToString()).ToList();
            foreach (Card c in playerCards)
            {
                Button card = c.Render();
                card.Clicked += PlayerCardClick;
                card.BindingContext = c;
                if (playerAction)
                {
                    if (middleCard.Color == c.Color || middleCard.Value == c.Value||c.Action==SpecialActions.Color) {
                        card.BorderWidth = 2;
                        card.BorderColor = Color.Black;
                    }
                }
                PlayersCards.Children.Add(card);
            }
            ComputerCards.Text = "Komputer ma: " + computerCards.Count();
        }
        private void PlayerCardClick(object sender, EventArgs e)
        {
            if (win) return;
            Card card=(sender as Button).BindingContext as Card;
            if (playerAction)
            {
                if((sender as Button).BorderColor == Color.Black)
                {
                    if (card.Action != SpecialActions.Normal)
                        SpecialCard(computerCards,card);
                    middleCard=card;
                    playerCards.Remove(card);
                    RenderPlayerCards();
                    WinCheck();
                    playerAction = false;
                }
            }
        }
        void WinCheck()
        {

        }
        void SpecialCard(List<Card> target,Card c)
        {

        }
        void ComputerMove() { }
        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}

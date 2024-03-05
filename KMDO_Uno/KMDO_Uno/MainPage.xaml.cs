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
            if(middleCard==null) 
            {
                middleCard = deck.deck[0];
                deck.deck.RemoveAt(0);  
            }else
            {
                middleCard = c;
            }
            LastCardText.Text = middleCard.Value.ToString();
            LastCardStack.BackgroundColor = middleCard.Color;
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
        async void WinCheck()
        {
            if(playerCards.Count == 0) 
            {
                win = true;
                await DisplayAlert("Wygrana!", "Wygrał gracz", "OK");
            }
            if(computerCards.Count == 0) 
            {
                win = true;
                await DisplayAlert("Wygrana!", "Wygrał komputer", "OK");
            }
        }
        void SpecialCard(List<Card> Target, Card c)
        {
            if (deck.deck.Count < 10)
                deck.GenerateDeck();
            if(c.Action == SpecialActions.AddTwo)
            {
                for (int i = 0; i < 2; i++)
                {
                    Target.Add(deck.deck[i]);
                    deck.deck.RemoveAt(0);
                }
            }
            if (c.Action == SpecialActions.AddFour)
            {
                for (int i = 0; i < 4; i++)
                {
                    Target.Add(deck.deck[i]);
                    deck.deck.RemoveAt(0);
                }
            }
        }
        void ComputerMove() 
        {
            if (win) return;
            List<Card> possibleMove = computerCards.Where(e=>(e.Value==middleCard.Value||e.Color==middleCard.Color || e.Action == SpecialActions.Color)).ToList();
            if(possibleMove.Count > 0) 
            {
                Random r = new Random();
                int n = r.Next(possibleMove.Count);
                Task.Delay(1000);
                SetMiddleCard(possibleMove[n]);
                ComputerAction.Text= "Komputer dał"+ possibleMove[n].Value+ " "+possibleMove[n].Color.ToString();
                SpecialCard(playerCards, possibleMove[n]);
                computerCards.Remove(possibleMove[n]);
                playerAction = true;
            }else
            {
                Task.Delay(1000);
                computerCards.Add(deck.deck[0]);
                ComputerAction.Text = "Komputer dobrał";
                deck.deck.RemoveAt(0);
            }
            playerAction = true;
            WinCheck();
            RenderPlayerCards();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            if(playerAction&&!win)
            {
                playerCards.Add(deck.deck[0]);
                deck.deck.RemoveAt(0);
                playerAction = false;
                ComputerMove();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMDO_Uno.models
{
    public class Deck
    {
        public List<Card> deck = new List<Card>();
        private readonly List<System.Drawing.Color> colors = new List<System.Drawing.Color>()
        {
            System.Drawing.Color.Red, System.Drawing.Color.Green,
            System.Drawing.Color.Blue, System.Drawing.Color.Yellow,
        };
        private readonly List<string> Values = new List<string>() {
            "1","2","3","4","5","6","7","8","9","+2","+4","kolor"
        };
        public Deck()
        {
            GenerateDeck();
        }
        public void GenerateDeck()
        {
            foreach (var value in Values)
            {
                foreach (var color in colors)
                {
                    deck.Add(new Card()
                    {
                        Action = GetAction(value),
                        Value = value,
                        Color
                        = color
                    });
                }
            }
            Shuffle();
        }
        private void Shuffle()
        {
            Random r=new Random();
            deck=deck.OrderBy(z=>r.Next()).ToList();
        }
        private SpecialActions GetAction(string value)
        {
            switch (value)
            {
                case "+2":
                    return SpecialActions.AddTwo;
                case "+4":
                    return SpecialActions.AddFour;
                case "kolor":
                    return SpecialActions.Color;
                default:
                    return SpecialActions.Normal;
            }
        }
        public List<Card> GetPlayerCards(int n)
        {
            List<Card> cards = deck.GetRange(0,n).OrderBy(z=>z.Color.ToString()).ToList();
            deck.RemoveRange(0,n);
            return cards;
        }
    }
}

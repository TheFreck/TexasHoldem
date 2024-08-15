using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace TexasHoldem
{
    public class Poker
    {

        public static (string type, string[] ranks) Hand(string[] holeCards, string[] communityCards)
        {
            var cards = holeCards.Concat(communityCards);
            var (suits, ranks) = SortCards(cards);

            var flushCards = suits.Where(s => s.Value.Count >= 5);
            bool flush = flushCards.Count() > 0 ? true : false;
            var fourCards = cards.GroupBy(c => c.Substring(0, c.Length - 1)).Where(g => g.Count() == 4);
            bool four = fourCards.Count() > 0;
            var threeCards = cards.GroupBy(c => c.Substring(0, c.Length - 1)).Where(g => g.Count() == 3);
            bool three = threeCards.Count() > 0;
            var twoCards = cards.GroupBy(c => c.Substring(0, c.Length - 1)).Where(g => g.Count() == 2);
            bool twoPair = twoCards.Count() > 1;
            var pairCards = cards.GroupBy(c => c.Substring(0, c.Length - 1)).Where(g => three ? g != threeCards.FirstOrDefault() && g.Count() == 2 : g.Count() == 2);
            bool pair = pairCards.Count() > 0;
            bool fullHouse = three && pairCards.Count() > 0;
            var o = 0;
            var cardsOfStraight = ranks.TakeWhile((r, i) =>
                r != null && ranks[i - 1] != null
                );
            var straightCards = new List<(int, string)>();
            for (var i = 13; i >= 0; i--)
            {
                if (ranks[i] == null)
                {
                    straightCards = new List<(int, string)>();
                }
                else if (straightCards.Count > 0 && straightCards.Last().Item1 == i + 1)
                {
                    straightCards.Add((i, String.Join("", ranks[i])));
                    if (straightCards.Count >= 5) break;
                }
                else
                {
                    straightCards.Add((i, String.Join("", ranks[i])));
                    if (straightCards.Count >= 5) break;
                }
            }

            return ("nothing", new[] { "A", "Q", "9", "6", "3" });
        }

        public static Tuple<Dictionary<char, List<int>>, List<string>[]> SortCards(IEnumerable<string> cards)
        {
            var suits = new Dictionary<char, List<int>>
            {
                {'♠',new List<int>() },
                {'♥',new List<int>() },
                {'♣',new List<int>() },
                {'♦',new List<int>() }
            };
            var ranks = new List<string>[14];

            foreach (var card in cards)
            {
                var rank = card.Substring(0, card.Length - 1);
                var rankInt = 0;
                switch (rank)
                {
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                    case "10":
                        rankInt = Convert.ToInt16(rank);
                        break;
                    case "J":
                        rankInt = 11;
                        break;
                    case "Q":
                        rankInt = 12;
                        break;
                    case "K":
                        rankInt = 13;
                        break;
                    case "A":
                        rankInt = 14;
                        break;
                }
                suits[card.Last()].Add(rankInt);
                if (ranks[rankInt] != null)
                {
                    ranks[rankInt].Add(card.Last().ToString());
                }
                else
                {
                    ranks[rankInt] = new List<string> { card.Last().ToString() };
                }
            }

            return new (suits, ranks);
        }

        public static (string type, string[] ranks) SortHand(Dictionary<char, List<int>> suits, List<string>[] ranks)
        {
            var fourCard = Array.IndexOf(ranks,ranks.Where(r => r!=null && r.Count == 4).FirstOrDefault());
            var ablah = ranks.Select((_, i) => (i, _)).Where(r => r.Item2 != null && r.Item1 != fourCard);
            var nextBest4 = ranks.Select((_, i) => (i,_)).Where(r=> r.Item2 != null && r.Item2.Count > 0 && r.Item1 != fourCard).Max(m => m.Item1);
            var fourSuits = fourCard > 0 ? ranks[fourCard] : new List<string>();
            bool four = ranks.Where(r => r != null && r.Count == 4).Any();
            var threeCard = Array.IndexOf(ranks, ranks.Where((r, i) => i != fourCard && r != null && r.Count == 3).FirstOrDefault());
            var nextBest3 = ranks.Select((_, i) => (i, _)).Where(r => r.Item2 != null && r.Item1 != threeCard).Max(m => m.Item1);
            var threeSuits = threeCard > 0 ? ranks[threeCard] : new List<string>();
            bool three = ranks.Where((r, i) => i != fourCard && r != null && r.Count == 3).Any();
            var pair1Card = Array.IndexOf(ranks,ranks.Where((r, i) => i != threeCard && r != null && r.Count == 2).FirstOrDefault());
            var pair1Suits = ranks[pair1Card];
            var pair2Card = Array.IndexOf(ranks,ranks.Where((r, i) => i != threeCard && r != null && r.Count == 2).LastOrDefault());
            var pair2Suits = ranks[pair2Card];
            bool twoPair = pair1Card != null && pair2Card != null && pair1Card != pair2Card;
            var nextBestTwoPair = ranks.Select((_, i) => (i, _)).Where(r => r.Item2.Count > 0 && r.Item1 != pair1Card && r.Item1 != pair2Card).Max(m => m.Item1);
            var pairCard = Array.IndexOf(ranks, ranks.Where((r, i) => i != threeCard && r != null && r.Count == 2).LastOrDefault());
            var pairSuits = ranks[pairCard];
            bool pair = pairCard != null && pairCard > 1;
            var nextBestPair = ranks.Select((_, i) => (i, _)).Where(r => r.Item2 != null && r.Item1 != pairCard).Max(m => m.Item1);
            var fullCards = new [] { threeCard.ToString(), pairCard.ToString() };
            bool fullHouse = three && pair;
            var flushCards = suits.Where(s => s.Value.Count >= 5).FirstOrDefault();
            bool flush = suits.Where(s => s.Value.Count >= 5).Any();
            var straightCards = new List<(int, string, string)> ();
            for(var i=14; i>= 0; i--)
            {
                if (ranks[i].Count > 0 && (straightCards.LastOrDefault().Item1 == i+1 || straightCards.Count == 0))
                {
                    straightCards.Add((i,BackToString(i), String.Join("",ranks[i])));
                    if (straightCards.Count == 5) 
                        break;
                }
                else
                {
                    straightCards = new List<(int, string, string)>();
                }
            }
            bool straight = straightCards.Count >= 5;

            var strFlushCards = new List<string>();
            if (flush && straight)
            {
                foreach(var card in straightCards)
                {
                    if (card.Item2.Contains(flushCards.Key))
                    {
                        strFlushCards.Add(card.Item2+flushCards.Key);
                    }
                }
            }
            bool strFlush = strFlushCards.Count >= 5;

            var nothing = ranks.Select((r,i) => (r,i)).Where(r => r.r != null).OrderByDescending(d => BackToString(d.i)).Select(c => BackToString(c.i));

            return strFlush ? ("straight-flush", strFlushCards.ToArray()) :
                four ? ("four-of-a-kind", fourSuits.Select(s => new string[] { BackToString(fourCard), BackToString(nextBest4) }).FirstOrDefault()) :
                fullHouse ? ("full house", fullCards.ToArray()) :
                flush ? ("flush", flushCards.Value.OrderByDescending(v => v).Select(f => BackToString(f)).ToArray()) :
                straight ? ("straight", straightCards.Select(s => s.Item2).Take(5).ToArray()) :
                three ? ("three-of-a-kind", new string[] { BackToString(threeCard), BackToString(nextBest3) }) :
                twoPair ? ("two pair", new string[] {BackToString(Math.Max(pair1Card,pair2Card)),BackToString(Math.Min(pair1Card, pair2Card)), BackToString(nextBestTwoPair)}) :
                pair ? ("pair", new string[] {BackToString(pairCard), BackToString(nextBestPair)}) :
                ("nothing", nothing.ToArray());
        }

        public static string BackToString(int rank)
        {
            return rank == 14 ? "A" : rank == 13 ? "K" : rank == 12 ? "Q" : rank == 11 ? "J" : rank.ToString();
        }
    }
}

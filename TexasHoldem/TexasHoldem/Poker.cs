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
            var organized = holeCards.Concat(communityCards)
                .Select(c =>
                (
                    c.First() == '1' ? 10 :
                    c.First() == 'A' ? 14 :
                    c.First() == 'K' ? 13 :
                    c.First() == 'Q' ? 12 :
                    c.First() == 'J' ? 11 :
                    Convert.ToInt32(c.First().ToString()),
                c.Substring(0, c.Length - 1),
                c.Last())
                )
                .OrderByDescending(o => o);

            var groups = organized.GroupBy(
                    c => c,
                    h => organized,
                    (card, hand) => new
                    {
                        Card = card,
                        Ranks = hand.First().Where(h => h.Item1 == card.Item1).ToArray(),
                        Suits = hand.First().Where(h => h.Item3 == card.Item3).ToArray(),
                        Straight = hand.First().Where(h => h.Item1 >= card.Item1 - 4 && h.Item1 <= card.Item1).DistinctBy(d => d.Item2).ToArray(),
                        StraightFlush = hand.First().Where(h => h.Item1 >= card.Item1 - 4 && h.Item1 <= card.Item1 && h.Item3 == card.Item3).DistinctBy(d => d.Item2).ToArray(),
                    }
                ).ToArray();

            if (groups.Where(s => s.StraightFlush.Count() >= 5).Count() > 0)
                return ("straight-flush",
                    groups.Where(s => s.StraightFlush.Count() >= 5).FirstOrDefault()?.StraightFlush.Select(s => s.Item2).ToArray());
            else if (groups.Where(s => s.Ranks.Count() == 4).Count() > 0)
                return ("four-of-a-kind", new[]
                {
                    groups.Where(s => s.Ranks.Count() == 4).FirstOrDefault()?.Ranks.Select(s => s.Item2).First(),
                    groups.Where(s => s.Ranks.Count() < 4).First().Card.Item2
                });
            else if (groups.Where(s => s.Ranks.Count() >= 3).Count() > 0 && groups.Where(s => s.Ranks.Count() >= 2).DistinctBy(d => d.Card.Item2).Count() >= 2)
            {
                var x1 = groups.Where(s => s.Ranks.Count() >= 3).DistinctBy(d => d.Card.Item2).ToArray();
                var x2 = groups.Where(s => s.Ranks.Count() >= 2).DistinctBy(d => d.Card.Item2).Count() > 2;
                var one = groups.Where(s => s.Ranks.Count() == 3).First().Card.Item2;
                var two = groups.Where(s => s.Ranks.Count() >= 2 && s.Card.Item2 != one).First().Card.Item2;
                return ("full house", new[]{
                    one,
                    groups.Where(s => s.Ranks.Count() >= 2 && s.Card.Item2 != one).First().Card.Item2
                });
            }
            else if (groups.Where(s => s.Suits.Count() >= 5).Count() > 0)
                return ("flush",
                    groups.Where(s => s.Suits.Count() >= 5).First().Suits.Select(s => s.Item2).Take(5).ToArray());
            else if (groups.Where(s => s.Straight.Count() >= 5).Count() > 0)
                return ("straight",
                    groups.Where(s => s.Straight.Count() >= 5).First().Straight.Select(s => s.Item2).Take(5).ToArray());
            else if (groups.Where(s => s.Ranks.Count() == 3).Count() > 0)
            {
                var three = groups.Where(s => s.Ranks.Count() == 3).First().Ranks.Select(s => s.Item2).First();
                return ("three-of-a-kind", new[]{
                    three,
                    groups.Where(s => s.Card.Item2 != three).First().Card.Item2,
                    groups.Where(s => s.Card.Item2 != three).Skip(1).First().Card.Item2
                });
            }
            else if (groups.Where(s => s.Ranks.Count() == 2).DistinctBy(d => d.Card.Item2).Count() >= 2)
            {
                var pair1 = groups.Where(s => s.Ranks.Count() == 2).Select(s => s.Card.Item2).Distinct().First();
                var pair2 = groups.Where(s => s.Ranks.Count() == 2).Select(s => s.Card.Item2).Distinct().Skip(1).First();
                return ("two pair", new[]{
                    pair1,
                    pair2,
                    groups.Where(s => s.Ranks.First().Item2 != pair1 && s.Ranks.First().Item2 != pair2).First().Card.Item2
                });
            }
            else if (groups.Where(s => s.Ranks.Count() == 2).Count() > 0)
                return ("pair", new[]{
                    groups.Where(s => s.Ranks.Count() == 2).First().Card.Item2,
                    groups.Where(s => s.Ranks.Count() == 1).First().Card.Item2,
                    groups.Where(s => s.Ranks.Count() == 1).Skip(1).First().Card.Item2,
                    groups.Where(s => s.Ranks.Count() == 1).Skip(2).First().Card.Item2,
                });
            else
                return ("nothing", organized.Select(s => s.Item2).Take(5).ToArray());
        }
    }
}

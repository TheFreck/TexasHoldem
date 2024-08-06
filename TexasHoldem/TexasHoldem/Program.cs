using TexasHoldem;

var hand = Poker.Hand(new[] { "K♠", "A♦" }, new[] { "J♣", "Q♥", "10♥", "2♥", "3♦" });

Console.WriteLine(hand.type);
for(var i=0; i<hand.ranks.Length; i++)
{
    Console.WriteLine(hand.ranks[i]);
}
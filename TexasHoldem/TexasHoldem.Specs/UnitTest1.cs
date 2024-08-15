using Machine.Specifications;

namespace TexasHoldem.Specs
{
    public class When_Taking_Dealt_Cards
    {
        Establish context = () =>
        {
            holeInputs = new string[][]
            {
                new[] { "K♠", "J♦" },
                new[] { "K♠", "Q♦" },
                new []{ "K♠", "Q♦" },
                new[] { "4♠", "9♦" },
                new[] { "A♠", "K♦" },
                new[] { "2♠", "3♦" },
            };
            communityInputs = new string[][]
            {
                new[] { "10♥", "9♥", "Q♣", "K♥", "Q♥" },
                new[] { "K♣", "Q♥", "K♥", "2♥", "3♦" },
                new []{ "J♣", "Q♥", "9♥", "2♥", "3♦" },
                new[] { "J♣", "Q♥", "Q♠", "2♥", "Q♦" },
                new[] { "J♥", "5♥", "10♥", "Q♥", "3♥" },
                new[] { "2♣", "2♥", "3♠", "3♥", "2♦" },
            };
            expectations = new (string type, string[] ranks)[]
            {
                ("straight", new []{"K","Q","J","10","9"}),
                ("full house", new []{"Q","Q","Q","K","K"}),
                ("two pair", new[] { "K", "J", "9" }),
                ("three-of-a-kind", new[] { "Q", "J", "9" }),
                ("flush", new[] { "Q", "J", "10", "5", "3" }),
                ("four-of-a-kind",  new[] { "2", "3" }),
            };
            answer = new (string type, string[] ranks)[holeInputs.Length];
        };

        Because of = () =>
        {
            for (var i = 0; i < holeInputs.Length; i++)
            {
                answer[i] = Poker.Hand(holeInputs[i], communityInputs[i]);
            }
        };

        It Should_Return_A_Tuple_With_Type_And_Ranks = () =>
        {
            for (var i = 0; i < answer.Length; i++)
            {
                answer[i].type.ShouldEqual(expectations[i].type);
                for (var j = 0; j < answer[i].ranks.Length; j++)
                {
                    answer[i].ranks[j].ShouldEqual(expectations[i].ranks[j]);
                }
            }
        };

        private static string[][] holeInputs;
        private static string[][] communityInputs;
        private static (string type, string[] ranks)[] expectations;
        private static (string type, string[] ranks)[] answer;
    }

    public class When_Sorting_Hand_By_Rank_And_Suit
    {
        Establish context = () =>
        {
            cards = new string[][]
            {
                new[] { "K♠", "J♦","10♥", "9♥", "Q♣", "K♥", "Q♥" }
            };
            suits = new Dictionary<char, List<int>> {
                { '♠', new List<int> { 13 } },
                { '♦', new List<int> { 11 } },
                { '♥', new List<int> { 13, 12, 10, 9 } },
                { '♣', new List<int> { 12 } }
            };
            ranks = new List<string>[] { null, null, null, null, null, null, null, null, null,
                new List<string> { "♥" },
                new List<string> { "♥" },
                new List<string> { "♦" },
                new List<string> { "♣", "♥" },
                new List<string> { "♠", "♥" },
                null 
            };

            expectations = new Tuple<Dictionary<char, List<int>>, List<string>[]>[]
            {
                new (suits, ranks)
            };
            answers = new Tuple<Dictionary<char, List<int>>, List<string>[]>[cards.Length];
        };

        Because of = () =>
        {
            for (var i = 0; i < cards.Length; i++)
            {
                answers[i] = Poker.SortCards(cards[i]);
            }
        };

        It Should_Return_Dictionary_Of_Suits = () =>
        {
            for (var i = 0; i < expectations.Length; i++)
            {
                var suits = 4;
                var expectation = expectations[i];
                for (var j = 0; j < suits; j++)
                {
                    var suit = expectation.Item1.Keys.ToArray()[j];
                    var sortedSuit = answers[i].Item1[suit].OrderByDescending(x => x);
                    for (var k = 0; k < sortedSuit.Count(); k++)
                    {
                        sortedSuit.ToArray()[k].ShouldEqual(expectation.Item1[suit][k]);
                    }
                }
            }
        };

        It Should_Return_An_Array_Of_Ranks = () =>
        {
            for (var i = 0; i < expectations.Length; i++)
            {
                var expectation = expectations[i].Item2;
                var answer = answers[i].Item2;
                for (var j = 0; j < expectation.Length; j++)
                {
                    if (expectation[j] != null)
                    {
                        for (var k = 0; k < expectation[j].Count; k++)
                        {
                            if (expectation[j][k] != null)
                            {
                                for (var l = 0; l < expectation[j][k].Length; l++)
                                {
                                    answer[j][k].ShouldEqual(expectation[j][k]);
                                }
                            }
                        }
                    }
                }
            }
        };

        private static string[][] cards;
        private static Dictionary<char, List<int>> suits;
        private static List<string>[] ranks;
        private static Tuple<Dictionary<char, List<int>>, List<string>[]>[] expectations;
        private static Tuple<Dictionary<char, List<int>>, List<string>[]>[] answers;
    }

    public class When_Building_A_Hand_From_Sorted_Cards
    {
        Establish context = () =>
        {
            suits = new Dictionary<char, List<int>>[] 
            {
                new Dictionary<char, List<int>>
                {
                    { '♠', new List<int> { 2 } },
                    { '♦', new List<int> { 3 } },
                    { '♥', new List<int> { 5, 2, 9, 8, 14 } },
                    { '♣', new List<int> { } }
                },
                new Dictionary<char, List<int>>
                {
                    { '♠', new List<int> { 2 } },
                    { '♦', new List<int> { 3 } },
                    { '♥', new List<int> { 5, 7, 9, 8, 6 } },
                    { '♣', new List<int> { } }
                },
                new Dictionary<char, List<int>>
                {
                    { '♠', new List<int> { 2, 7 } },
                    { '♦', new List<int> { 7, 3, 9 } },
                    { '♥', new List<int> { 7 } },
                    { '♣', new List<int> { 7 } }
                },
                new Dictionary<char, List<int>>
                {
                    { '♠', new List<int> { 14,13 } },
                    { '♦', new List<int> { 14,9 } },
                    { '♥', new List<int> { 3 } },
                    { '♣', new List<int> { 14,3 } }
                },
                new Dictionary<char, List<int>>
                {
                    { '♠', new List<int> { 14,10 } },
                    { '♦', new List<int> {  11 } },
                    { '♥', new List<int> { 13,12,9 } },
                    { '♣', new List<int> {  } }
                },
                new Dictionary<char, List<int>>
                {
                    { '♠', new List<int> { 4 } },
                    { '♦', new List<int> {  4,11 } },
                    { '♥', new List<int> { 9 } },
                    { '♣', new List<int> {  4} }
                },
                new Dictionary<char, List<int>>
                {
                    { '♠', new List<int> { 9 } },
                    { '♦', new List<int> {  3, 14 } },
                    { '♥', new List<int> { 9 } },
                    { '♣', new List<int> {  3 } }
                },
                new Dictionary<char, List<int>>
                {
                    { '♠', new List<int> { 7 } },
                    { '♦', new List<int> {  2,3 } },
                    { '♥', new List<int> { 7 } },
                    { '♣', new List<int> { 4 } }
                }
            };
            ranks = new List<string>[][] 
            {
                new List<string>[]
                {
                    new List<string>{ },
                    new List<string>{ }, 
                    new List<string>{ "♠","♥"}, 
                    new List<string>{ "♦"}, 
                    new List<string>{ }, 
                    new List<string>{ "♥"},
                    new List<string>{ }, 
                    new List<string>{ }, 
                    new List<string>{ "♥"},
                    new List<string>{ "♥"},
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ "♥" }
                },
                new List<string>[]
                {
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ "♠"},
                    new List<string>{ "♦"},
                    new List<string>{ },
                    new List<string>{ "♥"},
                    new List<string>{ "♥"},
                    new List<string>{ "♥"},
                    new List<string>{ "♥"},
                    new List<string>{ "♥"},
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ }
                },
                new List<string>[]
                {
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ "♠"},
                    new List<string>{ "♦"},
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ "♠", "♦", "♥", "♣" },
                    new List<string>{ },
                    new List<string>{ "♦"},
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ }
                },
                new List<string>[]
                {
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ "♥", "♣"},
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ "♦"},
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ "♠"},
                    new List<string>{ "♠", "♦", "♣" }
                },
                new List<string>[]
                {
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ "♥"},
                    new List<string>{ "♠"},
                    new List<string>{ "♦"},
                    new List<string>{ "♥"},
                    new List<string>{ "♠","♥"},
                    new List<string>{ "♠" }
                },
                new List<string>[]
                {
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ "♠","♦","♣"},
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ "♥"},
                    new List<string>{ },
                    new List<string>{ "♦"},
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{  }
                },
                new List<string>[]
                {
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ "♦","♣"},
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ "♥","♠"},
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ "♦" }
                },
                new List<string>[]
                {
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ "♦"},
                    new List<string>{ "♦"},
                    new List<string>{ "♣"},
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ "♥","♠"},
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ },
                    new List<string>{ }
                }
            };
            expectations = new (string type, string[] ranks)[]
            {
                ("flush", new []{"A","9","8","5","2"}),
                ("straight-flush", new []{"9","8","7","6","5"}),
                ("four-of-a-kind", new []{ "7","9"}),
                ("full house", new []{"K","3"}),
                ("straight", new []{"A","K","Q","J","10"}),
                ("three-of-a-kind", new []{"4","J"}),
                ("two pair", new []{"9","3","A"}),
                ("pair", new []{ "7","4"})
            };
            answers = new (string type, string[] ranks)[suits.Length];
        };

        Because of = () =>
        {
            for (var i = 0; i < suits.Length; i++)
            {
                for (var j = 0; j < suits[i].Count; j++)
                {
                    answers[i] = Poker.SortHand(suits[i], ranks[i]);
                }
            }
        };

        It Should_Return_A_Hand_And_The_Cards_That_Make_It = () =>
        {
            for (var i=0; i<answers.Length; i++)
            {
                answers[i].type.ShouldEqual(expectations[i].type);
                for(var j=0; j < answers[i].ranks.Length; j++)
                {
                    answers[i].ranks[j].ShouldEqual(expectations[i].ranks[j]);
                }
            }
        };

        private static Dictionary<char, List<int>>[] suits;
        private static List<string>[][] ranks;
        private static (string type, string[] ranks)[] expectations;
        private static (string type, string[] ranks)[] answers;
    }
}
using Machine.Specifications;

namespace TexasHoldem.Specs
{
    public class When_Finding_Poker_Hands_From_Deal
    {
        Establish context = () =>
        {
            //river = new string[][]
            //{
            //    new string[] { "K♠", "K♦" },
            //    new string[] { "5♠", "7♦" },
            //    new string[] { "K♠", "J♦" },
            //    new string[] { "K♠", "K♦" },
            //    new string[] { "K♠", "7♦" },
            //    new string[] { "3♠", "2♦" },
            //    new string[] { "7♠", "4♦" },
            //    new string[] { "K♠", "J♠" },
            //    new string[] { "5♦", "7♦" },
            //    new string[] { "9♦", "7♦" },
            //    new string[] { "5♦", "K♦" }
            //};
            //hole = new string[][]
            //{
            //    new string[] { "4♥", "K♥", "4♣", "Q♥", "2♥" },
            //    new string[] { "2♥", "2♥", "3♣", "7♥", "6♥" },
            //    new string[] { "9♥", "10♥", "3♣", "Q♥", "7♥" },
            //    new string[] { "K♥", "K♥", "3♣", "Q♥", "2♥" },
            //    new string[] { "2♥", "2♥", "3♣", "Q♥", "2♥" },
            //    new string[] { "6♥", "5♥", "3♣", "J♥", "A♥" },
            //    new string[] { "6♥", "9♥", "5♣", "Q♥", "8♥" },
            //    new string[] { "10♠", "3♥", "3♠", "Q♠", "9♠" },
            //    new string[] { "4♥", "4♦", "8♦", "7♥", "6♦" },
            //    new string[] { "4♥", "4♦", "J♦", "7♥", "6♦" },
            //    new string[] { "2♥", "4♠", "8♦", "J♠", "9♠" }
            //};
            //expectations = new (string type, string[] ranks)[]
            //{
            //    ("full house", new []{"K","4"}),
            //    ("two pair", new []{"7","2","6"}),
            //    ("straight", new []{"K","Q","J","10","9"}),
            //    ("four-of-a-kind", new []{"K","Q"}),
            //    ("three-of-a-kind", new []{"2","K","Q"}),
            //    ("pair", new []{"3","A","J","6"}),
            //    ("straight", new []{"9","8","7","6","5"}),
            //    ("straight-flush", new []{"K","Q","J","10","9"}),
            //    ("straight-flush", new []{"8","7","6","5","4"}),
            //    ("flush", new []{"J","9","7","6","4"}),
            //    ("nothing", new []{"K","J","9","8","5"})
            //};

            hole = new string[][] {
                new []{ "K♠","A♦" },
                new []{ "K♠","Q♦" },
                new []{ "K♠","J♦" },
                new []{ "4♠","9♦" },
                new []{ "Q♠","2♦" },
                new []{ "A♠","K♦" },
                new []{ "A♠","A♦" },
                new []{ "2♠","3♦" }
            };
            river = new string[][]
            {
                new []{ "J♣","Q♥","9♥","2♥","3♦" },
                new []{ "J♣","Q♥","9♥","2♥","3♦" },
                new []{ "J♣","K♥","9♥","2♥", "3♦" },
                new []{ "J♣","Q♥","Q♠","2♥","Q♦" },
                new []{ "J♣","10♥","9♥","K♥","3♦" },
                new []{ "J♥","5♥","10♥","Q♥","3♥" },
                new []{ "K♣","K♥","A♥","Q♥","3♦" },
                new []{ "2♣","2♥","3♠","3♥","2♦" }
            };
            expectations = new (string type, string[] ranks)[]
            {
                ("nothing",new []{"A","K","Q","J","9"}),
                ("pair", new []{"Q","K","J","9"}),
                ("two pair", new []{"K","J","9"}),
                ("three-of-a-kind", new []{"Q","J","9"}),
                ("straight", new []{"K","Q","J","10","9"}),
                ("straight", new []{"A","K","Q","J","10"}),
                ("full house", new []{"A","K"}),
                ("four-of-a-kind", new []{"2","3"})
            };
            answers = new (string type, string[] ranks)[expectations.Length];
        };

        Because of = () =>
        {
            for (var i = 0; i < hole.Length; i++)
            {
                answers[i] = Poker.Hand(hole[i], river[i]);
            }
        };

        It Should_Return_The_Optimal_Hand_Type = () =>
        {
            for (var i = 0; i < expectations.Length; i++)
            {
                answers[i].type.ShouldEqual(expectations[i].type);
            }
        };

        It Should_Return_The_Correct_Card_Ranks = () =>
        {
            for (var i = 0; i < expectations.Length; i++)
            {
                for (var j = 0; j < answers[i].ranks.Length; j++)
                {
                    if (answers[i].ranks[j] != expectations[i].ranks[j])
                    {
                        var ans = answers[i].ranks[j];
                        var exp = expectations[i].ranks[j];
                    }
                    answers[i].ranks[j].ShouldEqual(expectations[i].ranks[j]);
                }
            }
        };

        private static string[][] river;
        private static string[][] hole;
        private static (string type, string[] ranks)[] expectations;
        private static (string type, string[] ranks)[] answers;
    }

    //public class When_Finding_Consecutive_Ranks
    //{
    //    Establish context = () =>
    //    {
    //        cards = new List<string>[]
    //        {
    //            new List<string> { "K♠", "K♦","4♥", "K♥", "4♣", "Q♥", "2♥" },
    //            new List<string> { "5♠", "7♦","2♥", "2♦", "3♣", "7♥", "6♥" },
    //            new List<string> { "K♠", "J♦","9♥", "10♥","3♣", "Q♥", "7♥" },
    //            new List<string> { "K♠", "K♦","K♥", "K♥", "3♣", "Q♥", "2♥" },
    //            new List<string> { "K♠", "7♦","2♥", "2♥", "3♣", "Q♥", "2♥" },
    //            new List<string> { "3♠", "2♦","6♥", "5♥", "3♣", "J♥", "A♥" },
    //            new List<string> { "7♠", "4♦","6♥", "9♥", "5♣", "Q♥", "8♥" },
    //            new List<string> { "K♠", "J♠","10♠","3♥", "3♠", "Q♠", "9♠" },
    //            new List<string> { "5♦", "7♦","4♥", "4♦", "8♦", "7♥", "6♦" },
    //            new List<string> { "9♦", "7♦","4♥", "4♦", "J♦", "7♥", "6♦" },
    //            new List<string> { "5♦", "K♦","2♥", "4♠", "8♦", "J♠", "9♠" }
    //        };
    //        expected = new (string type, string[] ranks)[]
    //        {
    //            ("full house", new []{"K","4"}),
    //            ("two pair", new []{"7","2", "6"}),
    //            ("straight", new []{"K","Q","J","10","9"}),
    //            ("four-of-a-kind", new []{"K","Q"}),
    //            ("three-of-a-kind", new []{"2","K"}),
    //            ("pair", new []{"3","A","J","6"}),
    //            ("straight", new []{"9","8","7","6","5"}),
    //            ("straight-flush", new []{"K","Q","J","10","9"}),
    //            ("straight-flush", new []{"8","7","6","5","4"}),
    //            ("flush", new []{"J","9","7","6","4"}),
    //            ("nothing", new []{"K","J","9","8","5"})
    //        };
    //        answers = new (string type, string[] ranks)[expected.Length];
    //    };

    //    Because of = () =>
    //    {
    //        for (var i = 0; i < cards.Length; i++)
    //        {
    //            answers[i] = Poker.FindStraightsAlt(cards[i]);
    //        }
    //    };

    //    It Should_Return_Correct_Type = () =>
    //    {
    //        for (var i = 0; i < expected.Length; i++)
    //        {
    //            answers[i].type.ShouldEqual(expected[i].type);
    //        }
    //    };

    //    It Should_Return_Correct_Ranks = () =>
    //    {
    //        for (var i = 0; i < expected.Length; i++)
    //        {
    //            for (var j = 0; j < expected[i].ranks.Length; j++)
    //            {
    //                if (answers[i].ranks[j] != expected[i].ranks[j])
    //                {
    //                    var ans = answers[i].ranks[j];
    //                    var exp = expected[i].ranks[j];
    //                }
    //                answers[i].ranks[j].ShouldEqual(expected[i].ranks[j]);
    //            }
    //        }
    //    };

    //    private static List<string>[] cards;
    //    private static (string type, string[] ranks)[] expected;
    //    private static (string type, string[] ranks)[] answers;
    //}
}
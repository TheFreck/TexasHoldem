using Machine.Specifications;

namespace TexasHoldem.Specs
{
    public class When_Taking_Dealt_Cards
    {
        Establish context = () =>
        {
            holeInputs = new string[][]
            {
            // a
                //new[] { "K♠", "J♦" },
                //new[] { "K♠", "Q♦" },
                //new[] { "K♠", "J♦" },
                new[] { "4♠", "9♦" },
            //    new[] { "A♠", "K♦" },
            ////b
            //    new[] { "2♠", "3♦" },
            //    new[] { "4♠", "K♦" },
            //    new[] { "A♠","2♦" },
            //    new []{ "A♠","K♦" },
            //    new []{ "A♠","2♦" },
            ////c
            //    new []{ "A♠","4♦" },
            //    new []{ "4♠","4♦" },
            //    new []{ "K♠","J♦" },
            //    new []{ "K♠","Q♦" },
            //    new []{ "2♠","Q♦" },
            ////d
            //    new []{ "Q♠","Q♦" },
            //    new []{ "K♠","J♦" },
            //    new []{ "K♠","J♦" },
            //    new []{ "6♥","2♥" },
            //    new []{ "J♠","9♠" },
            ////e
            //    new []{ "A♠","2♦" },
            //    new []{ "A♠","K♦" },
            //    new []{ "A♠","2♦" },
            //    new []{ "A♠","4♦" },
            //    new []{ "4♠","4♦" },
            ////f
            //    new []{"A♠","2♦",},
            //    new []{"A♠","K♦",},
            //    new []{"A♠","2♦",},
            //    new []{"A♠","4♦",},
            //    new []{"4♠","4♦",},
            ////g
            //    new []{"K♠","J♦",},
            //    new []{"K♠","Q♦",},
            //    new []{"2♠","Q♦",},
            //    new []{"Q♠","Q♦",},
            //    new []{"K♠","J♦",},
            ////h 
            //    new []{"K♠","J♦",},
            //    new []{"K♠","Q♦",},
            //    new []{"Q♣","Q♦",},
            //    new []{"A♥","A♠",},
            //    new []{"A♠","Q♥",},
            ////i
            //    new []{"6♠","7♥",},
            //    new []{"2♠","3♥",},
            //    new []{"2♠","6♥",},
            //    new []{"4♠","3♥",},
            //    new []{"4♠","2♥",},
            ////j
            //    new []{"7♥","2♠",},
            //    new []{"A♠","A♦",},
            //    new []{"A♠","A♦",},
            //    new []{"A♠","K♦",},
            //    new []{"A♠","A♦",},
            };
            communityInputs = new string[][]
            {
            //a
                //new[] { "10♥","9♥","Q♣","K♥","Q♥" },
                //new[] { "K♣","Q♥","K♥","2♥","3♦" },
                //new[] { "J♣","K♥","9♥","2♥","3♦" },
                new[] { "J♣","Q♥","Q♠","2♥","Q♦" },
            //    new[] { "J♥","5♥","10♥","Q♥","3♥" },
            ////b
            //    new[] { "2♣","2♥","3♠","3♥","2♦" },
            //    new[] { "3♦","3♠","5♥","10♥","2♣" },
            //    new []{ "3♣","4♥","5♥","7♥","8♦" },
            //    new []{ "3♣","4♥","2♥","7♥","8♦" },
            //    new []{ "3♣","4♥","9♥","7♥","4♦" },
            ////c 
            //    new []{ "3♣","4♥","9♥","7♥","10♦" },
            //    new []{ "3♣","A♥","9♥","7♥","10♦" },
            //    new []{ "Q♣","Q♥","9♥","2♥","2♦" },
            //    new []{ "J♣","Q♥","9♥","2♥","2♦" },
            //    new []{ "J♣","Q♥","9♥","2♥","K♦" },
            ////d
            //    new []{ "K♣","J♥","9♥","2♥","2♦" },
            //    new []{ "J♣","K♥","9♥","2♥","2♦" },
            //    new []{ "Q♣","Q♥","9♥","2♥","Q♦" },
            //    new []{ "K♣","Q♠","7♣","Q♣","3♣" },
            //    new []{ "10♣","10♦","8♠","9♥","J♥" },
            ////e
            //    new []{ "3♣","4♥","5♥","7♥","8♦" },
            //    new []{ "3♣","4♥","2♥","7♥","8♦" },
            //    new []{ "3♣","4♥","9♥","7♥","4♦" },
            //    new []{ "3♣","4♥","9♥","7♥","10♦" },
            //    new []{ "3♣","A♥","9♥","7♥","10♦" },
            ////f
            //    new []{"3♣","4♥","5♥","7♥","8♦",},
            //    new []{"3♣","4♥","2♥","7♥","8♦",},
            //    new []{"3♣","4♥","9♥","7♥","4♦",},
            //    new []{"3♣","4♥","9♥","7♥","10♦",},
            //    new []{"3♣","A♥","9♥","7♥","10♦",},
            ////g
            //    new []{"Q♣","Q♥","9♥","2♥","2♦",},
            //    new []{"J♣","Q♥","9♥","2♥","2♦",},
            //    new []{"J♣","Q♥","9♥","2♥","K♦",},
            //    new []{"K♣","J♥","9♥","2♥","2♦",},
            //    new []{"J♣","K♥","9♥","2♥","2♦",},
            ////h 
            //    new []{"Q♣","Q♥","9♥","2♥","Q♦",},
            //    new []{"Q♣","Q♥","9♥","2♥","J♦",},
            //    new []{"K♠","Q♥","9♥","2♥","J♦",},
            //    new []{"A♣","K♥","Q♥","J♥","10♦",},
            //    new []{"K♥","10♠","J♠","9♠","8♦",},
            ////i 
            //    new []{"3♥","4♠","5♠","10♠","10♦",},
            //    new []{"4♥","5♠","6♠","10♠","10♦",},
            //    new []{"4♥","5♠","3♠","10♠","10♦",},
            //    new []{"2♥","5♠","6♠","10♠","10♦",},
            //    new []{"3♥","5♠","6♠","10♠","10♦",},
            ////j 
            //    new []{"A♣","K♥","2♦","7♣","2♥",},
            //    new []{"K♣","K♥","A♥","Q♥","Q♦",},
            //    new []{"K♣","K♥","A♥","J♥","Q♦",},
            //    new []{"K♣","K♥","A♥","J♥","Q♦",},
            //    new []{"K♣","K♥","A♥","Q♥","K♦",}

            };
            expectations = new (string type, string[] ranks)[]
            {
            //a
                //("straight", new []{"K","Q","J","10","9"}),
                //("full house", new []{"K","Q"}),
                //("two pair", new [] {"K","J","9"}),
                ("three-of-a-kind", new [] {"Q","J","9"}),
            //    ("flush", new [] {"Q","J","10","5","3"}),
            ////b
            //    ("four-of-a-kind",  new [] {"2","3"}),
            //    ("pair", new []{"3","K","10","5"}),
            //    ("nothing", new []{"A","8","7","5","4"}),
            //    ("nothing", new []{"A","K","8","7","4"}),
            //    ("pair", new []{"4","A","9","7"}),
            ////c 
            //    ("pair", new []{"4","A","10","9"}),
            //    ("pair", new []{"4","A","10","9"}),
            //    ("two pair", new []{"Q","2","K"}),
            //    ("two pair", new []{"Q","2","K"}),
            //    ("two pair", new []{"Q","2","K"}),
            ////d 
            //    ("two pair", new []{"Q","2","K"}),
            //    ("two pair", new []{"K","J","9"}),
            //    ("three-of-a-kind", new []{"Q","K","J"}),
            //    ("pair", new []{"Q","K","7","6"}),
            //    ("two pair", new []{"J","10","9"}),
            ////e
            //    ("nothing", new []{"A","8","7","5","4"}),
            //    ("nothing", new[]{"A","K","8","7","4"}),
            //    ("pair", new []{"4","A","9","7"}),
            //    ("pair", new []{"4","A","10","9"}),
            //    ("pair", new []{"4","A","10","9"}),
            ////f
            //    ("nothing", new []{"A","8","7","5","4"}),
            //    ("nothing", new []{"A","K","8","7","4"}),
            //    ("pair", new []{"4","A","9"}),
            //    ("pair", new []{"4","A","10"}),
            //    ("pair", new []{"4","A","10","9"}),
            ////g
            //    ("two pair", new []{"Q","2","K"}),
            //    ("two pair", new []{"Q","2","K"}),
            //    ("two pair", new []{"Q","2","K"}),
            //    ("two pair", new []{"Q","2","K"}),
            //    ("two pair", new []{"K","J","9"}),
            ////h
            //    ("three-of-a-kind", new []{"Q","K","J"}),
            //    ("three-of-a-kind", new []{"Q","K","J"}),
            //    ("three-of-a-kind", new []{"Q","K","J"}),
            //    ("straight", new []{"A","K","Q","J","10"}),
            //    ("straight", new []{"A","K","Q","J","10"}),
            ////i
            //    ("straight", new []{"7","6","5","4","3"}),
            //    ("straight", new []{"6","5","4","3","2"}),
            //    ("straight", new []{"6","5","4","3","2"}),
            //    ("straight", new []{"6","5","4","3","2"}),
            //    ("straight", new []{"6","5","4","3","2"}),
            ////j
            //    ("full house", new []{"2","7"}),
            //    ("full house", new []{"A","K"}),
            //    ("full house", new []{"A","K"}),
            //    ("full house", new []{"K","A"}),
            //    ("full house", new []{"A","K"}),

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

        It Should_Return_Correct_Hand_Type = () =>
        {
            for (var i = 0; i < expectations.Length; i++)
            {
                if (answer[i].type != expectations[i].type)
                {
                    var ans = answer[i].type;
                    var exp = expectations[i].type;
                }
                answer[i].type.ShouldEqual(expectations[i].type);
            }
        };

        It Should_Return_Correct_Hand_Cards = () =>
        {
            for (var i = 0; i < expectations.Length; i++)
            {
                for (var j = 0; j < expectations[i].ranks.Length; j++)
                {
                    if (answer[i].ranks[j] != expectations[i].ranks[j])
                    {
                        var ans = answer[i].ranks[j];
                        var exp = expectations[i].ranks[j];
                    }
                    answer[i].ranks[j].ShouldEqual(expectations[i].ranks[j]);
                }
            }
        };

        private static string[][] holeInputs;
        private static string[][] communityInputs;
        private static (string type, string[] ranks)[] expectations;
        private static (string type, string[] ranks)[] answer;
    }
}
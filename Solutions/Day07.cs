using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2023.Solutions
{
    internal class Day07
    {
        public static string testData = "32T3K 765\r\nT55J5 684\r\nKK677 28\r\nKTJJT 220\r\nQQQJA 483";
        public static string testData2 = "J5955 744\r\n965QJ 110\r\n846TT 779\r\n8JK8K 145\r\n224T4 935\r\n43245 864\r\nK66A3 261\r\nT6993 484\r\n9TJTT 365\r\nT5669 803\r\n63663 644\r\nK44JJ 332\r\n39369 122\r\nA57AT 724\r\n77A2A 561\r\nTA338 847\r\n66J6J 401\r\n7779Q 255\r\nA5AAA 720\r\n32223 500\r\n94744 763\r\nT444K 188\r\n3K3K3 635\r\nK492K 663\r\n29292 871\r\nTT44T 412\r\nJJJJJ 131\r\n86446 140\r\n68883 416\r\n456AT 16\r\n22595 518\r\nJT28T 386\r\nA7AJ6 994\r\nQ5333 565\r\nAA2A9 739\r\n4272Q 529\r\nJ84T6 242\r\nAA4Q9 947\r\n55J35 303\r\nT4QA9 775\r\n7JJJA 894\r\n8T778 549\r\nK59A7 155\r\n7KK7J 280\r\nT44JT 971\r\n667Q7 347\r\nAKAKA 815\r\n3969J 300\r\n3TTT3 673\r\n463J2 316\r\nKT3KJ 538\r\n72T77 474\r\n3A9A3 842\r\n55JKK 627\r\nAQAKA 843\r\nK53A8 42\r\n82222 691\r\n44674 684\r\nQ8Q8T 216\r\n28828 274";
        //public static string[] input = testData.Split('\n', StringSplitOptions.TrimEntries);
        public static string[] input = TextFormatter.ToLines("C:\\Users\\pette\\source\\repos\\AoC2023\\Data\\Day7.txt");

        public static void SolutionA()
        {
            List<Hand> hands = new();
            input.ToList().ForEach(text =>
            {
                string[] temp = text.Split(' ');
                hands.Add(new Hand { Cards = temp[0], Bid = Convert.ToInt32(temp[1]) });
            });

            foreach (Hand hand in hands)
            {
                hand.HandValue = CheckHand(hand.Cards);
                hand.CardValues = CardsToValues(hand.Cards);
            }

            for (int i = 0; i < hands.Count; i++)
            {
                for (int j = 0; j < hands.Count - 1; j++)
                {
                    if (hands[j].HandValue > hands[j + 1].HandValue)
                    {
                        Hand temp = hands[j];
                        hands[j] = hands[j + 1];
                        hands[j + 1] = temp;
                    }
                    else if (hands[j].HandValue == hands[j + 1].HandValue)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            if (hands[j].CardValues[k] > hands[j + 1].CardValues[k])
                            {
                                Hand temp = hands[j];
                                hands[j] = hands[j + 1];
                                hands[j + 1] = temp;
                                break;
                            }
                            else if (hands[j].CardValues[k] < hands[j + 1].CardValues[k])
                            {
                                break;
                            }
                        }
                    }
                }
            }
            int sum = 0;
            int counter = 1;
            foreach (Hand hand in hands)
            {
                //Console.WriteLine(hand.HandValue + " - " + hand.Cards + " - " + hand.CardValues[0] + "," + hand.CardValues[1] + "," + hand.CardValues[2] + "," + hand.CardValues[3] + "," + hand.CardValues[4]);
                sum += hand.Bid * counter;
                counter++;
            }

            Console.WriteLine(sum);
        }

        public static void SolutionB()
        {
            List<Hand> hands = new();
            input.ToList().ForEach(text =>
            {
                string[] temp = text.Split(' ');
                hands.Add(new Hand { Cards = temp[0], Bid = Convert.ToInt32(temp[1]) });
            });

            foreach (Hand hand in hands)
            {
                hand.HandValue = CheckHand2(hand.Cards);
                hand.CardValues = CardsToValues2(hand.Cards);
            }

            for (int i = 0; i < hands.Count; i++)
            {
                for (int j = 0; j < hands.Count - 1; j++)
                {
                    if (hands[j].HandValue > hands[j + 1].HandValue)
                    {
                        Hand temp = hands[j];
                        hands[j] = hands[j + 1];
                        hands[j + 1] = temp;
                    }
                    else if (hands[j].HandValue == hands[j + 1].HandValue)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            if (hands[j].CardValues[k] > hands[j + 1].CardValues[k])
                            {
                                Hand temp = hands[j];
                                hands[j] = hands[j + 1];
                                hands[j + 1] = temp;
                                break;
                            }
                            else if (hands[j].CardValues[k] < hands[j + 1].CardValues[k])
                            {
                                break;
                            }
                        }
                    }
                }
            }
            int sum = 0;
            int counter = 1;
            foreach (Hand hand in hands)
            {
                //Console.WriteLine(hand.HandValue + " - " + hand.Cards + " - " + hand.CardValues[0] + "," + hand.CardValues[1] + "," + hand.CardValues[2] + "," + hand.CardValues[3] + "," + hand.CardValues[4]);
                sum += hand.Bid * counter;
                counter++;
            }

            Console.WriteLine(sum);
        }

        public static int CheckHand(string hand)
        {
            List<int> checkedCards = new();
            int first = 0;
            int second = 0;
            for (int i = 0; i < 5; i++)
            {
                int matches = 0;
                if (!checkedCards.Contains(i))
                {
                    for (int j = i + 1; j < 5; j++)
                    {
                        if (!checkedCards.Contains(j))
                        {
                            if (hand[i] == hand[j])
                            {
                                matches++;
                                checkedCards.Add(j);
                            }
                        }
                    }
                }
                if (matches > 0)
                {
                    if (first == 0)
                    {
                        first = matches;
                    }
                    else
                    {
                        second = matches;
                    }
                }
            }
            if (first == 1 && second == 0)
                return 1;
            if (first == 1 && second == 1)
                return 2;
            if (first == 2 && second == 0)
                return 3;
            if ((first == 2 && second == 1) || (first == 1 && second == 2))
                return 4;
            if (first == 3)
                return 5;
            if (first == 4)
                return 6;
            return 0;
        }

        public static int CheckHand2(string hand)
        {
            List<int> checkedCards = new();
            int first = 0;
            int second = 0;
            int jokers = 0;
            for (int i = 0; i < 5; i++)
            {
                int matches = 0;
                if (hand[i] == 'J')
                {
                    jokers++;
                }
                else
                {
                    if (!checkedCards.Contains(i))
                    {
                        for (int j = i + 1; j < 5; j++)
                        {
                            if (!checkedCards.Contains(j))
                            {
                                if (hand[i] == hand[j])
                                {
                                    matches++;
                                    checkedCards.Add(j);
                                }
                            }
                        }
                    }
                }
                if (matches > 0)
                {
                    if (first == 0)
                    {
                        first = matches;
                    }
                    else
                    {
                        second = matches;
                    }
                }
            }
            if (jokers > 0)
            {
                if (jokers == 5)
                {
                    jokers--;
                }
                first += jokers;
            }
            if (first == 1 && second == 0)
                return 1;
            if (first == 1 && second == 1)
                return 2;
            if (first == 2 && second == 0)
                return 3;
            if ((first == 2 && second == 1) || (first == 1 && second == 2))
                return 4;
            if (first == 3)
                return 5;
            if (first == 4)
                return 6;
            return 0;
        }

        public static List<int> CardsToValues(string hand)
        {
            List<int> result = new();
            foreach (char c in hand)
            {
                if (Char.IsDigit(c))
                {
                    result.Add(Convert.ToInt32(c.ToString()));
                }
                else
                {
                    if (Enum.TryParse<CardValues>(c.ToString(), out CardValues card))
                    {
                        result.Add((int)card);
                    }
                }
            }
            return result;
        }

        public static List<int> CardsToValues2(string hand)
        {
            List<int> result = new();
            foreach (char c in hand)
            {
                if (Char.IsDigit(c))
                {
                    result.Add(Convert.ToInt32(c.ToString()));
                }
                else
                {
                    if (Enum.TryParse<CardValues2>(c.ToString(), out CardValues2 card))
                    {
                        result.Add((int)card);
                    }
                }
            }
            return result;
        }
    }

    public class Hand
    {
        public string Cards { get; set; }
        public List<int> CardValues { get; set; }
        public int Bid { get; set; }
        public int HandValue { get; set; }
    }
    enum Hands : int
    {
        highCard = 0,
        onePair = 1,
        twoPair = 2,
        threeKind = 3,
        fullHouse = 4,
        fourKind = 5,
        fiveKind = 6,
    }
    enum CardValues : int
    {
        T = 10,
        J = 11,
        Q = 12,
        K = 13,
        A = 14,
    }
    enum CardValues2 : int
    {
        T = 10,
        J = 1,
        Q = 12,
        K = 13,
        A = 14,
    }
}

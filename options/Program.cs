using System;
using System.Collections.Generic;

namespace options
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var men = new List<Man>()
            {
                new Man('A', new List<char>("IHJGF")),
                new Man('B', new List<char>("FHJGI")),
                new Man('C', new List<char>("HGJFI")),
                new Man('D', new List<char>("HIFJG")),
                new Man('E', new List<char>("JFHGI")),
            };
            var women = new List<Woman>()
            {
                new Woman('F', new List<char>("BECAD")),
                new Woman('G', new List<char>("DECAB")),
                new Woman('H', new List<char>("ECDAB")),
                new Woman('I', new List<char>("BECDA")),
                new Woman('J', new List<char>("DCBAE")),
            };

            var lonelyGuys = new List<Man>(men);

            // Carry on until there are no lonely guys anymore.
            while (lonelyGuys.Count > 0)
            {
                // Just go trough the womens again and again.
                for (int i = 0; i < women.Count; i++)
                {
                    // Create a list of the men, who want the women. 
                    var bachelors = new List<Man>();
                    // Add the lonely guys, who want to propose to the women.
                    for (int j = 0; j < lonelyGuys.Count; j++)
                    {
                        if (lonelyGuys[j].NextChoiceId == women[i].Id)
                        {
                            bachelors.Add(lonelyGuys[j]);
                            lonelyGuys[j].NextChoiceTried();
                        }
                    }

                    if (bachelors.Count > 0)
                    {
                        // Give the Husband a chance to prove himself again.
                        if (women[i].Partner != null)
                        {
                            bachelors.Add(women[i].Partner);
                            lonelyGuys.Add(women[i].Partner);
                            women[i].Partner = null;
                        }
                        // Let the women choose.
                        women[i].Partner = bachelors[0];
                        for (int j = 1; j < bachelors.Count; j++)
                        {
                            var keepPartner = true;
                            for (int k = 0; k < women[i].Choices.Count; k++)
                            {
                                if (women[i].Choices[k] == women[i].Partner.Id)
                                {
                                    keepPartner = true;
                                    break;
                                }
                                else if (women[i].Choices[k] == bachelors[j].Id)
                                {
                                    keepPartner = false;
                                    break;
                                }
                            }
                            if (!keepPartner)
                            {
                                women[i].Partner = bachelors[j];
                            }
                        }
                        lonelyGuys.Remove(women[i].Partner);
                    }
                }
            }

            // Write the pairs to console
            for (int i = 0; i < women.Count; i++)
            {
                Console.WriteLine(women[i].Id + " : " + women[i].Partner.Id + " " + men.Count);
            }
        }
    }

    public class Person
    {
        public List<char> Choices { get; set; }
        public char Cne { get; set; }

        public Person(char cne, List<char> choices)
        {
            Cne= cne;
            Choices = choices;
        }
    }

    public class Woman : Person
    {
        public Etudiant  Partner { get; set; }

        public Woman(char id, List<char> choices) : base(id, choices) { }
    }

    public class Etudiant : Person
    {
        public HashSet<char> ChoicesTried { get; set; } = new HashSet<char>();
        public char? NextChoiceCne
        {
            get
            {
                char? choiceCne = null;

                for (int i = 0; i < Choices.Count; i++)
                {
                    if (!ChoicesTried.Contains(Choices[i]))
                    {
                        choiceCne = Choices[i];
                        break;
                    }
                }
                return choiceCne;
            }
        }
        public Woman Partner { get; set; }

        public Etudiant(char cne, List<char> choices) : base(cne, choices) { }

        public void NextChoiceTried()
        {
            ChoicesTried.Add((char)NextChoiceCne);
        }
    }
}
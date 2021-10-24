using System;
using System.Collections.Generic;

namespace StableMarriage
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var men = new List<Etudiant>()
            {
                new Etudiant('A', new List<char>("IHJGF"),13),
                new Etudiant('B', new List<char>("FHJGI"),14),
                new Etudiant('C', new List<char>("HGJFI"),12),
                new Etudiant('D', new List<char>("HIFJG"),11),
                new Etudiant('E', new List<char>("JFHGI"),16),
            };
            var women = new List<Option>()
            {
                new Option(1),
                new Option(2),
                new Option(1),

            };

            var SansOption = new List<Etudiant>();

            
            while (SansOption.Count > 0)
            {
                
                for (int i = 0; i < Option.Count; i++)
                {
                    // Create a list of the men, who want the women. 
                    var student = new List<Etudiant>();
                    // Add the lonely guys, who want to propose to the women.
                    for (int j = 0; j < SansOption.Count; j++)

                    {
                        if (SansOption[j].NextChoices == Option[i].cap)
                        {
                            student.Add(SansOption[j]);
                            SansOption[j].NextChoiceTried();
                        }
                    }

                    if (student.Count > 0)
                    {
                        // Give the Husband a chance to prove himself again.
                        if (Option[i].Partner != null)
                        {
                            student.Add(Option[i].Partner);
                            SansOption.Add(Option[i].Partner);
                            Option[i].Partner = null;
                        }
                        // Let the women choose.
                        Option[i].Partner = student[0];
                        for (int j = 1; j < student.Count; j++)
                        {
                            var keepPartner = true;
                            for (int k = 0; k < women[i].Choices.Count; k++)
                            {
                                if (Option[i].Choices[k] == Option[i] Partner.cap)
                                
                                    keepPartner = true;
                                    break;
                                }
                                else if (Option[i].Choices[k] == student[j].Cne)
                                {
                                    keepPartner = false;
                                    break;
                                }
                            }
                            if (!keepPartner)
                            {
                                Option[i].Partner = student[j];
                            }
                        }
                        SansOption.Remove(Option[i].Partner);
                    }
                }
            }

            // Write the pairs to console
            for (int i = 0; i < Option.Count; i++)
            {
                Console.WriteLine(Option[i].Id + " : " + Option[i].Partner.Id + " " +Etudiant.Count);
            }
        }
    }

    public class Person
    {
        public List<char> Choices { get; set; }
        public char Cne { get; set; }
        public int Note { get; set; }
        public Person(char cne, List<char> choices, int note)
        {
            Cne = cne;
            Choices = choices;
            Note = note;
        }
    }

    public class Option
    {
        public Etudiant Partner { get; set; }
        public Option (int cap);
        
    }


    public class Etudiant : Person
    {
        public HashSet<char> ChoicesTried { get; set; } = new HashSet<char>();
        public char? NextChoices
        {
            get
            {
                char? choices = null;

                for (int i = 0; i < Choices.Count; i++)
                {
                    if (!ChoicesTried.Contains(Choices[i]))
                    {
                        choices = Choices[i];
                        break;
                    }
                }
                return choices;
            }
        }
        public Option Partner { get; set; }

        public Etudiant(char cne, List<char> choices,int note) : base( cne , choices,note) { }

        public void NextChoiceTried()
        {
            ChoicesTried.Add((char)NextChoices);
        }
    }
}

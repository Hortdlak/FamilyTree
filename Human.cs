using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;

namespace FamilyTree
    {
        internal enum Gender
        {
            [Description("Мужской")]
            Male,
            [Description("Женский")]
            Female
        }

        internal class Human
        {
            #region Initialization

            internal string Name { get; set; }
            internal string Surname { get; set; }
            internal Gender Gender { get; set; }
            private DateTime Birthday { get; set; }
            private Human? Father { get; set; }
            private Human? Mother { get; set; }
            internal List<Human> Children { get; set; }
            internal Human? Husband { get; set; }
            internal Human? Wife { get; set; }

            internal Human(string name, string surname, Gender gender, DateTime birthday)
            {
                Name = name;
                Surname = surname;
                Gender = gender;
                Birthday = birthday;
                Children = new List<Human>();
            }

            #region Custom Gender
            private string GetEnumDescription(Enum value)
            {
                var fieldInfo = value.GetType().GetField(value.ToString());
                
                var attributes = (DescriptionAttribute[])fieldInfo.
                    GetCustomAttributes(typeof(DescriptionAttribute), false);

                return attributes.Length > 0 ? attributes[0].Description : value.ToString();
            }
            internal string GetGenderDescription()
            {
                return GetEnumDescription(Gender);
            }
            #endregion

            #endregion

            #region Methods

            #region Add
            internal void AddWife(Human wife)
            {
                Wife = wife;
            }

            internal void AddHusband(Human husband)
            {
                Husband = husband;
            }

            internal void AddChild(Human child)
            {
                Children.Add(child);
            }

            internal void AddFather(Human father)
            {
                Father = father;
            }
            internal void AddMother(Human mother)
            {
                Mother = mother;
            }

            #endregion

            #region Get

            private static List<Human> GetSiblings(Human person)
            {
                List<Human> siblings = new List<Human>();

                if (person.Father != null && person.Mother != null)
                {
                    // Объединяем списки детей отца и матери, удаляем текущего человека и
                    // убираем повторяющиеся элементы
                    siblings = person.Father.Children.Concat(person.Mother.Children)
                                                      .Where(child => !child.Equals(person))
                                                      .ToList();

                    // Удаляем повторения
                    siblings = RemoveDuplicates(siblings);
                }

                return siblings;
            }

            private static List<Human> GetChildren(Human person)
            {
                List<Human> children = new List<Human>();

                if (person.Children != null)
                {
                    children = person.Children.ToList();
                }

                children = RemoveDuplicates(children);

                return children;
            }

            #endregion

            #region Show

            internal void ShowFather()
            {
                Console.Write($"Отец: \n{Father}");
            }

            internal void ShowMother()
            {
                Console.Write($"Мать: \n{Mother}");
            }

            internal void ShowParents()
            {
                if (Father != null)
                {
                    Father.PrintInfo();
                }
                else
                {
                    Console.WriteLine("Отец не известен");
                }

                if (Mother != null)
                {
                    Mother.PrintInfo();
                }
                else
                {
                    Console.WriteLine("Мать не известна");
                }
            }

            internal void ShowChildren(Human person)
            {
                List<Human> children = GetChildren(person);

                if (children.Count == 0)
                {
                    return;
                }

                Console.WriteLine(children.Count == 1 ? "Ребенок:" : "Дети:");

                foreach (var child in children)
                {
                    Console.WriteLine(child);
                }
            }

            internal void ShowSiblings()
            {
                List<Human> siblings = Human.GetSiblings(this);

                if (siblings.Count == 0)
                {
                    return;
                }

                string siblingType = DetermineSiblingType(siblings);

                Console.WriteLine(siblings.Count == 1 ? $"{siblingType}:" : $"{siblingType}ы:");

                foreach (var sibling in siblings)
                {
                    Console.WriteLine(sibling);
                }
            }
        #endregion

            internal bool Equals(Human other)
            {
                if (other == null) return false;
                if (Name != other.Name || Surname != other.Surname || Gender != other.Gender || Birthday != other.Birthday)
                {
                    return false;
                }
                return true;
            }

            internal void isAdult(int adultAge = 18)
            {
                if (Birthday.AddYears(adultAge) <= DateTime.Today)
                {
                    Console.WriteLine("Да, совершеннолетний");
                }
                else
                {
                    Console.WriteLine("Нет, несовершеннолетний");
                }
            }
            internal void howManyYears()
            {
                string text;

                int age = DateTime.Today.Year - Birthday.Date.Year;

                if (DateTime.Today.Month < Birthday.Date.Month || DateTime.Today.Day < Birthday.Date.Day)
                {
                    age--;
                }

                switch (age)
                {
                    case 1:
                        text = "год";
                        break;

                    case 2 - 4:
                        text = "годa";
                        break;

                    default:
                        text = "лет";
                        break;
                }

                Console.WriteLine($"{age} {text}");
            }

            internal string MiddleLineOutput(string text, int maximum_length = 25)
            {
                int string_length = text.Length;

                int padding_length = (maximum_length - string_length) / 2;

                StringBuilder strBuild = new StringBuilder();

                strBuild.Append(' ', padding_length);

                strBuild.Append(text);

                strBuild.Append(' ', padding_length);

                string result = strBuild.ToString();

                return result;
            }

            internal static List<Human> RemoveDuplicates(List<Human> inputList)
            {
                HashSet<Human> set = new HashSet<Human>();
                List<Human> uniqueList = new List<Human>();

                foreach (var item in inputList)
                {
                    if (set.Add(item))
                    {
                        uniqueList.Add(item);
                    }
                }

                return uniqueList;
            }

            private string DetermineSiblingType(List<Human> siblings)
            {
                bool allMales = siblings.All(sibling => sibling.Gender == Gender.Male);
                bool allFemales = siblings.All(sibling => sibling.Gender == Gender.Female);

                if (allMales)
                {
                    return siblings.Count == 1 ? "Брат:" : "Братья:\n";
                }
                else if (allFemales)
                {
                    return siblings.Count == 1 ? "Сестра:" : "Сестры:\n";
                }
                else
                {
                    return "Братья и сестры:\n";
                }
            }

            internal void PrintInfo()
            {
                Console.WriteLine(ToString());
            }

            public override string ToString()
            {
                return
                    $"{Name} {Surname}," +
                    $"\nПол: {GetGenderDescription()}" +
                    $"\nДата рождения: {Birthday.ToShortDateString()}";

            }

            public void PrintGenealogicTree(int level = 1)
        {
            Console.WriteLine(MiddleLineOutput($"Поколение номер: {level}"));

            Console.WriteLine($"\n{ToString()}\n");

            Husband?.PrintInfo();

            Wife?.PrintInfo();

            Console.WriteLine($"{new string('-', 25)} \n");

            foreach (var child in Children)
            {
                child.PrintGenealogicTree(level + 1);
            }
        }

            #endregion
        }

}

namespace FamilyTree
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Создание объекта Ивана, мужа Марии
            Human Ivan = new Man("Иван", "Иванов", new DateTime(1980, 5, 10));

            #endregion

            #region Создание объекта Марии, жены Ивана

            Human Maria = new Woman("Мария", "Иванова", new DateTime(1985, 7, 15));
            Maria.AddHusband(Ivan);
            Ivan.AddWife(Maria);

            #endregion

            #region Создание объекта Алексея, ребенка Марии и Ивана
            Human Alexei = new Man("Алексей", "Иванов", new DateTime(2010, 05, 23));
            Ivan.AddChild(Alexei);
            Maria.AddChild(Alexei);
            Alexei.AddFather(Ivan);
            Alexei.AddMother(Maria);
            #endregion

            #region Создание объектов для бабушки и дедушки с стороны Ивана

            Human Catherine = new Woman("Екатерина", "Петрова", new DateTime(1932, 6, 10));
            Ivan.AddMother(Catherine);
            Catherine.AddChild(Ivan);

            Human Gregory = new Man("Григорий", "Петров", new DateTime(1930, 8, 20));
            Ivan.AddFather(Gregory);
            Catherine.AddHusband(Gregory);
            Gregory.AddWife(Catherine);
            Gregory.AddChild(Ivan);
            #endregion

            #region Создание объектов для бабушки и дедушки с стороны Марии

            Human Elena = new Woman("Елена", "Иванова", new DateTime(1934, 3, 5));
            Maria.AddMother(Elena);
            Elena.AddChild(Maria);

            Human Dmitriy = new Man("Дмитрий", "Иванов", new DateTime(1935, 1, 17));
            Maria.AddFather(Dmitriy);
            Elena.AddHusband(Dmitriy);
            Dmitriy.AddWife(Elena);
            Dmitriy.AddChild(Maria);
            #endregion

            #region Создание объектов для брата и его жены с стороны Марии

            Human Svetlana = new Woman("Светлана", "Иванова", new DateTime(1984, 7, 27));

            Human Egor = new Man("Егор", "Иванов", new DateTime(1984, 8, 13));
            Egor.AddMother(Elena);
            Elena.AddChild(Egor);
            Egor.AddFather(Dmitriy);
            Dmitriy.AddChild(Egor);
            Svetlana.AddHusband(Egor);
            Egor.AddWife(Svetlana);
            #endregion

            #region Вывод генеалогического дерева

            Console.WriteLine($"{new string('-', 25)}\n" +
                $"Генеалогическое дерево:\n" +
                $"{new string('-', 25)}");

            Dmitriy.PrintGenealogicTree();
            #endregion
        }

    }
}
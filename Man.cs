namespace FamilyTree
{
    internal class Man : Human
    {

        public Man(string name, string surname, DateTime birthday)
        : base(name, surname, Gender.Male, birthday)
        {

        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

}

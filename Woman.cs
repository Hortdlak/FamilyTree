namespace FamilyTree
{
    internal class Woman : Human
    {

        public Woman (string name, string surname, DateTime birthday) 
            : base(name, surname, Gender.Female, birthday)
        {

        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

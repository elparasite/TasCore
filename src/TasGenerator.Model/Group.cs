namespace TasGenerator.Model
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}

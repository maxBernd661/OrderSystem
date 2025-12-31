namespace OrderSystem.Win.View
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DetailViewAttribute(Type type, string id) : Attribute
    {
        public DetailViewAttribute(Type type) : this(type, null)
        {
        }

        public Type Type { get; set; } = type;

        public string Id { get; set; } = id;
    }
}
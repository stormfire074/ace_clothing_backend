namespace webapp.SharedServies
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]

    public class ServiceLayerObjectNameAttribute : Attribute
    {
        public ServiceLayerObjectNameAttribute(string name)
        {
            Name = name;
        }

        // Property to hold the attribute's value
        public string Name { get; }
    }
}
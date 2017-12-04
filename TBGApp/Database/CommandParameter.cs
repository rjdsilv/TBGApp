namespace TBGApp.Database
{
    public class CommandParameter
    {
        public string Name { get; }
        public object Value { get; }

        public CommandParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
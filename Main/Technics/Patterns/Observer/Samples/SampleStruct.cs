namespace YNL.Pattern.Observer
{
    // This struct is the data of event
    public struct SampleStruct
    {
        public string Name;
        public int Amount;

        public SampleStruct(string name, int amount)
        {
            Name = name;
            Amount = amount;
        }

        // This method used to execute the events
        public static void Trigger(string name, int amount)
        {
            SampleStruct s = new SampleStruct(name, amount);
            Observer.TriggerSingle(s);      // Use this if you use single observer
            Observer.TriggerMultiple(s);    // Use this if you use multiple observers
        }

        public override string ToString()
        {
            return $"SamepleStruct: [{Name}, {Amount}]";
        }
    }
}

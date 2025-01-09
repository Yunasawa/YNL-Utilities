namespace YNL.Utilities.Patterns
{
    // This struct is the data of event
    public struct DriverEvent
    {
        public string Model;
        public DriverAction Action;

        private static DriverEvent _e;

        // This method used to execute the events
        public static void Trigger(string model, DriverAction action)
        {
            _e.Model = model;
            _e.Action = action;
            Observer.TriggerMultiple(_e);    // Use this if you use multiple observers
        }

        public override string ToString()
        {
            return $"{Model} is {Action} (Driver Action)";
        }
    }

    public enum DriverAction
    {
        Start, Drive, Stop
    }
}

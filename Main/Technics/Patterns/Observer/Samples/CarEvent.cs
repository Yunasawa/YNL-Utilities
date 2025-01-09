namespace YNL.Utilities.Patterns
{
    // This struct is the data of event
    public struct CarEvent
    {
        public string Model;
        public CarAction Action;

        private static CarEvent _e;

        // This method used to execute the events
        public static void Trigger(string model, CarAction action)
        {
            _e.Model = model;
            _e.Action = action;
            Observer.TriggerMultiple(_e);    // Use this if you use multiple observers
        }

        public override string ToString()
        {
            return $"{Model} is {Action} (Car Action)";
        }
    }

    public enum CarAction
    {
        Buy, Sell, Give
    }
}

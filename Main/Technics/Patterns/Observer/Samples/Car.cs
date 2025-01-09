using UnityEngine;

namespace YNL.Utilities.Patterns
{
    // Use IListener for objects that listen to events
    public class Car : MonoBehaviour, IListener<CarEvent>, IListener<DriverEvent>
    {
        private void Awake()
        {
            // Register events
            this.RegisterMultiple<CarEvent>();
            this.RegisterMultiple<DriverEvent>();
        }

        private void OnDestroy()
        {
            // Unregister events
            this.UnregisterMultiple<CarEvent>();
            this.UnregisterMultiple<DriverEvent>();
        }

        // Receive CarEvent data
        public void Invoke(CarEvent data)
        {
            Debug.Log($"From Car: {data}");
        }

        // Receive DriverEvent data
        public void Invoke(DriverEvent data)
        {
            Debug.Log($"From Car: {data}");
        }
    }
}

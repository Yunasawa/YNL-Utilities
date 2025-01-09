using UnityEngine;

namespace YNL.Utilities.Patterns
{
    // Use IListener for objects that listen to events
    public class Driver : MonoBehaviour, IListener<DriverEvent>
    {
        private void Awake()
        {
            // Register events
            this.RegisterMultiple<DriverEvent>();
        }

        private void OnDestroy()
        {
            // Unregister events
            this.UnregisterMultiple<DriverEvent>();
        }

        // Receive DriverEvent data
        public void Invoke(DriverEvent data)
        {
            Debug.Log($"From Driver: {data}");
        }
    }
}

using UnityEngine;

namespace YNL.Pattern.Observer
{
    // Use IListener for objects that listen to events
    public class SampleObject : MonoBehaviour, IListener<SampleStruct>
    {
        public string Name;

        private void Awake()
        {
            // Register events
            this.RegisterMultiple<SampleStruct>();
        }

        private void OnDestroy()
        {
            // Unregister events
            this.UnregisterMultiple<SampleStruct>();
        }

        public void Invoke(SampleStruct data)
        {
            Debug.Log($"{Name}: {data}");
        }

        [ContextMenu("Execute")]
        public void ExecuteEvent()
        {
            // This is how to invoke the events
            SampleStruct.Trigger("Sword", 10);
        }
    }
}

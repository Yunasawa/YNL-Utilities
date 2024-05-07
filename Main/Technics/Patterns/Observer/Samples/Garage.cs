using UnityEngine;

namespace YNL.Pattern.Observer
{
    // Use IListener for objects that listen to events
    public class Garage : MonoBehaviour
    {
        [ContextMenu("Execute Car Event")]
        public void ExecuteCarEvent()
        {
            // This is how to invoke the events
            CarEvent.Trigger("GTR R35", CarAction.Buy);
        }
        
        [ContextMenu("Execute Driver Event")]
        public void ExecuteDriverEvent()
        {
            // This is how to invoke the events
            DriverEvent.Trigger("GTR R35", DriverAction.Drive);
        }
    }
}

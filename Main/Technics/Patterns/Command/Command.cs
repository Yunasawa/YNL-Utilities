using YNL.Utilities.Patterns;

namespace YNL.Utilities.Patterns
{
    public abstract class Command<T> : IListener<T> where T : struct
    {
        protected Command() { this.RegisterSingle(); }
        ~Command() { this.UnregisterSingle(); }

        public abstract void Invoke(T @event);
    }
}
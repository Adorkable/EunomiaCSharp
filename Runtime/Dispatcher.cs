using System;
using System.Collections.Generic;

namespace Eunomia
{
    // Based on: https://www.lucidumstudio.com/home/2017/12/5/lucidum-studio-async-code-execution-in-unity

    /// <summary>
    /// Utility class for ensuring a set of Actions are Invoked in order from a particular thread
    /// </summary>
    public class Dispatcher
    {
        private readonly List<Action> pending = new List<Action>();
        private volatile bool pendingActions = false;

        public Action<Exception> logUnhandledExceptions = null;

        public void Invoke(Action fn)
        {
            lock (pending)
            {
                pending.Add(fn);
                pendingActions = true;
            }
        }

        public void InvokePending()
        {
            if (!pendingActions)
            {
                return;
            }

            IEnumerable<Action> invoking;
            lock (pending)
            {
                if (pending.Count == 0)
                {
                    return;
                }

                invoking = pending.Copy();

                pending.Clear();
                pendingActions = false;
            }

            invoking.ForEach((action) =>
            {
                try
                {
                    action.Invoke();
                }
                catch (Exception exception)
                {
                    if (logUnhandledExceptions != null)
                    {
                        logUnhandledExceptions.Invoke(exception);
                    }
                    // TODO: else collect and throw all exceptions
                }
            });
        }
    };
};

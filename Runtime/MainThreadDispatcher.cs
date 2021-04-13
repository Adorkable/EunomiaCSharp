using System;
using System.Collections.Generic;

namespace Eunomia
{
    // Based on: https://www.lucidumstudio.com/home/2017/12/5/lucidum-studio-async-code-execution-in-unity
    public class MainThreadDispatcher
    {
        private readonly List<Action> pending = new List<Action>();

        public void Invoke(Action fn)
        {
            lock (pending)
            {
                this.pending.Add(fn);
            }
        }

        private void InvokePending()
        {
            List<Action> invoke;
            lock (pending)
            {
                invoke = new List<Action>(pending);
                pending.Clear();
            }

            invoke.ForEach((action) =>
            {
                action.Invoke();
            });
        }

        public virtual void Update()
        {
            this.InvokePending();
        }
    };
};

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eunomia
{
    // TODO: should we add Main Dispatch support?
    /// <summary>
    /// Set State Order of Operations:
    /// 1. Test valid Transition
    /// 2. Transition::Perform Before
    /// 3. To State::Perform Before Enter
    /// 4. Transition::Perform After
    /// 5. To State::Perform After Enter
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public class StateMachine<TState> where TState : IComparable
    {
        public struct Transition
        {
            // TODO: only sync and only async constructors so it isn't unclear which order they execute if you use both?
            public Func<Task> performBefore;
            public Action performBeforeSync;

            public TState from;
            public TState to;

            public Func<Task> performAfter;
            public Action performAfterSync;

            private static async Task Perform(Func<Task> async, Action sync)
            {
                if (async != null)
                {
                    await async();
                }

                sync?.Invoke();
            }

            public async Task PerformBefore()
            {
                await Perform(performBefore, performBeforeSync);
            }

            public async Task PerformAfter()
            {
                await Perform(performAfter, performAfterSync);
            }
        };

        public TState Current { get; private set; }

        private bool currentlyTransitioning = false;
        private Transition currentTransition;

        private class StateInformation
        {
            public TState state;

            public List<Transition> transitions = new List<Transition>();

            // TODO: should these be arrays?
            public Func<Task> performBeforeEnter;
            public Action performBeforeEnterSync;

            public Func<Task> performAfterEnter;
            public Action performAfterEnterSync;

            public Transition GetStateTransition(TState to)
            {
                var findResult = transitions.Find((transition, index) => transition.to.Equals(to));
                if (findResult.found == false)
                {
                    throw new InvalidTransitionException(state, to);
                }

                return findResult.result;
            }

            private static async Task Perform(Func<Task> async, Action sync)
            {
                if (async != null)
                {
                    await async();
                }

                sync?.Invoke();
            }

            public async Task PerformBeforeEnter()
            {
                await Perform(performBeforeEnter, performBeforeEnterSync);
            }

            public async Task PerformAfterEnter()
            {
                await Perform(performAfterEnter, performAfterEnterSync);
            }
        };

        private Dictionary<TState, StateInformation> states = new Dictionary<TState, StateInformation>();

        public bool throwIfLogged = false;
        public Action<TState, TState> logTransitions = null;
        public Action<TState, TState, InvalidTransitionException> logInvalidStateTransition = null;
        public Action<TState, TState, Exception> logUnhandledExceptions = null;
        public Action<TState, TState, TState> logAttemptedTransitionDuringTransition = null;

        public StateMachine(TState initialState)
        {
            Current = initialState;
        }

        private StateInformation GetStateInformation(TState state)
        {
            if (states.ContainsKey(state))
            {
                return states[state];
            }

            return null;
        }

        private StateInformation GetOrCreateStateInformation(TState state)
        {
            if (!states.ContainsKey(state))
            {
                states[state] = new StateInformation() { state = state };
            }

            return GetStateInformation(state);
        }

        public void AddTransition(Transition transition)
        {
            var information = GetOrCreateStateInformation(transition.from);

            // TODO: check for duplicates
            information.transitions.Add(transition);
        }

        public void AddTransitions(Transition[] transitions)
        {
            transitions.ForEach((transition, index) => AddTransition(transition));
        }

        public void SetPerformBeforeEnter(TState state, Func<Task> perform)
        {
            var information = GetOrCreateStateInformation(state);

            // TODO: warn about overwriting
            information.performBeforeEnter = perform;
        }

        public void SetPerformBeforeEnterSync(TState state, Action perform)
        {
            var information = GetOrCreateStateInformation(state);

            // TODO: warn about overwriting
            information.performBeforeEnterSync = perform;
        }

        public void SetPerformAfterEnter(TState state, Func<Task> perform)
        {
            var information = GetOrCreateStateInformation(state);

            // TODO: warn about overwriting
            information.performAfterEnter = perform;
        }

        public void SetPerformAfterEnterSync(TState state, Action perform)
        {
            var information = GetOrCreateStateInformation(state);

            // TODO: warn about overwriting
            information.performAfterEnterSync = perform;
        }

        public class InvalidStateException : Exception
        {
            public TState State { get; }

            public InvalidStateException(TState state, TState to) : base($"Invalid state '{state.ToString()}'")
            {
                State = state;
            }
        }

        public class InvalidTransitionException : Exception
        {
            public TState From { get; }
            public TState To { get; }

            public InvalidTransitionException(TState from, TState to) : base($"Invalid transition from '{from.ToString()}' to '{to.ToString()}'")
            {
                From = from;
                To = to;
            }
        }

        private Transition GetStateTransition(TState to)
        {
            try
            {
                var information = GetStateInformation(Current);
                if (information == null)
                {
                    throw new InvalidTransitionException(Current, to);
                }

                return information.GetStateTransition(to);
            }
            catch (InvalidTransitionException exception)
            {
                logInvalidStateTransition?.Invoke(Current, to, exception);

                throw;
            }
        }

        public class AttemptedTransitionDuringTransitionException : Exception
        {
            public TState From { get; }
            public TState To { get; }
            public Transition CurrentTransition { get; }

            public AttemptedTransitionDuringTransitionException(TState from, TState to, Transition currentTransition) :
                base($"Attempted Transition from '{from.ToString()}' to '{to.ToString()}' during Transition  '{currentTransition.from.ToString()}' to '{currentTransition.to.ToString()}S'")
            {
                this.From = from;
                this.To = to;
                this.CurrentTransition = currentTransition;
            }
        }

        public async Task SetState(TState to)
        {
            lock (this)
            {
                if (currentlyTransitioning == true)
                {
                    var exception = new AttemptedTransitionDuringTransitionException(Current, to, currentTransition);

                    logAttemptedTransitionDuringTransition?.Invoke(currentTransition.@from, currentTransition.to, to);

                    if (logAttemptedTransitionDuringTransition == null || throwIfLogged)
                    {
                        throw exception;
                    }
                }

                currentlyTransitioning = true;
                currentTransition = GetStateTransition(to);
            }

            List<Exception> unhandledExceptions = new List<Exception>();

            logTransitions?.Invoke(Current, to);

            try
            {
                await currentTransition.PerformBefore();
            }
            catch (Exception exception)
            {
                logUnhandledExceptions?.Invoke(Current, to, exception);

                unhandledExceptions.Add(exception);
            }

            var toInformation = GetStateInformation(to);

            if (toInformation != null)
            {
                try
                {
                    await toInformation.PerformBeforeEnter();
                }
                catch (Exception exception)
                {
                    logUnhandledExceptions?.Invoke(Current, to, exception);

                    unhandledExceptions.Add(exception);
                }
            }

            Current = to;

            lock (this)
            {
                currentlyTransitioning = false;
            }

            try
            {
                await currentTransition.PerformAfter();
            }
            catch (Exception exception)
            {
                logUnhandledExceptions?.Invoke(Current, to, exception);

                unhandledExceptions.Add(exception);
            }

            if (toInformation != null)
            {
                try
                {
                    await toInformation.PerformAfterEnter();
                }
                catch (Exception exception)
                {
                    logUnhandledExceptions?.Invoke(Current, to, exception);

                    unhandledExceptions.Add(exception);
                }
            }

            if (unhandledExceptions.Count > 0 && (logUnhandledExceptions == null || throwIfLogged))
            {
                // TODO: create exception with all
                throw unhandledExceptions.First();
            }
        }
    };
};

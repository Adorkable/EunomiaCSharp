# State Machine
**StateMachine** is a simple state machine.

* One determines an initial state at construction

  ```csharp
	state = new StateMachine<State>(State.Attract);
  ```

* One describes other states that can transitioned to from that initial state and the states that can be transitioned to from THOSE state, and so on, sometimes looping back

  ```csharp
    state.AddTransition(
      new StateTransition() { from = State.Attract, to = State.Interactive }
    );
	
   state.AddTransitions(new[] {  
      new StateTransition() { from = State.Interactive, to = State.InteractiveClosing },
      new StateTransition() { from = State.InteractiveClosing, to = State.Attract },
    );
  ```

* One registers a variety of methods to respond to state changes

  ```csharp
    state.AddTransitions(new[] {  
      new StateTransition() { 
        from = State.Interactive, to = State.InteractiveClosing, 
        performBeforeSync = () => {...}, performAfterSync = () => {...} 
      },
      new StateTransition() { 
        from = State.InteractiveClosing, to = State.Attract, 
        performBefore = async () => {...}, performAfter = async () => {...} 
      },
    );
  ```
  
* One registers a variety of methods to respond to entering a state

  ```csharp
	state.SetPerformBeforeEnter(State.Interactive, async () => {...});
	state.SetPerformBeforeEnterSync(State.Interactive, () => {...});
	state.SetPerformAfterEnter(State.Interactive, async () => {...});
	state.SetPerformAfterEnterSync(State.Interactive, () => {...});
  ```

* One attempts to change state, with exceptions optionally. thrown for invalid attempts, such as a non-existent transition or an attempt during a previous transition.

  *Example:*
  
  * This works successfully because above we're currently at `State.Attract` and we've registered a transition from `Attract` to `Interactive`

  ```csharp
    state.SetState(State.Interactive);
  ```
  
  This will perform all `performBefore(Enter)(Sync)` and `performAfter(Enter)(Sync)` properly. In this way we can enforce clear, consistent transition and setup of a state.
  
  
  * If we're now at `Interactive` this will throw an exception because there is no transition from `Interactive` directly back to `Attract`, only to `InteractiveClosing` first.
  
  ```csharp
    state.SetState(State.Attract);
  ```
  
  In this way we can enforce only supported state changes, state changes that make sense to the object or representation we've created.
  

## Order of execution of registered responses:

1. Transition's async *PerformBefore*
1. Transition's sync *PerformBeforeSync*
1. New State's async *PerformBeforeEnter*
1. New State's sync *PerformBeforeEnterSync*
1. **New State made Current State**
1. Transition's async *PerformAfter*
1. Transition's sync *PerformAfterSync*
1. New State's async *PerformAfterEnter*
1. New State's sync *PerformAfterEnterSync*

Because our new state becomes our current state before *PerformAfter(Enter)(Sync)* registered methods are performed technically a *PerformAfter(Enter)(Sync)* can queue another state change, just be careful we aren't still transitioning when the state change is performed! 😜

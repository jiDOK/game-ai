public class StateMachine<T>
{
  public T owner { get; }
  public State<T> previousState { get; private set; } = null;
  public State<T> currentState { get; private set; }
  public State<T> anyState { get; private set; }

  public StateMachine(T owner, State<T> currentState, State<T> anyState)
  {
    this.owner = owner;
    this.currentState = currentState;
    this.anyState = anyState;
  }

  public void Update()
  {
    currentState.Execute(owner);
    anyState.Execute(owner);
  }

  public void ChangeState(State<T> state)
  {
    previousState = currentState;
    currentState.OnStateExit(owner);
    currentState = state;
    currentState.OnStateEnter(owner);
  }

  public void RevertToPreviousState()
  {
    //if (previousState == null) return;
    ChangeState(previousState);
  }

  public bool IsInState(State<T> state)
  {
    return state.GetType() == currentState.GetType();
  }

  public bool HandleMessage(Telegram telegram)
  {
    if(currentState.OnMessage(owner, telegram))
    {
      return true;
    }
    if(anyState.OnMessage(owner, telegram))
    {
      return true;
    }
    return false;
  }
}

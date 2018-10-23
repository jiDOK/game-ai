public abstract class State<T>
{
  public abstract void Execute(T owner);
  public virtual void OnStateEnter(T owner) { }
  public virtual void OnStateExit(T owner) { }
}

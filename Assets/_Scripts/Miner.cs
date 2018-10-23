using UnityEngine;

public class Miner : MonoBehaviour
{
  public enum Location { Mine, Bank, Home, Saloon }
  State<Miner> currentState;
  public State<Miner> enterMineAndDigForNugget { get; } = new EnterMineAndDigForNugget();
  public State<Miner> quenchThirst { get; } = new QuenchThirst();
  public State<Miner> visitBankAndDepositGold { get; } = new VisitBankAndDepositGold();
  public State<Miner> goHomeAndSleepTilRested { get; } = new GoHomeAndSleepTilRested();
  public State<Miner> anyState { get; } = new AnyState();
  public Location currentLocation { get; set; }
  public int goldCarried { get; set; }
  public int moneyInBank { get; set; }
  public int daySuccess { get; set; }
  public int fatigue { get; set; }
  public int thirst { get; set; }
  public float restingTime { get; set; }

  void Start()
  {
    ChangeState(enterMineAndDigForNugget);
  }

  void Update()
  {
    currentState?.Execute(this);
    anyState.Execute(this);
  }


  public void ChangeState(State<Miner> state)
  {
    currentState?.OnStateExit(this);
    currentState = state;
    currentState.OnStateEnter(this);
  }
}

using UnityEngine;

public class Miner : MonoBehaviour
{
  public enum Location { Mine, Bank, Home, Saloon }
  public StateMachine<Miner> fsm { get; private set; }
  public State<Miner> enterMineAndDigForNugget { get; } = new EnterMineAndDigForNugget();
  public State<Miner> quenchThirst { get; } = new QuenchThirst();
  public State<Miner> visitBankAndDepositGold { get; } = new VisitBankAndDepositGold();
  public State<Miner> goHomeAndSleepTilRested { get; } = new GoHomeAndSleepTilRested();
  public State<Miner> sing { get; } = new SingAndRevert();
  public State<Miner> anyState { get; } = new AnyState();
  public Location currentLocation { get; set; }
  public int goldCarried { get; set; }
  public int moneyInBank { get; set; }
  public int daySuccess { get; set; }
  public int fatigue { get; set; }
  public int thirst { get; set; }
  public float restingTime { get; set; }

  private void Start()
  {
    fsm = new StateMachine<Miner>(this, enterMineAndDigForNugget, anyState);
  }

  void Update()
  {
    fsm.Update();
  }
}

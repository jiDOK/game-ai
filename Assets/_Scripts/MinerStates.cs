using UnityEngine;
using static Miner;

public class EnterMineAndDigForNugget : State<Miner>
{
  int digTimer;
  int currentTime;

  public override void OnStateEnter(Miner owner)
  {
    if (owner.currentLocation != Location.Mine)
    {
      Debug.Log("Walking to the gold mine");
      owner.currentLocation = Location.Mine;
    }
  }

  public override void Execute(Miner owner)
  {
    currentTime = (int)Time.time;
    Dig(owner);
    if (owner.goldCarried > 2)
    {
      owner.fsm.ChangeState(owner.visitBankAndDepositGold);
    }
    else if (owner.thirst > 1)
    {
      owner.fsm.ChangeState(owner.quenchThirst);
    }
  }

  void Dig(Miner owner)
  {
    if (digTimer == currentTime)
    {
      return;
    }
    else
    {
      digTimer = currentTime;
      int revenue = Random.Range(0, 3);
      owner.goldCarried += revenue;
      owner.daySuccess += revenue;
      owner.fatigue += 2;
      owner.thirst++;
      string revString = revenue == 0 ? "no" : revenue.ToString();
      string suffix = revenue != 1 ? "s" : "";
      Debug.Log($"Pickin' up {revString} nugget{suffix}");
    }
  }
}

public class QuenchThirst : State<Miner>
{
  int timer;
  int currentTime;

  public override void OnStateEnter(Miner owner)
  {
    if (owner.currentLocation != Location.Saloon)
    {
      Debug.Log("I'm thirsty! Going to the Saloon...");
      owner.currentLocation = Location.Saloon;
    }
  }

  public override void Execute(Miner owner)
  {
    owner.fatigue++;
    DecreaseThirst(owner);
    if (owner.thirst == 0)
    {
      Debug.Log("My thirst is quenched, back to work!");
      owner.fsm.ChangeState(owner.enterMineAndDigForNugget);
    }
  }

  void DecreaseThirst(Miner owner)
  {
    currentTime = (int)Time.time;
    if (timer == currentTime) { return; }
    timer = currentTime;
    owner.thirst--;
    Debug.Log("Feeling a little less thirsty. Thirst level: " + owner.thirst);
  }
}

public class VisitBankAndDepositGold : State<Miner>
{
  public override void OnStateEnter(Miner owner)
  {
    if (owner.currentLocation != Location.Bank)
    {
      Debug.Log("Pockets full, going to the bank");
      owner.currentLocation = Location.Bank;
    }
    owner.moneyInBank += owner.goldCarried;
    owner.goldCarried = 0;
    Debug.Log($"Depositing gold, total savings now: {owner.moneyInBank}");
    if (owner.daySuccess > 10)
    {
      owner.daySuccess = 0;
      Debug.Log("Enough for today, going home");
      owner.fsm.ChangeState(owner.goHomeAndSleepTilRested);
    }
    else
    {
      Debug.Log("Got to find more, back to work!");
      owner.fsm.ChangeState(owner.enterMineAndDigForNugget);
    }
  }

  public override void Execute(Miner owner)
  {
  }
}

public class GoHomeAndSleepTilRested : State<Miner>
{
  float timer;
  public override void OnStateEnter(Miner owner)
  {
    if (owner.currentLocation != Location.Home)
    {
      owner.fatigue = 0;
      owner.daySuccess = 0;
      Debug.Log("Home at last!");
      owner.currentLocation = Location.Home;
    }
  }

  public override void Execute(Miner owner)
  {
    timer += Time.deltaTime;
    if (timer > 3f)
    {
      timer = 0;
      Debug.Log("Good Morning! I am going to the mine");
      owner.fsm.ChangeState(owner.enterMineAndDigForNugget);
    }
  }
}

public class SingAndRevert : State<Miner>
{
  public override void OnStateEnter(Miner owner)
  {
    string line = Random.value > 0.5f ? 
      "I am Gold Miner and I'm OK, I sleep all night and I work all day" : 
      "I am Gold Miner and I'm allright, I work all day and I sleep all night";
    Debug.Log(line);
    owner.fsm.RevertToPreviousState();
  }

  public override void Execute(Miner owner)
  {
  }
}

public class AnyState : State<Miner>
{
  float singigTimer = 10f;

  public override void Execute(Miner owner)
  {
    singigTimer -= Time.deltaTime;
    if (singigTimer <= 0)
    {
      singigTimer = Random.Range(8f, 20f);
      owner.fsm.ChangeState(owner.sing);
    }

    else if (owner.fatigue > 10)
    {
      Debug.Log("Yawwn, I'm too tired, going home for today");
      owner.fsm.ChangeState(owner.goHomeAndSleepTilRested);
    }
  }
}
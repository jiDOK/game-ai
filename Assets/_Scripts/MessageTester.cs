using UnityEngine;

public class MessageTester : BaseGameEntity
{
  [SerializeField] Miner miner;

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      MessageDispatcher.Instance.DispatchMessage(id, miner.id, Msg.Greeting, 4f, new GreetingInfo("buddy"));
      MessageDispatcher.Instance.DispatchMessage(id, miner.id, Msg.GoldRush, 2f, ExtraInfo.Empty());
    }
  }
}

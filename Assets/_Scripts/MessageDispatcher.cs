using System.Collections.Generic;
using UnityEngine;

public class MessageDispatcher : Singleton<MessageDispatcher>
{
  SortedSet<Telegram> telegrams = new SortedSet<Telegram>(new ByDispatchTime());

  //protected override void Awake()
  //{
  //  base.Awake();
  //  //bla
  //}

  void Update()
  {
    DispatchDelayedMessages();
  }

  void Discharge(BaseGameEntity receiver, Telegram telegram)
  {
    //bool success = receiver.HandleMessage(telegram);
    //if(success == false)
    //{
    //  Debug.LogError("Message not handled!");
    //}

    if (!receiver.HandleMessage(telegram))
    {
      Debug.Log("Message not handled!");
    }
  }

  public void DispatchMessage(int senderID, int receiverID, Msg messageType, float delay, ExtraInfo info)
  {
    BaseGameEntity receiver = EntityManager.Instance.entityDict[receiverID];
    var telegram = new Telegram(senderID, receiverID, messageType, delay, info);
    if (delay <= 0f)
    {
      Discharge(receiver, telegram);
    }
    else
    {
      telegram.dispatchTime = Time.time + delay;
      telegrams.Add(telegram);
    }
  }

  void DispatchDelayedMessages()
  {
    while (telegrams.Count > 0 && telegrams.Min.dispatchTime <= Time.time)
    {
      //if (telegrams.Min.dispatchTime - Time.time < -2f)
      //{
      //  telegrams.Remove(telegrams.Min);
      //}
      //else
      //{
      var telegram = telegrams.Min;
      BaseGameEntity receiver = EntityManager.Instance.entityDict[telegram.receiverID];
      telegrams.Remove(telegram);
      Discharge(receiver, telegram);
      //}
    }
  }
}

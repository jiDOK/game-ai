using System.Collections.Generic;

public class Telegram
{
  public int senderID { get; }
  public int receiverID { get; }
  public Msg messageType { get; }
  public float dispatchTime { get; set; }
  public ExtraInfo info { get; }

  public Telegram(int senderID, int receiverID, Msg messageType, float dispatchTime, ExtraInfo info)
  {
    this.senderID = senderID;
    this.receiverID = receiverID;
    this.messageType = messageType;
    this.dispatchTime = dispatchTime;
    this.info = info;
  }
}

public class ByDispatchTime : IComparer<Telegram>
{
  public int Compare(Telegram x, Telegram y)
  {
    return x.dispatchTime.CompareTo(y.dispatchTime);
  }
}

public class ExtraInfo
{
  public static ExtraInfo Empty()
  {
    return new ExtraInfo();
  }
}

public class StandardInfo : ExtraInfo
{
  public string messageText { get; set; } = "";

  public StandardInfo(string msgText)
  {
    messageText = msgText;
  }
}

public class GreetingInfo : ExtraInfo
{
  public string nickName { get; set; } = "";

  public GreetingInfo(string nickName)
  {
    this.nickName = nickName;
  }
}

public enum Msg
{
  Standard, Greeting, Attack, GoldRush
}

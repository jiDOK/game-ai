using UnityEngine;

public class BaseGameEntity : MonoBehaviour
{
  public int id { get; private set; }

  protected virtual void Awake()
  {
    id = gameObject.GetInstanceID();
  }

  protected virtual void OnEnable()
  {
    EntityManager.Instance.RegisterEntity(this);
  }

  protected virtual void OnDisable()
  {
    EntityManager.Instance.RemoveEntity(this);
  }

  public virtual bool HandleMessage(Telegram telegram)
  {
    return false;
  }
}

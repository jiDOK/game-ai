using System.Collections.Generic;

public class EntityManager : Singleton<EntityManager>
{
  public Dictionary<int, BaseGameEntity> entityDict = new Dictionary<int, BaseGameEntity>();

  public void RegisterEntity(BaseGameEntity entity)
  {
    entityDict.Add(entity.id, entity);
  }

  public void RemoveEntity(BaseGameEntity entity)//TODO: bool weitergeben
  {
    entityDict.Remove(entity.id);
  }

  public void RemoveEntity(int id)//TODO: bool weitergeben
  {
    entityDict.Remove(id);
  }
}

using UnityEngine;
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
  static T instance;
  public static T Instance
  {
    get
    {
      if (instance == null)
      {
        instance = (T)FindObjectOfType(typeof(T));
        if (instance == null)
        {
          GameObject singletonGO = new GameObject();
          instance = singletonGO.AddComponent<T>();
          singletonGO.name = "Singleton " + typeof(T).ToString();
        }
        //Debug.Log("singleton getter called");
        DontDestroyOnLoad(instance.gameObject);
      }
      return instance;
    }
  }

  protected virtual void Awake()
  {
    if (instance != null && instance != this)
    {
      Destroy(gameObject);
    }
    else if (instance == null)
    {
      DontDestroyOnLoad(gameObject);
      instance = (T)this;
    }
  }
}
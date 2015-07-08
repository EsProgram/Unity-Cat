using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utility
{
  public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
  {
    private static T instance;

    public static T Instance
    {
      get
      {
        if(instance == null)
        {
          instance = FindObjectOfType<T>();

#if UNITY_EDITOR
          if(instance == null)
            Debug.LogWarning(typeof(T) + "is nothing");
#endif
        }
        return instance;
      }
    }

    private void Awake()
    {
      if(!ReferenceEquals(this, Instance))
      {
        Destroy(this);
        return;
      }
      DontDestroyOnLoad(this.gameObject);
    }
  }
}
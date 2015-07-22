using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Item : MonoBehaviour,IItem
{

  public virtual void Get()
  {
    //TODO:アイテム取得時のアニメーションを書き込む
    throw new NotImplementedException();
  }
}

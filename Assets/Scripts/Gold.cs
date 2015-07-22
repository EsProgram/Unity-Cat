using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gold : Item
{
  public override void Get()
  {
    base.Get();
  }

  protected override void AnimationEnd()
  {
    Destroy(gameObject);
  }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Extension
{
  public static class Vector3Extension
  {
    /// <summary>
    /// 座標系のYとZを転置させたVectorを返す
    /// </summary>
    /// <param name="origin">変換前のVector</param>
    /// <returns></returns>
    public static Vector3 TransYZ(this Vector3 origin)
    {
      return new Vector3(origin.x, origin.z, origin.y);
    }
  }
}
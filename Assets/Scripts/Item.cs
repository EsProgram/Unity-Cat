using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Item : MonoBehaviour, IItem
{
  //アニメーションでどのくらい上に上がるか
  const float HIGHT = 0.5f;
  //アニメーションで上昇していくスピード
  const float UP_SPEED = 0.5f;

  public virtual void Get()
  {
    StartCoroutine(GetAnimation());
  }

  /// <summary>
  /// アイテム取得時のアニメーションを実行する
  /// </summary>
  private IEnumerator GetAnimation()
  {
    Debug.Log("GET ANIMATION START");

    var startPosY = transform.position.y;
    var materials = new List<Material>();

    //マテリアル全てを取得
    foreach(Transform child in transform)
    {
      if(child != null && child.GetComponent<Renderer>() != null && child.GetComponent<Renderer>().material != null)
        foreach(Material mat in child.GetComponent<Renderer>().materials)
          materials.Add(mat);
    }

    //位置アニメーション
    while(startPosY + HIGHT > transform.position.y)
    {
      transform.Translate(Vector3.up * UP_SPEED * Time.deltaTime, Space.World);
      yield return new WaitForEndOfFrame();
    }

    //TODO:透過アニメーション 
  }
}
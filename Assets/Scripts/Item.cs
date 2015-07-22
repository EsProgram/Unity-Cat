using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Item : MonoBehaviour, IItem
{
  //アニメーションでどのくらい上に上がるか
  const float HIGHT = 0.5f;
  //アニメーションで上昇していく速度
  const float UP_SPEED = 0.5f;
  //アニメーションで透過する速度
  const float ALPHA_SPEED = 0.05f;
  //既にGetメソッドが呼ばれた場合true
  bool god;
  bool isEndAnimation;
  protected bool IsEndAnimation { get { return IsEndAnimation; } }

  public virtual void Get()
  {
    //2度め以降のGet呼び出しを無効にする
    if(god)
      return;
    god = true;
    StartCoroutine(GetAnimation());
  }

  /// <summary>
  /// アニメーション終了時に呼ばれる
  /// </summary>
  protected abstract void AnimationEnd();

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
    while(true)
    {
      foreach(var mat in materials)
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a - ALPHA_SPEED);
      if(materials.All(m => m.color.a <= 0))
        break;
      yield return new WaitForEndOfFrame();
    }
    isEndAnimation = true;
    AnimationEnd();
  }
}
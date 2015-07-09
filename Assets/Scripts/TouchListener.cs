using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TouchListener : SingletonMonoBehaviour<TouchListener>
{
  public void Awake()
  {
    TouchManager.Instance.TouchStart += OnTouchStart;
  }

  /// <summary>
  /// タッチ開始時の動作
  /// </summary>
  void OnTouchStart(object sender, CustomInputEventArgs args)
  {
    Debug.Log("Call Touch Start");
    //タッチした位置にタグ名がPlayerのオブジェクトがあればそれを取得する
    var playerHit = Physics.RaycastAll(Camera.main.ScreenPointToRay(args.Input.ScreenPosition))
      .FirstOrDefault(hit => { return hit.collider.gameObject.tag == "Player"; });

    //プレイヤーのゲームオブジェクトを取得
    var player = playerHit.collider != null ? playerHit.collider.gameObject : null;

    //何かしらの処理
  }


}

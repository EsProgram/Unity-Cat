using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TouchListener : SingletonMonoBehaviour<TouchListener>
{

  bool touchPlayer;


  public void Awake()
  {
    TouchManager.Instance.TouchStart += OnTouchStart;
    TouchManager.Instance.Drag += OnDrag;
    TouchManager.Instance.TouchEnd += OnTouchEnd;
  }

  /// <summary>
  /// タッチ開始時の動作
  /// </summary>
  void OnTouchStart(object sender, CustomInputEventArgs args)
  {
    //タッチした位置にタグ名がPlayerのオブジェクトがあればそれを取得する
    var playerHit = Physics.RaycastAll(Camera.main.ScreenPointToRay(args.Input.ScreenPosition))
      .FirstOrDefault(hit => { return hit.collider.gameObject.tag == "Player"; });

    //プレイヤーのゲームオブジェクトを取得
    var player = playerHit.collider != null ? playerHit.collider.gameObject : null;

    //何かしらの処理
    if(player != null)
      touchPlayer = true;
    
  }

  /// <summary>
  /// ドラッグ時のイベント
  /// </summary>
  void OnDrag(object sender, CustomInputEventArgs args)
  {
    if(touchPlayer)
    {
      Debug.Log("Drag");
    }
  }

  /// <summary>
  /// リリース時のイベント
  /// </summary>
  void OnTouchEnd(object sender, CustomInputEventArgs args)
  {
    touchPlayer = false;
  }


}

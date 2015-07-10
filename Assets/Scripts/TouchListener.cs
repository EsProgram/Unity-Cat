using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Extension;

/// <summary>
/// タッチ入力に応じた処理のイベントを提供する
/// </summary>
public class TouchListener : SingletonMonoBehaviour<TouchListener>
{
  GameObject touchObject;

  /// <summary>
  /// 現在タッチしているオブジェクト
  /// </summary>
  public GameObject TouchObject { get { return touchObject; } }

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
    //最初にヒットしたオブジェクトを取得する
    RaycastHit hit;
    if(Physics.Raycast(Camera.main.ScreenPointToRay(args.Input.ScreenPosition), out hit))
      touchObject = hit.collider.gameObject;
  }

  /// <summary>
  /// ドラッグ時のイベント
  /// </summary>
  void OnDrag(object sender, CustomInputEventArgs args)
  {
    if(touchObject != null)
      touchObject.transform.Translate(args.Input.DeltaPosition.TransYZ() * args.Input.DeltaTime);
  }

  /// <summary>
  /// リリース時のイベント
  /// </summary>
  void OnTouchEnd(object sender, CustomInputEventArgs args)
  {
    touchObject = null;
  }


}

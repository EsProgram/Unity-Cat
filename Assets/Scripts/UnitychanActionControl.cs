using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class UnitychanActionControl : MonoBehaviour
{
  const float ROTATE_SPEED = 10f;

  public bool AllowClickEvent { get; set; }

  private NavMeshAgent agent;
  private Animator anim;
  //走り判定のベクトルの大きさ
  private const float RUN_DECISION = 0.3f;


  public UnitychanActionControl()
  {
    AllowClickEvent = true;
  }

  public void Awake()
  {
    agent = GetComponent<NavMeshAgent>();
    anim = GetComponent<Animator>();
    TouchListener.Instance.ClickEvent += Instance_ClickEvent;
  }

  /// <summary>
  /// クリック時のイベント
  /// </summary>
  private void Instance_ClickEvent(object sender, CustomInputEventArgs e)
  {
    if(!AllowClickEvent)
      return;

    var animState = anim.GetCurrentAnimatorStateInfo(0);
    switch(TouchListener.Instance.TouchObject.tag)
    {
      case "Ground":
        //タッチした位置に移動
        if(animState.IsName("Standing@loop") || animState.IsName("Running@loop"))
          agent.SetDestination(TouchListener.Instance.HitPoint);
        break;

      case "Treasure":
        //距離が一定値以内であれば宝箱を開ける
        if(animState.IsName("Standing@loop"))
        {
          RaycastHit hit;
          //宝の方向を向いていればGETする
          if(Physics.Linecast(transform.position, transform.position + transform.forward, out hit))
          {
            if(hit.collider.tag == "Treasure")
            {
              var item = TouchListener.Instance.TouchObject.GetComponent<TreasureBoxControl>().Open();

              //アイテム取得処理
              if(item != null)
              {
                anim.SetTrigger("Glad");
                item.Get();
              }
            }
          }
          //向いていなければ宝の方向を向く
          else
            StartCoroutine(RotateCoroutine());
        }
        break;
      case "UnityChan":
        if(animState.IsName("Standing@loop"))
        {
          //2通りのアニメーションをそれぞれ確率で発生させる
          int randValue = UnityEngine.Random.Range(0, 100);
          if(randValue < 40)
            anim.SetTrigger("Touch1");
          else
            anim.SetTrigger("Touch2");
        }
        break;

      default:
        break;
    }
  }

  /// <summary>
  /// Unityちゃんのアニメーションを更新する
  /// </summary>
  private void AnimationUpdate()
  {
    if(agent.velocity.magnitude > RUN_DECISION)
      anim.SetBool("Run", true);
    else
      anim.SetBool("Run", false);
  }

  private IEnumerator RotateCoroutine()
  {
    for(int i = 0; i < 60; ++i)
    {
      var diff = (TouchListener.Instance.TouchObject.transform.position - transform.position).normalized;
      //diff.y = 0;
      diff = Vector3.Lerp(transform.forward, diff, Time.deltaTime * ROTATE_SPEED);
      transform.rotation = Quaternion.LookRotation(diff);
      yield return new WaitForEndOfFrame();
    }
  }


  public void Update()
  {
    AnimationUpdate();
  }
}

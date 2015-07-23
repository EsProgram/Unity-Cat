using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class UnitychanActionControl : MonoBehaviour
{
  //宝箱タッチ時の回転動作スピード
  const float ROTATE_SPEED = 10f;
  //走りアニメーションに遷移する速度ベクトルの大きさ
  private const float RUN_DECISION = 0.3f;

  public bool AllowClickEvent { get; set; }

  private NavMeshAgent agent;
  private Animator anim;


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
        {
          agent.velocity = Vector3.zero;
          agent.SetDestination(TouchListener.Instance.HitPoint);
        }
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
  /// Unityちゃんの走りアニメーションを更新する
  /// </summary>
  private void RunAnimationUpdate()
  {
    if(agent.velocity.magnitude > RUN_DECISION)
    {
      if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Running@loop"))
        anim.SetBool("Run", true);
    }
    else
      if(anim.GetCurrentAnimatorStateInfo(0).IsName("Running@loop"))
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
    RunAnimationUpdate();
  }
}

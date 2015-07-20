using UnityEngine;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class UnitychanActionControl : MonoBehaviour
{
  private NavMeshAgent agent;
  private Animator anim;
  //走り判定のベクトルの大きさ
  private const float RUN_DECISION = 0.3f;

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
    switch(TouchListener.Instance.TouchObject.tag)
    {
      case "Ground":
        //タッチした位置に移動
        agent.SetDestination(TouchListener.Instance.HitPoint);
        break;

      case "Treasure":
        //距離が一定値以内であれば宝箱を開ける
        //TODO:距離一定値条件と回転動作を加える
        TouchListener.Instance.TouchObject.GetComponent<TreasureBoxControl>().Open();
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

  public void Update()
  {
    AnimationUpdate();
  }
}

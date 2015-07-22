using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TreasureBoxControl : MonoBehaviour
{
  public GameObject treasureItem = null;
  private Animator anim;
  //一度でもOpenされたかどうか
  bool alreadyOpend;

  /// <summary>
  /// 一度でもOpenされたことがあればtrueを返す
  /// </summary>
  public bool AlreadyOpend { get { return alreadyOpend; } }

  public void Awake()
  {
    anim = GetComponent<Animator>();
  }

  /// <summary>
  /// 宝箱を開く
  /// </summary>
  /// <returns>宝箱から取得したゲームオブジェクト</returns>
  public IItem Open()
  {
    anim.SetBool("IsOpen", true);

    //開いたことがなければお宝GET
    if(!AlreadyOpend)
    {
      alreadyOpend = true;
      if(treasureItem != null)
      {
        var item = Instantiate<GameObject>(treasureItem);
        return item.GetComponent<Item>();
      }
    }

    //2度目以降開いても何も入ってない
    return null;
  }

  public void Close()
  {
    anim.SetBool("IsOpen", false);
  }
}

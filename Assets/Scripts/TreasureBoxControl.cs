using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TreasureBoxControl : MonoBehaviour
{
  private Animator anim;

  public void Awake()
  {
    anim = GetComponent<Animator>();
  }

  public void Open()
  {
    anim.SetBool("IsOpen", true);
  }

  public void Close()
  {
    anim.SetBool("IsOpen", false);
  }
}

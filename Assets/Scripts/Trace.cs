using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Trace : MonoBehaviour
{
  public Transform traceTarget;
  public Transform lookAt;
  public float maxRange = 5f;
  public float minRange = 3f;
  public float speed = 3f;


  public void Update()
  {
    //離れ過ぎたら近づく
    if(Vector3.Distance(traceTarget.position, transform.position) > maxRange)
    {
      var dir = Vector3.Lerp(Vector3.zero, traceTarget.position - transform.position, Time.deltaTime);
      transform.position += dir.normalized * Time.deltaTime * speed;
    }
    //近過ぎたら離れる
    if(Vector3.Distance(traceTarget.position, transform.position) < minRange)
    {
      var dir = Vector3.Lerp(Vector3.zero, traceTarget.position - transform.position, Time.deltaTime);
      transform.position -= dir.normalized * Time.deltaTime * speed;
    }
    transform.LookAt(lookAt);
  }
}

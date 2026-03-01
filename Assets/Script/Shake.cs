using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public bool _Shakestart {  get;  set; }
    public AnimationCurve curve;
    public float duration = 1f;
    public float angleduration = 1f;
    public bool _getdamage {  get; set; }
  
    
    public IEnumerator Shakeing(Camera MainCam)
    {
       
        Vector3 startPos = MainCam.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength=curve.Evaluate(elapsedTime/duration);
           MainCam.transform.position = startPos + Random.insideUnitSphere*strength;
            yield return null;
        }
        transform.position = startPos;
    }
  
}

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
  
    void Update()
    {
        //if (_Shakestart)
        //{
        //    _Shakestart = false;
        //    StartCoroutine(Shakeing());
        //}
        //if (_playerController.GetDamage==true)
        //{
        //    _playerController._GetDamage = false;
        //    StartCoroutine(Getdam());
        //}
       
    }
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
    IEnumerator Getdam()
    {
        Vector3 startrot=transform.eulerAngles;
        float elapsedTimer = 0f;
        while (elapsedTimer < angleduration)
        {
            elapsedTimer += Time.deltaTime;
            transform.eulerAngles += new Vector3(-1.5f, 0, 0);
            yield return null;
        }
        transform.eulerAngles = startrot;

    }
   
}

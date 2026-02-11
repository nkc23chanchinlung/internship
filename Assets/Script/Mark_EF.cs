using UnityEngine;
using DG.Tweening;

public class Mark_EF : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.transform.DORotate(Vector3.up * 180, 5f).SetLoops(-1, LoopType.Incremental);
    }

    // Update is called once per frame
    
}

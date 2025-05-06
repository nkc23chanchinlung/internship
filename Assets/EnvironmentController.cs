using DG.Tweening;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    [SerializeField] GameObject Water;
    Vector3 WaterPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        WaterPos = Water.transform.position;
        Debug.Log(WaterPos);
    }
    void Start()
    {
       
        Sea();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Sea()
    {
        
        Water. transform.DOLocalMove(new Vector3(7, 84.5f, -440), 5f)
     .SetLoops(-1, LoopType.Yoyo)
        .SetEase(Ease.InOutQuart);
    }
}

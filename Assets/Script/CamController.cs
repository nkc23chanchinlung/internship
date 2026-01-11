using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using Unity.VisualScripting;





public class CamController : MonoBehaviour
{
    public Transform _target { get; set; }
    private float x=0;
    private float y=0;
    [SerializeField] float distance = 4f;
    [SerializeField] float camy;
    [SerializeField] float camsize;
    [SerializeField] Transform playerpos;
    Vector3 Mousepos;
   
    
    public float _distance
    {
        get
        {
            return distance;
        }
    }
    
    float Maxdistance = 8f;
    float Mindistance = 2.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        GameManager.OnGameStart += OnGameStart;
    }
    private void OnDisable()
    {
        GameManager.OnGameStart -= OnGameStart;
    }
    void OnGameStart()
    {
        _target = GameObject.FindWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        Mousepos = Input.mousePosition;
        

        //カメラカーソルによる移動
        if (Mousepos.y > Screen.height - 500) x = -4.0f;
        else if (Mousepos.y < 300) x = -2.0f;
        else x = -3.0f; 
        if (Mousepos.x> Screen.width - 300) y = -0.5f;
        else if (Mousepos.x < 300) y = 0.5f;
        else y = 0f;

    }
    private void FixedUpdate()
    {
        if (GameManager.instance.IsOpenMoviePlaying) return;
        
        Cam(_target);
    }
    //カメラコントロールメソッド
    public void Cam(Transform target)
    {

        target = _target;
        distance = Mathf.Clamp(distance, Mindistance, Maxdistance);
        var scroll = Input.mouseScrollDelta.y;
        distance -= scroll * 0.2f;

        //カメラプレイや追跡
        this.transform.DOMove(_target.position + new Vector3(y, distance, x+7f), 1f).SetEase(Ease.OutSine); 
        
       
       
        if (Input.GetMouseButton(1))
        {
            var mouseX = Input.GetAxis("Mouse X");
            transform.RotateAround(_target.position, Vector3.up, mouseX * 2);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, y, transform.eulerAngles.z);
        }
    }
    
    
}

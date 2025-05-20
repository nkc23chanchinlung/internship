using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;


//public
//value ; _target


public class CamController : MonoBehaviour
{
    public Transform _target { get; set; }
    [SerializeField] float x;
    [SerializeField] float distance = 4f;
    [SerializeField] float camy;
    [SerializeField] float camsize;
    [SerializeField] Transform playerpos;
    bool isobest;
    bool onlyonce;
    private float y;
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
    void Start()
    {
        _target = UnityEngine.GameObject.FindWithTag("Player").transform;
        y = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        Cam(_target);
        obestcam();
      
    }
    public void Cam(Transform target)
    {

        target = _target;
        distance = Mathf.Clamp(distance, Mindistance, Maxdistance);
        var scroll = Input.mouseScrollDelta.y;
        distance -= scroll * 0.1f;
        transform.position = _target.position + new Vector3(0, distance, x);
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
    void obestcam()  //遮蔽物判定(未完成)
    {
        RaycastHit hit;
        Physics.BoxCast(transform.position, new Vector3(camsize, camsize, camsize),transform.forward, out hit, Quaternion.identity,camy);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "terrain")
            {
               
                isobest = true;
               
            }
            else isobest = false;
        }
        else isobest = false;


        if (isobest && !onlyonce)
        {
            distance -= 1.5f;
            gameObject.transform.DOMove(_target.transform.position + new Vector3(0, distance, x), 0.1f);
            onlyonce = true;
        }
        else if (!isobest && onlyonce)
        {
            distance += 1.5f;
            gameObject.transform.DOMove(_target.transform.position + new Vector3(0, distance, x), 0.1f);
            onlyonce = false;
        }




    }
    void OnDrawGizmos()
    {
       Vector3 halfExtents = new Vector3(camsize, camsize, camsize);
        float maxDistance = 5f;
        Color gizmoColor=Color.red;
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward * camy;
        Quaternion rotation = Quaternion.identity; // 必要なら transform.rotation に変更可能

        Gizmos.color = gizmoColor;

        // 始点のボックス
        Gizmos.matrix = Matrix4x4.TRS(origin, rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, halfExtents * 2);

        // 終点のボックス（BoxCast の最大距離）
        Vector3 end = origin + direction * maxDistance;
        Gizmos.matrix = Matrix4x4.TRS(end, rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, halfExtents * 2);

        // 始点〜終点を結ぶ線
        Gizmos.matrix = Matrix4x4.identity;
        Gizmos.DrawLine(origin, end);
    }
}

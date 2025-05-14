using UnityEngine;
using UnityEngine.UIElements;

//public
//value ; _target


public class CamController : MonoBehaviour
{
    public Transform _target { get; set; }
    [SerializeField] float x;
    [SerializeField] float distance = 4f;
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
}

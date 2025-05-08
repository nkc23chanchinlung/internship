using UnityEngine;

public class ObjCreate : MonoBehaviour
{
    [SerializeField] Material defaultmat;
    
    Vector3 pos = Vector3.zero;
    CamController camcontroller;
    [SerializeField]float sizex,sizey,sizez;
    [SerializeField] LayerMask layerMask;

    MeshCollider col;
    Material mat;
    bool isfinish;
    bool iscreate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isfinish = false;
        camcontroller = Camera.main.GetComponent<CamController>();
        mat=GetComponent<MeshRenderer>().material;
        col = GetComponent<MeshCollider>();
        col.isTrigger = true;
       

    }

    // Update is called once per frame
    void Update()
    {
        if (isfinish) return;
        FindMouse();
        pos = gameObject.transform.position;
        pos.x = Mathf.Round(pos.x / 1f);
       // pos.y = Mathf.Round(pos.y / 1f);
        pos.z = Mathf.Round(pos.z / 1f);
        gameObject.transform.position = pos;


    }
    void FindMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * camcontroller._distance, Color.red);    //Debug—p************
        if (Physics.Raycast(ray, out hit, 10f,layerMask))
        {

            gameObject.transform.position = new Vector3(hit.point.x + sizex, hit.point.y +sizey, hit.point.z + sizez);

            if (Input.GetMouseButtonDown(0)&&iscreate)
            {
                GetComponent<MeshRenderer>().material = defaultmat;
                col.isTrigger = false;
                isfinish = true;

            }
        }
      

    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "GameObj")
        {
            mat.color = Color.red;
            iscreate = false;
        }
        else
        {
            mat.color = Color.green;
            iscreate = true;
        }
        }
}

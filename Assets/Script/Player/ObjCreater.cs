using UnityEngine;

public class ObjCreater : MonoBehaviour
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
        MouseRay();
        pos = gameObject.transform.position;
        pos.x = Mathf.Round(pos.x / 1f);
       
        pos.z = Mathf.Round(pos.z / 1f);
        gameObject.transform.position = pos;


    }
    void MouseRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * camcontroller._distance, Color.red);    //Debug—p************
        if (Physics.Raycast(ray, out hit, 10f,layerMask))
        {
            
            gameObject.transform.position = new Vector3(hit.point.x + sizex, hit.point.y + sizey, hit.point.z + sizez);
            if(Input.GetKeyDown(KeyCode.Q))
            {
                gameObject.transform.Rotate(0, 90, 0);
            }
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<MeshRenderer>().material = defaultmat;
                gameObject.layer = LayerMask.NameToLayer("Ground");
                gameObject.tag = "GameObj";
                col.isTrigger = false;
                isfinish = true;
            }
        }
        else mat.color = Color.red;
    }
    
      

    
    
}

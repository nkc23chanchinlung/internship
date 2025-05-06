using UnityEngine;

public class Create : MonoBehaviour
{
    [SerializeField] GameObject AcceptObj;
    Vector3 pos = Vector3.zero;
    CamController camcontroller;
    [SerializeField]float sizex,sizey,sizez;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camcontroller = Camera.main.GetComponent<CamController>();

    }

    // Update is called once per frame
    void Update()
    {
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
        if (Physics.Raycast(ray, out hit, 10f))
        {

            gameObject.transform.position = new Vector3(hit.point.x + sizex, hit.point.y +sizey, hit.point.z + sizez);

            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(AcceptObj, new Vector3(pos.x, pos.y, pos.z), Quaternion.identity);
                Destroy(gameObject);

            }
        }

    }
}

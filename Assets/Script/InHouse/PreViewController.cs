using System.Collections.Generic;
using UnityEngine;



//ñ¢äÆê¨ÅAîzóÒÇÃÇ«Ç±ÇÎÇÕïœ
public class PreViewController : MonoBehaviour
{
    [SerializeField] GameObject[] PreViewObj;
    int choosedweapon = 0;
    Vector3 rot = Vector3.zero;
    Vector3 mousepos;
    float mouseX,mouseY;
    float speed;
    public Dictionary<GameObject, int> previewobj_dic = new Dictionary<GameObject, int>();
    GameObject choosedobj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

   
    void Start()
    {
      
        speed = 5f;
       
    }

    // Update is called once per frame
    void Update()
    {
        
        
        mouseX *= 0.97f;
        mouseY *= 0.97f;
        mousepos = Input.mousePosition;
        if (Input.GetMouseButton(0))
        {
            mouseX += Input.GetAxis("Mouse X");
            mouseY += Input.GetAxis("Mouse Y");
            

        }
        // rot += new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0) * 2f * speed;
        rot += new Vector3(mouseY, -mouseX, 0) * speed*Time.deltaTime;
        PreViewObj[choosedweapon].transform.rotation = Quaternion.Euler(rot);
    }
    public void SetPreviewNum(GameObject gameObject)
    {
       
        Showpreview(gameObject);
        
    }
    void Showpreview(GameObject gameObject)
    {
        for (int i = 0; i < PreViewObj.Length; i++)
        {
            PreViewObj[i].SetActive(false);
        }
        Debug.Log(previewobj_dic[gameObject]);
        PreViewObj[previewobj_dic[gameObject]].SetActive(true);
    }
}

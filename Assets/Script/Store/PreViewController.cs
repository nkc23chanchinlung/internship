
using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// 武器のプレビューを制御するクラス
/// </summary>
public class PreViewController : MonoBehaviour
{
    [SerializeField] GameObject[] PreviewObj;
    int choosedweapon = 0;
    Vector3 rot = Vector3.zero;
   
    float mouseX,mouseY;
    [SerializeField]int speed;
    [SerializeField] GameObject PreviewPanel;
    public bool IsPreviewing { get; private set; } = false;
    [SerializeField]Text Weaponname;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
        
        mouseX *= 0.97f;
        mouseY *= 0.97f;
        
        if (Input.GetMouseButton(0))
        {
            mouseX += Input.GetAxis("Mouse X");
            mouseY += Input.GetAxis("Mouse Y");
            

        }
        
        rot += new Vector3(mouseY, -mouseX, 0) * speed*Time.deltaTime;
        if(PreviewPanel.activeSelf)
        PreviewObj[choosedweapon].transform.rotation = Quaternion.Euler(rot+new Vector3(0,90,0));
       
    }

    //武器のプレビューを表示する
    public void Showpreview(int weaponnum,string name)
    {
        Weaponname.text= name;
        IsPreviewing = true;
        PreviewPanel.SetActive(true);
       
        if (PreviewObj != null && PreviewObj.Length > 0)
        {
            PreviewObj[weaponnum].SetActive(true);
        }
        choosedweapon = weaponnum;

    }
    public void Quitpreview()
    {
        IsPreviewing = false;
        PreviewPanel.SetActive(false);
        
            PreviewObj[choosedweapon].SetActive(false);
        
        rot = Vector3.zero;
        mouseX = 0;
        mouseY = 0;
    }
}

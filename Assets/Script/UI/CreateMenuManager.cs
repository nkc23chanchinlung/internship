

using UnityEngine;
using UnityEngine.UI;



public class CreateMenuManager : MonoBehaviour
{
    [SerializeField] GameObject weaponpanel;
    [SerializeField] GameObject[] obj;
    

   public void weapon()
    {
        gameObject.SetActive(false);
        weaponpanel.SetActive(true);
    }
    public void weaponback()
    {
        gameObject.SetActive(true);
        weaponpanel.SetActive(false);
    }
    public void CT_Obj(int num)
    {
       ;
        Debug.Log(num);
        gameObject.SetActive(false);

        Instantiate(obj[num]);


    }
}

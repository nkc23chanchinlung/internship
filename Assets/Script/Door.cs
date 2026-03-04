using UnityEngine;
using UnityEngine.UI;


class Door : MonoBehaviour
{
    [SerializeField] UnityEngine.GameObject accapt;
    [SerializeField] Text accapt_text;
    Accapt accaptscript;
    [SerializeField]UIManager uIManager;
    bool isIndoor = false;
    [SerializeField]PlayerController _player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void FixedUpdate()
    {
        if (isIndoor)
        {
            if (Input.GetKeyDown(KeyCode.E)&&this.gameObject.name== "Door1")
            {
                GameManager.Instance.enterShop = 1;
                uIManager.FadeControl("ShopScene");
                DataManager.Instance.SavePlayerData(_player);
            }
            else if(Input.GetKeyDown(KeyCode.E) && this.gameObject.name == "Door2")
            {
                GameManager.Instance.enterShop = 2;
                uIManager.FadeControl("ShopScene");
                DataManager.Instance.SavePlayerData(_player);
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        accaptscript = accapt.GetComponent<Accapt>();
        if (other.CompareTag("Player"))
        {
            accapt.SetActive(true);
           accaptscript.isIndoor = true;
            accapt_text.text = "ƒVƒ‡ƒbƒv‚É“ü‚é";
            isIndoor = true;
            

        }
      
    }

    private void OnTriggerExit(Collider other)
    {
        accaptscript = accapt.GetComponent<Accapt>();
        if (other.CompareTag("Player"))
        {
            accapt.SetActive(false);
            accaptscript.isIndoor = false;
            accapt_text.text ="";
            isIndoor = false;
        }
    }
}



using UnityEngine;
using UnityEngine.UI;


class Door : MonoBehaviour
{
    [SerializeField] UnityEngine.GameObject accapt;
    [SerializeField] Text accapt_text;
    Accapt accaptscript;
    [SerializeField]UIManager uIManager;
    bool isIndoor = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void FixedUpdate()
    {
        if (isIndoor)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                uIManager.FadeControl("ShopScene");
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



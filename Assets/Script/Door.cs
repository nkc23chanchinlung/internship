using UnityEngine;
using UnityEngine.UI;


class Door : MonoBehaviour
{
    [SerializeField] GameObject accapt;
    [SerializeField] Text accapt_text;
    Accapt accaptscript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        accaptscript = accapt.GetComponent<Accapt>();
        if (other.CompareTag("Player"))
        {
            accapt.SetActive(true);
           accaptscript.isIndoor = true;
            accapt_text.text = "“ü‚é(E)";


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
        }
    }
}



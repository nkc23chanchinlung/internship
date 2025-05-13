using UnityEngine;


 class Door : MonoBehaviour
{
    [SerializeField] GameObject accapt;
    Accapt accaptscript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        accaptscript = accapt.GetComponent<Accapt>();
        if (other.CompareTag("Player"))
        {
            accapt.SetActive(true);
           accaptscript.isIndoor = true;


        }
      
    }
    private void OnTriggerExit(Collider other)
    {
        accaptscript = accapt.GetComponent<Accapt>();
        if (other.CompareTag("Player"))
        {
            accapt.SetActive(false);
            accaptscript.isIndoor = false;
        }
    }
}



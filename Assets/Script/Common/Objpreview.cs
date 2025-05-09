using UnityEngine;

public class Objpreview : MonoBehaviour
{
    
    public void prefebpreview(GameObject target,Material mat)
    {
        GetComponent<MeshRenderer>().material = mat;
        
    }

}

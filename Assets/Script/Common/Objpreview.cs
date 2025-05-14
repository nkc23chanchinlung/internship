using UnityEngine;

public class Objpreview : MonoBehaviour
{
    
    public void prefebpreview(UnityEngine.GameObject target,Material mat)
    {
        GetComponent<MeshRenderer>().material = mat;
        
    }

}

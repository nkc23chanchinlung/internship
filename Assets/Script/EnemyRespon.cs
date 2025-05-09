using System.Drawing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyRespon : MonoBehaviour
{
    public int maxEnemies = 10;
    public int size = 50;
    [SerializeField]GameObject[] Enemyprefeb;
    [SerializeField]GameObject Target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Enemyprefeb= Resources.LoadAll<GameObject>("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            Respon(Target);

        }   
    }
    public void Respon(GameObject Target)
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            float posx = Mathf.Sin(Mathf.PI * 2 * i / maxEnemies) * size;
            float posz = Mathf.Cos(Mathf.PI * 2 * i / maxEnemies) * size;
            Instantiate(Enemyprefeb[Random.Range(0, Enemyprefeb.Length)], new Vector3(posx, 0, posz) + Target.transform.position, Quaternion.identity);
        }
    }
}

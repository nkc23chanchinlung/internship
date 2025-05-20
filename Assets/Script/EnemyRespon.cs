
using UnityEngine;


public class EnemyRespon : MonoBehaviour
{
    [SerializeField] int Maxdistance;
    [SerializeField] int Mindistance;
    [SerializeField] int x, z;
    
    public int maxEnemies = 10;
    public int size = 50;
    [SerializeField] UnityEngine.GameObject[] Enemyprefeb;
    [SerializeField] UnityEngine.GameObject Target;
    public bool dayupdate { get; set; } = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Enemyprefeb = Resources.LoadAll<GameObject>("Enemy");
       
    }

    // Update is called once per frame
    void Update()
    {
        
        if (dayupdate==true)
        {
            Respon(Target);
            dayupdate = false;

        }   
    }
    public void Respon(UnityEngine.GameObject Target)
    {
        for (int x = -this.x; x < this.x; x++)
        {
            for (int z = -this.z; z < this.z; z++)
            {
                if(Target!=null)
                Instantiate(Enemyprefeb[Random.Range(0, Enemyprefeb.Length)], 
                    new Vector3(x * Random.Range(Mindistance, Maxdistance),
                    0, z * Random.Range(Mindistance, Maxdistance)) + Target.transform.position, Quaternion.identity);
                else Instantiate(Enemyprefeb[Random.Range(0, Enemyprefeb.Length)], 
                    new Vector3(x * Random.Range(Mindistance, Maxdistance),
                    0, 
                    z * Random.Range(Mindistance, Maxdistance)), Quaternion.identity);
            }
        }
       
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;





/// <summary>
/// 敵の基底クラス
/// </summary>
public class Enemy : EnemyMovement
{
   
    
    [SerializeField]UIManager uimanager;
    protected enum Status { Idle, Doubt, Hostile,Attack, num };            //敵の状態
    protected Status status = Status.Hostile;
    [Header("索敵範囲")]
    [Tooltip("敵の索敵範囲")]
    [Range(1, 10)]                                                //Inspector上での表示
    [SerializeField] int Enemies;
    protected float speed;
    [SerializeField] int leagth;
   

   
    protected Transform target;
    protected Transform Player;
    Transform House;

    [Header("敵のステータス")]
    public int MaxHp;   //敵の最大HP
    public int Hp;      //敵のHP   
    public int Attack;  //攻撃力
    public int defense; //防御力



    private const float intervalX = 0.1f;
    private const float intervalY = 0.1f;
   
    private float angervalue;                                       //敵の怒り値
    private float dinstance;
    private float targetedge;

    [SerializeField]protected GameObject lifebar;
    protected NavMeshAgent agent;
    [SerializeField]GameObject bulletprefab;
    [SerializeField] GameObject Damageprefeb;
   
    Collider targetcol;
    Vector3 targetsize;                                              //目標の大きさ
    protected Vector3 Sponpoint;                                              //スポーン位置


    [SerializeField] Text Debug_Status;

    [SerializeField] GameObject Hand;
    Rigidbody rb;

    protected ObjAnimetor enemyAnimetor; //敵のアニメーションを管理するクラス

    protected virtual void Init()
    {
        
        uimanager = GameObject.Find("-----UIManager-----").GetComponent<UIManager>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        House = GameObject.Find("House").transform;
       
        agent = GetComponent<NavMeshAgent>();
        

        rb = GetComponent<Rigidbody>();

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    protected void Debug_text() //*****Debug用のテキスト表示関数*****
    {
        Debug_Status.text = "Status:" + status.ToString() + "\n" +
            "target:" + target.gameObject.name.ToString() + "\n" +
            "anger:" + angervalue.ToString() + "\n";
    }
    protected void angerprocess()
    {
        if (angervalue >= 90) target = Player;
        else if(angervalue <= 0) target = null; //怒り値が0の時は家をターゲットにする

        if (target == null) status = Status.Idle; //ターゲットがいない場合はIdle状態にする




    }
    /// <summary>
    /// 索敵の関数
    /// </summary>
    protected void visibility()
    {
        for (int i=-Enemies; i< Enemies; i++)
        {

            for (int j = -Enemies; j < Enemies; j++)
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, (transform.forward + new Vector3(intervalX * i, 0, 0)), out hit, leagth);
                if (hit.collider != null)
                {
                   
                    if (hit.collider.tag == "Player")
                    {
                        angervalue = 100;
                       Hostile(hit);
                    }
                    else if (hit.collider.tag == "GameObj")
                    {
                        Hostile(hit);

                    }
                    
                }
                
                Debug.DrawRay(transform.position, (transform.forward+new Vector3(intervalX * j, intervalY*i, 0))* leagth, Color.red);
               
            }
        }

    }
    /// <summary>
    /// 行動の関数
    /// </summary>
    protected  virtual void movement()
    {

      
        //状態切り替え
        dinstance = Vector3.Distance(target.position, transform.position);
        if (dinstance >= 10) angervalue--;
        angervalue = Mathf.Clamp(angervalue, 0, 100);


        if (dinstance <= targetedge + 2) status = Status.Attack;
        else status = Status.Hostile;


        if (agent == null || !agent.isOnNavMesh)
        {
            Debug.LogWarning("Agent is not on NavMesh or is missing.");
            return;
        }

      
    }

    protected void Idle(Vector3 Sponspace)
    {
        agent.SetDestination(Sponspace);

    }
    

    //敵対の状態の処理
    protected void Hostile(RaycastHit hit)
    {
        target = hit.collider.transform;
        status = Status.Hostile;
        if (hit.collider.tag == "Player")
        {
            targetcol = hit.collider.GetComponent<CapsuleCollider>();
            angervalue += 5;
        }
        else if (hit.collider.tag == "GameObj")
        {
            targetcol = hit.collider.GetComponent<BoxCollider>();
        }
      
        targetsize = targetcol.bounds.size;
      
        targetedge = targetsize.magnitude;
    }

    protected void GetDamage(int damage,float hidetime)    //敵がダメージを受ける関数
    {
        lifebar.SetActive(true);
        angervalue += 60;
        uimanager.Damagevalue(transform, damage);
        Hp -= damage;
        uimanager.displayeffect(lifebar.GetComponent<Image>(), null, 1.0f);
        Invoke("hidelifebar", hidetime);
    }

        IEnumerator hidelifebar()     　　　　　　　//ライフバーを非表示にするコルーチン
    {
        uimanager.hideeffect(lifebar.GetComponent<Image>(), 1.0f);
        return null;
    }


    protected void Meleeattack(float cooldowntime)  　　　　　//アニメーションイベントから呼び出される攻撃関数
    {
        BoxCollider col =Hand.GetComponentInChildren<BoxCollider>();
       
        StartCoroutine(meleeattack(col, cooldowntime));

    }
    protected void Setlifebar(GameObject bar,float Hp,float MaxHP)
    {
        Image hpbar = bar.GetComponent<Image>();
        hpbar.fillAmount = Hp / MaxHp;
        hpbar.transform.rotation = Camera.main.transform.rotation;
    }

}
    




    

   

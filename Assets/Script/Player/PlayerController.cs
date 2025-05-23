
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Playerの行動を管理するクラス
/// </summary>
public class PlayerController : MonoBehaviour
{
    ObjAnimetor playerAnimetor;
    float movex, movez;

    public bool IsCreate { get; set; }

    private bool IsRun, IsJumping, InGround, IsWalking,IsWalkBack;//状態構造体
    [Header("Player")]
    [SerializeField] private int MaxSpeed, JumpForce;
    [SerializeField]private float acceleration;
    [SerializeField]public int MaxHp { get; private set; } = 100; //敵最大のHP
    [SerializeField]public int Hp { get; set; } = 100;//敵のHP
    [SerializeField] float rayy, raydis;  //Rayの長さ
    Vector3 moveDirection;
    Vector3 lastMoveDirection;
    Vector3 roteuler;
    [SerializeField] float MouseSpeedX;
    [SerializeField] float MouseSpeedY;
   
    
    Plane plane = new Plane();
    float distance = 0;
    bool IsShooting = false;
    Rigidbody rigidbody;
    float vec;
    float maxvec = 5f;
    public float friction = 0.5f;
    
   

    private void Awake()
    {
     playerAnimetor = new ObjAnimetor(1f,gameObject);
     rigidbody = GetComponent<Rigidbody>();

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
    // GameOver();

     if (InGround) 
     movement();

     Jump();
     CheakGround();
     Cameramethod();
     playerAnimetor.Animetor(IsWalkBack, vec, InGround,IsShooting);
    }
    /// <summary>
    /// 行動処理
    /// </summary>
    void movement()
    {

        IsShooting = Input.GetMouseButton(0) && !IsCreate ? true : false;
        float movex = Input.GetAxis("Horizontal");
        float movez = Input.GetAxis("Vertical");

        // 入力方向を取得
        Vector3 moveDirection = new Vector3(movex, 0, movez).normalized;
        acceleration = Mathf.Clamp(acceleration, 0, MaxSpeed);
        if(vec<maxvec)//移動速度制限
        rigidbody.AddForce(moveDirection * acceleration, ForceMode.VelocityChange);
        
        if(rigidbody.linearVelocity.magnitude<0.1f)
            rigidbody.linearVelocity = Vector3.zero;//誤アニメーション防止

        vec = rigidbody.linearVelocity.magnitude;
        Vector3 vetorvec = rigidbody.linearVelocity;

    }
    void Jump()
    {
        if (moveDirection.magnitude > 0)
        {
          lastMoveDirection =transform.forward* moveDirection.z;
          lastMoveDirection += transform.right * moveDirection.x;
        }

        if (Input.GetKeyDown(KeyCode.Space) && InGround)
        {
          GetComponent<Rigidbody>().AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
          GetComponent<Rigidbody>().AddForce(lastMoveDirection * acceleration, ForceMode.Impulse);
          InGround = false;
        }
    }
    void CheakGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position +
            new Vector3(0, rayy, 0), 
            Vector3.down, 
            out hit, raydis))
        {
          InGround = true;
        }
        else
        {
          InGround = false;
        }
        Debug.DrawRay(transform.position +
            new Vector3(0, rayy, 0), 
            Vector3.down * raydis,
            Color.red);

    }
    void Cameramethod()
    {
        // カメラとマウスの位置を元にRayを準備
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // プレイヤーの高さにPlaneを更新して、カメラの情報を元に地面判定して距離を取得
        plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
        if (plane.Raycast(ray, out distance))
        {

            // 距離を元に交点を算出して、交点の方を向く
            var lookPoint = ray.GetPoint(distance);
            transform.LookAt(lookPoint);


        }
    }
    public void GetDamage()
    {
        Hp -= 10;
       
    }
    void GameOver()
    {
        if (Hp <= 0)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}


using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;

/// <summary>
/// Playerの行動を管理するクラス
/// </summary>
public class PlayerController : MonoBehaviour
{
    ObjAnimetor playerAnimetor;
    float movex, movez;

    public bool IsCreate { get; set; }

    private bool IsRun, IsJumping, InGround, IsWalking,IsWalkBack,IsRoll;//状態構造体
    [Header("Player")]
    [SerializeField] private int MaxSpeed, JumpForce;
    [SerializeField]private float acceleration;      //加速度
    [SerializeField]public int MaxHp { get; private set; } = 100; //最大のHP
    [SerializeField]public int Hp { get; set; } = 100;//敵のHP
    [SerializeField] float rayy, raydis;  //Rayの長さ
    Vector3 moveDirection;
    Vector3 lastMoveDirection;  
    Vector3 roteuler;
    [SerializeField] float MouseSpeedX;
    [SerializeField] float MouseSpeedY;
    [SerializeField] GameObject overridesources;
    [SerializeField] GameObject Pin;
    [Header("PInの高さ")]
    [SerializeField]float pinHeight = 50f; //Pinの高さ


    Plane plane = new Plane();
    float distance = 0;
    bool IsShooting = false;
    [SerializeField] EquipSystem equipSystem;
    Rigidbody rigidbody;
    float vec;
    float forwardDot;
    float maxvec = 5f;
    public float friction = 0.5f;
    bool invincible;
  






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
        cheakdirecion();
        PlayerMapPin();


        if (GameManager.GameStop) return; //ゲームが停止している場合は処理を中断
    // GameOver();

     if (InGround&&!IsRoll) 
     movement();
    // PlayerMapPin();
     Jump();
     CheakGround();
     Cameramethod();
     playerAnimetor.Animetor(IsWalkBack, vec, InGround,IsShooting,IsRoll,false,equipSystem.IsReloading);
    }
    void cheakdirecion()
    {
        Vector3 velocity = rigidbody.linearVelocity;
        forwardDot = Vector3.Dot(transform.forward, velocity.normalized);
        if (forwardDot < 0)
        {
            IsWalkBack = true; //後ろに歩いている場合
        }
        else
        {
            IsWalkBack = false; //前に歩いている場合
        }
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
    void PlayerMapPin()
    {
        Vector3 Pinpos;
        Pinpos=Pin.transform.position;
        Pinpos.y = pinHeight; //Pinの高さを設定
        Pinpos.x = transform.position.x; //PinのX座標をプレイヤーのX座標に合わせる
        Pinpos.z = transform.position.z; //PinのZ座標をプレイヤーのZ座標に合わせる
        Pin.transform.position = Pinpos; //Pinの位置を更新
       
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
        if(Input.GetKeyDown(KeyCode.LeftShift) && InGround&&!IsRoll)
        {
           StartCoroutine(Roll());


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
            
            var absrot = overridesources.transform.rotation.y - transform.rotation.y;

            if (IsShooting) overridesources.transform.rotation = transform.rotation * Quaternion.Euler(0, 45, 0);  //射撃中マウスの向きに合わせる
            
            else  if (!IsShooting)
                overridesources.transform.LookAt(lookPoint);
           
            _=WaitForAsync(0.1f,()=>transform.LookAt(lookPoint)); // 0.1秒後にプレイヤーの向きを更新する
            

            


        }
    }
    
     private async Task WaitForAsync(float seconds, Action action)
    {
        await Task.Delay(TimeSpan.FromSeconds(seconds));
        action();
    }
    public void GetDamage()
    {
        if (invincible) return; // 無敵状態ならダメージを受けない
        Hp -= 10;
       
    }
    void GameOver()
    {
        if (Hp <= 0)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
    IEnumerator Roll()
    {
        IsRoll = true;
        invincible = true;
        GetComponent<Rigidbody>().AddForce(lastMoveDirection, ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);
        IsRoll = false;
        invincible = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetDamage();
        }
    }
}


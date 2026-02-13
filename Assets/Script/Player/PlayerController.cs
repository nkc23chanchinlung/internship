
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

    private bool IsRun, IsJumping, InGround, IsWalking,IsWalkBack,IsWalkRight,IsRoll;//状態構造体
    [Header("Player")]
    [SerializeField] private int MaxSpeed, JumpForce;
    [SerializeField]private float acceleration;      //加速度
    [SerializeField]public int MaxHp { get; private set; } = 100; //最大のHP
    [SerializeField]public int Hp { get; set; } = 100;//プレイヤーのHP
    [SerializeField] float rayy, raydis;  //Rayの長さ
    Vector3 moveDirection;
    Vector3 lastMoveDirection;  
    Vector3 roteuler;
    [SerializeField] float MouseSpeedX;
    [SerializeField] float MouseSpeedY;
    [SerializeField] GameObject overridesources;
    [SerializeField] GameObject Pin;
    [SerializeField] AudioSource Footaudio;
    [Header("PInの高さ")]
    [SerializeField]float pinHeight = 50f; //Pinの高さ
    [SerializeField] float animeionspeed;
    [SerializeField]UIManager uimanager;
    [SerializeField] GameObject hitEffect;

    Plane plane = new Plane();
    float distance = 0;
    bool IsShooting = false;
    [SerializeField] EquipSystem equipSystem;
    Rigidbody rigidbody;
    float vec;
    float forwardDot;
    float RightDot;
    float maxvec;
    public float friction = 0.5f;
    bool invincible;
    bool Gethit;

    [SerializeField] private float footseInterval=0.5f;   // 何秒ごとに音出す

    //[SerializeField] Material[] playermat;
    //[SerializeField] Renderer playerrenderer;
    private float nextFootTime = 0f;


    void FootSe()
    {
        if (Time.time >= nextFootTime)
        {
            Footaudio.PlayOneShot(Footaudio.clip);
            nextFootTime = Time.time + footseInterval;
        }
    }

    private void Awake()
    {
     playerAnimetor = new ObjAnimetor(animeionspeed, gameObject);
     rigidbody = GetComponent<Rigidbody>();

    }
    private void FixedUpdate()
    {
        if (GameManager.instance.gameStop) return;
        if (InGround && !IsRoll)
            movement();
        Cameramethod();
    }

    // Update is called once per frame
    void Update()
    {


        if (GameManager.instance.gameStop)
        {
            GameStop();
            rigidbody.linearVelocity = Vector3.zero;

            rigidbody.angularVelocity = Vector3.zero;
            Debug.Log("ゲーム停止中");
            return;
        }
        else
        {
            GameContinue();
        
        }
            cheakdirecion();
        PlayerMapPin();
       


        if (GameManager.instance.gameStop) return; //ゲームが停止している場合は処理を中断
     GameOver();


     Jump();
     CheakGround();
     
        

        playerAnimetor.Animetor(IsWalkBack,RightDot, forwardDot, InGround,IsShooting,IsRoll,false,equipSystem.IsReloading,Gethit);
    }
    void cheakdirecion()
    {
        Vector3 velocity = rigidbody.linearVelocity;
        forwardDot = Vector3.Dot(transform.forward, velocity.normalized);
        
        RightDot= Vector3.Dot(transform.right, velocity.normalized);
        if (rigidbody.linearVelocity.magnitude < 0.1f)
        {

            forwardDot=0f;
            RightDot =0f;


        }

        //誤アニメーション防止
        if (forwardDot < -0.1f)
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
        float movex = -Input.GetAxis("Horizontal");
        float movez = -Input.GetAxis("Vertical");

       
        if (movex != 0 || movez != 0)
        {
            FootSe();
        }

        // 入力方向を取得
        Vector3 moveDirection = new Vector3(movex, 0, movez).normalized;
        acceleration = Mathf.Clamp(acceleration, 0, MaxSpeed);
        if(vec<maxvec)//移動速度制限
        rigidbody.AddForce(moveDirection * acceleration, ForceMode.VelocityChange);

        //後ろ移動したら速度制限を下げる
        maxvec = IsWalkBack==true? 2f:5f;


       


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

            else if (!IsShooting)
                overridesources.transform.LookAt(lookPoint);

            //  _=WaitForAsync(0.1f,()=>transform.LookAt(lookPoint)); // 0.1秒後にプレイヤーの向きを更新する

            transform.LookAt(lookPoint);



        }
    }
    
     private async Task WaitForAsync(float seconds, Action action)
    {
        await Task.Delay(TimeSpan.FromSeconds(seconds));
        action();
    }
    //プレイヤーがダメージを受ける処理
    public void GetDamage(int Dmg)
    {
        if (invincible) return; // 無敵状態ならダメージを受けない
        Hp -= Dmg;
        uimanager.Damagevalue(transform, Dmg,Color.red);

        Gethit = true;

        if (Gethit)
        {
            _ = WaitForAsync(0.2f, () => Gethit = false);
        }

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
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("EnemyAtk"))
        {

            Instantiate(hitEffect, other.transform.position, other.transform.rotation * Quaternion.Euler(90, 0, 0));


            //playerrenderer.material.SetColor("BaseMap", Color.red);

        }
    }
     void GameStop()
    {
       Animator animator = GetComponent<Animator>();
        animator.speed = 0f;


    }
    void GameContinue()
    {
        Animator animator = GetComponent<Animator>();
        animator.speed = 1f;
    }
}


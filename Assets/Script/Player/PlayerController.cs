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
    ObjAnimetor _playerAnimetor;
    float moveX, moveZ;

    public bool IsCreate { get; set; }

    private bool _isRun, _isJumping, _isGround, _isWalking,_isWalkBack,_isWalkRight,_isRoll;//アニメション用フラグ
    [Header("Player")]
    [SerializeField] private int MAX_SPEED, _JUMPFORCE;
    [SerializeField]private float _acceleration;      //加速度
    public int MaxHp { get; private set; } = 100; //最大のHP
    public int Hp { get; set; } = 100;//プレイヤーのHP
    [SerializeField] float _rayY, _rayDis;  //Rayの長さ
    Vector3 _moveDirection;
    Vector3 _lastMoveDirection;  
    Vector3 _roteuler;
    [SerializeField] float _mouseSpeedX;
    [SerializeField] float _mouseSpeedY;
    [SerializeField] GameObject _overrideSources;
    [SerializeField] GameObject _pin;
    [SerializeField] AudioSource _footAudio;
    [Header("PInの高さ")]
    [SerializeField]float _pinHeight = 50f; //Pinの高さ
    [SerializeField] float _animeionSpeed;
    [SerializeField]UIManager _uiManager;
    [SerializeField] GameObject _hitEffect;

    Plane _plane = new Plane();
    float _distance = 0;
    bool _isShooting = false;
    [SerializeField] EquipSystem _equipSystem;
    Rigidbody _rigidBody;
    float _vec;
    float _forwardDot;
    float _rightDot;
    float _maxVec;
    public float Friction = 0.5f;
    bool _invincible;
    bool _getHit;

    [SerializeField] private float _footseInterval=0.5f;   // 何秒ごとに音出す

    //[SerializeField] Material[] playermat;
    //[SerializeField] Renderer playerrenderer;
    private float nextFootTime = 0f;


    void FootSe()
    {
        if (Time.time >= nextFootTime)
        {
            _footAudio.PlayOneShot(_footAudio.clip);
            nextFootTime = Time.time + _footseInterval;
        }
    }

    private void Awake()
    {
     _playerAnimetor = new ObjAnimetor(_animeionSpeed, gameObject);
     _rigidBody = GetComponent<Rigidbody>();

    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.GameStop) return;
        if (_isGround && !_isRoll)
            movement();
        Cameramethod();
    }

    // Update is called once per frame
    void Update()
    {


        if (GameManager.Instance.GameStop)
        {
            GameStop();
            _rigidBody.linearVelocity = Vector3.zero;

            _rigidBody.angularVelocity = Vector3.zero;
            Debug.Log("ゲーム停止中");
            return;
        }
        else
        {
            GameContinue();
        
        }
            cheakdirecion();
        PlayerMapPin();
       


        if (GameManager.Instance.GameStop) return; //ゲームが停止している場合は処理を中断
     GameOver();


     Jump();
     CheakGround();
     
        

        _playerAnimetor.Animetor(_isWalkBack,_rightDot, _forwardDot, _isGround,_isShooting,_isRoll,false,_equipSystem.IsReloading,_getHit);
    }
    void cheakdirecion()
    {
        Vector3 velocity = _rigidBody.linearVelocity;
        _forwardDot = Vector3.Dot(transform.forward, velocity.normalized);
        
        _rightDot= Vector3.Dot(transform.right, velocity.normalized);
        if (_rigidBody.linearVelocity.magnitude < 0.1f)
        {

            _forwardDot=0f;
            _rightDot =0f;


        }

        //誤アニメーション防止
        if (_forwardDot < -0.1f)
        {
            _isWalkBack = true; //後ろに歩いている場合
        }
        else
        {
            _isWalkBack = false; //前に歩いている場合
        }
        
    }
    /// <summary>
    /// 行動処理
    /// </summary>
    void movement()
    {
        
        

        _isShooting = Input.GetMouseButton(0) && !IsCreate ? true : false;
        float movex = -Input.GetAxis("Horizontal");
        float movez = -Input.GetAxis("Vertical");

       
        if (movex != 0 || movez != 0)
        {
            FootSe();
        }

        // 入力方向を取得
        Vector3 moveDirection = new Vector3(movex, 0, movez).normalized;
        _acceleration = Mathf.Clamp(_acceleration, 0, MAX_SPEED);
        if(_vec<_maxVec)//移動速度制限
        _rigidBody.AddForce(moveDirection * _acceleration, ForceMode.VelocityChange);

        //後ろ移動したら速度制限を下げる
        _maxVec = _isWalkBack==true? 2f:5f;


       


        _vec = _rigidBody.linearVelocity.magnitude;
        Vector3 vetorvec = _rigidBody.linearVelocity;

    }
    void PlayerMapPin()
    {
        Vector3 Pinpos;
        Pinpos=_pin.transform.position;
        Pinpos.y = _pinHeight; //Pinの高さを設定
        Pinpos.x = transform.position.x; //PinのX座標をプレイヤーのX座標に合わせる
        Pinpos.z = transform.position.z; //PinのZ座標をプレイヤーのZ座標に合わせる
        _pin.transform.position = Pinpos; //Pinの位置を更新
       
    }
    void Jump()
    {
        if (_moveDirection.magnitude > 0)
        {
          _lastMoveDirection =transform.forward* _moveDirection.z;
          _lastMoveDirection += transform.right * _moveDirection.x;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
        {
          GetComponent<Rigidbody>().AddForce(Vector3.up * _JUMPFORCE, ForceMode.Impulse);
          GetComponent<Rigidbody>().AddForce(_lastMoveDirection * _acceleration, ForceMode.Impulse);
          _isGround = false;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && _isGround&&!_isRoll)
        {
            
            StartCoroutine(Roll());


        }
       
    }
    void CheakGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position +
              new Vector3(0, _rayY, 0), 
            Vector3.down, 
            out hit, _rayDis))
        {
          _isGround = true;
        }
        else
        {
          _isGround = false;
        }
        Debug.DrawRay(transform.position +
            new Vector3(0, _rayY, 0), 
            Vector3.down * _rayDis,
            Color.red);

    }
    void Cameramethod()
    {
        // カメラとマウスの位置を元にRayを準備
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // プレイヤーの高さにPlaneを更新して、カメラの情報を元に地面判定して距離を取得
        _plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
        if (_plane.Raycast(ray, out _distance))
        {
           
            // 距離を元に交点を算出して、交点の方を向く
            var lookPoint = ray.GetPoint(_distance);
            
            var absrot = _overrideSources.transform.rotation.y - transform.rotation.y;

            if (_isShooting) _overrideSources.transform.rotation = transform.rotation * Quaternion.Euler(0, 45, 0);  //射撃中マウスの向きに合わせる

            else if (!_isShooting)
                _overrideSources.transform.LookAt(lookPoint);

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
        if (_invincible) return; // 無敵状態ならダメージを受けない
        Hp -= Dmg;
        _uiManager.DamageValue(transform, Dmg,Color.red);

        _getHit = true;

        if (_getHit)
        {
            _ = WaitForAsync(0.2f, () => _getHit = false);
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
        _isRoll = true;
        _invincible = true;
        GetComponent<Rigidbody>().AddForce(_lastMoveDirection, ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);
        _isRoll = false;
        _invincible = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "EnemyAtk")
        {
            Instantiate(_hitEffect, other.transform.position, other.transform.rotation * Quaternion.Euler(90, 0, 0));
            Bullet _bullet = other.gameObject.GetComponent<Bullet>();

            if (_bullet != null)
            {
                int damage = _bullet.damage;

                GetDamage(damage);
                Destroy(other.gameObject);
                
               
            }
            //gethit = true;


        }

        //if (other.gameObject.CompareTag("EnemyAtk"))
        //{

        //    Instantiate(_hitEffect, other.transform.position, other.transform.rotation * Quaternion.Euler(90, 0, 0));


        //    //playerrenderer.material.SetColor("BaseMap", Color.red);

        //}
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


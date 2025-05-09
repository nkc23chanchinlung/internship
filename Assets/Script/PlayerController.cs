using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    ObjAnimetor playerAnimetor;
    float movex, movez;

    
    private bool IsRun, IsJumping, InGround, IsWalking,IsWalkBack;
    [Header("Player")]
    [SerializeField] private int MaxSpeed, JumpForce;
    private float speed;
    [SerializeField] float rayy, raydis;
    Vector3 moveDirection;
    Vector3 lastMoveDirection;
    Vector3 roteuler;
    [SerializeField] float MouseSpeedX;
    [SerializeField] float MouseSpeedY;
    private float Mincam = -30f;
    private float Maxcam = 60f;
    [SerializeField] Camera cam;
    Plane plane = new Plane();
    float distance = 0;
    public float _speed
    {
        get
        {
            return speed;
        }

    }


    private void Awake()
    {
        playerAnimetor = new ObjAnimetor(1f,gameObject); 
       
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
       
    }

    // Update is called once per frame
    void Update()
    {
     if(InGround) movement();
        Jump();
        CheakGround();
        Cameramethod();
        playerAnimetor.Animetor(IsWalkBack, speed, InGround);
    }
    void movement()
    {
#if false
        float movex = Input.GetAxis("Horizontal");
        float movez = Input.GetAxis("Vertical");

        // 入力方向を取得
        Vector3 moveDirection = new Vector3(movex, 0, movez).normalized;

        // キーが押されているなら、その方向を保存
        if (moveDirection.magnitude > 0)
        {
            lastMoveDirection = moveDirection;
        }

        // 速度の制限
        speed = Mathf.Clamp(speed, 0, MaxSpeed);
        if (speed < 0.1f) speed = 0;




        // 位置を更新（キーを離しても lastMoveDirection で動き続ける）
        transform.localPosition += lastMoveDirection * speed * Time.deltaTime;

        // 移動がある場合、加速
        if (movex != 0 || movez != 0)
        {
            speed += 0.1f;
            IsWalking = true;
        }
        else
        {
            // 徐々に減速（滑らかに止まる）
            speed *= 0.87f;
            IsWalking = false;
        }
#endif
        float movex = Input.GetAxis("Horizontal");
        float movez = Input.GetAxis("Vertical");
        moveDirection = new Vector3(movex, 0, movez).normalized;
        
        
        if (moveDirection.magnitude > 0)
        {
            lastMoveDirection = transform.forward * moveDirection.z;
            lastMoveDirection += transform.right * moveDirection.x;
        }
        transform.localPosition += lastMoveDirection * speed * Time.deltaTime;
        // 速度の制限
        speed = Mathf.Clamp(speed, 0, MaxSpeed);
        if (speed < 0.1f) speed = 0;

        if (movex != 0 || movez != 0)
        {
            speed += 0.1f;
            IsWalking = true;
        }
        else
        {
            // 徐々に減速（滑らかに止まる）
            speed *= 0.87f;
            IsWalking = false;
        }
        IsWalkBack = movez < 0 ? true:false;

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
            GetComponent<Rigidbody>().AddForce(lastMoveDirection * speed, ForceMode.Impulse);
            InGround = false;
        }
    }
    void CheakGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0, rayy, 0), Vector3.down, out hit, raydis))
        {
            InGround = true;
        }
        else
        {
            InGround = false;
        }
        Debug.DrawRay(transform.position + new Vector3(0, rayy, 0), Vector3.down * raydis, Color.red);

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
}

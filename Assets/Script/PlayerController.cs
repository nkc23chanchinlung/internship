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

        // ���͕������擾
        Vector3 moveDirection = new Vector3(movex, 0, movez).normalized;

        // �L�[��������Ă���Ȃ�A���̕�����ۑ�
        if (moveDirection.magnitude > 0)
        {
            lastMoveDirection = moveDirection;
        }

        // ���x�̐���
        speed = Mathf.Clamp(speed, 0, MaxSpeed);
        if (speed < 0.1f) speed = 0;




        // �ʒu���X�V�i�L�[�𗣂��Ă� lastMoveDirection �œ���������j
        transform.localPosition += lastMoveDirection * speed * Time.deltaTime;

        // �ړ�������ꍇ�A����
        if (movex != 0 || movez != 0)
        {
            speed += 0.1f;
            IsWalking = true;
        }
        else
        {
            // ���X�Ɍ����i���炩�Ɏ~�܂�j
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
        // ���x�̐���
        speed = Mathf.Clamp(speed, 0, MaxSpeed);
        if (speed < 0.1f) speed = 0;

        if (movex != 0 || movez != 0)
        {
            speed += 0.1f;
            IsWalking = true;
        }
        else
        {
            // ���X�Ɍ����i���炩�Ɏ~�܂�j
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
        // �J�����ƃ}�E�X�̈ʒu������Ray������
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // �v���C���[�̍�����Plane���X�V���āA�J�����̏������ɒn�ʔ��肵�ċ������擾
        plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
        if (plane.Raycast(ray, out distance))
        {

            // ���������Ɍ�_���Z�o���āA��_�̕�������
            var lookPoint = ray.GetPoint(distance);
            transform.LookAt(lookPoint);


        }
    }
}

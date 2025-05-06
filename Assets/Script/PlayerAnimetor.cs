using UnityEngine;


public class PlayerAnimetor : MonoBehaviour
{
    public float animSpeed;
    private Animator anim;
    [SerializeField] PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("WalkBack", playerController._iswalkback);
        anim.SetFloat("speed", playerController._speed / 5);
        anim.SetBool("Jump", !playerController._inGround);
        anim.speed = animSpeed;
    }
    private void FixedUpdate()
    {
       
    }
}

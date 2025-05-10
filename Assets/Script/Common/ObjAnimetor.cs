using UnityEngine;

/// <summary>
/// Player�A�j���[�V�����N���X, WalkBack,Jump,Speed��3�̃p�����[�^������
/// </summary>
public class ObjAnimetor : MonoBehaviour
{
    private float animSpeed;
    private Animator anim;

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /// <summary>
    /// �A�j���[�V����������
    /// </summary>
    /// <param name="animspeed">�A�j���[�V�����Đ����x</param>
    /// <param name="Obj">�A�j���[�V�������s�I�u�W�F�N�g</param>
   public ObjAnimetor(float animspeed,GameObject Obj)
    {
        animSpeed = animspeed;
        anim =Obj.GetComponent<Animator>();

    }
    /// <summary>
    /// �A�j���[�V�����̎��s
    /// </summary>
    /// <param name="iswalkback"></param>
    /// <param name="speed"></param>
    /// <param name="inGround"></param>
    public void Animetor(bool iswalkback, float speed, bool inGround, bool Shooting)
    {
        
        anim.SetBool("WalkBack", iswalkback);
        anim.SetFloat("speed", speed / 5);
        anim.SetBool("Jump", !inGround);
        anim.SetBool("Shooting",Shooting );
        anim.speed = animSpeed;
    }
}

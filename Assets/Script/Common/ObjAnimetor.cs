using UnityEngine;

/// <summary>
/// Playerアニメーションクラス, WalkBack,Jump,Speedの3つのパラメータを持つ
/// </summary>
public class ObjAnimetor : MonoBehaviour
{
    private float animSpeed;
    private Animator anim;

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /// <summary>
    /// アニメーション初期化
    /// </summary>
    /// <param name="animspeed">アニメーション再生速度</param>
    /// <param name="Obj">アニメーション実行オブジェクト</param>
   public ObjAnimetor(float animspeed, UnityEngine.GameObject Obj)
    {
        animSpeed = animspeed;
        anim =Obj.GetComponent<Animator>();

    }
    /// <summary>
    /// アニメーションの実行
    /// </summary>
    /// <param name="iswalkback"></param>
    /// <param name="speed"></param>
    /// <param name="inGround"></param>
    public virtual void Animetor(bool iswalkback,float iswalkright, float speed, bool inGround, bool Shooting,bool isroll,bool atk,bool reloading)
    {
        
        anim.SetBool("WalkBack", iswalkback);
        anim.SetFloat("WalkRight", iswalkright);
        anim.SetFloat("Speed", speed / 5);
        anim.SetBool("Jump", !inGround);
        anim.SetBool("Shooting",Shooting );
        anim.speed = animSpeed;
        anim.SetBool("IsRoll", isroll);
        anim.SetBool("Atk", atk);
        anim.SetBool("reloading", reloading);
    }
    /// <summary>
    /// アニメーション制御オーバーロード
    /// </summary>
    /// <param name="iswalkback"></param>
    /// <param name="speed"></param>
    /// <param name="inGround"></param>
    /// <param name="Shooting"></param>
    /// <param name="atk"></param>
    public void Animetor(bool iswalkback, bool inGround, bool Shooting, bool atk, bool Idle, float speed) 
    { 
        anim.SetBool("WalkBack", iswalkback);
        anim.SetFloat("Speed", speed / 5);
        anim.SetBool("Jump", !inGround);
        anim.SetBool("Shooting", Shooting);
        anim.SetBool("Idle", Idle);
        anim.speed = animSpeed;
        anim.SetBool("Atk", atk);
        
    }
}

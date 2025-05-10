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
   public ObjAnimetor(float animspeed,GameObject Obj)
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
    public void Animetor(bool iswalkback, float speed, bool inGround, bool Shooting)
    {
        
        anim.SetBool("WalkBack", iswalkback);
        anim.SetFloat("speed", speed / 5);
        anim.SetBool("Jump", !inGround);
        anim.SetBool("Shooting",Shooting );
        anim.speed = animSpeed;
    }
}

using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //敵の行動関する基底クラス
    public bool atking = false;
    public bool shooting = false;   
    public bool meleeing = false;   


    /// <summary>
    /// 敵の射撃関数
    /// </summary>
    /// <param name="cooldowntime">クールダウン</param>
    /// <param name="bullet">撃つオブジェクト</param>
    /// <returns></returns>
    public virtual IEnumerator Shoot(GameObject bullet,float cooldowntime)
    {
        atking = true;
        Instantiate(
           bullet,
            transform.position + transform.forward,
            transform.rotation
        );
        yield return new WaitForSeconds(cooldowntime);
        atking = false;
    }
    //Shootのオーバーロード
    /// <summary>
    public IEnumerator Shoot(GameObject bullet, float cooldowntime, GameObject obj, Vector3 pos, string tag)
    {
        GameObject bulletpre =
          Instantiate(
           bullet,
           obj.transform.position + transform.forward + pos,
           transform.rotation
        );

        bulletpre.tag = tag;

        yield return new WaitForSeconds(cooldowntime);
    }
    /// <summary>
    /// 近接攻撃のコルーチン
    /// </summary>
    /// <param name="col">攻撃判定</param>
    /// <param name="cooldowntime">クールダウン</param>
    /// <returns></returns>
    public IEnumerator meleeattack(BoxCollider col,float cooldowntime)
    {
        meleeing = true;
        col.enabled = true;
        //yield return new WaitForSeconds(cooldowntime);
        yield return null;
        //if(col.enabled== true)
        //col.enabled = false;
        //meleeing = false;
        //yield return null;

        //yield return new WaitForSeconds(cooldowntime);


    }
}


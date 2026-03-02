using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //敵の行動関する基底クラス
    public bool atking = false;
    public bool shooting = false;   //遠距離攻撃敵が使う
    public bool meleeing = false;   //近距離攻撃敵が使う


    /// <summary>
    /// 敵の射撃関数
    /// </summary>
    /// <param name="cooldowntime">クールダウン</param>
    /// <param name="bullet">撃つオブジェクト</param>
    /// <returns></returns>
    public virtual IEnumerator Shoot(GameObject bullet,int Damage,float cooldowntime,Vector3 PosAdjust,AudioSource Se)
    {
        if (!shooting)
        {
            shooting = true;
            Debug.Log("Shoot");
           var bulletPre =Instantiate(
           bullet,
            transform.position + (transform.forward * 2) + PosAdjust, transform.rotation
        );
          bulletPre.GetComponent<Bullet>().damage = Damage;
            bulletPre.gameObject.tag = "EnemyAtk";

            Se.PlayOneShot(Se.clip);
            

            yield return new WaitForSeconds(cooldowntime);
            shooting = false;
        }
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
        //col.enabled = true;
        yield return new WaitForSeconds(cooldowntime);
        //if (col.enabled == true)
        //    col.enabled = false;
        meleeing = false;
        yield return new WaitForSeconds(cooldowntime);


    }
}


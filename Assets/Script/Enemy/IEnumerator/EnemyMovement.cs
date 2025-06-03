using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //敵の行動関する基底クラス
    public bool atking = false;
    

    /// <summary>
    /// 敵の射撃関数
    /// </summary>
    /// <param name="cooldowntime">クールダウン</param>
    /// <param name="bullet">撃つオブジェクト</param>
    /// <returns></returns>
    public IEnumerator Shoot(GameObject bullet,float cooldowntime)
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
    /// <summary>
    /// 近接攻撃のコルーチン
    /// </summary>
    /// <param name="col">攻撃判定</param>
    /// <param name="cooldowntime">クールダウン</param>
    /// <returns></returns>
    public IEnumerator meleeattack(BoxCollider col,float cooldowntime)
    {
        
        atking = true;
        col.enabled = true;
        yield return new WaitForSeconds(cooldowntime);
        col.enabled = false;
        atking = false;
        

    }
}


using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //敵の行動関する基底クラス
    public bool cooldown = false;

    /// <summary>
    /// 敵の射撃関数
    /// </summary>
    /// <param name="cooldowntime">クールダウン</param>
    /// <param name="bullet">撃つオブジェクト</param>
    /// <returns></returns>
    public IEnumerator Shoot(float cooldowntime,GameObject bullet)
    {
        cooldown = true;
        Instantiate(
           bullet,
            transform.position + transform.forward,
            transform.rotation
        );
        yield return new WaitForSeconds(cooldowntime);
        cooldown = false;
    }
}
public interface IEnemyMovement
{
    void GetDamage(int damage);
}

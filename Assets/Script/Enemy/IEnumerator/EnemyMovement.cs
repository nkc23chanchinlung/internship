using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //�G�̍s���ւ�����N���X
    public bool cooldown = false;

    /// <summary>
    /// �G�̎ˌ��֐�
    /// </summary>
    /// <param name="cooldowntime">�N�[���_�E��</param>
    /// <param name="bullet">���I�u�W�F�N�g</param>
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

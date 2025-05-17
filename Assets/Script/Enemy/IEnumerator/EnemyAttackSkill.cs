using System.Collections;
using UnityEngine;

public class EnemyAttackSkill : MonoBehaviour
{
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

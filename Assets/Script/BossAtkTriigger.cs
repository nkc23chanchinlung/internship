using UnityEngine;

public class BossAtkTriigger : MonoBehaviour
{
    int Atk;
    Boss boss;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        boss = gameObject.GetComponentInParent<Boss>();
        Atk = boss.Attack;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (boss.hasHit) return;
        if (other.tag == "Player")
        {
            boss.hasHit = true;
            PlayerController player = other.GetComponent<PlayerController>();
            player.GetDamage(Atk); // プレイヤーにAtkのダメージを与える
            Debug.Log("BossAtkCol Hit Player");
            BoxCollider collider = GetComponent<BoxCollider>();

            collider.enabled = false; // 攻撃が当たった後、コライダーを無効にする
        }
    }

}

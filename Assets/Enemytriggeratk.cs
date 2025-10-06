using UnityEngine;

public class Enemytriggeratk : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.GetDamage(); // プレイヤーに10のダメージを与える
            BoxCollider collider = GetComponent<BoxCollider>();
            collider.enabled = false; // 攻撃が当たった後、コライダーを無効にする
        }
    }
}

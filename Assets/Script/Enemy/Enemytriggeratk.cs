using Unity.VisualScripting;
using UnityEngine;

public class Enemytriggeratk : MonoBehaviour
{
    int Atk;
    Swordmen swordmen;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        GameManager.OnGameStart += OnGameStart;
    }

    void OnDisable()
    {
        GameManager.OnGameStart -= OnGameStart;
    }

    void OnGameStart()
    {
        swordmen = gameObject.GetComponentInParent<Swordmen>();
        Atk = swordmen.Attack;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        
        if (other.tag == "Player")
        {
            Debug.Log("Enemy Attack Hit Player");
            //swordmen.hasHit = true;
            //PlayerController player = other.GetComponent<PlayerController>();
            //player.GetDamage(Atk); // プレイヤーにAtkのダメージを与える
            //BoxCollider collider = GetComponent<BoxCollider>();
            //collider.enabled = false; // 攻撃が当たった後、コライダーを無効にする

        }
    }
}

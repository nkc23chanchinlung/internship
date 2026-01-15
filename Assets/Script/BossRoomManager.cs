using UnityEngine;

public class BossRoomManager : MonoBehaviour
{
    public int enemyCount { get; set; } = 0;


    private void Update()
    {
        bossreturn();
    }

    void bossreturn()
    {
        if (enemyCount <= 0)
        {
            //ボスルームクリア処理
            Debug.Log("Boss Room Clear!");
        }
    }
}

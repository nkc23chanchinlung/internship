using UnityEngine;

public class BossRoomManager : MonoBehaviour
{
    public int enemyCount { get; set; } = 0;
    [SerializeField] GameObject bossobj;
    Boss boss;  //Bossスクリプトへの参照(そちらに設定した)


    private void Awake()
    {
        boss = bossobj.GetComponent<Boss>();
    }

    private void Update()
    {
        bossreturn();
    }

    void bossreturn()
    {
        if (enemyCount <= 0&& !boss.wave2clear&&boss.wave2flag==true)
        {
            //ボスルームクリア処理
            Debug.Log("Boss Room Clear!");
            bossobj.SetActive(true);
            boss.wave2clear = true;
        }
    }
}

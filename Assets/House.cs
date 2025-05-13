using UnityEngine;


public class House : MonoBehaviour
{
    public int MaxHp { get; } = 100; //家のHPの最大値
    public int Hp { get; set; } //家のHP
   
         // Start is called once before the first execution of Update after the MonoBehaviour is created
void Start()
    {
        Hp = MaxHp; //家のHPを最大値で初期化
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("House HP: " + Hp);
    }
    void GameOver()
    {
       if(Hp <= 0)
        {
            // ゲームオーバー処理
            Debug.Log("Game Over");
            // ここにゲームオーバーの処理を追加
        }
    }
    public void GetDamage(int damage)
    {
        Hp -= damage;
    }
}

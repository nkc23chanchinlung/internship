using UnityEngine;


public class House : MonoBehaviour
{
    public int MaxHp { get; } = 1000; //�Ƃ�HP�̍ő�l
    public int Hp { get; set; } //�Ƃ�HP
   
         // Start is called once before the first execution of Update after the MonoBehaviour is created
void Start()
    {
        Hp = MaxHp; //�Ƃ�HP���ő�l�ŏ�����
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void GameOver()
    {
       if(Hp <= 0)
        {
            // �Q�[���I�[�o�[����
            Debug.Log("Game Over");
            // �����ɃQ�[���I�[�o�[�̏�����ǉ�
        }
    }
    public void GetDamage(int damage)
    {
        Hp -= damage;
    }
}

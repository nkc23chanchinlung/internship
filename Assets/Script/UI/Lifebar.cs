using UnityEngine;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour
{
     Image lifebar;
    EnemyController enemyController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyController=GetComponentInParent<EnemyController>();
        lifebar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        lifebar.fillAmount = (float)enemyController.Hp / enemyController.MaxHp;
        transform.rotation = Camera.main.transform.rotation;
    }
}

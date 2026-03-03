using UnityEngine;

public class Wall : MonoBehaviour
{
   [SerializeField] Transform _playerPos;
    Material _wallMat;
    void OnEnable()
    {

        GameManager.OnGameStart += OnGameStart;
    }
    void OnDisable()
    {
        GameManager.OnGameStart -= OnGameStart;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnGameStart()
    {
         _wallMat =gameObject. GetComponent<Renderer>().material;
        _playerPos = GameObject.FindGameObjectWithTag("Player").transform;

    }
    

    // Update is called once per frame
    void Update()
    {
        if(_playerPos == null) return;
        float alpha;
        float distance = Vector3.Distance(transform.position, _playerPos.position);
        
        alpha = Mathf.Clamp(distance, 125, 255); // 距離に応じて透明度を計算
        if (distance <= 15) alpha-=0.5f;
        else alpha+=0.5f;
            _wallMat.color = new Color(1, 1, 1, alpha / 255f); // プレイヤーからの距離に応じて壁の透明度を変える
    }
}

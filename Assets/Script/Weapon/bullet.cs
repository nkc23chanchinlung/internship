using UnityEngine;

/// <summary>
/// ’e‚ÌŠî’êƒNƒ‰ƒX
/// </summary>
public class Bullet : MonoBehaviour
{
    int speed=20;
    public float damage { get; set; }
    [SerializeField] GameObject _vfx;
    [SerializeField] AudioClip _struckSE;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        _vfx = Resources.Load<GameObject>("Effect/Sparkles Handler");
        if (_vfx == null) Debug.LogError("vfx is null");
        Debug.Log("bulletdmg:" + damage);
        
        Destroy(gameObject, 3f);
        _struckSE = Resources.Load<AudioClip>("Sound/SE/struckSE");
    }

    // Update is called once per frame
    void Update()
    {
            Bulletprocess();
    }
    public void Bulletprocess()
    {
            transform.position += transform.forward * speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag== "Obstacles")
        {
            Instantiate(_vfx, transform.position, transform.rotation* Quaternion.Euler(new Vector3(0,180,0)));
            AudioManager.Instance.PlaySE(_struckSE);
            Destroy(this.gameObject);
        }
    }


}

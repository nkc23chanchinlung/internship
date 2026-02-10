using UnityEngine;

//âπê∫ä÷òAä«óùÉNÉâÉX
public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;

    [SerializeField]AudioSource BGMPlayer;
    [SerializeField]AudioSource SEPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void FixedUpdate()
    {
        SEPlayer = GameObject.FindGameObjectWithTag("SEPlayer").GetComponent<AudioSource>();
    }
    public void PlayBGM(AudioClip Clip)
    {
        BGMPlayer = GameObject.FindGameObjectWithTag("BGMPlayer").GetComponent<AudioSource>();
        BGMPlayer.clip = Clip;
    }
    public void PlaySE(AudioClip Clip)
    {
        BGMPlayer = GameObject.FindGameObjectWithTag("BGMPlayer").GetComponent<AudioSource>();
        SEPlayer.PlayOneShot(Clip);
    }
    void CheakGameManagerExist()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }
}

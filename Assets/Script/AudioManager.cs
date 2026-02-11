using UnityEngine;

//âπê∫ä÷òAä«óùÉNÉâÉX
public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;

    [SerializeField]public AudioSource BGMPlayer { get;private set; }
    [SerializeField]public AudioSource SEPlayer { get;private set; }

    [SerializeField] AudioClip[] BGMCliplist = { };
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void FixedUpdate()
    {
        CheakGameManagerExist();
        
        //SEPlayer = GameObject.FindGameObjectWithTag("SEPlayer").GetComponent<AudioSource>();
    }
    public void PlayBGM(AudioClip Clip)
    {
        BGMPlayer = GameObject.FindGameObjectWithTag("BGMPlayer").GetComponent<AudioSource>();
        BGMPlayer.clip = Clip;
    }
    public void PlaySE(AudioClip Clip)
    {
        SEPlayer = GameObject.FindGameObjectWithTag("SEPlayer").GetComponent<AudioSource>();
        SEPlayer.PlayOneShot(Clip);
    }
   
   private void CheakGameManagerExist()
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

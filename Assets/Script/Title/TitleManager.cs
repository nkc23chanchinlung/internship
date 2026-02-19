using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    GameManager _gameManager;
    [SerializeField]Image _titleFrame;
    GameObject titleFrameObj;
    [Header("Alpha")]
    [Tooltip("0-100")]
    [Range(0, 100)]
    [SerializeField]float alpha = 100;

    public bool _IsStart { get; set; } = false;
    [SerializeField]Loading _loadingScript;
    bool _isOnce = false;
  
    //ボタン
    [Header("Button")]
    [SerializeField]
    GameObject[] _buttonList = { };
    [SerializeField]
    Text[] _buttonTextList = { };
    [SerializeField]
    Image[] _buttonImageList = { };
    [SerializeField]
    GameObject[] selectionimg = { };

    [SerializeField] Texture2D cursor;

    //音制御
    [Header("Audio")]
    [SerializeField] AudioManager _audioManager;
    [SerializeField] AudioSource _bgmAudio;
    [SerializeField] AudioClip _titleBGM;
    [SerializeField] AudioClip _seClip;
    [SerializeField] Text _bgmVolueText;
    [SerializeField] Text _seVolueText;
    [SerializeField] Slider _bgmVolueSilder;
    [SerializeField] Slider _seVolueSilder;
    [SerializeField] Sprite _mute;
    [SerializeField] Sprite _unMute;
    [SerializeField] Image[] _volueImageList;
    AudioSource _seAudio;

    //ボタン番号
    int _butNum = 0;
    // ボタンの当たり判定用画像サイズ
    int _imgSizeX = 300;
    int _imgSizeY = 60;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _seAudio = GetComponent<AudioSource>();
        titleFrameObj = _titleFrame.gameObject;
        titleFrameObj.SetActive(false);
        _gameManager = GameManager.instance;
        _audioManager.PlayBGM("TitleScene");
        _bgmAudio = GameObject.FindGameObjectWithTag("BGMPlayer").GetComponent<AudioSource>();
      
            
    }

    // Update is called once per frame
    void Update()
    {
        TitleMos();
        SoundControl();

        _titleFrame.color = new Color(0, 0, 0, alpha / 100);

        if (alpha >=0&&!_IsStart)
        {
            TitleStart_EF();
        }
       
        if (_IsStart)
        {
            GameStart_EF("GameScene");
        }

    }

    //音量処理メソッド
    void SoundControl()
    {
        _audioManager.bgmVolume = _bgmVolueSilder.value;
        _audioManager.seVolume = _seVolueSilder.value;
        _bgmAudio.volume = _audioManager.bgmVolume;
        _seAudio.volume = _audioManager.seVolume;
        _bgmVolueText.text = ((int)(_audioManager.bgmVolume * 100)).ToString();
        _seVolueText.text = ((int)(_audioManager.seVolume * 100)).ToString();



        //音量Imgを切り替え
        _volueImageList[0].sprite = _audioManager.bgmVolume <= 0 ? _mute : _unMute;
        _volueImageList[1].sprite = _audioManager.seVolume <= 0 ? _mute : _unMute;

    }

    //ゲーム開始のエフェクト
    public void GameStart_EF(string SceneName)
    {
        titleFrameObj.SetActive(true);
        alpha += 2;
        if (alpha >= 100)
        {
            if (!_isOnce)
            {
                _loadingScript.NextScene(SceneName);
                _isOnce = true;
            }
        }
    }

    //タイトル画面入るエフェクト
    void TitleStart_EF()
    {
        titleFrameObj.SetActive(true);
        alpha -= 2;

        if (alpha <= 30)
        {
            alpha = 0;
            titleFrameObj.SetActive(false);
        }
    }

    /// <summary>
    /// マウスで操作するときの関数
    /// </summary>
    public void TitleMos()
    {
        Vector3 mousepos = Input.mousePosition;
        selectionimg[_butNum].SetActive(true);

        //ボタンの当たり判定
        for (int i = 0; i < _buttonList.Length; i++)
        {
            if (i != _butNum) selectionimg[i].SetActive(false);
            var buttonpos = _buttonList[i].transform.position;
            if (mousepos.x > buttonpos.x - _imgSizeX / 2
                && mousepos.x < buttonpos.x + _imgSizeX / 2
                && mousepos.y > buttonpos.y - _imgSizeY / 2
                && mousepos.y < buttonpos.y + _imgSizeY / 2)
            {
                if (_butNum != i)
                {
                    Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);

                    AudioManager.Instance.PlaySE(_seClip);
                    _seAudio.PlayOneShot(_seClip);
                    _butNum = i;
                }
            }
            else
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
        }
    }
}

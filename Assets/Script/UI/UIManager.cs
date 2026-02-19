using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal; // URP用

/// <summary>
/// UIを管理するクラス
/// </summary>
[System.Serializable]
public class UIManager :UIEffect
{
    

    [Header("UI")]
    [Header("GameObject")]
    [SerializeField] GameObject _gameCanvas;
   
    [SerializeField] GameObject _menuPanel;
    [SerializeField] GameObject _storyPanel;
    [SerializeField] GameObject _mapPanel;
    [SerializeField] GameObject _damageValuePrefeb;
    [SerializeField] GameObject _coinPrefeb;
    [SerializeField] GameObject _coinUI;
    [SerializeField] GameObject _statusUI;
    [SerializeField] GameObject _miniMap;
    [SerializeField] GameObject _minMapUi;
    //使わない
    [SerializeField] GameObject StorePanel;
    [SerializeField] GameObject WeaponPanel;
    [SerializeField]Volume GlobalVolume;
    private ColorAdjustments colorAdjustments;

    [Header("Image")]
    [SerializeField] Image _bossHpBar;
    [SerializeField] Image _fade;
    [SerializeField]Image _lifeBar;
    [SerializeField]Image _bossFrame;
    [SerializeField]Image _warningImage;


    [Header("Text")]
    [SerializeField] Text _reloadingText;
    [SerializeField] EquipSystem _equipSystem;
    [SerializeField] Text _magazineText;
    [SerializeField] Text _coinText;
    [SerializeField] Text _lifeBarText;
    [SerializeField] Text _percent;
    Image Magazine_Image;
    [SerializeField]PlayerController _playerController;
    
    House house;
    GameManager gameManager;
    bool PanelOpen = false;
    float displaylife=100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        GameManager.OnGameStart += OnGameStart;
    }
    private void OnDisable()
    {
        GameManager.OnGameStart -= OnGameStart;
    }
    void OnGameStart()
    {
        _playerController =GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
        _coinUI.SetActive(true);
        _statusUI.SetActive(true);
        _miniMap.SetActive(true);


    }
    private void Start()
    {
        StorePanel.SetActive(false);
        blinkinge_effect(_reloadingText);
        _mapPanel.transform.localScale = Vector3.zero;
        _menuPanel.transform.localScale = Vector3.zero;
        hideeffect(_fade, 1f);  　　　　　　　　　　　　　//Fade処理
        gameManager = GameManager.instance;
        _minMapUi.transform.DORotate(Vector3.forward * 30, 1f).SetEase(Ease.Linear).SetLoops(-1,LoopType.Incremental);
    }
    // Update is called once per frame
    void Update()
    {
       if(_playerController == null) return;
        GameStop();
        _playerController.IsCreate = PanelOpen;

        if (Input.GetKeyDown(KeyCode.B)&&!PanelOpen)
        {

            Panel_Open(StorePanel);

            if (WeaponPanel.activeSelf)
            {
                WeaponPanel.SetActive(false);
            }
        }
        else if (PanelOpen && Input.GetKeyDown(KeyCode.B))
        {
            StorePanel.SetActive(false);
            WeaponPanel.SetActive(false);
        }

        if (WeaponPanel.activeSelf||StorePanel.activeSelf)　　　　　　　　　　　　　　　　　　　　              //パネルが開いているか
        {
            PanelOpen = true;
        }
        else PanelOpen = false;

        Hpbar(_playerController.Hp,_playerController.MaxHp);
        


       // PanelOpen = ZoomPanel(Lead, KeyCode.T, new Vector3(0.02f, 0.02f, 0.02f));                            //リードのパネルを開くか閉じるか
        PanelOpen = ZoomPanel(_mapPanel, KeyCode.Tab, new Vector3(0.5f, 0.7f, 0.7f));                         //マップのパネルを開くか閉じるか
        gameManager.gameStop= ZoomPanel(_menuPanel, KeyCode.Escape, new Vector3(0.5f, 1.3f, 0.5f));           //メニューのパネルを開くか閉じるか
       
        SetCoin(GameManager.Coin);                                                                 //コインの数を更新                                 

        //メニューが開いているときの処理
        if (_menuPanel.activeSelf)
        {
            GameStop();
            GameManager.instance.StopGame();
            
        }
        else
        {
            GameManager.instance.ContinueGame();　　　　//メニューを開いているときは時間を止める
        }


        if (_equipSystem.IsReloading)
        {
            Show_Reloading_text();
        }
        else
        {
            _reloadingText.gameObject.SetActive(false);
            
        }

    }
   public void SearchMagazine()
    {
        _magazineText = GameObject.Find("magazine").GetComponent<Text>();
    }
   
    /// <summary>
    /// パネルを開くと消す
    /// </summary>
    /// <param name="panel">パネル</param>
    public void Panel_Open(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }
    /// <summary>
    /// マガジンのUI
    /// </summary>
    /// <param name="Magazine">マガジン</param>
    /// <param name="MaxMagazine">最大のマガジン</param>
    public void SetMagazine(int Magazine,int MaxMagazine)
    {
       _magazineText.text=Magazine.ToString() + "/" + MaxMagazine.ToString();
        //Magazine_Image = GameObject.Find("magazinebar").GetComponent<Image>();
        //Magazine_Image.fillAmount = (float)Magazine / (float)MaxMagazine;
    }
    public void memo()
    {
        StorePanel.SetActive(true);
    }

    public void BossHpbar(int Hp,int MaxHp)
    {
        if(_bossFrame.gameObject.activeSelf==false)
        {
            Debug.Log("BossHpbarActive");
            _bossFrame.gameObject.SetActive(true);
        }
        _bossHpBar.fillAmount = (float)Hp / (float)MaxHp;
        var life = ((float)Hp / (float)MaxHp) * 100;
        _percent.text = life.ToString("F0") + "%";

    }
    public void WarningImg(float duration)
    {
        _warningImage.gameObject.SetActive(true);
        if(_warningImage.gameObject.activeSelf)
        {
            _warningImage.DOFade(0, duration).OnComplete(() =>
            {
                _warningImage.gameObject.SetActive(false);
                Color c = _warningImage.color;
                c.a = 1;
                _warningImage.color = c;
            });
        }
    }
    void Hpbar(int Hp,int MaxHp)                                                                          //プレイヤーのHPバー
    {
       Animator animator= _lifeBar.GetComponentInParent<Animator>();
        _lifeBar.fillAmount = (float)Hp / (float)MaxHp;
        var life = ((float)Hp / (float)MaxHp) * 100;
        if (displaylife > life) displaylife--;
        else if (displaylife <life) displaylife++;
        _lifeBarText.text = displaylife.ToString("")+"%";
        if (life <= 70) animator.speed = 1.2f;
        else if(life<50) animator.speed = 1.5f;
        else if(life<30) animator.speed = 2f;
        //else animator.speed = 1f;
    }
   
    void Show_Reloading_text()
    {
        _reloadingText.gameObject.SetActive(true);
        _reloadingText.text = "Reloading...";
        
    }
    public void Damagevalue( Transform obj,int damage,Color color)　　　　　　　　　　　　　　　　　　　　//ダメージ表記
    {
        Text Damage_text = _damageValuePrefeb.GetComponent<Text>();
        Damage_text.text = damage.ToString();
        Damage_text.color = color;
        var center = 0.5f * new Vector3(Screen.width, Screen.height);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(obj.position)-center;

        GameObject damageInstance = Instantiate(
        _damageValuePrefeb,
        screenPos,
        Quaternion.identity
        );
       
        damageInstance.transform.SetParent(_gameCanvas.transform, false);
        DamageEffect(damageInstance);
        
    }
    /// <summary>
    /// ドロップしたコインの処理
    /// </summary>
    /// <param name="obj"></param>
    public void Coin(Transform obj)　　　　　　　　　　　　　　　　　　　　//コイン
    {
        var center = 0.5f * new Vector3(Screen.width, Screen.height);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(obj.position) - center;

        GameObject CoinInstance = Instantiate(
        _coinPrefeb,
        screenPos,
        Quaternion.identity
        );

       //コイン生成した処理
        CoinInstance.transform.SetParent(_gameCanvas.transform, false);
        AudioSource audioSource =CoinInstance.GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip);
        DamageEffect(CoinInstance);
        Transform CoinUI = GameObject.Find("Coin_UI").transform;
        CoinInstance.transform.DOMove(CoinUI.position, 1f).SetDelay(1f).OnComplete(() =>
        {
            Destroy(CoinInstance);
            GameManager.Coin += 1; //コインを増やす

        });

    }

    public void SetCoin(int coin)　　　　　　　　　　　　　　　　　　　　//コインの数
    {
        _coinText.text = coin.ToString("D2");
    }
    public void FadeControl(string nextscene)
    {
        displayeffect(_fade,nextscene,0.5f);
    }
    /// <summary>
    /// パネルの表示と非表示を制御するメソッド
    /// </summary>
    /// <param name="Obj">対象のパネル</param>
    /// <param name="key">表示するボタン</param>
    bool ZoomPanel(GameObject Obj,KeyCode key,Vector3 size)
    {
         if(Obj.activeSelf&& Input.GetKeyDown(key))
        {
            ereductioneffect(Obj, size);
        }
        else if(!Obj.activeSelf && Input.GetKeyDown(key))
        {
            Obj.SetActive(true);
            expansioneffect(Obj, size);
        }
        return Obj.activeSelf;    //パネルが開いているかどうかを返す

    }
    //ゲーム停止処理
    void GameStop()
    {
        if (GlobalVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            colorAdjustments.saturation.value=gameManager.gameStop? -100:100;
        }
    }
   
}

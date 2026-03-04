using System;
using System.Collections.Generic;
using UnityEngine;

//データ管理クラス
public class DataManager : MonoBehaviour
{
    [SerializeField]public List<WeaponDatabase> GunDatabase = new List<WeaponDatabase>();
    static public DataManager Instance { get; private set; }

    [Header("PlayerData")]
    public int PlayerHp=100;
    public int MaxPlayerHp=100;

    private void Awake()
    {
        CheakGameManagerExist();
    }
    
    //GameManagerの重複を防ぐ
    void CheakGameManagerExist()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    public void SavePlayerData(PlayerController player)
    {
        PlayerHp=player.Hp;
        MaxPlayerHp=player.MaxHp;
    }
    public void LoadPlayerData(PlayerController player)
    {
        player.Hp = PlayerHp;
        player.MaxHp = MaxPlayerHp;
    }
}

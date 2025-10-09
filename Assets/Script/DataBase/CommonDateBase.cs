using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// 全体のデータベースを管理するクラス
/// </summary>
public class CommonDateBase : MonoBehaviour
{
    [SerializeField] List<WeaponDatabase> GunDatabase = new List<WeaponDatabase>();
    [SerializeField] List<SkillDatabase> SkillDatabase = new List<SkillDatabase>();
    int money=0;
    

 
}


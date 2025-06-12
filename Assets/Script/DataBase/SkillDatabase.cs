using System;

public enum SkillType // スキルの種類を表す列挙型
{
    Attack, // 攻撃スキル
    Defense, // 防御スキル
    Buff, // バフスキル
    Support // 支援スキル
}
/// <summary>
/// スキルデータ管理クラス
/// </summary>
[Serializable]
public class SkillDatabase 
{
   public string skillName; // スキル名
    public string skillDescription; // スキルの説明
   public SkillType skillType; // スキルの種類

    public int skillLevel; // スキルのレベル
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/SkillData", fileName = "Skill Data")]
public class SkillData : ScriptableObject
{
    public List<string> SkillList = new List<string>()
    {
        "공격력 증가",
        "1",
        "2",
        "3",
        "4",
        "5"
    };
}

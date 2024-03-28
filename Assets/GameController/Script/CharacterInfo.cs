using System;
using UnityEngine;

[CreateAssetMenu]
public class CharacterInfo : ScriptableObject
{
    [Serializable]
    public struct SkillInfo{
        public string skillName;
        public string skillDescription;
    }

    public Sprite characterSprite;
    public GameObject characterPrefab;
    public SkillInfo[] skillInfos = new SkillInfo[3];
}
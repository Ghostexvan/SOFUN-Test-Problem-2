using System;
using UnityEngine;

[CreateAssetMenu]
public class PlayerTeam : ScriptableObject
{
    [SerializeField]
    private CharacterInfo[] characterInfos = new CharacterInfo[5];

    public CharacterInfo[] GetCharacterInfos(){
        return characterInfos;
    }
}
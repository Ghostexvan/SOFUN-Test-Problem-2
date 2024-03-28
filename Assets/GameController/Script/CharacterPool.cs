using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterPool : ScriptableObject
{
    public List<CharacterInfo> characterInfos = new List<CharacterInfo>();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    [SerializeField]
    public CharacterStat healthPoint;

    [SerializeField]
    public CharacterStat physicalDamage;

    [SerializeField]
    public CharacterStat magicalDamage;

    [SerializeField]
    public CharacterStat criticalChance;

    [SerializeField]
    public CharacterStat physicalResistance;

    [SerializeField]
    public CharacterStat magicalResistance;

    [SerializeField]
    public CharacterStat moveSpeed;
}

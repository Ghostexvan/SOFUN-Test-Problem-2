using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActiveSkill_KickMountains : Skill_Base
{
    [SerializeField]
    private string skillName = "Kick Mountains";

    [SerializeField]
    private string description = "Deals 300% PR as PD to an enemy with highest HP.";

    [SerializeField]
    private float physicalResistanceMultiplier = 3f;

    public ActiveSkill_KickMountains(){

    }

    public ActiveSkill_KickMountains(ActiveSkill_KickMountains activeSkill_KickMountains){
        this.skillName = activeSkill_KickMountains.skillName;
        this.description = activeSkill_KickMountains.description;
        this.physicalResistanceMultiplier = activeSkill_KickMountains.physicalResistanceMultiplier;
    }

    public override bool CheckCondition(CharacterActionController caster)
    {
        return caster.GetCurrentHealth() > 0 && caster.GetCurrentMana() == 100 && GameController.Instance.GetEnemiesTeam(caster.gameObject).Count > 0;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new ActiveSkill_KickMountains(this);
    }

    public override List<CharacterActionController> GetTargetList(CharacterActionController caster)
    {
        List<GameObject> enemies = GameController.Instance.GetEnemiesTeam(caster.gameObject);

        return new List<CharacterActionController>(){
            enemies[UnityEngine.Random.Range(0, enemies.Count)].GetComponent<CharacterActionController>()
        };
    }

    public override void ProcessSkill(CharacterActionController caster, List<CharacterActionController> targetList)
    {
        foreach(CharacterActionController target in targetList){
            target.DealPhysicalDamage(caster.GetCharacterData().physicalResistance.Value * physicalResistanceMultiplier, caster.GetCharacterData().moveSpeed.Value);
        }

        caster.ChangeCurrentManaPoint(-100);
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PassiveSkill_RoarOfDeath : Skill_Base
{
    [SerializeField]
    private string skillName = "Roar of Death";

    [SerializeField]
    private string description = "Increases Physical Damage by 50% once per battle when own HP is below 50%.";

    private bool isUsed = false;

    public PassiveSkill_RoarOfDeath(){

    }

    public PassiveSkill_RoarOfDeath(PassiveSkill_RoarOfDeath roarOfDeath){
        this.skillName = roarOfDeath.skillName;
        this.description = roarOfDeath.description;
        this.isUsed = roarOfDeath.isUsed;
    }

    public override bool CheckCondition(CharacterActionController caster)
    {
        if (isUsed || caster.GetCurrentHealth() <= 0){
            return false;
        }

        if (caster.GetCurrentHealth() / caster.GetCharacterData().healthPoint.Value <= 0.5){
            return true;
        }

        return false;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new PassiveSkill_RoarOfDeath(this);
    }

    public override List<CharacterActionController> GetTargetList(CharacterActionController caster)
    {
        return new List<CharacterActionController>(){
            caster
        };
    }

    public override void ProcessSkill(CharacterActionController caster, List<CharacterActionController> targetList)
    {
        foreach(CharacterActionController target in targetList){
            Debug.Log("Increase damage by 30%", target);
            target.GetCharacterData().physicalDamage.AddModifier(
                new StatModifier(0.3f, StatModType.PercentAdd, this)
            );
        }

        isUsed = true;
    }
}

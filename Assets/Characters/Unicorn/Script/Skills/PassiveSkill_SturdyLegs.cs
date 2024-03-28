using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PassiveSkill_SturdyLegs : Skill_Base
{
    [SerializeField]
    private string skillName = "Sturdy Legs";

    [SerializeField]
    private string description = "Increase PR by 25% when HP is above 50%.";

    [SerializeField]
    private float physicalResistanceMultiplier = 0.25f;

    private bool isUsed = false;

    public PassiveSkill_SturdyLegs(){

    }

    public PassiveSkill_SturdyLegs(PassiveSkill_SturdyLegs passiveSkill_SturdyLegs){
        this.skillName = passiveSkill_SturdyLegs.skillName;
        this.description = passiveSkill_SturdyLegs.description;
        this.physicalResistanceMultiplier = passiveSkill_SturdyLegs.physicalResistanceMultiplier;
        this.isUsed = passiveSkill_SturdyLegs.isUsed;
    }

    public override bool CheckCondition(CharacterActionController caster)
    {   
        if (isUsed && caster.GetCurrentHealth() / caster.GetCharacterData().healthPoint.Value < 0.5f){
            isUsed = false;
            caster.GetCharacterData().physicalResistance.RemoveAllModifiersFromSource(this);
        }

        return 
            caster.GetCurrentHealth() / caster.GetCharacterData().healthPoint.Value >= 0.5f && 
            !isUsed;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new PassiveSkill_SturdyLegs(this);
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
            target.GetCharacterData().physicalResistance.AddModifier(
                new StatModifier(
                    physicalResistanceMultiplier, 
                    StatModType.PercentAdd,
                    this
                )
            );
        }

        isUsed = true;
    }
}

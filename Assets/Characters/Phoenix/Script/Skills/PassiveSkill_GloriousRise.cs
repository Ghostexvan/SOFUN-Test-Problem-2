using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PassiveSkill_GloriousRise : Skill_Base
{
    [SerializeField]
    private string skillName = "Glorious Rise";

    [SerializeField]
    private string description = "When dying for the first time, revives with 50% HP.";

    [SerializeField]
    private bool isUsed = false;

    [SerializeField]
    private float healthMultiplier = 0.5f;

    public PassiveSkill_GloriousRise(){

    }

    public PassiveSkill_GloriousRise(PassiveSkill_GloriousRise passiveSkill_GloriousRise){
        this.skillName = passiveSkill_GloriousRise.skillName;
        this.description = passiveSkill_GloriousRise.description;
        this.isUsed = passiveSkill_GloriousRise.isUsed;
        this.healthMultiplier = passiveSkill_GloriousRise.healthMultiplier;
    } 

    public override bool CheckCondition(CharacterActionController caster)
    {
        return !isUsed && caster.GetCurrentHealth() <= 0;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new PassiveSkill_GloriousRise(this);
    }

    public override List<CharacterActionController> GetTargetList(CharacterActionController caster)
    {
        return new List<CharacterActionController>(){
            caster
        };
    }

    public override void ProcessSkill(CharacterActionController caster, List<CharacterActionController> targetList)
    {
        this.isUsed = true;
        caster.ChangeCurrentHealthPoint(caster.GetCharacterData().healthPoint.Value * healthMultiplier);
    }
}

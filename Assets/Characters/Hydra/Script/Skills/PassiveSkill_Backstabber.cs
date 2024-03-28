using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PassiveSkill_Backstabber : Skill_Base
{
    [SerializeField]
    private string skillName = "Backstabber";

    [SerializeField]
    private string description = "Always choose the enemy with the lowest MR.";

    [SerializeField]
    private bool isUsed = false;

    public PassiveSkill_Backstabber(){

    }

    public PassiveSkill_Backstabber(PassiveSkill_Backstabber passiveSkill_Backstabber){
        this.skillName = passiveSkill_Backstabber.skillName;
        this.description = passiveSkill_Backstabber.description;
        this.isUsed = passiveSkill_Backstabber.isUsed;
    }
    
    public override bool CheckCondition(CharacterActionController caster)
    {
        return !isUsed && caster.GetCurrentHealth() > 0;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new PassiveSkill_Backstabber(this);
    }

    public override List<CharacterActionController> GetTargetList(CharacterActionController caster)
    {
        return null;
    }

    public override void ProcessSkill(CharacterActionController caster, List<CharacterActionController> targetList)
    {
        this.isUsed = true;
        Debug.Log("Active Backstabber");
    }
}

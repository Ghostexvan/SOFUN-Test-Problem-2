using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PassiveSkill_PoisonousTouch : Skill_Base
{
    [SerializeField]
    private string skillName = "Poisonous Touch";

    [SerializeField]
    private string description = "Each attack inflicts a POISON effect on the target.";

    [SerializeField]
    private bool isUsed = false;

    public PassiveSkill_PoisonousTouch(){

    }

    public PassiveSkill_PoisonousTouch(PassiveSkill_PoisonousTouch passiveSkill_PoisonousTouch){
        this.skillName = passiveSkill_PoisonousTouch.skillName;
        this.description = passiveSkill_PoisonousTouch.description;
        this.isUsed = passiveSkill_PoisonousTouch.isUsed;
    }
    
    public override bool CheckCondition(CharacterActionController caster)
    {
        return !isUsed && caster.GetCurrentHealth() > 0;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new PassiveSkill_PoisonousTouch(this);
    }

    public override List<CharacterActionController> GetTargetList(CharacterActionController caster)
    {
        return null;
    }

    public override void ProcessSkill(CharacterActionController caster, List<CharacterActionController> targetList)
    {
        this.isUsed = true;
        Debug.Log("Active Poisonous Touch");
    }
}

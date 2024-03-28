using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PassiveSkill_FullVigilance : Skill_Base
{
    [SerializeField]
    private string skillName = "Full Vigilance";

    [SerializeField]
    private string description = "Inflicts HASTE effect on self when own HP is below 50%.";

    [SerializeField]
    private List<EffectObject_Base> effectList = new List<EffectObject_Base>();

    [SerializeField]
    private int cooldownTurns = 2;

    private int cooldownLeft = 0;

    public PassiveSkill_FullVigilance(){

    }

    public PassiveSkill_FullVigilance(PassiveSkill_FullVigilance passiveSkill_FullVigilance){
        this.skillName = passiveSkill_FullVigilance.skillName;
        this.description = passiveSkill_FullVigilance.description;
        this.effectList = passiveSkill_FullVigilance.effectList;
        this.cooldownLeft = passiveSkill_FullVigilance.cooldownLeft;
        this.cooldownTurns = passiveSkill_FullVigilance.cooldownTurns;
    }

    public override bool CheckCondition(CharacterActionController caster)
    {
        if (cooldownLeft != 0){
            cooldownLeft = Mathf.Max(0, cooldownLeft - 1);
        }

        return cooldownLeft == 0 && caster.GetCurrentHealth() > 0 && caster.GetCurrentHealth() / caster.GetCharacterData().healthPoint.Value <= 0.5f;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new PassiveSkill_FullVigilance(this);
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
            foreach(EffectObject_Base effect in effectList){
                target.AddActiveEffect(effect.GetEffectInstance());
            }
        }

        cooldownLeft = cooldownTurns;
    }
}

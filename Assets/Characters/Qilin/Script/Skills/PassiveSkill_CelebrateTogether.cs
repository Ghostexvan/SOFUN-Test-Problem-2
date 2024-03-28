using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PassiveSkill_CelebrateTogether : Skill_Base
{
    [SerializeField]
    private string skillName = "Celebrate Together";

    [SerializeField]
    private string description = "Gives all allies CHEER effect when own HP is below 30%.";

    [SerializeField]
    private List<EffectObject_Base> effectList = new List<EffectObject_Base>();

    [SerializeField]
    private int cooldownTurns = 3;

    private int cooldownLeft = 0;

    public PassiveSkill_CelebrateTogether(){

    }

    public PassiveSkill_CelebrateTogether(PassiveSkill_CelebrateTogether passiveSkill_CelebrateTogether){
        this.skillName = passiveSkill_CelebrateTogether.skillName;
        this.description = passiveSkill_CelebrateTogether.description;
        this.effectList = passiveSkill_CelebrateTogether.effectList;
        this.cooldownLeft = passiveSkill_CelebrateTogether.cooldownLeft;
        this.cooldownTurns = passiveSkill_CelebrateTogether.cooldownTurns;
    }

    public override bool CheckCondition(CharacterActionController caster)
    {
        if (cooldownLeft != 0){
            cooldownLeft = Mathf.Max(0, cooldownLeft - 1);
        }
        
        return 
            caster.GetCurrentHealth() > 0 && 
            caster.GetCurrentHealth() / caster.GetCharacterData().healthPoint.Value <= 0.3f &&
            cooldownLeft == 0;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new PassiveSkill_CelebrateTogether(this);
    }

    public override List<CharacterActionController> GetTargetList(CharacterActionController caster)
    {
        List<GameObject> allies = GameController.Instance.GetAllyTeam(caster.gameObject);

        List<CharacterActionController> alliesController = new List<CharacterActionController>();

        foreach (GameObject ally in allies){
            alliesController.Add(ally.GetComponent<CharacterActionController>());
        }

        return alliesController;
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

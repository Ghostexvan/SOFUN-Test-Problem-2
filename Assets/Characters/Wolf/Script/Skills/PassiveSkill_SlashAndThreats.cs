using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PassiveSkill_SlashAndThreats : Skill_Base
{
    [SerializeField]
    private string skillName = "Slash And Threats";

    [SerializeField]
    private string description = "Lost 5% HP and inflicts FEAR effect on a random enemy.";

    [SerializeField]
    private float healthPointLostMultiplier = 0.05f;

    [SerializeField]
    private List<EffectObject_Base> effectList;

    [SerializeField]
    private int cooldownTurns = 3;

    private int cooldownLeft = 0;

    public PassiveSkill_SlashAndThreats(){

    }

    public PassiveSkill_SlashAndThreats(PassiveSkill_SlashAndThreats passiveSkill_SlashAndThreats){
        this.skillName = passiveSkill_SlashAndThreats.skillName;
        this.description = passiveSkill_SlashAndThreats.description;
        this.healthPointLostMultiplier = passiveSkill_SlashAndThreats.healthPointLostMultiplier;
        this.cooldownLeft = passiveSkill_SlashAndThreats.cooldownLeft;
        this.cooldownTurns = passiveSkill_SlashAndThreats.cooldownTurns;
        this.effectList = passiveSkill_SlashAndThreats.effectList;
    }

    public override bool CheckCondition(CharacterActionController caster)
    {
        Debug.Log("Checking Slash and Threats");
        if (cooldownLeft != 0){
            cooldownLeft = Mathf.Max(0, cooldownLeft - 1);
        }

        return cooldownLeft == 0 && caster.GetCurrentHealth() / caster.GetCharacterData().healthPoint.Value > 0.05f;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new PassiveSkill_SlashAndThreats(this);
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
        foreach (CharacterActionController target in targetList)
        {
            foreach (EffectObject_Base effect in effectList)
            {
                target.AddActiveEffect(effect.GetEffectInstance());
            }
        }

        caster.ChangeCurrentHealthPoint(
            -caster.GetCharacterData().physicalDamage.Value * healthPointLostMultiplier,
            true
        );
        
        cooldownLeft = cooldownTurns;
    }
}

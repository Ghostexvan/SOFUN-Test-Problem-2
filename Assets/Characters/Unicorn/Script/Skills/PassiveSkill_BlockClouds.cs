using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PassiveSkill_BlockClouds : Skill_Base
{
    [SerializeField]
    private string skillName = "Block Clouds";

    [SerializeField]
    private string description = "Inflicts FEAR effect to a random enemy.";

    [SerializeField]
    private List<EffectObject_Base> effectList = new List<EffectObject_Base>();

    [SerializeField]
    private int cooldownTurns = 3;

    private int cooldownLeft = 0;

    public PassiveSkill_BlockClouds()
    {

    }

    public PassiveSkill_BlockClouds(PassiveSkill_BlockClouds passiveSkill_BlockClouds)
    {
        this.skillName = passiveSkill_BlockClouds.skillName;
        this.description = passiveSkill_BlockClouds.description;
        this.cooldownLeft = passiveSkill_BlockClouds.cooldownLeft;
        this.effectList = passiveSkill_BlockClouds.effectList;
        this.cooldownTurns = passiveSkill_BlockClouds.cooldownTurns;
    }

    public override bool CheckCondition(CharacterActionController caster)
    {
        if (cooldownLeft != 0){
            cooldownLeft = Mathf.Max(0, cooldownLeft - 1);
        }
        
        return cooldownLeft == 0 && caster.GetCurrentHealth() > 0 && GameController.Instance.GetEnemiesTeam(caster.gameObject).Count > 0;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new PassiveSkill_BlockClouds(this);
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

        cooldownLeft = cooldownTurns;
    }
}

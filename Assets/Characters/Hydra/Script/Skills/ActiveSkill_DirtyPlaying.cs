using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActiveSkill_DirtyPlaying : Skill_Base
{
    [SerializeField]
    private string skillName = "Dirty Playing";

    [SerializeField]
    private string description = "Deals 75% MD to all enemies.";

    [SerializeField]
    private float magicalDamageMultiplier = 0.75f;

    [SerializeField]
    private List<EffectObject_Base> effectList = new List<EffectObject_Base>();

    public ActiveSkill_DirtyPlaying(){

    }

    public ActiveSkill_DirtyPlaying(ActiveSkill_DirtyPlaying activeSkill_DirtyPlaying){
        this.skillName = activeSkill_DirtyPlaying.skillName;
        this.description = activeSkill_DirtyPlaying.description;
        this.magicalDamageMultiplier = activeSkill_DirtyPlaying.magicalDamageMultiplier;
        this.effectList = activeSkill_DirtyPlaying.effectList;
    }

    public override bool CheckCondition(CharacterActionController caster)
    {
        return caster.GetCurrentHealth() > 0 && caster.GetCurrentMana() == 100 && GameController.Instance.GetEnemiesTeam(caster.gameObject).Count > 0;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new ActiveSkill_DirtyPlaying(this);
    }

    public override List<CharacterActionController> GetTargetList(CharacterActionController caster)
    {
        List<GameObject> enemies = GameController.Instance.GetEnemiesTeam(caster.gameObject);

        List<CharacterActionController> enemiesController = new List<CharacterActionController>();

        foreach (GameObject enemy in enemies){
            enemiesController.Add(enemy.GetComponent<CharacterActionController>());
        }

        return enemiesController;
    }

    public override void ProcessSkill(CharacterActionController caster, List<CharacterActionController> targetList)
    {
        foreach(CharacterActionController target in targetList){
            if (target.DealMagicalDamage(caster.GetCharacterData().magicalDamage.Value * magicalDamageMultiplier, caster.GetCharacterData().moveSpeed.Value)){
                foreach(EffectObject_Base effect in effectList){
                    target.AddActiveEffect(effect.GetEffectInstance());
                }
            }
        }

        caster.ChangeCurrentManaPoint(-100);
    }
}

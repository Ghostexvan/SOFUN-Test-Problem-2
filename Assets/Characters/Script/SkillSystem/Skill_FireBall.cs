using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Skill_FireBall : Skill_Base
{
    [SerializeField]
    private string skillName;

    [SerializeField]
    private float damageMultiple = 1.5f;

    [SerializeField]
    private List<EffectObject_Base> effectList = new List<EffectObject_Base>();

    public Skill_FireBall() {

    }

    public override Skill_Base GetSkillInstance()
    {
        return new Skill_FireBall(this);
    }

    public Skill_FireBall(Skill_FireBall fireBall){
        this.skillName = fireBall.skillName;
        this.damageMultiple = fireBall.damageMultiple;
        this.effectList = fireBall.effectList;
    }

    public override bool CheckCondition(CharacterActionController caster)
    {       
        return caster.GetCurrentMana() == 100;
    }

    public override List<CharacterActionController> GetTargetList(CharacterActionController caster)
    {
        List<GameObject> targetList = GameController.Instance.GetEnemiesTeam(caster.gameObject);
        
        GameObject targetObject = targetList[0];

        for (int index = 1; index < targetList.Count; index++){
            if (targetList[index].GetComponent<CharacterActionController>().GetCurrentHealth() < targetObject.GetComponent<CharacterActionController>().GetCurrentHealth()){
                targetObject = targetList[index];
            }
        }

        Debug.Log("Got target ", targetObject);

        return new List<CharacterActionController>() {
            targetObject.GetComponent<CharacterActionController>()
        }; 
    }

    public override void ProcessSkill(CharacterActionController caster, List<CharacterActionController> targetList)
    {
        Debug.Log("Use fire ball");
        foreach(CharacterActionController target in targetList){
            target.ChangeCurrentHealthPoint(- damageMultiple * caster.GetCharacterData().physicalDamage.Value);
            foreach(EffectObject_Base effect in effectList){
                target.AddActiveEffect(effect.GetEffectInstance());
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ActiveSkillObject_TalentedStrategist : SkillObject_Base
{
    [SerializeField]
    private ActiveSkill_TalentedStrategist activeSkill_TalentedStrategist = new ActiveSkill_TalentedStrategist();

    public override Skill_Base GetSkillInstance() => activeSkill_TalentedStrategist.GetSkillInstance();
}

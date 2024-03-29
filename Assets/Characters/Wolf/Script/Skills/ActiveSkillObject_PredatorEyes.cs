using UnityEngine;

[CreateAssetMenu]
public class ActiveSkillObject_PredatorEyes : SkillObject_Base
{
    [SerializeField]
    private ActiveSkill_PredatorEyes activeSkill_PredatorEyes = new ActiveSkill_PredatorEyes();

    public override Skill_Base GetSkillInstance() => activeSkill_PredatorEyes.GetSkillInstance();
}

using UnityEngine;

[CreateAssetMenu]
public class ActiveSkillObject_WeCanMakeIt : SkillObject_Base
{
    [SerializeField]
    private ActiveSkill_WeCanMakeIt activeSkill_WeCanMakeIt = new ActiveSkill_WeCanMakeIt();

    public override Skill_Base GetSkillInstance() => activeSkill_WeCanMakeIt.GetSkillInstance();
}

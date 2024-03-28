using UnityEngine;

[CreateAssetMenu]
public class ActiveSkillObject_KickMountains : SkillObject_Base
{
    [SerializeField]
    private ActiveSkill_KickMountains activeSkill_KickMountains = new ActiveSkill_KickMountains();

    public override Skill_Base GetSkillInstance() => activeSkill_KickMountains.GetSkillInstance();
}

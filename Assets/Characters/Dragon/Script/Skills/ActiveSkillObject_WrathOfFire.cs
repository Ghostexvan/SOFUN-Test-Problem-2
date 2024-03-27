using UnityEngine;

[CreateAssetMenu]
public class ActiveSkillObject_WrathOfFire : SkillObject_Base
{
    [SerializeField]
    private ActiveSkill_WrathOfFire activeSkill_WrathOfFire = new ActiveSkill_WrathOfFire();

    public override Skill_Base GetSkillInstance() => activeSkill_WrathOfFire.GetSkillInstance();
}

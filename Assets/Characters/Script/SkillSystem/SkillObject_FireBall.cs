using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillObject_FireBall : SkillObject_Base
{
    [SerializeField]
    private Skill_FireBall skill_FireBall = new Skill_FireBall();

    public override Skill_Base GetSkillInstance() => skill_FireBall.GetSkillInstance();
}

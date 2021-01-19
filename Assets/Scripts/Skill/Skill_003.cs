﻿using System.Collections;
using UnityEngine;

public class Skill_003 : Skill
{
    private int strengthToDamageRatio;

    // Use this for initialization
    private Skill_003()
    {
        number = 3;
        name = "천벌";
        unitClass = UnitClass.Priest;
        grade = Grade.Normal;
        description = "두칸 안에 있는 단일 적에게 데미지를 입힌다.";
        criticalRate = 0;
        reuseTime = 0;
        domain = Domain.SelectOne;
        target = Target.AnyUnit;
        APSchema = "5;00100;01110;11111;01110;00100";
        RPSchema = "1;1";
        strengthToDamageRatio = 1;
    }

    public override void UseSkillToUnit(Unit unit)
    {
        Debug.LogError(name + " 스킬을 " + unit.name + "에 사용!");
        unit.GetDamage(GetComponent<Unit>().strength * strengthToDamageRatio + enhancedLevel);
        base.UseSkillToUnit(unit);
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Grade { NULL, Normal, Rare, Legend, Boss }

[System.Serializable]
public class Skill : MonoBehaviour
{
    public int number;                                              // 스킬 번호
    public new string name = "No Skill Name";                       // 스킬 이름
    public UnitClass unitClass = UnitClass.NULL;                    // 스킬 클래스
    public Grade grade = Grade.NULL;                                // 스킬 등급
    public Sprite skillImage; 

    [TextArea(1, 10)]
    public string description;                                      // 스킬 설명
    public int enhancedLevel;                                       // 강화도
    public int reuseTime;                                           // 재사용 대기시간
    public int currentReuseTime;

    [Range(0, 100)]
    public float criticalRate;                                      // 크리티컬율

    public enum Domain                                              // 스킬 사용가능 범위의 종류
    {
        NULL,
        Me,             // 나에게 사용, Unit
        RandomOne,      // 범위 내 대상에서 랜덤한 하나를 뽑는다.
        SelectOne,      // 범위 내 대상중에서 하나를 선택한다.
        Fixed,          // 복수 대상일 경우 범위가 고정된다.
        Rotate,         // 복수 대상일 경우 범위가 회전한다.
    }

    public enum Target                                              // 스킬의 대상
    {
        NULL,
        AnyTile,               // 타일을 대상으로 사용가능
        NoUnitTile,         // 유닛이 없는 곳에만 사용가능, 소환류 스킬에 사용
        AnyUnit,            // 적, 아군 구별 없이 사용가능
        PartyUnit,          // 내 파티에게만 사용가능
        FriendlyUnit,       // 아군에게만 사용가능
        EnemyUnit,          // 적에게만 사용가능
    }

    public Domain domain = Domain.NULL;
    public Target target = Target.NULL;

    public List<Vector2Int> AvailablePositions;                      // 사용가능한 위치
    public List<Vector2Int> RangePositions;                          // 다중 범위
    
    public virtual void UseSkillToTile(Vector2Int position)
    {
        currentReuseTime = reuseTime;
    }

    public virtual void UseSkillToTile(List<Vector2Int> positions)
    {
        foreach (var position in positions)
            UseSkillToTile(position);
    }

    public virtual void UseSkillToUnit(Unit unit)
    {
        // 스킬당 구현 필요
        currentReuseTime = reuseTime;
    }

    public virtual void UseSkillToUnit(List<Unit> units)
    {
        foreach (var unit in units)
            UseSkillToUnit(unit);
    }

    public List<Unit> GetUnitsInDomain(Unit skillUser) //
    {
        List<Unit> units = new List<Unit>();

        if (domain == Domain.Me)
            units.Add(skillUser);
        else if (domain == Domain.RandomOne)
        {
            List<Unit> tempUnits = GetUnitsInSelectOne(skillUser);

            int rand = Random.Range(0, units.Count);
            units.Add(tempUnits[rand]);
        }
        else if (domain == Domain.SelectOne)
            units = GetUnitsInSelectOne(skillUser);

        return units;
    }

    public List<Unit> GetUnitsInSelectOne(Unit skillUser) 
    {
        List<Unit> units = new List<Unit>();
        List<Unit> tempUnits = BattleManager.GetUnitsInPositions(GetPositionsInDomain(skillUser)); // 스킬 도메인을 받고 그위의 유닛들을 반환

        foreach (var unit in tempUnits)
        {
            if (target == Target.AnyUnit)
                units.Add(unit);
            else if (target == Target.EnemyUnit && unit.category == Category.Enemy)
                units.Add(unit);
            else if (target == Target.FriendlyUnit && (unit.category == Category.Friendly || unit.category == Category.Party))
                units.Add(unit);
            else if (target == Target.PartyUnit && unit.category == Category.Party)
                units.Add(unit);
        }

        return units;
    }

    public List<Vector2Int> GetPositionsInDomain(Unit skillUser) // 스킬 범위를 절대 위치로 바꾼다.
    {
        List<Vector2Int> positions = new List<Vector2Int>();

        foreach (var item in AvailablePositions)
        {
            List<Vector2Int> temp = UnitPosition.VectoredPosition(skillUser.unitPosition, item).UnitPositionToVector2IntList();
//            Debug.LogError(temp);
            foreach (var position in temp)
            {
                if (target == Target.NoUnitTile && !BattleManager.instance.AllTiles[position.x, position.y].IsUsable())
                    continue;
                positions.Add(position);
//                Debug.LogError(position);
            }

        }
        return positions;
    }

    public bool IsUsable()
    {
        if (currentReuseTime == 0)
            return true;
        else
            return false;
    }

/*    public List<Vector2Int> RangeToPositions(int range)
    {
        
    }*/

}
using System;

public interface IBaseEnemy
{
    bool CanMove();
    void WalkAnimation();
    void StopWalking();
    void Attack();
    void Dead();
    float AttackRange();
}

public interface IWalking
{
    bool CanMove();
    float WalkingSpeed();
}

public interface IAttackPlayer{
    void Attack();
    float AttackRange();
}

public interface IAttackRange
{
    void Attack();
    float AttackRange();
}
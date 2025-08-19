using UnityEngine;

public interface IAttackStrategy
{
    public string AttackName { get; }
    int Attack(GameObject self, GameObject target);

}

public interface ITakeDamage
{
    void TakeDamage(int damage);
}
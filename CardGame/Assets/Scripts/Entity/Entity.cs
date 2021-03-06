using Mirror;
using UnityEngine;

public abstract class Entity : NetworkBehaviour
{
    [SyncVar(hook = nameof(UpdateUIOnSync))] public bool CanAttack;
    [Command(requiresAuthority = false)] public void SetCanAttack(bool value) => CanAttack = value;
    public Targets ThisTarget { get; set; }

    [SyncVar] public int MaxHealth;
    [SyncVar(hook = nameof(UpdateUIOnSync))] public int Health;
    [SyncVar(hook = nameof(UpdateUIOnSync))] public int Damage;

    [SyncVar] public bool IsMonarch;
    [Command(requiresAuthority = false)] public void SetMonarch(bool value) => IsMonarch = value;
    [SyncVar] public bool IsLethal;
    [Command(requiresAuthority = false)] public void SetLethal(bool value) => IsLethal = value;
    [SyncVar] public bool IsKingpin;
    [Command(requiresAuthority = false)] public void SetKingpin(bool value) => IsKingpin = value;
    [SyncVar] public bool IsFeint;
    [Command(requiresAuthority = false)] public void SetFeint(bool value) => IsFeint = value;
    [SyncVar] public bool IsQuick;
    [Command(requiresAuthority = false)] public void SetQuick(bool value) => IsQuick = value;
    [SyncVar] public bool IsDualWield;
    [Command(requiresAuthority = false)] public void SetDualWield(bool value) => IsDualWield = value;
    [SyncVar] public bool CanAttackAgain;
    [Command(requiresAuthority = false)] public void SetCanAttackAgain(bool value) => CanAttackAgain = value;

    // Whenever the health of an entity changes, update all EntityObserver UI
    private void UpdateUIOnSync(int oldVar, int newVar) => EntitySubject.Notify();
    private void UpdateUIOnSync(bool oldVar, bool newVar) => EntitySubject.Notify();

    public void ResetEntity()
    {
        SetCanAttack(true);
        if(IsDualWield)
        {
            SetCanAttackAgain(true);
        }
    }

    [Command(requiresAuthority = false)]
    public void HealHealth(int amount)
    {
        if(amount + Health <= MaxHealth)
        {
            Health += amount;
        }
        else
        {
            Health = MaxHealth;
        }

    }

    [Command(requiresAuthority = false)]
    public void BuffDamage(int amount)
    {
        Damage += amount;
    }
    [Command(requiresAuthority = false)]
    public void BuffHealth(int amount)
    {
        MaxHealth += amount;
        Health += amount;
    }



    [Command(requiresAuthority = false)]
    public void TakeDamage(int damage)
    {
        if(damage == 0) { return; }
        if (IsFeint)
        {
            SetFeint(false);
            return;
        }

        Health -= damage;
        Debug.Log($"Taken {damage} damage");
        if (Health <= 0)
        {
            OnDeath();
        }
    }

    public void Attack(Entity target)
    {
        GameManager.Instance.CmdAttackEntity(this, target);
        SetCanAttack(false);
        if (CanAttackAgain)
        {
            SetCanAttack(true);
            SetCanAttackAgain(false);
        }
    }

    public abstract void OnDeath();
}

public partial class Hero
{
    public string Name { get; private set; }
    public uint Cost { get; private set; }

    public int Health
    {
        get { return _health.GetValue(); }
        set { _health.SetValue(value); }
    }
    public int MaxHealth
    {
        get { return _maxHealth.GetValue() };
        set { _maxHealth.SetValue(value) };
    }
    public int Mana { get; private set; }
    public int MaxMana { get; private set; }
    public int Armor { get; private set; }
    public float Speed { get; private set; }
    public float CriticalRate { get; private set; }
    public int AttackPower { get; private set; }
    public int SpellPower { get; private set; }
    public int Spell { get; private set; }

    public int Level { get; private set; }
    public int Experience { get; private set; }
    public int ExperienceToLevel { get; private set; }

    public int AttackRage { get; private set; }
    public float AttackSpeed { get; private set; }

    public bool InBattlefield { get; private set; }

    public int Damage(Hero from, int dmg, params ConditionEffect[] effs)
    {
        if (HasConditionEffect(ConditionEffects.Invincible))
            return 0;
        if (!HasConditionEffect(ConditionEffects.Paused) &&
            !HasConditionEffect(ConditionEffects.Stasis))
        {
            var def = this.ObjectDesc.Defense;
            if (noDef)
                def = 0;
            dmg = (int)StatsManager.GetDefenseDamage(this, dmg, def);
            int effDmg = dmg;
            if (effDmg > HP)
                effDmg = HP;
            if (!HasConditionEffect(ConditionEffects.Invulnerable))
                HP -= dmg;
            ApplyConditionEffect(effs);
            Owner.BroadcastPacketNearby(new Damage()
            {
                TargetId = this.Id,
                Effects = 0,
                DamageAmount = (ushort)dmg,
                Kill = HP < 0,
                BulletId = 0,
                ObjectId = from.Id
            }, this, null, PacketPriority.Low);

            counter.HitBy(from, time, null, dmg);

            if (HP < 0 && Owner != null)
            {
                Death(time);
            }

            return effDmg;
        }
        return 0;
    }

}
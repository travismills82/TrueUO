using Server.Items;
using Server.Mobiles;
using Server.Regions;
using System;
using System.Xml;

namespace Server.Engines.MyrmidexInvasion
{
    public class BattleRegion : DungeonRegion
    {
        public BattleSpawner Spawner { get; set; }

        public BattleRegion(XmlElement xml, Map map, Region parent)
            : base(xml, map, parent)
        {
        }

        public override void OnDeath(Mobile m)
        {
            base.OnDeath(m);

            bool nomaster = m is BaseCreature creature && creature.GetMaster() == null;

            if (BattleSpawner.Instance != null && BattleSpawner.Instance.Active && nomaster && Spawner != null)
            {
                Timer.DelayCall(TimeSpan.FromSeconds(.25), Spawner.RegisterDeath, (BaseCreature)m);
            }

            // the delay ensures the corpse is created after death
            Timer.DelayCall(() =>
                {
                    if (m.Corpse != null && (m is BritannianInfantry || m is TribeWarrior || m is TribeShaman || m is TribeChieftan || m is MyrmidexDrone || m is MyrmidexWarrior))
                    {
                        Mobile killer = m.LastKiller;

                        if (killer == null || killer is BaseCreature bc && !(bc.GetMaster() is PlayerMobile))
                        {
                            m.Corpse.Delete();
                        }
                    }
                });
        }

        public override void OnExit(Mobile m)
        {
            if (m is PlayerMobile mobile && Spawner != null)
            {
                Spawner.OnLeaveRegion(mobile);
            }

            base.OnExit(m);
        }

        public override bool OnDamage(Mobile m, ref int Damage)
        {
            Mobile attacker = m.FindMostRecentDamager(false);

            if (MyrmidexInvasionSystem.AreEnemies(m, attacker) && EodonianPotion.IsUnderEffects(attacker, PotionEffect.Kurak))
            {
                Damage *= 3;

                if (Damage > 0)
                    m.FixedEffect(0x37B9, 10, 5);
            }

            return base.OnDamage(m, ref Damage);
        }
    }
}

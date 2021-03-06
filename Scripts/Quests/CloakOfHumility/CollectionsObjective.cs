using Server.Mobiles;
using System;
namespace Server.Engines.Quests
{
    public class CollectionsObtainObjective : ObtainObjective
    {
        private bool m_HasObtained;

        public bool HasObtained
        {
            get => m_HasObtained;
            set => m_HasObtained = true;
        }

        public CollectionsObtainObjective(Type obtain, string name, int amount) : base(obtain, name, amount)
        {
            m_HasObtained = false;
        }

        public override bool Update(object o)
        {
            if (Quest == null || Quest.Owner == null)
                return false;

            if (m_HasObtained)
                return base.Update(o);

            return false;
        }

        public static void CheckReward(PlayerMobile pm, Item item)
        {
            if (pm.Quests != null)
            {
                for (var index = 0; index < pm.Quests.Count; index++)
                {
                    BaseQuest q = pm.Quests[index];

                    for (var i = 0; i < q.Objectives.Count; i++)
                    {
                        BaseObjective obj = q.Objectives[i];

                        if (obj is CollectionsObtainObjective objective && objective.Obtain == item.GetType())
                        {
                            objective.HasObtained = true;
                            pm.SendSound(q.UpdateSound);
                            return;
                        }
                    }
                }
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version

            writer.Write(m_HasObtained);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            reader.ReadInt();

            m_HasObtained = reader.ReadBool();
        }
    }
}

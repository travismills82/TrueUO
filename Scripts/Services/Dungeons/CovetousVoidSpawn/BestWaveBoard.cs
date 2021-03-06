using Server.Engines.VoidPool;
using Server.Mobiles;

namespace Server.Items
{
    public class BestWaveBoard : Item
    {
        public override bool ForceShowProperties => true;

        [Constructable]
        public BestWaveBoard() : base(7774)
        {
            Name = "Void Pool - Best Wave";
            Movable = false;
        }

        public override void OnDoubleClick(Mobile m)
        {
            if (m is PlayerMobile pm && pm.InRange(Location, 3))
            {
                pm.SendGump(new ScoresGump(pm.Map == Map.Felucca ? VoidPoolController.InstanceFel : VoidPoolController.InstanceTram, pm, ScoreType.BestWave));
            }
        }

        public BestWaveBoard(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            reader.ReadInt();
        }
    }
}

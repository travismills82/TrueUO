using System;

namespace Server.Engines.Quests
{
    public class Bravehorn : BaseEscort
    {
        public override Type[] Quests => new[] { typeof(DefendingTheHerdQuest) };

        [Constructable]
        public Bravehorn()
        {
            Name = "Bravehorn";
        }

        public Bravehorn(Serial serial)
            : base(serial)
        {
        }

        public override void InitBody()
        {
            InitStats(100, 100, 25);

            Blessed = false;
            Female = false;
            Body = 0xEA;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            reader.ReadInt();
        }
    }
}

namespace Server.Mobiles
{
    public class BravehornsMate : Hind
    {
        [Constructable]
        public BravehornsMate()
        {
            Name = "bravehorn's mate";
            Tamable = false;
        }

        public BravehornsMate(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            reader.ReadInt();
        }
    }
}

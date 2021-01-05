namespace Server.Items
{
    public class DaggerBelt : BaseWaist
    {
        public override int LabelNumber => 1159210;  // dagger belt

        [Constructable]
        public DaggerBelt()
            : base(0xA40E)
        {
            Weight = 3.0;
            Layer = Layer.Waist;
        }

        public DaggerBelt(Serial serial)
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

namespace Server.Items
{
    public class GreaterConfusionBlastPotion : BaseConfusionBlastPotion
    {
        [Constructable]
        public GreaterConfusionBlastPotion()
            : base(PotionEffect.ConfusionBlastGreater)
        {
        }

        public GreaterConfusionBlastPotion(Serial serial)
            : base(serial)
        {
        }

        public override int Radius => 7;
        public override int LabelNumber => 1072108;// a Greater Confusion Blast potion

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

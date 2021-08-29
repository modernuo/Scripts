namespace Server.Items
{
    [Serializable(0)]
    public partial class FireHide : BaseStaffHide
    {
        [SerializableField(0)]
        [SerializableFieldAttr("[CommandProperty(AccessLevel.GameMaster)]")]
        private int _effectHue;

        [SerializableField(1)]
        [SerializableFieldAttr("[CommandProperty(AccessLevel.GameMaster)]")]
        private int _effectSound = 0x225;

        [SerializableField(2)]
        [SerializableFieldAttr("[CommandProperty(AccessLevel.GameMaster)]")]
        private int _effectId = 0x3709;

        [SerializableField(3)]
        [SerializableFieldAttr("[CommandProperty(AccessLevel.GameMaster)]")]
        private int _effectRender;

        [SerializableField(4)]
        [SerializableFieldAttr("[CommandProperty(AccessLevel.GameMaster)]")]
        private int _effect = 5052;

        public override bool CastHide => false;

        public override string DefaultName => "Fire Hide";

        public override void HideEffects(Mobile from)
        {
            from.Hidden = !from.Hidden;

            from.PlaySound(0x208);

            from.FixedParticles(0x3709, 10, 30, 5052, EffectLayer.LeftFoot);

            if (from.Hidden)
            {
                Effects.SendLocationParticles(from, _effectId, 10, 30, _effectHue, _effectRender, _effect, 0);
            }
            else
            {
                from.FixedParticles(_effectId, 10, 30, _effect, _effectHue, _effectRender, EffectLayer.LeftFoot);
            }

            Effects.PlaySound(from, _effectSound);
            OnEndHideEffects(from);
        }

        [Constructible]
        public FireHide() : base(1161)
        {
        }
    }
}

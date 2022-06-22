using System;
using ModernUO.Serialization;

namespace Server.Items
{
    [SerializationGenerator(0)]
    public partial class MoleHide : BaseStaffHide
    {
        private class MoleInfo
        {
            public readonly Mobile _from;
            public int _count;

            public MoleInfo(Mobile from) => _from = from;
        }

        public override string DefaultName => "Mole Hide";

        public override bool CastHide => false;

        public override void HideEffects(Mobile from)
        {
            Action callback;
            var moleInfo = new MoleInfo(from);

            if (from.Hidden)
            {
                from.Z -= 10;
                from.Hidden = false;
                callback = () => DoIncZ_Callback(moleInfo);
            }
            else
            {
                callback = () => DoDecZ_Callback(moleInfo);
            }

            Timer.StartTimer(TimeSpan.FromMilliseconds(100), TimeSpan.FromMilliseconds(100), 10, callback);
            from.PlaySound(0x244);
        }

        private void DoIncZ_Callback(MoleInfo info)
        {
            info._from.Z++;
            info._count++;
            if (info._count > 9)
            {
                OnEndHideEffects(info._from);
            }
        }

        private void DoDecZ_Callback(MoleInfo info)
        {

            info._from.Z--;
            info._count++;
            if (info._count > 9)
            {
                info._from.Hidden = true;
                info._from.Z += 10;
                OnEndHideEffects(info._from);
            }
        }

        [Constructible]
        public MoleHide() : base(1717)
        {
        }
    }
}

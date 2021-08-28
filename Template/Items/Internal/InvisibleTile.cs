using System;
using System.Buffers.Binary;
using Server.Network;

namespace Server.Items
{
    [Serializable(0)]
    public partial class InvisibleTile : Item
    {
        private const ushort _gmItemId = 0x36FF;

        public override string DefaultName => "invisible tile";

        [Constructible]
        public InvisibleTile() : base(0x2198)
        {
            Movable = false;
        }

        public override void SendWorldPacketTo(NetState ns, ReadOnlySpan<byte> world = default)
        {
            var mob = ns.Mobile;
            if (AccessLevel.GameMaster >= mob?.AccessLevel)
            {
                base.SendWorldPacketTo(ns, world);
                return;
            }

            SendGMItem(ns);
        }

        private void SendGMItem(NetState ns)
        {
            // GM Packet
            var buffer = stackalloc byte[OutgoingEntityPackets.MaxWorldEntityPacketLength].InitializePacket();

            int length;

            if (ns.StygianAbyss)
            {
                length = OutgoingEntityPackets.CreateWorldEntity(buffer, this, ns.HighSeas);
                BinaryPrimitives.WriteUInt16BigEndian(buffer[8..10], _gmItemId);
            }
            else
            {
                length = OutgoingItemPackets.CreateWorldItem(buffer, this);
                BinaryPrimitives.WriteUInt16BigEndian(buffer[7..2], _gmItemId);
            }

            ns.Send(buffer[..length]);
        }
    }
}

using System;
using System.Buffers.Binary;
using Server.Network;

namespace Server.Items
{
    public class InvisibleTile : Item
    {
        private const ushort GMItemId = 0x36FF;

        public override string DefaultName => "invisible tile";

        [Constructible]
        public InvisibleTile()
            : base(0x2198)
        {
            Movable = false;
        }

        public InvisibleTile(Serial serial)
            : base(serial)
        {
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
                BinaryPrimitives.WriteUInt16BigEndian(buffer[8..10], GMItemId);
            }
            else
            {
                length = OutgoingItemPackets.CreateWorldItem(buffer, this);
                BinaryPrimitives.WriteUInt16BigEndian(buffer[7..2], GMItemId);
            }

            ns.Send(buffer[..length]);
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(1);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadEncodedInt();
        }
    }
}

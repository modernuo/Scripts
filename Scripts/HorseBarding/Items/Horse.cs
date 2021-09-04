using Server.Items;

namespace Server.Mobiles
{
	[TypeAlias(
        "Server.Mobiles.BrownHorse",
        "Server.Mobiles.DirtyHorse",
        "Server.Mobiles.GrayHorse",
        "Server.Mobiles.TanHorse"
    )]
    
    public class Horse : BaseMount
    {
        private static readonly int[] m_IDs =
            {
            0xC8, 0x3E9F,
            0xE2, 0x3EA0,
            0xE4, 0x3EA1,
            0xCC, 0x3EA2
            };

        private bool m_BardingExceptional;
        private Mobile m_BardingCrafter;
        private int m_BardingHP;
        private bool m_HasBarding;
        private CraftResource m_BardingResource;
        public Body OrigBody;

        [Constructible]
        public Horse(string name = "a horse") : base(
            name,
            0xE2,
            0x3EA0,
            AIType.AI_Animal,
            FightMode.Aggressor,
            10,
            1,
            0.2,
            0.4
        )
        {
            BaseSoundID = 0xA8;

            var random = Utility.Random(4);

            Body = m_IDs[random * 2];
            ItemID = m_IDs[random * 2 + 1];
            OrigBody = Body;

            SetStr(22, 98);
            SetDex(56, 75);
            SetInt(6, 10);

            SetHits(28, 45);
            SetMana(0);

            SetDamage(3, 4);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 15, 20);

            SetSkill(SkillName.MagicResist, 25.1, 30.0);
            SetSkill(SkillName.Tactics, 29.3, 44.0);
            SetSkill(SkillName.Wrestling, 29.3, 44.0);

            Fame = 300;
            Karma = 300;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 29.1;
        }

        public Horse(Serial serial) : base(serial)
        {
        }

        public override string CorpseName => "a horse corpse";

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile BardingCrafter
        {
            get => m_BardingCrafter;
            set
            {
                m_BardingCrafter = value;
                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool BardingExceptional
        {
            get => m_BardingExceptional;
            set
            {
                m_BardingExceptional = value;
                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int BardingHP
        {
            get => m_BardingHP;
            set
            {
                m_BardingHP = value;
                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool HasBarding
        {
            get { return m_HasBarding; }
            set
            {
                m_HasBarding = value;

                if (m_HasBarding)
                {
                    Hue = CraftResources.GetHue(m_BardingResource);
                    Body = 284;
                    ItemID = 0x3E92;
                }
                else
                {
                    Hue = 0;
                    Body = OrigBody;
                    for (var i = 0; i < m_IDs.Length; i += 2)
                    {
                        if (m_IDs[i] == OrigBody)
                        {
                            ItemID = m_IDs[i + 1];
                            break;
                        }
                    }
                }
                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource BardingResource
        {
            get => m_BardingResource;
            set
            {
                m_BardingResource = value;

                if (m_HasBarding)
                {
                    Hue = CraftResources.GetHue(value);
                }

                InvalidateProperties();
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int BardingMaxHP => m_BardingExceptional ? 2500 : 1000;

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (m_HasBarding && m_BardingExceptional && m_BardingCrafter != null)
                list.Add(1060853, m_BardingCrafter.Name); // armor exceptionally crafted by ~1_val~
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(1); // version

            writer.Write(m_BardingExceptional);
            writer.Write(m_BardingCrafter);
            writer.Write(m_HasBarding);
            writer.Write(m_BardingHP);
            writer.Write((int)m_BardingResource);
            writer.WriteEncodedInt(OrigBody);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();

            switch (version)
            {
                case 1:
                    {
                        m_BardingExceptional = reader.ReadBool();
                        m_BardingCrafter = reader.ReadEntity<Mobile>();
                        m_HasBarding = reader.ReadBool();
                        m_BardingHP = reader.ReadInt();
                        m_BardingResource = (CraftResource)reader.ReadInt();
                        OrigBody = reader.ReadEncodedInt();
                        break;
                    }
            }

            if (BaseSoundID == -1)
            {
                BaseSoundID = 0x16A;
            }
        }
    }
}           

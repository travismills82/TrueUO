using Server.Guilds;
using Server.Mobiles;
using System;
using System.Collections.Generic;

namespace Server.Engines.VvV
{
    public class VvVGuildStats
    {
        public Guild Guild { get; }
        public int Score { get; set; }
        public int Kills { get; set; }
        public int ReturnedSigils { get; set; }

        public VvVGuildStats(Guild g)
        {
            Guild = g;
        }

        public void Serialize(GenericWriter writer)
        {
            writer.Write(0);

            writer.Write(Score);
            writer.Write(Kills);
            writer.Write(ReturnedSigils);
        }

        public VvVGuildStats(Guild g, GenericReader reader)
        {
            reader.ReadInt();

            Guild = g;

            Score = reader.ReadInt();
            Kills = reader.ReadInt();
            ReturnedSigils = reader.ReadInt();
        }
    }

    public class BattleTeam : IComparable<BattleTeam>
    {
        public Guild Guild { get; }
        public int Score { get; set; }
        public int Silver { get; set; }

        public int Kills { get; set; }
        public int Assists { get; set; }
        public int Deaths { get; set; }
        public int Stolen { get; set; }

        public int ReturnedSigils => ViceReturned + VirtueReturned;

        public int ViceReturned { get; set; }
        public int VirtueReturned { get; set; }

        public int Disarmed { get; set; }

        public List<VvVPlayerBattleStats> PlayerStats { get; }

        public int CompareTo(BattleTeam team)
        {
            if (Score > team.Score)
                return -1;

            if (Score == team.Score)
                return 0;

            return 1;
        }

        public BattleTeam(Guild g)
        {
            Guild = g;

            PlayerStats = new List<VvVPlayerBattleStats>();
        }

        public BattleTeam(GenericReader reader)
        {
            reader.ReadInt();

            PlayerStats = new List<VvVPlayerBattleStats>();

            Guild = reader.ReadGuild() as Guild;
            Score = reader.ReadInt();
            Silver = reader.ReadInt();

            Kills = reader.ReadInt();
            Assists = reader.ReadInt();
            Deaths = reader.ReadInt();
            Stolen = reader.ReadInt();

            ViceReturned = reader.ReadInt();
            VirtueReturned = reader.ReadInt();
            Disarmed = reader.ReadInt();

            int count = reader.ReadInt();
            for (int i = 0; i < count; i++)
            {
                PlayerMobile pm = reader.ReadMobile() as PlayerMobile;
                VvVPlayerBattleStats stats = new VvVPlayerBattleStats(reader, pm);

                if (pm != null)
                    PlayerStats.Add(stats);
            }
        }

        public void Serialize(GenericWriter writer)
        {
            writer.Write(0);

            writer.Write(Guild);
            writer.Write(Score);
            writer.Write(Silver);

            writer.Write(Kills);
            writer.Write(Assists);
            writer.Write(Deaths);
            writer.Write(Stolen);

            writer.Write(ViceReturned);
            writer.Write(VirtueReturned);
            writer.Write(Disarmed);

            writer.Write(PlayerStats.Count);
            PlayerStats.ForEach(stats =>
                {
                    writer.Write(stats.Player);
                    stats.Serialize(writer);
                });
        }
    }

    public class VvVPlayerBattleStats
    {
        public PlayerMobile Player { get; }
        public double Points { get; }

        public int Kills { get; set; }
        public int Assists { get; set; }
        public int Deaths { get; set; }
        public int Stolen { get; set; }

        public int ReturnedSigils => ViceReturned + VirtueReturned;

        public int ViceReturned { get; set; }
        public int VirtueReturned { get; set; }

        public int Disarmed { get; set; }

        public VvVPlayerBattleStats(PlayerMobile pm)
        {
            Player = pm;
        }

        public void Serialize(GenericWriter writer)
        {
            writer.Write(1);

            writer.Write(Points);

            writer.Write(Kills);
            writer.Write(Assists);
            writer.Write(Deaths);
            writer.Write(Stolen);
            writer.Write(ViceReturned);
            writer.Write(VirtueReturned);
            writer.Write(Disarmed);
        }

        public VvVPlayerBattleStats(GenericReader reader, PlayerMobile pm)
        {
            int version = reader.ReadInt();

            Player = pm;
            Points = reader.ReadDouble();

            if (version == 0)
                reader.ReadInt();

            Kills = reader.ReadInt();
            Assists = reader.ReadInt();
            Deaths = reader.ReadInt();
            Stolen = reader.ReadInt();
            ViceReturned = reader.ReadInt();
            VirtueReturned = reader.ReadInt();
            Disarmed = reader.ReadInt();
        }
    }

    /// <summary>
    /// Obsolete, no longer used.  Left in for serialization purposes
    /// </summary>
    public class VvVGuildBattleStats : IComparable<VvVGuildBattleStats>
    {
        public Guild Guild { get; }
        public double Points { get; }

        public int Silver { get; }
        public int Kills { get; }
        public int Assists { get; }
        public int Deaths { get; }
        public int Stolen { get; }

        public int ReturnedSigils => ViceReturned + VirtueReturned;

        public int ViceReturned { get; }
        public int VirtueReturned { get; }

        public int Disarmed { get; }

        public VvVGuildBattleStats(Guild g)
        {
            Guild = g;
        }

        public int CompareTo(VvVGuildBattleStats stats)
        {
            if (Points > stats.Points)
                return 1;

            if (Points == stats.Points)
                return 0;

            return -1;
        }

        public void Serialize(GenericWriter writer)
        {
            writer.Write(0);

            writer.Write(Points);

            writer.Write(Silver);
            writer.Write(Kills);
            writer.Write(Assists);
            writer.Write(Deaths);
            writer.Write(Stolen);
            writer.Write(ViceReturned);
            writer.Write(VirtueReturned);
            writer.Write(Disarmed);
        }

        public VvVGuildBattleStats(GenericReader reader, Guild g)
        {
            reader.ReadInt();

            Guild = g;
            Points = reader.ReadDouble();

            Silver = reader.ReadInt();
            Kills = reader.ReadInt();
            Assists = reader.ReadInt();
            Deaths = reader.ReadInt();
            Stolen = reader.ReadInt();
            ViceReturned = reader.ReadInt();
            VirtueReturned = reader.ReadInt();
            Disarmed = reader.ReadInt();
        }
    }
}

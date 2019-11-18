using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace XNArtillery
{
    enum MatchTypes
    {
        Null,
        PvC,
        PvP,
        Lan,
        CvC
    }

    [Serializable()]
    class MatchSettings
    {
        public int turn;
        public int windInfluence;
        public int CPUIntelligence;
        public string opponentName;

        public MatchTypes matchType;
        public IPEndPoint opponentEndPoint;

        public MatchSettings()
        {
            turn = Global.Random(2);
            windInfluence = 0;
            CPUIntelligence = 0;
            opponentName = "";
            matchType = MatchTypes.Null;
            opponentEndPoint = null;
        }

        public MatchSettings(MatchSettings settings, string myName)
        {
            turn = settings.turn;
            windInfluence = settings.windInfluence;
            CPUIntelligence = 0;
            opponentName = myName;
            matchType = settings.matchType;
            opponentEndPoint = null;
        }
    }
}

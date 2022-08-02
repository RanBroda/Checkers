using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public class Player
    {
        private string m_PlayerName;
        private int m_TotalPlayerScore;
        private int m_PlayerLeftCheckers;
        private char m_PlayersCheckerSymbol;
        private string m_PlayerPreviousMove;
        private string m_PlayerNextMove;
        private Checker[] m_PlayersCheckers;
        private bool m_IsComputer;

        public string PlayerName { get; set; }

        public int TotalPlayerScore { get; set; }

        public int PlayerLeftCheckers { get; set; }

        public char PlayersCheckerSymbol { get; set; }

        public string PlayerPreviousMove { get; set; }

        public string PlayerNextMove { get; set; }

        public Checker[] PlayersCheckers { get; set; }

        public bool IsComputer { get; set; }
    }
}

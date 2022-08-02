using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public class Checker
    {
        private Player m_checkerOwner;
        private List<(int, int)> m_possibleValidMoves;
        private bool m_isKing = false;
        private bool m_isCaptured = false;
        private int m_Row;
        private int m_Column;

        public Player CheckerOwner { get; set; }
        public List<(int, int)> PossibleValidMoves
        {
            get
            {
                if (m_possibleValidMoves == null)
                {
                    m_possibleValidMoves = new List<(int, int)>();
                }
                return m_possibleValidMoves;
            }
            set
            {
                m_possibleValidMoves = value;

            }
        }

        public bool IsKing { get; set; }
        public bool IsCaptured { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

    }
}

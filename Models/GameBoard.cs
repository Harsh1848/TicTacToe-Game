using System;

namespace TicTacToe_Game.Models
{
    public class Game
    {
        public string[] Board { get; private set; }
        public string Winner { get; private set; }
        public string CurrentPlayer { get; private set; }
        public string PlayerX { get; set; }
        public string PlayerO { get; set; }
        public int ScoreX { get; private set; }
        public int ScoreO { get; private set; }
        public bool IsComputerPlayer { get; set; }

        public Game()
        {
            Reset();
        }

        public void MakeMove(int position)
        {
            if (Board[position] == null && Winner == null)
            {
                Board[position] = CurrentPlayer;

                if (CheckWin())
                {
                    Winner = CurrentPlayer;
                    UpdateScore();
                }
                else if (Array.TrueForAll(Board, b => b != null))
                {
                    Winner = "Draw";
                }

                CurrentPlayer = CurrentPlayer == "X" ? "O" : "X";
            }
        }

        public void Reset()
        {
            Board = new string[9];
            Winner = null;
            CurrentPlayer = "X";
        }

        public void ResetScores()
        {
            ScoreX = 0;
            ScoreO = 0;
        }

        private bool CheckWin()
        {
            int[][] lines = new int[][]
            {
                new int[] { 0, 1, 2 },
                new int[] { 3, 4, 5 },
                new int[] { 6, 7, 8 },
                new int[] { 0, 3, 6 },
                new int[] { 1, 4, 7 },
                new int[] { 2, 5, 8 },
                new int[] { 0, 4, 8 },
                new int[] { 2, 4, 6 },
            };

            foreach (var line in lines)
            {
                if (Board[line[0]] != null &&
                    Board[line[0]] == Board[line[1]] &&
                    Board[line[0]] == Board[line[2]])
                {
                    return true;
                }
            }

            return false;
        }

        private void UpdateScore()
        {
            if (Winner == "X") ScoreX++;
            if (Winner == "O") ScoreO++;
        }
    }
}

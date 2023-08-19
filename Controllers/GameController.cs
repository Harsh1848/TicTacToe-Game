using Microsoft.AspNetCore.Mvc;
using System;
using TicTacToe_Game.Models;

namespace TicTacToe_Game.Controllers
{
    public class GameController : Controller
    {
        private static Game _game = new Game();

        public IActionResult Index()
        {
            return View(_game);
        }

        [HttpPost]
        public IActionResult MakeMove(int position)
        {
            _game.MakeMove(position);

            if (_game.IsComputerPlayer && _game.Winner == null)
            {
                ComputerMove();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Reset()
        {
            _game.Reset();
            return RedirectToAction("Index");
        }

        public IActionResult SetComputerPlayer(bool isComputerPlayer)
        {
            _game.IsComputerPlayer = isComputerPlayer;
            _game.Reset();
            return RedirectToAction("Index");
        }

        public IActionResult SetPlayerNames(string playerX, string playerO)
        {
            _game.PlayerX = playerX;
            _game.PlayerO = playerO;
            return RedirectToAction("Index");
        }

        public IActionResult ResetScores()
        {
            _game.ResetScores();
            return RedirectToAction("Index");
        }

        private void ComputerMove()
        {
            int bestScore = int.MinValue;
            int bestMove = -1;

            for (int i = 0; i < _game.Board.Length; i++)
            {
                if (_game.Board[i] == null)
                {
                    _game.Board[i] = "O";
                    int score = Minimax(_game.Board, 0, false);
                    _game.Board[i] = null;

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = i;
                    }
                }
            }

            _game.MakeMove(bestMove);
        }

        private int Minimax(string[] board, int depth, bool isMaximizing)
        {
            string winner = CheckWinner(board);
            if (winner != null)
            {
                return Score(winner);
            }

            if (isMaximizing)
            {
                int bestScore = int.MinValue;
                for (int i = 0; i < board.Length; i++)
                {
                    if (board[i] == null)
                    {
                        board[i] = "O"; // Computer's move
                        int score = Minimax(board, depth + 1, false);
                        board[i] = null;
                        bestScore = Math.Max(score, bestScore);
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                for (int i = 0; i < board.Length; i++)
                {
                    if (board[i] == null)
                    {
                        board[i] = "X"; // Human's move
                        int score = Minimax(board, depth + 1, true);
                        board[i] = null;
                        bestScore = Math.Min(score, bestScore);
                    }
                }
                return bestScore;
            }
        }

        private string CheckWinner(string[] board)
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
                if (board[line[0]] != null &&
                    board[line[0]] == board[line[1]] &&
                    board[line[0]] == board[line[2]])
                {
                    return board[line[0]];
                }
            }

            if (Array.TrueForAll(board, b => b != null))
            {
                return "Draw";
            }

            return null;
        }

        private int Score(string winner)
        {
            if (winner == "O") return 1;
            if (winner == "X") return -1;
            return 0;
        }
    }
}

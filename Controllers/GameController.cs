using Microsoft.AspNetCore.Mvc;
using TicTacToe_Game.Models;

namespace TicTacToe_Game.Controllers
{ 
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            var gameBoard = new GameBoard
            {
                Board = new char[3, 3],
                CurrentPlayer = 'X'
            };
            
            return View(gameBoard);
        }

        [HttpGet]
        public IActionResult MakeMove(int row, int col)
        {
            var gameBoard = new GameBoard();

            if (TempData.ContainsKey("Board") && TempData["Board"] is char[,] board)
            {
                gameBoard.Board = board;
            }
            else
            {
                gameBoard.Board = new char[3, 3];
            }

            if (TempData.ContainsKey("CurrentPlayer") && TempData["CurrentPlayer"] is char currentPlayer)
            {
                gameBoard.CurrentPlayer = currentPlayer;
            }
            else
            {
                gameBoard.CurrentPlayer = 'X';
            }


            TempData["Board"] = gameBoard.Board;
            TempData["CurrentPlayer"] = gameBoard.CurrentPlayer;

            return RedirectToAction("Index");
        }

        private bool CheckForWinner(char[,] board, char player)
        {
            // Check rows, columns, and diagonals for a winner
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player)
                    return true;

                if (board[0, i] == player && board[1, i] == player && board[2, i] == player)
                    return true;
            }

            if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player)
                return true;

            if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player)
                return true;

            return false;
        }
    }
}


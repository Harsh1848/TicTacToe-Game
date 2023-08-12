namespace TicTacToe_Game.Models
{
    public class GameBoard
    {
        public char[,] Board { get; set; } = new char[3, 3];
        public char CurrentPlayer { get; set; } = 'X';
        public string Winner { get; set; } = null;
    }
}



using System;
namespace TwoD_Game {
	public class User_Player :Player {
		public User_Player (String name, int row, int col, String symbol, TheStateSpace ss)
		: base (name, row, col, symbol, ss) { 
		}
		public override void playerMovementController () {
			Console.WriteLine ("\nPlease enter 1 for right 2 for left 3 for down 4 for up \n");
			int commandNum = Convert.ToInt32(Console.ReadLine()) - 1;
			move ((dir)commandNum);

		}

	}
}

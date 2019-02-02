using System;
namespace TwoD_Game {
	public class Random_Player : Player {
		public Random_Player (String name, int row, int col, String symbol, TheStateSpace ss) 
		: base(name, row, col, symbol, ss) {
		}

		public override void playerMovementController () {
			Random rnd = new Random ();
			int commandNum = rnd.Next (0, 4);
			move ((dir)commandNum);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Collections;
namespace TwoD_Game {
	public abstract class Player{
		public int coins;
		public String name;
		public int row;
		public int col;
		public String symbol;
		public TheStateSpace ss;  //every Player Needs To Know The State SPace
		public bool alive;
		public bool winner;
		public Player (String name, int row, int col, String symbol, TheStateSpace ss) {
			coins = 0;
			this.name = name;
			this.row = row;
			this.col = col;
			this.symbol = symbol;
			alive = true;
			this.ss = ss;
			winner = false;
		}
		// this one has actual effect
		public void symbolInterpreter (String sym) {
			switch (sym) {
				case Symbols.left:
					col--;
					symbolInterpreter (ss.stateSpace [row, col].symbol);
					break;
				case Symbols.right:
					col++;
					symbolInterpreter (ss.stateSpace [row, col].symbol);
					break;
				case Symbols.down:
					row++;
					symbolInterpreter (ss.stateSpace [row, col].symbol);
					break;
				case Symbols.up:
					row--;
					symbolInterpreter (ss.stateSpace [row, col].symbol);
					break;
				case Symbols.minus90Coins:
					coins -= 90;
					break;
				case Symbols.plus50Coins:
					coins += 50;
					break;
				case Symbols.plusFiveCoins:
					coins += 5;
					break;
				case Symbols.minusFiveCoins:
					coins -= 5;
					break;
				case Symbols.neutralTile: //do nothing
					break;
				case Symbols.youDie:
					alive = false;
					break;
				case Symbols.end:
					winner = true;
					break;
			}
		}

		public void move (dir commandNum) {
			switch (commandNum) {
				case dir.right:
					if (col < ss.stateSpace.GetLength (1)-1)
						col++;
					break;
				case dir.left:
					if (col > 0)
						col--;
					break;
				case dir.down:
					if (row < ss.stateSpace.GetLength (0)-1)
						row++;
					break;
				case dir.up:
					if (row > 0)
						row--;
					break;
				default:
					Console.WriteLine ("problem with the move function in Player class");
					break;
			}
			symbolInterpreter (ss.stateSpace [row, col].symbol);
		}

		public abstract void playerMovementController ();
	
			public void showPlayerStats () {
			Console.WriteLine (name + " stats are:\n" +
				"has this much coins " + coins + "\n" +
				"is he alive? " + alive + "\n" +
				"did he win? " + winner);
			Console.WriteLine ("\n");
		}
	}
}

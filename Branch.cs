using System;
using System.Collections.Generic;

namespace TwoD_Game {

	public enum dir {
		right, left, down, up, errorFlag
	}
	public class Branch {
		public List<Tile> tiles;
		public int value;
		public dir brIdentity;

		public Branch (Tile dirTile, int _row, int _col ) {
			tiles = new List<Tile> ();
			value = 0;
			brIdentity = determineTilePosRelativeToPosGiven (dirTile, _row, _col);
			tiles.Add (dirTile);
			if (brIdentity == dir.errorFlag)
			Environment.Exit (1);
		}

		private dir determineTilePosRelativeToPosGiven (Tile t,int _row, int _col) {
			if (t.row > _row)
				return dir.down;
			else if (t.row < _row)
				return dir.up;
			else if (t.col > _col)
				return dir.right;
			else if (t.col < _col)
				return dir.left;
			return dir.errorFlag;
				 
		}

		public void calcValue () {
			Tile t = new Tile ();
			int numberOfRuns = tiles.Count / 2 ; // number of tiles in any branch is  multiple of 2
			for (int i = 1, j=0; j < numberOfRuns; i += 2, j++) { //first tile is landTile, second tile is direction
				t = tiles [i];
				inspectNonDirectionTile (tiles [i]);
				calcManhattenDistance (tiles [i]);
			}
		}

		private void calcManhattenDistance (Tile t) {
			int md = (t.row + t.col ) * 10 ; //10 here is scaling factor
			value += md;
		}

		private void inspectNonDirectionTile (Tile tile) {
			switch (tile.symbol) {
				case Symbols.minus90Coins:
					value -= 90;
					break;
				case Symbols.minusFiveCoins:
					value -= 5;
					break;
				case Symbols.neutralTile:
					value += 1;
					break;
				case Symbols.plus50Coins:
					value += 50;
					break;
				case Symbols.plusFiveCoins:
					value += 5;
					break;
				case Symbols.end:
					value += 500;
					break;
				default:
					Console.WriteLine ("wrong entry to inspectTile Method in Smart Player");
					Environment.Exit (-1); 
					break;
			}
		}

	}
}

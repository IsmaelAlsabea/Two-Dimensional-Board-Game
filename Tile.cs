using System;
namespace TwoD_Game {
	public class Tile {
		public String symbol;
		public int row;
		public int col;
		public Tile(String s, int r, int c) {
			symbol = s;
			row = r;
			col = c;
		}

		public Tile () {
			symbol = String.Empty;
			row = -1;
			col = -1;
		}
	}
}

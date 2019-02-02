using System;
using System.Collections.Generic;
namespace TwoD_Game {
	public class Smart_Player : Player {
		public Smart_Player (String name, int row, int col, String symbol, TheStateSpace ss)
		: base (name, row, col, symbol, ss) {
		}

		public override void playerMovementController () {
			dir d = dir.errorFlag;
			int tileNum = -1;
			int bestVal = 0;
			List<Branch> brs = makeBranches (row, col);
			List<Branch> brTemp = new List<Branch> ();
			for (int i = 0; i < brs.Count; i++) {
				if (brs [i].tiles [1].symbol == Symbols.end) {
					bestVal = brs [i].value;
					break;
				}
				brTemp = makeBranches (brs [i].tiles [1].row, brs [i].tiles [1].col);
				tileNum = pickTheBestMove (brTemp);
				if (tileNum == -1)
					continue;
				brs [i].tiles.Add (brTemp [tileNum].tiles [0]);
				brs [i].tiles.Add (brTemp [tileNum].tiles [1]);
				brs [i].calcValue ();
				if (brs [i].value > bestVal)
					bestVal = brs [i].value;
			}
			for (int i = 0; i < brs.Count; i++)
				if (bestVal == brs [i].value) {
					d = brs [i].brIdentity;
					break;
				}
			move (d);
		}

		private List<Branch> makeBranches (int _row, int _col) {
			List<Tile> possibleMoves = getDirectionTiles (_row, _col);
			List<Branch> brs = new List<Branch> ();
			//created the branches, put the first tile in
			for (int i = 0; i < possibleMoves.Count; i++) {
				brs.Add (new Branch (possibleMoves [i], _row, _col));
				//put the first landTile in branch
				brs [i].tiles.Add (getNonDirectionTile (brs [i].tiles [0]));
				//calculate the value
				brs [i].calcValue ();
			}
			return brs;
		}

		private int pickTheBestMove (List<Branch> brList) {
			int bestVal = 0;
			for (int i = 0; i < brList.Count; i++)
				if (bestVal < brList [i].value)
					bestVal = brList [i].value;
			for (int i = 0; i < brList.Count; i++)
				if (bestVal == brList [i].value)
					return i;
			return -1;
		}

		private List<Tile> getDirectionTiles (int _row, int _col) {
			List<Tile> list = new List<Tile> ();

			if (isRightInBoundary (_col))
				if (!RightPointsToCurrentTile (_row, _col))
					if (!pointsToPlayerPos (ss.stateSpace [_row, _col + 1]))
						if (!PointsToADeathTile (ss.stateSpace [_row, _col + 1]))
							list.Add (ss.stateSpace [_row, _col + 1]);
			if (isLeftInBoundary (_col))
				if (!LeftPointsToCurrentTile (_row, _col))
					if (!pointsToPlayerPos (ss.stateSpace [_row, _col - 1]))
						if (!PointsToADeathTile (ss.stateSpace [_row, _col - 1]))
							list.Add (ss.stateSpace [_row, _col - 1]);
			if (isUpInBoundary (_row))
				if (!UpPointsToCurrentTile (_row, _col))
					if (!pointsToPlayerPos (ss.stateSpace [_row - 1, _col]))
						if (!PointsToADeathTile (ss.stateSpace [_row - 1, _col]))
							list.Add (ss.stateSpace [_row - 1, _col]);
			if (isDownInBoundary (_row))
				if (!DownPointsToCurrentTile (_row, _col))
					if (!pointsToPlayerPos (ss.stateSpace [_row + 1, _col]))
						if (!PointsToADeathTile (ss.stateSpace [_row + 1, _col]))
							list.Add (ss.stateSpace [_row + 1, _col]);
			return list;
		}
		// this one returns the tile behind the direction tile, IT DOES NOT INSPECT IT
		private Tile getNonDirectionTile (Tile t) {
			Tile s = new Tile ("", 0, 0);
			switch (t.symbol) {
				case Symbols.left:
					s = ss.stateSpace [t.row, t.col - 1];
					break;
				case Symbols.right:
					s = ss.stateSpace [t.row, t.col + 1];
					break;
				case Symbols.down:
					s = ss.stateSpace [t.row + 1, t.col];
					break;
				case Symbols.up:
					s = ss.stateSpace [t.row - 1, t.col];
					break;
				default:
					Console.WriteLine ("wrong entry to getNonDirectionTile Function in Smart Player");
					break;
			}
			return s;
		}

		private bool pointsToPlayerPos (Tile t) {
			t = getNonDirectionTile (t);
			if (t.row == row && t.col == col)
				return true;
			return false;
		}

		private bool PointsToADeathTile (Tile t) {
			t = getNonDirectionTile (t);
			if (t.symbol.Equals (Symbols.youDie))
				return true;
			return false;
		}

		private bool isLeftInBoundary (int _col) {
			if (_col > 0)
				return true;
			return false;
		}

		private bool isRightInBoundary (int _col) {
			if (_col < ss.stateSpace.GetLength (1) - 1)
				return true;
			return false;
		}
		private bool isDownInBoundary (int _row) {
			if (_row < ss.stateSpace.GetLength (0) - 1)
				return true;
			return false;
		}

		private bool isUpInBoundary (int _row) {
			if (_row > 0)
				return true;
			return false;
		}

		private bool LeftPointsToCurrentTile (int _row, int _col) {
			if (ss.stateSpace [_row, _col - 1].symbol == Symbols.right)
				return true;
			return false;
		}

		private bool RightPointsToCurrentTile (int _row, int _col) {
			if (ss.stateSpace [_row, _col + 1].symbol == Symbols.left)
				return true;
			return false;
		}
		private bool UpPointsToCurrentTile (int _row, int _col) {
			if (ss.stateSpace [_row - 1, _col].symbol == Symbols.down)
				return true;
			return false;
		}

		private bool DownPointsToCurrentTile (int _row, int _col) {
			if (ss.stateSpace [_row + 1, _col].symbol == Symbols.up)
				return true;
			return false;
		}

	}

}

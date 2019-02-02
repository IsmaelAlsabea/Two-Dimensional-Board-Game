using System;
using System.IO;
namespace TwoD_Game {
	public class TheStateSpace {
		public Tile [,] stateSpace;
		public TheStateSpace (String path) {
			String str = String.Empty;
			String [] lines = File.ReadAllLines (path);
			stateSpace = new Tile [lines.Length, sizeOfCol(lines [0].Length)];
			int y = 0;
			for (int x = 0; x < lines.Length; x++) {
				y = 0;
				while(lines[x].IndexOf(' ')!=-1){
					str = lines [x].Substring (0, lines [x].IndexOf (' '));
					
					stateSpace [x, y] = new Tile (str, x, y);
					lines [x] = lines [x].Remove (0, str.Length+1); //the plus 1 is for the space
					y++;
				}
			 //for the last tile in the string line
				str = lines [x];
				stateSpace [x, y] = new Tile (str, x, y);
			}
		}

		private int sizeOfCol (int length) {
			int x = (length + 1) / 3;
			//the length of the column consist of 2 chars for each tile + number of tiles -1 for 
			//spaces, Thus the equation is X+X+Y, which X is half the number of characters and Y
			// is the number of space characters Therefore the equation would be X+X+X-1= colSize
			// 3X= colSize+1 => X=(colSize+1)/3

			return x;
		}
	}
}
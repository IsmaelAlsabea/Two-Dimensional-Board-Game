using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TwoD_Game {
	class Driver {
		public static void Main (string [] args) {

			string path = string.Empty;
			path = getFilePath ();

			if (path == string.Empty)
				System.Environment.Exit (1);

			TheStateSpace ss = new TheStateSpace (path);
			Player p1 = new Smart_Player ("Mr. Smart", 0, 0, "P1", ss);
			Player p2 = new Random_Player ("Mr. Random", 0, 2, "P2", ss);
			Player p3 = new User_Player ("Mr. User", 2, 0, "P3", ss);
			List<Player> players = new List<Player> { p1, p2, p3 };
			bool aPlayerWon = false;
			display (ss, players);
			while (true) {
				if (aPlayerWon || players.Count == 0)
					break;
				for (int i = 0; i < players.Count; i++) {
					players [i].playerMovementController ();
					if (players [i].winner == true)
						aPlayerWon = true;
					else if (players [i].alive == false)
						players.Remove (players [i]);
				}
				display (ss, players);
			}
		}

		private static void display (TheStateSpace ss, List<Player> players) {
			Console.WriteLine ();
			for (int x = 0; x < ss.stateSpace.GetLength (0); x++) {
				for (int y = 0; y < ss.stateSpace.GetLength (1); y++) {
					int p = -1;
					for (int i = 0; i < players.Count; i++) {
						if (players [i].row == x && players [i].col == y) {
							p = i;
							break;
						}
					}
					if (p != -1)
						Console.Write (players [p].symbol);
					else
						Console.Write (ss.stateSpace [x, y].symbol);
				}
				Console.WriteLine ();
			}
			Console.WriteLine ();
			for (int i = 0; i < players.Count; i++)
				players [i].showPlayerStats ();
		}

		/*		public static string getFilePath () {
					string path = string.Empty;
					OpenFileDialog fdlg = new OpenFileDialog ();
					fdlg.Title = "select the text file that contains the board";
					fdlg.InitialDirectory = @"/home";
					fdlg.Filter = "txt files (*.txt)|*.txt";
					if (fdlg.ShowDialog () == DialogResult.OK)
						path = fdlg.FileName;
					fdlg.Dispose ();

					return path;
				}

			*/

		public static string getFilePath () {
			string path = string.Empty;
			Console.WriteLine ("Please provide the path to the file that contains the game board\n\n" +
				" For Example: /home/ismael/Projects/2D_Game/2D_Game/StateSpace.txt ");
			path = Console.ReadLine ();
			return path;
		}
	}
}


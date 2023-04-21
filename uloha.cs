public class Uloha_a
{
	// zadání
  static string text;
	static List<List<int>> grid;
	public static void Set(API api){
		text = api.getValJSON("text");
	  grid = api.getValJSON("grid").get2dArrStr();
	}
	// řešení
	public static string Solve(){
		int n = grid.Count;
		string abc="ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		var resGrid = new List<List<string>>();
		List<string> row;
		int current_row=0,ch=0;
		bool done = false;
		Random rnd = new Random();

		while(!done){
			// napln resGrid nahodnymi znaky z abc
			for(int i = 0; i < n; i++){
				row=new List<string>();
				for(int j = 0; j < n; j++)
					row.Add(abc.getVal(rnd.Next(0, abc.Length)));
				resGrid.Add(row);
			}
			resGrid.Add(new List<string>());

			// vloz text do resGrid po radcich podle grid
			for(int i = 0; i < n; i++){
				for(int j = 0; j < grid[i].Count; j++){
					if(ch==text.Length){ // skonci, kdyz si naplnil text
						done=true;
						break;
					}
					resGrid[i+current_row][grid[i][j]]=text.getVal(ch);
					ch++;
				}
			}
			current_row+=n+1;
		}
		return resGrid.ToStr();
	}
}


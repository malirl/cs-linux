public class Uloha_b
{
	static string txt;
	// zadání
	public static void Set(string str){
		txt=str;

	}
	// řešení
	public static string Solve(){
		Queue<int> q = new Queue<int>();
		Dictionary<string, int> hash = new Dictionary<string, int>();	

		// Add some data.
		hash.Add("diamond", 500);
		hash.Add("ruby", 200);
		hash.Add("pearl", 100);

		// Get value that exists.
		int value1 = hash["diamond"];
		Console.WriteLine("get DIAMOND: " + value1);

		// Get value that does not exist.
		hash.TryGetValue("coal", out int value2);
		Console.WriteLine("get COAL: " + value2);

		// Loop over items in collection.
		foreach (KeyValuePair<string, int> pair in hash)
		{
			Console.WriteLine("KEY: " + pair.Key);
			Console.WriteLine("VALUE: " + pair.Value);
		}

		IEnumerable<int> result = Enumerable.Range(0, 5);
		var linq = result.Where(x=>x==3).Select((x,i)=>x*i); 

		// Part 2: loop over IEnumerable.
		foreach (int value in result)
			Console.WriteLine("IENUMERABLE: "+value);

		// a[^i]
		// h.ToString().PadLeft(2,'0')






		Console.WriteLine(txt);
		return null;
	}

	// static void sth(){
	// throw new NotImplementedException();
	// }


}


using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Net;

public class Program{
	static API api = new API();
	static GUI gui = new GUI(); 
	static Uloha[] ulohy = {
		new Uloha("grid-simple/encode",taskState.resolved),
		new Uloha("b",taskState.unresolved),
		new Uloha("c",taskState.unresolved),


	};
	static string res="";

	public static void Main(string[] args){

		api.URL = "https://sifrovani.maturita.delta-www.cz/";

		if(args.Length==0){
			Console.WriteLine("chybi parametr v string[] args! na linuxu:\\n'mono res/out.exe <nazev_ulohy>'\\n'mono res/out.exe all' -> spusti vsechny ulohy v terminalu\\n'mono res/out.exe gui' -> okenni aplikace\\n'mono res/out.exe' -> vypis stav uloh");
			foreach(var uloha in ulohy){
				switch(uloha.status){
					case taskState.unresolved:
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine(uloha.name+" (nedoresena)");
						break;
					case taskState.resolved:
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine(uloha.name+" (funkcni)");
						break;
				}
			}
			return;
		}

		switch(args[0]){
			case "gui":
				gui.set();
				gui.show();
				return;
			case "all":
				foreach(var uloha in ulohy)
					Main(new string[]{uloha.name});
				return;
			default:
				if((res=solveTask(args[0]))==null)
					return;
				break;
		}

		api.saveToken();
		// verify 
		api.URLencoded = "encoded=" + res;
		api.EndPoint = "verify";
		api.post();
		bool verified = api.getValJSON("success")=="true";
		if(verified)
			Console.WriteLine("zakodovani se zdarilo");
		else
			Console.WriteLine("zakodovani se nezdarilo");

	}

	// cs grid-simple/encode,...
	static string solveTask(string task){
		Console.WriteLine("\\nULOHA: "+task);
		switch(task){
			case "grid-simple/encode":
				api.EndPoint = "grid-simple/encode";
				api.get();
				Uloha_a.Set(api);
				return Uloha_a.Solve();
			case "b":
				Console.WriteLine("Cesta k souboru:");
				var file=new StreamReader(Console.ReadLine()); // ../test.txt
				// string line=file.ReadLine();
				// string[] words = line.Split('');
				Uloha_b.Set(file.ReadToEnd());
				return Uloha_b.Solve();
			case "c":
				Uloha_c.Set(api);
				return Uloha_c.Solve();




			default:
				Console.WriteLine("Uloha neexistuje");
				return null;
		}
	}
}
public enum taskState{
	resolved,
	unresolved,
	failed,
}
public class Uloha{
	public string name;
	public taskState status;
	public Uloha(string name,taskState status){
		this.name=name;
		this.status=status;
	}
}

public static class extensions{
	public static string getVal(this string str, int x){
		if(x < 0 || x >= str.Length)
			return "";
		else
			return str.ToCharArray()[x].ToString();
	}
	public static List<List<int>> get2dArrStr(this string str){
		List<List<int>> list = new List<List<int>>();
		Regex regex = new Regex(@"\[((?:-?\d,?)*)\]");
		var match = regex.Match(str);
		int num;
		while (match.Success){ 
			var nums = new List<int>();
			list.Add(nums);
			for(int i = 0; i < match.Groups[1].Value.Split(',').Length; i++)
				if(int.TryParse(match.Groups[1].Value.Split(',')[i], out num))
					nums.Add(num);
			match=match.NextMatch();
		}
		return list;
	}
	public static string ToStr<T>(this List<List<T>> arr)
		=> arr.Select(x => string.Join("", x) + "\\n").ToList().Aggregate((x, y) => x + y);
	//	static string arrToStr<T>(List<List<T>> arr)
	//		=> arr.Select(x => "[" + string.Join(",", x) + "]").ToList().Aggregate((x, y) => x + "," + y);



}


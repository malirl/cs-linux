/// <summary>
/// API class 
/// </summary>
public class API{
	WebClient client;
	string json,token,url,endPoint,urlEncoded;
	public string EndPoint{
		get{
			return endPoint;
		}
		set{
			endPoint=value;
		}
	}
	public string URLencoded{
		get{
			return urlEncoded;
		}
		set{
			urlEncoded=value;
		}
	}
	public string URL{
		get{
			return url;
		}
		set{
			url=value;
		}
	}

	public API(){
		client = new WebClient();
	}
	public bool get(){
		Console.WriteLine("GET: "+url+endPoint);
		json = client.DownloadString(url+endPoint);
		return true;
	}
	public bool post(){
		Console.WriteLine("POST: "+url+endPoint);
		client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
		json=client.UploadString(url+endPoint,"token="+token+"&"+urlEncoded);
		return true;
	}
	public void saveToken(string key="token"){
		token=getValJSON(key);
	}
	public string getValJSON(string key){
		Console.Write("val for key: '"+key);
		string reg = "key\":\"?([^\"]*)(,\"|\",|\"}|})";
		Regex regex = new Regex(reg.Replace("key",key));
		string val=regex.Match(json).Groups[1].Value;
		Console.Write("' is: '"+val+"'\\n");
		return val;
	}
}


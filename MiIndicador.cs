using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace Practico1
{
	public class RequestIndicadores
	{
		private static readonly string svcURL = "https://www.mindicador.cl";
		private static readonly string pathAPI = "api";
		//private static readonly string moneda = "uf";
		private static readonly string fecha = System.DateTime.Now.ToString("dd-MM-yyyy"); //"21-12-2017"; //string.Empty;
		private static JavaScriptSerializer js = new JavaScriptSerializer();


		public static string LlamaServicio(string moneda)
		{
			string EmpResponse = string.Empty;
			var qryString = string.Concat(pathAPI, "/", moneda, "/", fecha);
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(svcURL);
				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage Res = client.GetAsync(qryString).Result;
				if (Res.IsSuccessStatusCode)
				{
					EmpResponse = Res.Content.ReadAsStringAsync().Result;

					var objeto = js.DeserializeObject(EmpResponse);
				}
			}
			return EmpResponse;
		}


		public static string ObtenerUf()
		{
			/*
			 * {
				"version": "1.7.0",
				"autor": "mindicador.cl",
				"codigo": "uf",
				"nombre": "Unidad de fomento (UF)",
				"unidad_medida": "Pesos",
				"serie": [

					{
						"fecha": "2023-08-29T04:00:00.000Z",
						"valor": 36121.01

					}
				]
			}*/

			string response = string.Empty; ;

			var result = LlamaServicio("uf");
			JObject json = JObject.Parse(result);
			response = json["serie"][0]["valor"].ToString();

			return response;
		}


		public static string ObtenerDolar()
		{
			string response = string.Empty; ;

			var result = LlamaServicio("dolar");
			JObject json = JObject.Parse(result);
			response = json["serie"][0]["valor"].ToString();

			return response;
		}
	}
}

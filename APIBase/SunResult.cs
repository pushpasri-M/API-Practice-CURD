using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIBase
{
    public class SunResult
    {
        public static async Task<sunModel> LoadCom(int Comnum = 0)
        {
            string url = "https://api.sunrise-sunset.org/json?lat=36.7201600&lng=-4.4203400&date=today";


            using (HttpResponseMessage reponse = await APIHelper.httpClient.GetAsync(url))
            {
                if (reponse.IsSuccessStatusCode)
                {
                    sunModel comic = await reponse.Content.ReadAsAsync<sunModel>();
                    return comic;
                }
                else
                {
                    throw new Exception(reponse.ReasonPhrase);
                }
            }
        }
    }
}

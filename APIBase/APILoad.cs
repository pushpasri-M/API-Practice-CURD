using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIBase
{
    static public class APILoad
    {
        private static int MaxComNum;
        public static async Task<ComicData> LoadCom(int Comnum=0)
        {
            string url = "";
            if (Comnum > 0)
            {
                url= $"https://xkcd.com/{Comnum}/info.0.json";
            }
            else
            {
               url= "https://xkcd.com/info.0.json";
            }
            using(HttpResponseMessage reponse= await APIHelper.httpClient.GetAsync(url))
            {
                if (reponse.IsSuccessStatusCode)
                {
                    ComicData comic= await reponse.Content.ReadAsAsync<ComicData>();
                    if (Comnum == 0)
                    {
                        MaxComNum = comic.Num;
                    }
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

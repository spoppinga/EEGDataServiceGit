using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using EEGDataService.Models;

namespace EEGDataService
{
   
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EEGController : ApiController
    {
       
        private List<EEGData> test = new List<EEGData>();
        private SQLiteService sqLiteService = new SQLiteService();

        [Route]
        [HttpGet]
        public async Task<IEnumerable> GetEEGData()
        {
            var result = await sqLiteService.GetEEGData();
            return result;
        }




        [Route]
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]IEnumerable<EEGData> value)
        {
            
            await sqLiteService.WriteEEGData(value);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
            Console.WriteLine("Put method called with value = " + value);
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
            Console.WriteLine("Delete method called with id = " + id);
        }
    }
}
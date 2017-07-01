using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
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
        private SQLiteService sqLiteService;

        [Route]
        [HttpGet]
        public async Task<IEnumerable> GetEEGData()
        {
            await sqLiteService.GetEEGData();

            test.Add(new EEGData{Id = 1,RawData = 200});

            test.Add(new EEGData{Id = 2,RawData = 400});
            return test;
        }

        


        // POST api/values 
        public void Post([FromBody]string value)
        {
            Console.WriteLine("Post method called with value = " + value);
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
using System;
using System.Data;

namespace EEGDataService.Models
{
    
    public class EEGData 
    {
        public int RawData { get; set; }
        public int Id { get; set; }
        public DateTime Time { get; set; }
    }
}
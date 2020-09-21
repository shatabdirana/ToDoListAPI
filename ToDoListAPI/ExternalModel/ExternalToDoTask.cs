using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListAPI.ExternalModel
{
    public class ExternalToDoTask
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime? createDate { get;  set; }
        public bool status { get; set; }
        public DateTime? modifiedDate { get; set; }
        public string bucketname { get; set; }
        public int? bucketId { get; set; }
       
    }
}

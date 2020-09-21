using System;
using System.Collections.Generic;

namespace ToDoListAPI.Models
{
    public partial class Bucket
    {
        public Bucket()
        {
            ToDoTask = new HashSet<ToDoTask>();
        }

        public int BucketId { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDateTime { get; set; }

        public virtual ICollection<ToDoTask> ToDoTask { get; set; }
    }
}

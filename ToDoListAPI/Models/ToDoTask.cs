using System;
using System.Collections.Generic;

namespace ToDoListAPI.Models
{
    public partial class ToDoTask
    {
        public int TaskId { get; set; }
        public int? BucketId { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Status { get; set; }

        public virtual Bucket Bucket { get; set; }
    }
}

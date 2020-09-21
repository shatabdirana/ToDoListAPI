using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoListAPI.ExternalModel;
using ToDoListAPI.Models;

namespace ToDoListAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ToDoTasksController : ControllerBase
    {
        private readonly ToDoListAppContext context;
        public ToDoTasksController(ToDoListAppContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IQueryable<ExternalToDoTask> Get()
        {
            var result = from toDoTask in this.context.ToDoTask
                         join bucket in this.context.Bucket on
                         toDoTask.BucketId equals bucket.BucketId
                         into Details
                         from defaultVal in Details.DefaultIfEmpty()
                         select new ExternalToDoTask()
                         {
                             id = toDoTask.TaskId,
                             name = toDoTask.Name,
                             createDate = toDoTask.CreateDateTime,
                             status = toDoTask.Status,
                             modifiedDate = toDoTask.ModifiedDate,
                             bucketname = defaultVal.Name,
                             bucketId = toDoTask.BucketId
                         };
            return result;

        }

        [HttpGet("GetTasks")]
        public IEnumerable<ExternalToDoTask> Get(int id)
        {

            return from toDoTask in this.context.ToDoTask
                   where toDoTask.TaskId == id
                   join bucket in this.context.Bucket on
                   toDoTask.BucketId equals bucket.BucketId
                   into Details
                   from defaultVal in Details.DefaultIfEmpty()
                   select new ExternalToDoTask()
                   {
                       id = toDoTask.TaskId,
                       name = toDoTask.Name,
                       createDate = toDoTask.CreateDateTime,
                       status = toDoTask.Status,
                       modifiedDate = toDoTask.ModifiedDate,
                       bucketname = defaultVal.Name,
                       bucketId = toDoTask.BucketId
                   };

        }

        [HttpGet("GetBucket")]
        public IEnumerable<ExternalToDoTask> GetBucketTasks(int id)
        {

            var result = from toDoTask in this.context.ToDoTask
                         where toDoTask.BucketId == id
                         select new ExternalToDoTask()
                         {
                             id = toDoTask.TaskId,
                             name = toDoTask.Name,
                             createDate = toDoTask.CreateDateTime,
                             status = toDoTask.Status,
                             modifiedDate = toDoTask.ModifiedDate,
                             bucketname = GetBucketName(id),
                             bucketId = id
                         };
            return result;

        }

        [HttpGet("GetBucketName")]
        public string GetBucketName(int id)
        {

            var result = from bucket in this.context.Bucket
                         where bucket.BucketId == id
                         select bucket.Name;
            if (result.Any())
            {

                return result.First().ToString();
            }
            else
            {
                return String.Empty;
            }

        }

        [HttpGet("GetAllBucket")]
        public IEnumerable<ExternalBucket> GetBucketName()
        {

            var result = from bucket in this.context.Bucket
                         select new ExternalBucket
                         {
                             bucketId = bucket.BucketId,
                             bucketName = bucket.Name
                         };
            return result;

        }

        private int GetBucketID(string bucketName)
        {

            var result = from bucket in this.context.Bucket
                         where bucket.Name.ToUpper() == bucketName.ToUpper()
                         select bucket.BucketId;
            return result.Any() ? result.First() : -1;

        }

        [HttpPost]
        public IActionResult Post(ExternalToDoTask toDoTask)
        {
            //int? bucketID = null;
            //if (!String.IsNullOrEmpty(toDoTask.bucketname))
            //{
            //    bucketID = GetBucketID(toDoTask.bucketname);
            //    bucketID = bucketID == -1 ? null : bucketID;
            //}

            var taskEntity = new ToDoTask
            {
                Name = toDoTask.name,
                BucketId = toDoTask.bucketId
            };


            this.context.ToDoTask.Add(taskEntity);

            try
            {
                this.context.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }


        }

        [HttpPut]
        public IActionResult Put(ExternalToDoTask toDoTask)
        {
            var dbTask = this.context.ToDoTask.SingleOrDefault(x => x.TaskId == toDoTask.id);
            dbTask.Name = toDoTask.name;
            dbTask.BucketId = toDoTask.bucketId;
            dbTask.Status = toDoTask.status;
            dbTask.ModifiedDate = DateTime.Now;

            this.context.ToDoTask.Update(dbTask);

            try
            {
                this.context.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }


        }

        [HttpDelete]
        public IActionResult DeleteTask(int id)
        {
            var task = this.context.ToDoTask.SingleOrDefault(x => x.TaskId == id);
            if (task ==null)
            {
                return NotFound();
            }

            this.context.ToDoTask.Remove(task);

            try
            {
                this.context.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPost("AddBucket")]
        public IActionResult Post(String bucketName)
        {

            if (String.IsNullOrEmpty(bucketName))
                return BadRequest();

            var bucket = new Bucket
            {
                Name = bucketName
            };


            this.context.Bucket.Add(bucket);

            try
            {
                this.context.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }




        }
    }
}

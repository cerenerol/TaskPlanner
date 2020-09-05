using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_planner.Models
{
    public class Tasks
    {
        public Tasks()
        {
        }

        public Tasks(int ıd, string taskName, string detail, DateTime createdAt)
        {
            TaskId = ıd;
            TaskName = taskName;
            Detail = detail;
            CreatedAt = createdAt;
        }

        public int TaskId { get; set; }
        public string TaskName { get; set; }

        public string Detail { get; set; }

       // public bool IsDone { get; set; }


        //  public Guid CategoryId { get; set; }

        //public Category Category { get; set; }

        //public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

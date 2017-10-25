using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmPrismSample.Models
{
    public class ToDoItem
    {
        public int Id { get; }
        public string Name { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public bool Done { get; set; }

        public ToDoItem()
        {
            Id = Guid.NewGuid().GetHashCode();
        }
    }
}

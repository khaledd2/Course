using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Shared.ViewModels
{
    public class DataTableVM<T> where T : class
    {
        public IEnumerable<T> Data { get; private set; } = new List<T>();
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }

        public DataTableVM(int dataSize, int currentPage, int pageSize, List<T> data)
        {
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling((double)dataSize / pageSize);
            Data = data;
        }
    }
}

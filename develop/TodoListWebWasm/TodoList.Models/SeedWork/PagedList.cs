using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Models.SeedWork
{
    public class PagedList<T>
    {
        public MetaData MetaData { get; set; }
        public List<T> Items { set; get; }

        public PagedList() { }
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,         //Tổng số dòng
                PageSize = pageSize,        //Số dòng trên 1 page
                CurrentPage = pageNumber,   //Page hiện tại
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            Items = items;
        }
    }
}

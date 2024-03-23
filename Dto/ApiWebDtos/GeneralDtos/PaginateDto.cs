namespace Dto.ApiWebDtos.GeneralDtos
{
    public class PaginateDto
    {
        public virtual int CurrentPage { get; set; }
        public virtual int PageSize { get; set; }
        public virtual int TotalCount { get; set; }
        // ceiling : yukarı yuvarlar , divide : bölme
        public virtual int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalCount, PageSize));

        public virtual bool isPrevious => CurrentPage > 1;
        public virtual bool isNext => CurrentPage < TotalPages;



        public List<int> GeneratePaginationLinks(int currentPage, int totalPages, int visiblePages = 3)
        {
            List<int> pagination = new List<int>();

            if (totalPages <= visiblePages)
            {
                for (int i = 1; i <= totalPages; i++)
                {
                    pagination.Add(i);
                }
            }
            else
            {
                int startPage = Math.Max(currentPage - 1, 1);
                int endPage = Math.Min(startPage + visiblePages - 1, totalPages);

                if (endPage - startPage < visiblePages - 1)
                {
                    startPage = Math.Max(endPage - visiblePages + 1, 1);
                }

                if (startPage > 1)
                {
                    pagination.Add(1);
                    if (startPage > 2)
                    {
                        pagination.Add(-1); // Add "..." for hidden pages
                    }
                }

                for (int i = startPage; i <= endPage; i++)
                {
                    pagination.Add(i);
                }

                if (endPage < totalPages)
                {
                    if (endPage < totalPages - 1)
                    {
                        pagination.Add(-1); // Add "..." for hidden pages
                    }
                    pagination.Add(totalPages);
                }
            }

            return pagination;
           
        }

        


    }

}

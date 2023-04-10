using Core.Models;

namespace Api.ViewModels;

public class PaginacaoViewModel<T>
{
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public bool HasNext { get; set; }
    public bool HasPrevious { get; set; }

    public PaginacaoViewModel(PagedList<T> lista)
    {
        TotalCount = lista.TotalCount;
        PageSize = lista.PageSize;
        CurrentPage = lista.CurrentPage - 1;
        TotalPages = lista.TotalPages;
        HasNext = lista.HasNext;
        HasPrevious = lista.HasPrevious;
    }
}

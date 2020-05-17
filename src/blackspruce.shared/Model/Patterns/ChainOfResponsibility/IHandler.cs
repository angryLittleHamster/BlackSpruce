#nullable enable
using System.Threading.Tasks;

namespace BlackSpruce.Shared.Patterns.ChainOfResponsibility
{
    /// <summary>
    /// A Handler can be an array, list, or collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHandler<T> where T : class
    {
        IHandler<T> SetNext(IHandler<T> next);
        void Handle(T request);
        Task HandleAsync(T request);
        
    }
}

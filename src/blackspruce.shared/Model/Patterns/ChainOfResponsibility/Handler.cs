
using System.Threading.Tasks;

namespace BlackSpruce.Shared.Patterns.ChainOfResponsibility
{
    public abstract class Handler<T> : IHandler<T> where T : class
    {
        private  IHandler<T> Next { get; set; }
        public virtual void Handle(T request)
        {
            if (Next != null)
            {
                Next.Handle(request);
            }
        }

        public virtual async Task HandleAsync(T request)
        {
            if (Next != null)
            {
                await Next.HandleAsync(request);
            }
            
        }

        public IHandler<T> SetNext(IHandler<T> next)
        {
            Next = next;
            return Next;
        }
    }
}

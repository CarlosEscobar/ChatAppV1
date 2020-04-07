using ChatAppV1.DataAccess.Entities;
using ChatAppV1.DataAccess.Context;
using ChatAppV1.DataAccess.Repositories;
using System.Threading.Tasks;

namespace ChatAppV1.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChatAppContext dbContext;
        public GenericRepository<Message> MessageRepository { get; private set; }

        public UnitOfWork(ChatAppContext context)
        {
            this.dbContext = context;
            this.MessageRepository = new GenericRepository<Message>(this.dbContext);
        }

        public void Dispose()
        {
            this.dbContext.Dispose();
        }

        public async Task Commit()
        {
            await this.dbContext.SaveChangesAsync();
        }
    }
}

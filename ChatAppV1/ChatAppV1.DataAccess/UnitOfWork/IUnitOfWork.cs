using ChatAppV1.DataAccess.Entities;
using ChatAppV1.DataAccess.Repositories;
using System;
using System.Threading.Tasks;

namespace ChatAppV1.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        GenericRepository<Message> MessageRepository { get; }

        Task Commit();
    }
}

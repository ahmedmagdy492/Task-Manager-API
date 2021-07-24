using Task_Manager.Models;
using Task_Manager.Repository;

namespace Task_Manager.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<GroupMessage, string> GroupMessagesRepository { get; }
        IRepository<Group, string> GroupRepository { get; }
        IRepository<User, string> UserRepository { get; }
        IRepository<WorkTask, string> WorkTaskRepository { get; }

        bool Save();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Manager.Data;
using Task_Manager.Models;
using Task_Manager.Repository;

namespace Task_Manager.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _context;

        public UnitOfWork(AppDBContext context)
        {
            _context = context;
            UserRepository = new Repository<User, string>(_context);
            GroupRepository = new Repository<Group, string>(_context);
            WorkTaskRepository = new Repository<WorkTask, string>(_context);
            GroupMessagesRepository = new Repository<GroupMessage, string>(_context);
        }

        public IRepository<User, string> UserRepository { get; private set; }
        public IRepository<Group, string> GroupRepository { get; private set; }
        public IRepository<WorkTask, string> WorkTaskRepository { get; private set; }
        public IRepository<GroupMessage, string> GroupMessagesRepository { get; private set; }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}

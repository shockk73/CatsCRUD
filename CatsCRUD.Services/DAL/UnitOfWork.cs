using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CatsCRUD.Services.Entites;
using CatsCRUD.Services.Models;

namespace CatsCRUD.Services.DAL
{

    public interface IUnitOfWork
    {
        ICatRepository<Cat> CatRepository { get; }

        ICatRepository<User> UserRepository { get; }

        Task SaveAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatsContext _context;
        private ICatRepository<Cat> _catRepository;
        private ICatRepository<User> _userRepository;

        public UnitOfWork(CatsContext context)
        {
            _context = context;
        }


        public virtual ICatRepository<Cat> CatRepository
        {
            get
            {

                if (_catRepository == null)
                {
                    _catRepository = new CatRepository<Cat>(_context);
                }
                return _catRepository;
            }
        }

        public virtual ICatRepository<User> UserRepository
        {
            get
            {

                if (_userRepository == null)
                {
                    _userRepository = new CatRepository<User>(_context);
                }
                return _userRepository;
            }
        }

        public virtual async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}

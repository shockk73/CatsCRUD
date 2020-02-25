using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatsCRUD.Services.DAL;
using CatsCRUD.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace CatsCRUD.Services
{
    public interface ICatService
    {
        Task AddAsync(Cat cat);

        Task DeleteAsync(int id);

        Task<Cat> GetAsync(int id);

        Task<IEnumerable<Cat>> GetAllAsync();

        Task UpdateAsync(Cat cat);
    }


    public class CatService : ICatService
    {

        readonly IUnitOfWork _unitOfWork;

        public CatService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Cat cat)
        {
            await _unitOfWork.CatRepository.InsertAsync(cat);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.CatRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Cat> GetAsync(int id)
        {
            return await _unitOfWork.CatRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Cat>> GetAllAsync()
        {
            return await _unitOfWork.CatRepository.GetAsync(null, null, "");
        }

        public async Task UpdateAsync(Cat cat)
        {
            await _unitOfWork.CatRepository.UpdateAsync(cat);
            await _unitOfWork.SaveAsync();
        }
    }
}

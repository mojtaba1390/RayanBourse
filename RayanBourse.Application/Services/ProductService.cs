using RayanBourse.Application.Interfaces;
using RayanBourse.Domain.Entities;
using RayanBourse.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RayanBourse.Application.Services
{
    public class ProductService : IProductService
    {
        private IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Delete(Product entity)
        {
            _unitOfWork.ProductRepository.Delete(entity);

        }

        public IEnumerable<Product> Find(Expression<Func<Product, bool>> predicate)
        {
            return _unitOfWork.ProductRepository.Find(predicate);
        }

        public Product Get(int id)
        {
            return _unitOfWork.ProductRepository.Get(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _unitOfWork.ProductRepository.GetAll();
        }



        public void Save(Product entity)
        {
            _unitOfWork.ProductRepository.Save(entity);
        }

        public void Update(Product entity)
        {
            _unitOfWork.ProductRepository.Update(entity);
        }
    }
}

using Microsoft.EntityFrameworkCore;
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
            Validate(entity, EntityState.Added);
            _unitOfWork.ProductRepository.Save(entity);
        }

        public void Update(Product entity)
        {
            try
            {
                Validate( entity, EntityState.Modified);
                _unitOfWork.ProductRepository.Update(entity);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void Validate( Product product, EntityState state)
        {
            try
            {
                var entity = _unitOfWork.ProductRepository
                    .Find(x => x.ProduceDate == product.ProduceDate && x.ManufactureEmail.Trim() == product.ManufactureEmail)
                  .FirstOrDefault();
                switch (state)
                {

                    case EntityState.Modified:
                        if (entity == null)
                            throw new Exception("Inserted product does not excist in database");

                        if (entity.UserId.Trim()!=product.UserId)
                            throw new Exception("modifing product  allow just by user creation itself");


                        break;
                    case EntityState.Added:
                        if (entity != null)
                            throw new Exception("product is existed in database");

                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

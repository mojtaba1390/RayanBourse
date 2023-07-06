using Microsoft.EntityFrameworkCore;
using RayanBourse.Application.Interfaces;
using RayanBourse.Domain;
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
            Product? product = GetEntityByManufactorEmailAndProductData(entity);

            Validate(product,null, EntityState.Deleted);
            _unitOfWork.ProductRepository.Delete(product);

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



        public async Task<Product> Save(Product entity)
        {
            Product? product = GetEntityByManufactorEmailAndProductData(entity);

            Validate(databaseProduct: product,newProduct:entity, EntityState.Added);
            await _unitOfWork.ProductRepository.Save(entity);
            return entity;
        }

        public void Update(Product entity)
        {
            try
            {
                Product? product = GetEntityByManufactorEmailAndProductData(entity);

                Validate(databaseProduct: product, newProduct: entity, EntityState.Modified);
                _unitOfWork.ProductRepository.Update(entity);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void Validate( Product databaseProduct,Product newProduct, EntityState state)
        {
            try
            {
                switch (state)
                {

                    case EntityState.Modified:
                        if (databaseProduct == null)
                            throw new Exception("Inserted product does not exist in database");

                        if (databaseProduct.UserId.Trim() != newProduct.UserId)
                            throw new Exception("modifing product  allow just by user creation itself");

                        if (!Helper.IsValidMobileNumber(newProduct.ManufacturePhone))
                            throw new Exception("phone number is invalid!");


                        break;
                    case EntityState.Added:
                        if (databaseProduct != null)
                            throw new Exception("product is existed in database");

                        if (!Helper.IsValidEmail(newProduct.ManufactureEmail))
                            throw new Exception("email format is invalid!");

                        if (!Helper.IsValidMobileNumber(newProduct.ManufacturePhone))
                            throw new Exception("phone number is invalid!");
                        break;
                    case EntityState.Deleted:
                        if (databaseProduct == null)
                            throw new Exception("expected product does not exist in database");

                        if (databaseProduct.IsAvailable != EnumYesNo.Yes)
                            throw new Exception("expected product is not available");

                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Product? GetEntityByManufactorEmailAndProductData(Product product)
        {
            return FindWithIncludes(x => x.ProduceDate == product.ProduceDate && x.ManufactureEmail.Trim() == product.ManufactureEmail, new[] { "User" } )
              .FirstOrDefault();
        }

        public List<Product> FindWithIncludes(Expression<Func<Product, bool>> predicate, string[] includes)
        {
            return _unitOfWork.ProductRepository.FindWithIncludes(predicate, includes);
        }
    }
}

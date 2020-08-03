using Anonym.Business.Abstract;
using Anonym.Business.Constants.Messages;
using Anonym.DataAccess.Abstract;
using Anonym.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anonym.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IResult Add(Category category)
        {
            _categoryDal.Add(category);
            return new SuccessResult(CrudMessages.CategoryAdded);
        }

        public IResult Delete(Category category)
        {
            _categoryDal.Delete(category);
            return new SuccessResult(CrudMessages.CategoryDeleted);
        }

        public IDataResult<Category> GetById(string categoryId)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryId == categoryId));
        }

        public IDataResult<List<Category>> GetList()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetList().ToList());
        }

        public IResult Update(Category category)
        {
            _categoryDal.Update(category);
            return new SuccessResult(CrudMessages.CategoryUpdated);
        }
    }
}

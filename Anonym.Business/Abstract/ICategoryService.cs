using Anonym.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<List<Category>> GetList();
        IDataResult<Category> GetById(string categoryId);
        IResult Add(Category category);
        IResult Delete(Category category);
        IResult Update(Category category);
    }
}

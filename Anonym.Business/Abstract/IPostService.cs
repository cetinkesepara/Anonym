using Anonym.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Business.Abstract
{
    public interface IPostService
    {
        IDataResult<List<Post>> GetList();
        IDataResult<Post> GetById(string postId);
        IResult Add(Post post);
        IResult Delete(Post post);
        IResult Update(Post post);
    }
}

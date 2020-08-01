using Anonym.Business.Abstract;
using Anonym.Business.Constants.Messages;
using Anonym.DataAccess.Abstract;
using Anonym.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Business.Concrete
{
    public class PostManager : IPostService
    {
        private readonly IPostDal _postDal;

        public PostManager(IPostDal postDal)
        {
            _postDal = postDal;
        }

        public IResult Add(Post post)
        {
            _postDal.Add(post);
            return new SuccessResult(CrudMessages.PostAdded);
        }

        public IResult Delete(Post post)
        {
            _postDal.Delete(post);
            return new SuccessResult(CrudMessages.PostDeleted);
        }

        public IDataResult<Post> GetById(string postId)
        {
            return new SuccessDataResult<Post>(_postDal.Get(p => p.PostId == postId));
        }

        public IResult Update(Post post)
        {
            _postDal.Update(post);
            return new SuccessResult(CrudMessages.PostUpdated);
        }
    }
}

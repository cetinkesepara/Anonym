using Anonym.Business.Abstract;
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
    public class ChatRoomManager : IChatRoomService
    {
        private readonly IChatRoomDal _chatRoomDal;

        public ChatRoomManager(IChatRoomDal chatRoomDal)
        {
            _chatRoomDal = chatRoomDal;
        }

        public IResult Add(ChatRoom chatRoom)
        {
            _chatRoomDal.Add(chatRoom);
            return new SuccessResult();
        }

        public IResult Delete(ChatRoom chatRoom)
        {
            _chatRoomDal.Delete(chatRoom);
            return new SuccessResult();
        }

        public IDataResult<ChatRoom> GetById(string chatRoomId)
        {
            return new SuccessDataResult<ChatRoom>(_chatRoomDal.Get(c => c.ChatRoomId == chatRoomId));
        }

        public IDataResult<List<ChatRoom>> GetList()
        {
            return new SuccessDataResult<List<ChatRoom>>(_chatRoomDal.GetList().ToList());
        }

        public IResult Update(ChatRoom chatRoom)
        {
            _chatRoomDal.Update(chatRoom);
            return new SuccessResult();
        }
    }
}

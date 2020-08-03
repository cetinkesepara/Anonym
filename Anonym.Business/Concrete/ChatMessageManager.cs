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
    public class ChatMessageManager : IChatMessageService
    {
        private readonly IChatMessageDal _chatMessageDal;

        public ChatMessageManager(IChatMessageDal chatMessageDal)
        {
            _chatMessageDal = chatMessageDal;
        }

        public IResult Add(ChatMessage chatMessage)
        {
            _chatMessageDal.Add(chatMessage);
            return new SuccessResult();
        }

        public IResult Delete(ChatMessage chatMessage)
        {
            _chatMessageDal.Delete(chatMessage);
            return new SuccessResult();
        }

        public IDataResult<ChatMessage> GetById(string chatMessageId)
        {
            return new SuccessDataResult<ChatMessage>(_chatMessageDal.Get(c => c.ChatMessageId == chatMessageId));
        }

        public IDataResult<List<ChatMessage>> GetList()
        {
            return new SuccessDataResult<List<ChatMessage>>(_chatMessageDal.GetList().ToList());
        }

        public IResult Update(ChatMessage chatMessage)
        {
            _chatMessageDal.Update(chatMessage);
            return new SuccessResult();
        }
    }
}

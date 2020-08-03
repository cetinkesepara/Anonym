using Anonym.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Business.Abstract
{
    public interface IChatMessageService
    {
        IDataResult<List<ChatMessage>> GetList();
        IDataResult<ChatMessage> GetById(string chatMessageId);
        IResult Add(ChatMessage chatMessage);
        IResult Delete(ChatMessage chatMessage);
        IResult Update(ChatMessage chatMessage);
    }
}

using Anonym.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Business.Abstract
{
    public interface IChatRoomService
    {
        IDataResult<List<ChatRoom>> GetList();
        IDataResult<ChatRoom> GetById(string chatRoomId);
        IResult Add(ChatRoom chatRoom);
        IResult Delete(ChatRoom chatRoom);
        IResult Update(ChatRoom chatRoom);
    }
}

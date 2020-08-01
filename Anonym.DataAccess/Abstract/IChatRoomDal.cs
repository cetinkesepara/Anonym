using Anonym.Entities.Concrete;
using Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.DataAccess.Abstract
{
    public interface IChatRoomDal : IEntityRepository<ChatRoom>
    {
    }
}

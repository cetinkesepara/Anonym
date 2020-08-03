using Anonym.Business.Abstract;
using Anonym.Business.Concrete;
using Anonym.DataAccess.Abstract;
using Anonym.DataAccess.Concrete.EntityFramework;
using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PostManager>().As<IPostService>().SingleInstance();
            builder.RegisterType<EfPostDal>().As<IPostDal>().SingleInstance();

            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();

            builder.RegisterType<ChatRoomManager>().As<IChatRoomService>().SingleInstance();
            builder.RegisterType<EfChatRoomDal>().As<IChatRoomDal>().SingleInstance();

            builder.RegisterType<ChatMessageManager>().As<IChatMessageService>().SingleInstance();
            builder.RegisterType<EfChatMessageDal>().As<IChatMessageDal>().SingleInstance();

        }
    }
}

using Anonym.Business.Abstract;
using Anonym.Business.Concrete;
using Anonym.Business.Utilities.Security.Jwt.Abstract;
using Anonym.Business.Utilities.Security.Jwt.Concrete;
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
            builder.RegisterType<PostManager>().As<IPostService>().InstancePerLifetimeScope();
            builder.RegisterType<EfPostDal>().As<IPostDal>().InstancePerLifetimeScope();

            builder.RegisterType<CategoryManager>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().InstancePerLifetimeScope();

            builder.RegisterType<ChatRoomManager>().As<IChatRoomService>().InstancePerLifetimeScope();
            builder.RegisterType<EfChatRoomDal>().As<IChatRoomDal>().InstancePerLifetimeScope();

            builder.RegisterType<ChatMessageManager>().As<IChatMessageService>().InstancePerLifetimeScope();
            builder.RegisterType<EfChatMessageDal>().As<IChatMessageDal>().InstancePerLifetimeScope();

            builder.RegisterType<UserManager>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<EfUserDal>().As<IUserDal>().InstancePerLifetimeScope();

            builder.RegisterType<EfUserClaimDal>().As<IUserClaimDal>().InstancePerLifetimeScope();

            builder.RegisterType<UserLoginManager>().As<IUserLoginService>().InstancePerLifetimeScope();
            builder.RegisterType<EfUserLoginDal>().As<IUserLoginDal>().InstancePerLifetimeScope();

            builder.RegisterType<EfRoleDal>().As<IRoleDal>().InstancePerLifetimeScope();

            builder.RegisterType<EfRoleClaimDal>().As<IRoleClaimDal>().InstancePerLifetimeScope();

            builder.RegisterType<EfUserRoleDal>().As<IUserRoleDal>().InstancePerLifetimeScope();

            builder.RegisterType<SettingManager>().As<ISettingService>().InstancePerLifetimeScope();
            builder.RegisterType<EfSettingDal>().As<ISettingDal>().InstancePerLifetimeScope();

            builder.RegisterType<UserTokenManager>().As<IUserTokenSevice>().InstancePerLifetimeScope();
            builder.RegisterType<EfUserTokenDal>().As<IUserTokenDal>().InstancePerLifetimeScope();

            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
        }
    }
}

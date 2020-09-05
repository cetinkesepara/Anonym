using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Business.Constants.Errors
{
    public static class ErrorNames
    {
        public static string UnauthorizedUser = "UnauthorizedUser";
        public static string UnverifiedEmail = "UnverifiedEmail";
        public static string AlreadyRegisteredUser = "AlreadyRegisteredUser";
        public static string NotFoundUser = "NotFoundUser";
        public static string AlreadyConfirmatedEmail = "AlreadyConfirmatedEmail";
        public static string ConfirmationErrorEmail = "ConfirmationErrorEmail";
        public static string ActivatedErrorEmail = "ActivatedErrorEmail";

        public static string DefaultError = "DefaultError";
    }
}

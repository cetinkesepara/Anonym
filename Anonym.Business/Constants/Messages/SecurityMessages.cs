using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Business.Constants.Messages
{
    public static class SecurityMessages
    {
        public static string LoginCheckError = "Email adresiniz veya şifreniz yanlış!";
        public static string LoginSuccessful = "Giriş yapıldı";
        public static string ConfirmEmailSuccessful = "Üyeliğinizi etkinleştirmek için e-posta adresinize bir bağlantı gönderildi. Lütfen e-posta adresinizi kontrol ediniz.";
        public static string UserEmailAlreadyConfirmed = "Bu e-posta adresi zaten etkinleştirilmiş.";
        public static string UserEmailActivated = "Üyeliğiniz etkinleştirildi. Şimdi giriş ekrarnından oturum açabilirsiniz.";
        public static string TokenHasExpiredForEmailConfirm = "Bu bağlantının süresi dolmuş lütfen yeniniden e-posta doğrulaması talep ediniz.";
        public static string InvalidTransaction = "Geçersiz bir işlem talebinde bulunundu.";
        public static string SystemError = "Sistemde bir hata oluştu lütfen daha sonra tekrar deneyiniz.";
        public static string TokenNotExpired = "Hali hazırda geçerli olan bir bağlantı zaten gönderilmiş. Lütfen e-postalarınızı kontrol ediniz veya daha sonra tekrar deneyiniz.";
    }
}

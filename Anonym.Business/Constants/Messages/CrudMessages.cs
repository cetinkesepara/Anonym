using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Business.Constants.Messages
{
    public static class CrudMessages
    {
        public static string PostAdded = "İleti başarıyla eklendi.";
        public static string PostDeleted = "İleti başarıyla silindi.";
        public static string PostUpdated = "İleti başarıyla güncellendi.";

        public static string CategoryAdded = "Kategori başarıyla eklendi.";
        public static string CategoryDeleted = "Kategori başarıyla silindi.";
        public static string CategoryUpdated = "Kategori başarıyla güncellendi.";

        public static string UserAdded = "Üye kaydı oluşturuldu.";
        public static string UserExistsForEmail = "Bu email adresi ile daha önce zaten kaydolunmuş.";
        public static string UserNotFoundForEmail = "Bu email adresine ait bir kullanıcı bulunamadı.";

        public static string SettingAdded = "Yeni bir ayar başarıyla eklendi.";
        public static string SettingDeleted = "Ayar verisi başarıyla silindi.";
        public static string SettingUpdated = "Ayar verisi başarıyla güncellendi.";
    }
}

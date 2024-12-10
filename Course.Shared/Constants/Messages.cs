using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Shared.Constants
{
    public static class Messages
    {
        public static string Error => "حدث خطأ غير متوقع";
        public static string AddedSuccessfully => "تمت الإضافة بنجاح";
        public static string UpdatedSuccessfully => "تم التحديث بنجاح";
        public static string RemovedSuccessfully => "تم الحذف بنجاح";
        public static string NotFound => "لم يتم العثور على العنصر";
        public static string RetrievedSuccessfully => "تم جلب البيانات بنجاح";
        public static string ReferenceError => "لم يتم الحذف، تأكد من عدم وجود ارتباطات بهذا العنصر";

    }

}

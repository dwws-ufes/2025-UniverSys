using Magma.Extensions.Application.Models;
using System.ComponentModel;

namespace UniverSys.Application.Common.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription<T>(this T enumValue)
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                return null;

            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return description;
        }

        public static List<SelectItemEnum> GetSelectList<T>()
            where T : struct, IConvertible
        {
            var lista = new List<SelectItemEnum>();

            if (!typeof(T).IsEnum)
                return lista;

            foreach (int i in Enum.GetValues(typeof(T)))
            {
                var selectItem = new SelectItemEnum();
                selectItem.Id = i;
                selectItem.Text = GetDescription<T>((T)(object)i);
                lista.Add(selectItem);
            }

            return lista;

            //    //var description = enumValue.ToString();
            //    // var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            //    var fieldsInfo = typeof(T).GetFields();

            //foreach(var fieldInfo in fieldsInfo)
            //{
            //    if(!fieldInfo.FieldType.IsEnum)
            //        continue;

            //    var selectItem = new SelectItemEnum();

            //    selectItem.Id = fieldInfo.Name;
            //    selectItem.Text = fieldInfo.Name;

            //    var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
            //    if (attrs != null && attrs.Length > 0)
            //    {
            //        selectItem.Text = ((DescriptionAttribute)attrs[0]).Description;
            //    }

            //    lista.Add(selectItem);
            //}

            //return lista;
        }
    }

    //public class SelectItemEnum
    //{
    //    public int Id { get; set; }
    //    public string Text { get; set; }
    //}
}

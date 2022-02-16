using System.ComponentModel;

namespace ForestMarathone.AdditionalClasses
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
        public enum Status
        {
        [Description("Все")]
        All,
        [Description("Неудачные")]
        Passed,
        [Description("Удачные")]
        Failed
     }
}

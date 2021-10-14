using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

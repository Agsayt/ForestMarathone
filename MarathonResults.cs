//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ForestMarathone
{
    using System;
    using System.Collections.Generic;
    
    public partial class MarathonResults
    {
        public int Id { get; set; }
        public int RunnerId { get; set; }
        public int MarathonTypeId { get; set; }
        public int SetId { get; set; }
        public int StatusId { get; set; }
        public decimal TimeRun { get; set; }
        public int Plate { get; set; }
    
        public virtual MarathonStatus MarathonStatus { get; set; }
        public virtual Participant Participant { get; set; }
        public virtual Sets Sets { get; set; }
        public virtual TypeDistances TypeDistances { get; set; }
    }
}

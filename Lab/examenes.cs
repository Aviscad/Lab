//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lab
{
    using System;
    using System.Collections.Generic;
    
    public partial class examenes
    {
        public int id_examenes { get; set; }
        public int id_paciente { get; set; }
        public Nullable<int> id_orina { get; set; }
        public Nullable<int> id_heces { get; set; }
        public Nullable<int> id_hemograma { get; set; }
        public System.DateTime fecha { get; set; }
        public string reportado { get; set; }
        public virtual heces heces { get; set; }
        public virtual hemograma hemograma { get; set; }
        public virtual orina orina { get; set; }
        public virtual paciente paciente { get; set; }
    }
}

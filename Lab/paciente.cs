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
    
    public partial class paciente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public paciente()
        {
            this.examenes = new HashSet<examenes>();
        }
    
        public int id_paciente { get; set; }
        public Nullable<int> id_campaña { get; set; }
        public string nombre { get; set; }
        public string codigo { get; set; }
        public string genero { get; set; }
        public System.DateTime fecha_nacimiento { get; set; }
    
        public virtual campaña campaña { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<examenes> examenes { get; set; }
    }
}

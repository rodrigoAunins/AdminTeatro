//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoFinal.DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class LocalidadAsiento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LocalidadAsiento()
        {
            this.LocalidadEspectaculo = new HashSet<LocalidadEspectaculo>();
        }
    
        public long Id { get; set; }
        public int NumeroAsiento { get; set; }
        public string Entrada { get; set; }
        public bool EstadoAsiento { get; set; }
        public long Sectorid { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocalidadEspectaculo> LocalidadEspectaculo { get; set; }
        public virtual Sector Sector { get; set; }
    }
}

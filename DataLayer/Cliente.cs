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
    
    public partial class Cliente
    {
        public long Id { get; set; }
        public long Personaid { get; set; }
        public long Cuentaid { get; set; }
    
        public virtual Cuenta Cuenta { get; set; }
        public virtual Persona Persona { get; set; }
    }
}

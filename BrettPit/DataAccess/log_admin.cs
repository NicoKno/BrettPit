//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BrettPit.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class log_admin
    {
        public int id { get; set; }
        public System.DateTime timestamp { get; set; }
        public int uid_affected { get; set; }
        public int uid_performed { get; set; }
        public string changed_from { get; set; }
        public string changed_to { get; set; }
    
        public virtual users users { get; set; }
        public virtual users users1 { get; set; }
    }
}
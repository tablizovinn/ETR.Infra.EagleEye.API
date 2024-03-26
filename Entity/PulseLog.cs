using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ETR.Infra.EagleEye.API.Entity
{
    public class PulseLog
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }

        
        [Column(TypeName = "varchar(100)")] 
        public string TaskID { get; set; }

        
        [Column(TypeName = "varchar(100)")] 
        public string Status { get; set; }

        
        [Column(TypeName = "varchar(100)")] 
        public string ComputerName { get; set; }
    }
}

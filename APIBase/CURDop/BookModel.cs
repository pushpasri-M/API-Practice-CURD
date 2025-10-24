using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace APIBase.CURDop
{
    public class BookModel
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        [Required]
        public int StudentID { get; set; }
        [Required]
        public string StudentName { get; set; }
        [Required]
        public int BookISBN { get; set; }
    }

}

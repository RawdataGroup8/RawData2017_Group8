using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccesLayer.DBObjects
{
    public class RankedWords
    {
        [Key]
        public string Word { get; set; }
        public int Rank { get; set; }
    }
}

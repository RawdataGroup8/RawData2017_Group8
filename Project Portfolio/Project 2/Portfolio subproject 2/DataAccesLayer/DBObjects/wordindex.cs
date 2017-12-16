using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.DBObjects
{
    public class WordIndex
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public double TermFrequency { get; set; }
    }
}

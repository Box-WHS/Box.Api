using System.Collections.Generic;

namespace Box.Api.Types
{
    public class Box
    {
        public int IdBox { get; set; }
        public string Name { get; set; }
        public List<Fold> Folds { get; set; }
    }
}
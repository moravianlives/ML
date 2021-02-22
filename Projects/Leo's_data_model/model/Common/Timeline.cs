using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Zen.Base.Module;
using Zen.Pebble.FlexibleData.Historical;

namespace edu.bucknell.project.moravianLives.model.Common {
    public class Timeline : Data<Timeline>
    {
        [Key]
        public string Id { get; set; }

        public Dictionary<string, Fact> Facts { get; set; }

        public class Fact
        {
            public Fact() { }

            public Fact(HistoricDateTime date, string type, string descriptor, string reference)
            {
                Description = descriptor;
                Reference = reference;
                Date = date;
                Type = type;
            }
            public string Description { get; set; }
            public HistoricDateTime Date { get; set; }
            public string Reference { get; set; }
            public string Type { get; set; }

        }

        public void SetFact(string key, HistoricDateTime date, string type, string descriptor, string reference)
        {
            Facts ??= new Dictionary<string, Fact>();

            if (descriptor == null)
            {
                if (Facts.ContainsKey(key)) Facts.Remove(key);
            }
            else { Facts[key] = new Fact(date, type, descriptor, reference); }
        }
    }
}
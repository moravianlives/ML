using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using edu.bucknell.project.moravianLives.model.Common;
using Zen.Base.Extension;
using Zen.Base.Module;
using Zen.Base.Module.Data.CommonAttributes;
using Zen.Pebble.FlexibleData.Historical;
using Zen.Pebble.FlexibleData.String.Localization;

namespace edu.bucknell.project.moravianLives.model
{
    public class Event : Data<Event>, IFacts, IDataId
    {
        [Key] public string Id { get; set; } = Guid.NewGuid().ToShortGuid();

        [Display] public HistoricString Name { get; set; }

        public HistoricDateTime Date { get; set; }
        public List<string> Categories { get; set; }
        public string Parent { get; set; }
        public string Place { get; set; }
        public string Source { get; set; }
        public List<RoleDescriptor> Roles { get; set; } = new List<RoleDescriptor>();

        #region Implementation of IFacts

        public Dictionary<string, string> Facts { get; set; }
        public CultureVariantString Description { get; set; }

        #endregion

        public static IEnumerable<Event> ByPerson(string personId)
        {
            return Where(i => i.Roles.Any(j => j.PersonId == personId));
        }

        public static Event FetchUnique(string personId, string roleId, string typeId)
        {
            var targetModel = Where(i =>
                    i.Roles.Any(j => j.PersonId == personId && j.RoleId == roleId) && i.Categories.Contains(typeId))
                .FirstOrDefault();

            if (targetModel != null) return targetModel;

            // Let's create a new event and attach properties.

            targetModel = new Event { Categories = new List<string>() };


            targetModel.Roles.Add(new RoleDescriptor { PersonId = personId, RoleId = roleId });
            targetModel.Categories.Add(typeId);

            return targetModel.Save();
        }

        public class RoleDescriptor
        {
            public RoleDescriptor()
            {
            }

            public RoleDescriptor(string roleId, string personId)
            {
                RoleId = roleId;
                PersonId = personId;
            }


            public string RoleId { get; set; }
            public string PersonId { get; set; }
        }

        public class Category : Category<Event, Category>
        {
        }

        public class Role : Category<Event, Role>
        {
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Zen.Base.Extension;

namespace edu.bucknell.project.moravianLives.model.Extensions
{
    public static class ModelExtensions
    {

        public static void AddRole(this List<Event.RoleDescriptor> source, string roleId, string personId)
        {
            if (source == null) source = new List<Event.RoleDescriptor>();

            var target = source.FirstOrDefault(i => i.RoleId == roleId && i.PersonId == personId);
            if (target == null) source.Add(new Event.RoleDescriptor(roleId, personId));
        }

        public static Collaborator ToCollaborator(this string name)
        {
            var probe = Collaborator.Where(i => i.Name == name).FirstOrDefault();
            if (probe != null) return probe;

            var id = name.ToLower().Trim().ToFriendlyUrl();
            id += "-" + id.ToGuid();

            return Collaborator.Get(id) ?? new Collaborator {Name = name, Id = id}.Save();
        }

        public static Organization ToOrganization(this string name)
        {
            var probe = Organization.Where(i => i.Name == name).FirstOrDefault();
            if (probe != null) return probe;

            var id = name.ToLower().Trim().ToFriendlyUrl();
            id += "-" + id.ToGuid();

            return Organization.Get(id) ?? new Organization {Name = name, Id = id}.Save();
        }

        public static Source ToSource(this string name, Source primitive = null)
        {
            var probe = Source.Where(i => i.Name == name).FirstOrDefault();
            if (probe != null) return probe;

            var id = name.ToLower().Trim().ToFriendlyUrl();
            id += "-" + id.ToGuid();

            probe = Source.Get(id);

            if (probe != null) return probe;

            probe = primitive ?? new Source();
            probe.Name = name;
            probe.Id = id;

            probe.Save();

            return probe;
        }
    }
}
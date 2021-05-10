using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using edu.bucknell.project.moravianLives.model.Common;
using Zen.Base.Extension;
using Zen.Base.Module;

namespace edu.bucknell.project.moravianLives.model
{
    public class Collaborator : Data<Collaborator>
    {
        [Key] public string Id { get; set; } = Guid.NewGuid().ToShortGuid();

        [Display] public string Name { get; set; }

        public List<string> LinkedIds { get; set; }
        public List<string> Categories { get; set; }
        public List<string> Organizations { get; set; }
        public string Email { get; set; }

        public class Category : Category<Person, Category>
        {
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using PersonalWebsite.Database.Models;
using PersonalWebsite.Tests.Models;

namespace PersonalWebsite.ViewModels
{
    public class IndexViewModel
    {
        public List<Repo> Repos{ get; set; }
        public List<Experience> Experiences { get; set; }
    }
}
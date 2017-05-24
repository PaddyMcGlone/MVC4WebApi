using Videos.Models;

namespace Videos.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Videos.Models.VideoConext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Videos.Models.VideoConext context)
        {
            context.Videos.AddOrUpdate(
                new Video{Id = 1, Title = "MVC", Length = 60},
                new Video{Id = 2, Title = "C#", Length = 120}
                );
        }
    }
}

namespace EveEchoesPlanetaryProductionApi.Data.Models
{
    using System;

    using Microsoft.AspNetCore.Identity;

    public class Role : IdentityRole
    {
        public Role()
            : this(null)
        {
        }

        public Role(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}

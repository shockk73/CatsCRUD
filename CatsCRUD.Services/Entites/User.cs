using System;
using System.Collections.Generic;
using System.Text;

namespace CatsCRUD.Services.Entites
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}

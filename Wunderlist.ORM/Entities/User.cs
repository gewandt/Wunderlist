﻿using System.Collections.Generic;

namespace Wunderlist.ORM.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        
        public virtual Avatar Avatar { get; set; }

        public virtual ICollection<ToDoList> ToDoLists { get; set; }

        public User()
        {
            ToDoLists = new List<ToDoList>();
        }
    }
}

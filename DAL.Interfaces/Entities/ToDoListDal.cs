﻿namespace DAL.Interfaces.Entities
{
    public class ToDoListDal : IDalEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public UserDal User { get; set; }
    }
}

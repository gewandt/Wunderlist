﻿using Wunderlist.DataAccess.Interfaces.Entities;
using Wunderlist.ORM.Entities;

namespace Wunderlist.DataAccess.MsSql.Mappers.Property
{
    public static class MapperProperty
    {
        #region Wunderlist.DataAccess.MsSql to Model

        public static User ToModel(this UserDalEntity item)
        {
            User userEntity = new User
            {
                Id = item.Id,
                Name = item.Name,
                Password = item.Password,
                Avatar = item.Avatar,
                Email = item.Email,
                Salt = item.Salt
            };
            return userEntity;
        }

        public static ToDoList ToModel(this ToDoListDalEntity item)
        {
            ToDoList toDoListEntity = new ToDoList
            {
                Id = item.Id,
                Name = item.Name,
                UserId = item.UserId
            };
            return toDoListEntity;
        }

        #endregion

        #region Model to Wunderlist.DataAccess.MsSql

        public static UserDalEntity ToDal(this User item)
        {
            UserDalEntity userDalEntity = new UserDalEntity(item.Id)
            {
                Name = item.Name,
                Password = item.Password,
                Avatar = item.Avatar,
                Email = item.Email,
                Salt = item.Salt
            };
            return userDalEntity;
        }

        public static ToDoListDalEntity ToDal(this ToDoList item)
        {
            ToDoListDalEntity toDoListDalEntity = new ToDoListDalEntity(item.Id)
            {
                Name = item.Name,
                UserId = item.UserId
            };
            return toDoListDalEntity;
        }

        #endregion
    }
}

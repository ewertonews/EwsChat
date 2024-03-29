﻿using EwsChat.Data.Models;
using NUnit.Framework;
using System;
using EwsChat.Data.Exceptions;
using System.Linq;

namespace EwsChat.Data.Tests
{
    public class ChatUserRepositoryTests
    {
        private IChatUserRespository chatUserRespository;

        [SetUp]
        public void Setup()
        {
            chatUserRespository = new ChatUserRespository();
        }

        [Test]
        public void AddUserShouldAddUserSuccessfully()
        {
            var user = new ChatUser()
            {
                NickName = "TobiasMetal",
                UserId = Guid.NewGuid().ToString()
            };

            chatUserRespository.AddUserAsync(user).Wait();
            var userFromRecord = chatUserRespository.GetUserByIdAsync(user.UserId).Result;

            Assert.That(userFromRecord, Is.Not.Null);
        }

        [Test]
        public void AddUserShouldThrowUserAlreadyExistsException()
        {
            var user = new ChatUser()
            {
                NickName = "TobiasMetal",
                UserId = Guid.NewGuid().ToString(),
                ActiveRoomId = 1002
            };

            chatUserRespository.AddUserAsync(user).Wait();

            Assert.That(() => chatUserRespository.AddUserAsync(user), Throws.TypeOf<UserAlreadyExistsException>());
        }

        [Test]
        public void GetUserByIdShouldReturnUserWithGivenId()
        {
            AddTwoUsers();
            string idUser = Guid.NewGuid().ToString();
            var heavyMeatUser = new ChatUser()
            {
                NickName = "HaavyMeat",
                UserId = idUser,
            };
            chatUserRespository.AddUserAsync(heavyMeatUser).Wait();

            var searchedUser = chatUserRespository.GetUserByIdAsync(idUser).Result;

            Assert.That(searchedUser, Is.Not.Null);
            Assert.That(searchedUser.UserId, Is.EqualTo(idUser));
        }

        [Test]
        public void GetUserByIdShouldThrowNonExistentUserException()
        {
            string idUser = Guid.NewGuid().ToString();

            Assert.That(() => chatUserRespository.GetUserByIdAsync(idUser), Throws.TypeOf<UserNotFoundException>());
        }

        [Test]
        public void GetAllUsersShuldReturnAllUsersFromRepository()
        {
            AddTwoUsers();

            var allUsers = chatUserRespository.GetAllUsersAsync().Result;

            Assert.That(allUsers, Is.Not.Empty);
            Assert.That(allUsers.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetUsersOfRoomShouldReturnAllUsertsOfAgivenRoom()
        {
            AddTwoUsers();
            int punkRockRoomId = 1001;
            var anotherUser = new ChatUser()
            {
                UserId = Guid.NewGuid().ToString(),
                NickName = "emily182",
                ActiveRoomId = punkRockRoomId
            };
            chatUserRespository.AddUserAsync(anotherUser).Wait();

            var usersOfRoom = chatUserRespository.GetUsersOfRoomAsync(punkRockRoomId).Result;
            
            Assert.That(usersOfRoom, Is.Not.Empty);
            Assert.That(usersOfRoom.Count(), Is.EqualTo(2));            
        }

        [Test]
        public void UpdateUserShouldUpdateGivenUserSuccefully()
        {
            var userId = Guid.NewGuid().ToString();
            int punkRockRoom = 1001;
            var user = new ChatUser()
            {
                UserId = userId,
                NickName = "emily182",
                ActiveRoomId = 0
            };
            chatUserRespository.AddUserAsync(user).Wait();

            user.ActiveRoomId = punkRockRoom;

            var updatedUser = chatUserRespository.UpdateUserAsync(user).Result;

            Assert.That(updatedUser.ActiveRoomId, Is.EqualTo(punkRockRoom));
        }

        [Test]
        public void RemoveUserShouldRemoveGivenUserSuccessfully()
        {
            ChatUser userToRemove = AddTwoUsersAndReturnAnExtraUser();
            chatUserRespository.AddUserAsync(userToRemove).Wait();

            chatUserRespository.RemoveUserAsync(userToRemove.UserId).Wait();
            var updatedRepository = chatUserRespository.GetAllUsersAsync().Result;


            Assert.That(updatedRepository, Does.Not.Contain(userToRemove));
        }
        

        [Test]
        public void RemoveUserShouldThrowNonExistentUserException()
        {
            var userThatWasntAdded = AddTwoUsersAndReturnAnExtraUser();

            Assert.That(() => chatUserRespository.RemoveUserAsync(userThatWasntAdded.UserId), Throws.TypeOf<UserNotFoundException>());
        }

        private ChatUser AddTwoUsersAndReturnAnExtraUser()
        {
            AddTwoUsers();
            var userToRemove = new ChatUser()
            {
                NickName = "BillyGrunge",
                UserId = Guid.NewGuid().ToString()
            };
            return userToRemove;
        }

        private void AddTwoUsers()
        {
            var user1 = new ChatUser()
            {
                NickName = "TobiasMetal",
                UserId = Guid.NewGuid().ToString(),
                ActiveRoomId = 1002
            };

            var user2 = new ChatUser()
            {
                NickName = "PunkJack",
                UserId = Guid.NewGuid().ToString(),
                ActiveRoomId = 1001
            };

            chatUserRespository.AddUserAsync(user1).Wait();
            chatUserRespository.AddUserAsync(user2).Wait();
        }
    }
}

using EwsChat.Data.Models;
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
                UserId = Guid.NewGuid().ToString()
            };

            chatUserRespository.AddUserAsync(user).Wait();

            Assert.That(() => chatUserRespository.AddUserAsync(user), Throws.TypeOf<UserAlreadyExistsException>());
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
        public void RemoveUserShouldRemoveGivenUserSuccessfully()
        {
            ChatUser userToRemove = AddTwoUsersAndReturnAnExtraUser();
            chatUserRespository.AddUserAsync(userToRemove).Wait();

            chatUserRespository.RemoveUserAsync(userToRemove).Wait();
            var updatedRepository = chatUserRespository.GetAllUsersAsync().Result;


            Assert.That(updatedRepository, Does.Not.Contain(userToRemove));
        }
        

        [Test]
        public void RemoveUserShouldThrowNonExistentUserException()
        {
            var userThatWasntAdded = AddTwoUsersAndReturnAnExtraUser();

            Assert.That(() => chatUserRespository.RemoveUserAsync(userThatWasntAdded), Throws.TypeOf<NonExistentUserException>());
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
                UserId = Guid.NewGuid().ToString()
            };

            var user2 = new ChatUser()
            {
                NickName = "PunkJack",
                UserId = Guid.NewGuid().ToString()
            };

            chatUserRespository.AddUserAsync(user1).Wait();
            chatUserRespository.AddUserAsync(user2).Wait();
        }


        //Task<bool> RemoveUserAsync(ChatUser user);

    }
}

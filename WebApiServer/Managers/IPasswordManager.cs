﻿using Data_Access_Layer;

namespace WebApiServer.Managers
{
    public interface IPasswordManager
    {
        byte[] GetHash(string password);

        bool Compare(User user, byte[] hashPassword);

        bool Compare(User user, string password);

    }
}
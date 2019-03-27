﻿namespace Cocorico.Server.Models.Entities.Contract
{
    public interface IHashAssertable
    {
        /// <summary>
        /// For testing purposes. Returns hash for assert. Does not guarantee implementation of GetHashCode().
        /// </summary>
        /// <returns>Hash of instance fields</returns>
        int GetAssertHash();
    }
}

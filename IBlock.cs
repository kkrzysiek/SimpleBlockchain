using System;
namespace SimpleBlockchain
{
    public interface IBlock
    {
        byte[] Data { get; }

        byte[] Hash { get; set; }

        int Nonce { get; set; }

        byte[] PreviousHash { get; set; }

        DateTime Timestamp { get; }
    }
}

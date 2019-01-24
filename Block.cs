using System;
namespace SimpleBlockchain
{
    public class Block : IBlock
    {
        public byte[] Data { get; }

        public byte[] Hash { get; set; }

        public int Nonce { get; set; }

        public byte[] PreviousHash { get; set; }

        public DateTime Timestamp { get; }

        public override string ToString()
        {
            return $"{BitConverter.ToString(Hash).Replace("-", string.Empty)}:\n{BitConverter.ToString(PreviousHash).Replace("-", string.Empty)}\n {Nonce} {Timestamp}";
        }

        public Block(byte[] data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            Nonce = 0;
            PreviousHash = new byte[] { 0x00 };
            Timestamp = DateTime.Now;
        }
    }
}

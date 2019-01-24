using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace SimpleBlockchain
{
    public static class BlockChainExtensions
    {
        public static byte[] GenerateHash(this IBlock block)
        {
            using (SHA512 sha = new SHA512Managed())
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(ms))
            {
                bw.Write(block.Data);
                bw.Write(block.Nonce);
                bw.Write(block.Timestamp.ToBinary());
                bw.Write(block.PreviousHash);
                var star = ms.ToArray();
                return sha.ComputeHash(star);
            }
        }

        public static byte[] MineHash(this IBlock block, byte[] difficulty)
        {
            if (difficulty == null)
                throw new ArgumentNullException(nameof(difficulty));

            byte[] hash = new byte[0];
            int d = difficulty.Length;
            while (!hash.Take(2).SequenceEqual(difficulty))
            {
                block.Nonce++;
                hash = block.GenerateHash();
            }
            return hash;
        }

        public static bool IsValid(this IBlock block)
        {
            var bb = block.GenerateHash();
            return block.Hash.SequenceEqual(bb);
        }

        public static bool IsValidPreviousBlock(this IBlock block, IBlock previousBlock)
        {
            if (previousBlock == null)
                throw new ArgumentNullException(nameof(previousBlock));

            var prev = previousBlock.GenerateHash();
            return previousBlock.IsValid() && block.PreviousHash.SequenceEqual(prev);
        }

        public static bool IsValid(this IEnumerable<IBlock> items)
        {
            var em = items.ToList();
            return em.Zip(em.Skip(1), Tuple.Create).All(block => block.Item2.IsValid());
        }
    }
}

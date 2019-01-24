using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SimpleBlockchain
{
    public class BlockChain : IEnumerable<IBlock>
    {
        private List<IBlock> _items = new List<IBlock>();

        public BlockChain(byte[] difficulty, IBlock genesis)
        {
            Difficulty = difficulty;
            genesis.Hash = genesis.MineHash(difficulty);
            Items.Add(genesis);
        }

        public void Add(IBlock block)
        {
            if (Items.LastOrDefault() != null)
            {
                block.PreviousHash = Items.LastOrDefault()?.Hash;
            }
            block.Hash = block.MineHash(Difficulty);
            Items.Add(block);
        }

        public List<IBlock> Items
        {
            get => _items;
            set => _items = value;
        }

        public int Count => _items.Count;
        public byte[] Difficulty { get; }
        public IBlock this[int index]
        {
            get => Items[index];
            set => Items[index] = value;
        }

        public IEnumerator<IBlock> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}

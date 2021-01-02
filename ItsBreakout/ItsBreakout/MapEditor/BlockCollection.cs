using ItsBreakout.Source;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ItsBreakout.Engine
{

    public class BlockCollection
    {        
        // TODO Dont hardcode
        Vector2 blockSize = new Vector2(64,26);
        const string contentPath = @"Content\";

        public List<BlockData> Blocks { get; set; }

        public BlockCollection()
        {
            Blocks = new List<BlockData>();
        }

        public static BlockCollection Load(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BlockCollection));
            BlockCollection loadedMap;

            using (StreamReader reader = new StreamReader(contentPath + filePath))
            {
                loadedMap = (BlockCollection)serializer.Deserialize(reader);
            }

            return loadedMap;
        }

        public void Save(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BlockCollection));
            using (StringWriter sww = new StringWriter())
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                serializer.Serialize(writer, this);
                File.WriteAllText(filePath, sww.ToString());
            }
        }

        public void AddBlock(Vector2 mousePos)
        {
            // Position tile correctly
            Vector2 newBlockPosition = new Vector2(mousePos.X - (mousePos.X % blockSize.X), mousePos.Y - (mousePos.Y % blockSize.Y));

            // Check for duplicates
            foreach (BlockData block in Blocks)
            {
                if (block.Position == newBlockPosition)
                {
                    block.IncreaseHitPoints();
                    return;
                }
            }

            Blocks.Add(new BlockData(1, newBlockPosition, blockSize));
        }

        public bool DamageBlock(Vector2 position)
        {
            // Get correct tile from position
            Vector2 newBlockPosition = new Vector2(position.X - (position.X % blockSize.X), position.Y - (position.Y % blockSize.Y));

            for (int i = 0; i < Blocks.Count; i++)
            {
                if (Blocks[i].Position == newBlockPosition)
                {
                    DamageBlock(i);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// If function returns true block is destroyed.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool DamageBlock(int index)
        {
            if (index < 0 || index >= Blocks.Count) return false;

            if (Blocks[index].HitPoints > 1)
                Blocks[index].HitPoints--;
            else
            {
                Blocks.RemoveAt(index);
                return true;
            }
            return false;
        }
    }
}

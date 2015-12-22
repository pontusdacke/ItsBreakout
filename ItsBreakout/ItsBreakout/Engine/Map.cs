using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Breakout
{
    public class Map
    {
        List<GameObject> blocks;
        float blockSize = 32;

        public List<GameObject> Blocks
        {
            get
            {
                return blocks;
            }

            set
            {
                blocks = value;
            }
        }

        public Map()
        {
            Blocks = new List<GameObject>();
        }

        public static Map Load(string filePath, Texture2D blockTexture) // TODO: Remove the texture dependency
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Map));
            Map loadedMap;

            using (StreamReader reader = new StreamReader(filePath))
            {
                loadedMap = (Map)serializer.Deserialize(reader);
                foreach (var item in loadedMap.Blocks)
                {
                    item.Texture = blockTexture;
                }
            }

            return loadedMap;
        }

        public void Save(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Map));
            using (StringWriter sww = new StringWriter())
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                serializer.Serialize(writer, this);
                File.WriteAllText(filePath, sww.ToString());
            }
        }

        public void AddBlock(Vector2 mousePos, Texture2D texture) // TODO: remove texture dependency
        {
            // Position tile correctly
            Vector2 newBlockPosition = new Vector2(mousePos.X - (mousePos.X % blockSize), mousePos.Y - (mousePos.Y % blockSize));

            // Check for duplicates
            foreach (GameObject block in blocks)
            {
                if (block.Position == newBlockPosition)
                    return;
            }

            Blocks.Add(new GameObject(newBlockPosition, texture));
        }

        public void RemoveBlock(Vector2 mousePos)
        {
            // Position tile correctly
            Vector2 newBlockPosition = new Vector2(mousePos.X - (mousePos.X % blockSize), mousePos.Y - (mousePos.Y % blockSize));
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i].Position == newBlockPosition)
                    blocks.RemoveAt(i);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class TextReaderHelper
    {
        public static string ReadText(string filePath)
        {
            string path = Application.streamingAssetsPath + "/" + filePath;

            using (StreamReader reader = new StreamReader(path))
            {
                string text = reader.ReadToEnd();
                return text;
            }
        }

        public static string ReadLine(string filePath)
        {
            string path = Application.streamingAssetsPath + "/" + filePath;

            using (StreamReader reader = new StreamReader(path))
            {
                string text = reader.ReadLine();
                return text;
            }
        }
    }
}

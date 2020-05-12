using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01_CS313.K21
{
    class Normalize
    {
        float min = 0, max = 1;

        float[] ConvertMinMax(string[] rawInputFeature, float newMin = 0f, float newMax = 1f)
        {
            float[] toIntConverted = new float[rawInputFeature.Length];

            for (int i = 0; i < rawInputFeature.Length; i++)
            {
                float check = 0;
                if (float.TryParse(rawInputFeature[i], out check)) toIntConverted[i] = check;
                else toIntConverted[i] = 0;
            }

            float min = toIntConverted[0];
            float max = toIntConverted[0];

            for (int i = 0; i < toIntConverted.Length; i++)
            {
                if (max < toIntConverted[i])
                {
                    max = toIntConverted[i];
                }

                if (min > toIntConverted[i])
                {
                    min = toIntConverted[i];
                }
            }


            if (min != 0 || max != 0)
            {
                for (int i = 0; i < toIntConverted.Length; i++)
                {
                    toIntConverted[i] = ((toIntConverted[i] - min) / (max - min)) * (newMax - newMin) + newMin;
                }
            }

            return toIntConverted;
        }

        void getInput()
        {
            Console.WriteLine("Nhap vao min va max moi");
            min = float.Parse(Console.ReadLine());
            max = float.Parse(Console.ReadLine());
        }

        public string[] Run(string inputPath, string outputPath, string logPath, FileManager fManager)
        {
            this.getInput();
            string[] inputData = File.ReadAllLines(inputPath);

            string[] data = new String[inputData.Length - 1];
            for (int i = 1; i < inputData.Length; i++)
            {
                data[i - 1] = inputData[i];
            }

            string[] outputData = new String[inputData.Length];

            List<int> listIndex = new List<int>();
            string[] listTitle = inputData[0].Split(',');

            string[] listAttribute = data[0].Split(',');
            for (int i = 0; i < listAttribute.Length; i++)
            {
                float check = 0;
                if (float.TryParse(listAttribute[i], out check)) listIndex.Add(i);
            }

            for (int i = 0; i < listIndex.Count; i++)
            {
                string[] rawNumberic = fManager.GetRawNumberic(data, listIndex[i]);
                float[] attOutput = this.ConvertMinMax(rawNumberic,this.min,this.max);
                data = fManager.ReplaceElement(data, listIndex[i], attOutput);
            }

            using (StreamWriter sw = File.CreateText(logPath))
            {
                for (int i = 0; i < listIndex.Count; i++)
                {
                    sw.WriteLine("Thuộc tính {0}: mien gia tri moi [{1},{2}]", listTitle[listIndex[i]] ,min,max );
                }
            }

            fManager.WriteData(data, outputPath);
            Console.WriteLine("Done, please check log file and output file");
            return data;
        }
    }
}

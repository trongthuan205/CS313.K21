using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab01_CS313.K21
{
    class FileManager
    {

        public FileManager()
        {
            Console.Write("create file manager");
        }

        public enum FileType
        {
            Output = 0,
            Input = 1,
            Log = 2,
        }

        public string GetPath(FileType type)
        {
            string path = "";
            switch (type)
            {
                case FileType.Input:
                    path = @"input.txt";
                    break;
                case FileType.Output:
                     path = @"output.txt";
                    break;
                case FileType.Log:
                     path = "@log.txt";
                    break;
                default:
                    Console.WriteLine("invalid file type");
                    break;
            }

            if (!File.Exists(path))
            {
                Console.WriteLine("not exist file");
                File.CreateText(path);
            }
            return path;
        }

        //public bool WriteData(string[] data, FileType type )
        //{
        //    string path = this.GetPath(type);
        //    if (!String.IsNullOrEmpty(path))
        //    {
        //        File.WriteAllLines(path, data);
        //        return true;
        //    }

        //    Console.WriteLine("can not write file");
        //    return false;
        //}
        //public bool WriteData(string data, FileType type)
        //{
        //    string path = this.GetPath(type);
        //    if (!String.IsNullOrEmpty(path))
        //    {
        //        File.WriteAllText(path,data);
        //        return true;
        //    }

        //    Console.WriteLine("can not write file");
        //    return false;
        //}
        public bool WriteData(string data, string path)
        {
            if (!String.IsNullOrEmpty(path))
            {
                File.WriteAllText(path, data);
                return true;
            }

            Console.WriteLine("can not write file");
            return false;
        }
        public bool WriteData(string[] data, string path)
        {
            if (!String.IsNullOrEmpty(path))
            {
                File.WriteAllLines(path, data);
                return true;
            }

            Console.WriteLine("can not write file");
            return false;
        }

        public string[] GetData(FileType type)
        {
            string path = this.GetPath(type);

            if (!String.IsNullOrEmpty(path))
            {
                string[] data;
                data = File.ReadAllLines(path);
                return data;
            }

            Console.WriteLine("can not get the file");
            return null;
        }

        public void PrintData(string[] data)
        {
            foreach(string line in data)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("End of File");
        }

        public void PrintData(FileType type)
        {
            string[] data = this.GetData(type);

            foreach (string line in data)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("End of File");
        }

        public string[] ReplaceElement(string[] input,int indexNumeric, string[] change)
        {
            string[] output = new String[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                string[] detail = input[i].Split(',');
                detail[indexNumeric] = change[i];

                foreach(string element in detail)
                {
                    output[i] += element + ",";
                }
            }

            return output;
        }

        public string[] ReplaceElement(string[] input, int indexNumeric, float[] change)
        {
            string[] output = new String[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                string[] detail = input[i].Split(',');
                detail[indexNumeric] = change[i].ToString();

                for (int j = 0; j < detail.Length-1; j++)
                {
                    output[i] += detail[j] + ",";
                }
                output[i] += detail[detail.Length - 1];
            }

            return output;
        }

        public string[] SortFile(string[] inputData, int indexNumeric)
        {
            string[] outputData = new String[inputData.Length];

            for (int index = 0; index < inputData.Length; index++)
            {
                outputData[index] = inputData[index];
            }

            string[] compareArray = this.GetRawNumberic(inputData, indexNumeric);

            #region Bubble Sort Region
            int i, j;
            bool haveSwap = false;
            for (i = 0; i < compareArray.Length - 1; i++)
            {
                // i phần tử cuối cùng đã được sắp xếp
                haveSwap = false;
                for (j = 0; j < compareArray.Length - i - 1; j++)
                {
                    if (Int32.Parse(compareArray[j]) > Int32.Parse(compareArray[j + 1]))
                    {
                        SwapLine(ref compareArray[j], ref compareArray[j + 1]);
                        SwapLine(ref outputData[j], ref outputData[j + 1]);
                        haveSwap = true; // Kiểm tra lần lặp này có swap không
                    }
                }
                // Nếu không có swap nào được thực hiện => mảng đã sắp xếp. Không cần lặp thêm
                if (haveSwap == false)
                {
                    break;
                }
            }
            #endregion
            Console.WriteLine("after sort");
            this.PrintData(outputData);
            return outputData;
        }

        /// <summary>
        /// tách chỉ lấy các giá trị cần, ví dụ data 1 dòng có tên,sdt,tuổi; khi nhập index = 1 sẽ xuất ra 1 mảng
        /// gồm các sdt tương ứng thôi
        /// </summary>
        /// <param name="inputData"></param>
        /// <param name="indexNumberic"></param>
        /// <returns></returns>
        public string[] GetRawNumberic(string[] inputData, int indexNumberic)
        {
            string[] outputData = new String[inputData.Length];

            for (int i = 0; i < inputData.Length; i++)
            {
                string[] tempSplitResultofLine = inputData[i].Split(',');

                if(tempSplitResultofLine.Length >= indexNumberic)
                {
                    outputData[i] = tempSplitResultofLine[indexNumberic];
                }
                else
                {
                    Console.WriteLine("indexNumeric > line data");
                }
            }
            return outputData;
        }

        public void SwapLine(ref string line, ref string otherLine)
        {
            string tempLine = line;
            line = otherLine;
            otherLine = tempLine;
        }
    }
}

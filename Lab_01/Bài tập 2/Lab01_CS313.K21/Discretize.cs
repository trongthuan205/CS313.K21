using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Lab01_CS313.K21
{
    class Discretize
    {
        public enum typeDiscretize
        {
            EqualWidth = 0,
            EqualDepth = 1,
        }
        int numOfBin = 1;
        int indexNumberic = 0;
        typeDiscretize type = typeDiscretize.EqualDepth;

        public void GetInput()
        {
            Console.WriteLine("Nhap vao so gio");
            numOfBin = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Nhap vao thu tu thuoc tinh muon chia gio");
            indexNumberic = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Ban muon chia gio theo cong thuc nao\n");
            Console.WriteLine("1. Theo chieu sau \t 2. Theo chieu rong");
            int choose = Int32.Parse(Console.ReadLine());
            switch (choose)
            {
                case 1: this.type = typeDiscretize.EqualDepth;
                    break;
                case 2: this.type = typeDiscretize.EqualWidth;
                    break;
            }
        }

        public string[] Run(string inputPath, string outputPath, string logPath, FileManager fManager)
        {

            string[] inputData = File.ReadAllLines(inputPath);

            string[] data = new String[inputData.Length - 1];
            for (int i = 1; i < inputData.Length; i++)
            {
                data[i-1] = inputData[i ];
            }

            string[] outputData = new String[inputData.Length];

            //lấy sự lựa chọn người dùng
            this.GetInput();

            //input vào phải qua khâu sort
            inputData = fManager.SortFile(data, this.indexNumberic);
            
            string[] rawNumberic = fManager.GetRawNumberic(data, this.indexNumberic);

            #region get số lượng phần tử hoặc biên của giỏ
            int binRange = 0;
            binRange = this.getBinRange(rawNumberic);
            #endregion

            #region lấy ra list các biên giỏ
            Int32[,] arrBienGio = new Int32[this.numOfBin,3];
            if(this.type == typeDiscretize.EqualDepth)
            {
                for (int i = 0; i < this.numOfBin; i++)
                {
                    if(i != this.numOfBin - 1)
                    {
                        arrBienGio[i, 0] = Int32.Parse(rawNumberic[i * binRange]);
                        arrBienGio[i, 1] = Int32.Parse(rawNumberic[i * binRange + binRange - 1]);
                        arrBienGio[i, 2] = 0;
                    }
                    else
                    {
                        arrBienGio[i, 0] = Int32.Parse(rawNumberic[i * binRange]);
                        arrBienGio[i, 1] = Int32.Parse(rawNumberic[rawNumberic.Length-1]);
                        arrBienGio[i, 2] = 0;
                    }
                }
            }
            else if(this.type == typeDiscretize.EqualWidth)
            {
                int pivot = Int32.Parse(rawNumberic[0]);
                for (int i = 0; i < this.numOfBin; i++)
                {
                    arrBienGio[i, 0] = pivot;
                    arrBienGio[i, 1] = arrBienGio[i, 0] + binRange;
                    if (arrBienGio[i, 1] > Int32.Parse(rawNumberic[rawNumberic.Length - 1])) arrBienGio[i, 1] = Int32.Parse(rawNumberic[rawNumberic.Length - 1]);
                    arrBienGio[i, 2] = 0;
                    pivot = arrBienGio[i, 1];
                }
            }
            #endregion

            string[] listAtt = inputData[0].Split(',');
            string log = listAtt[indexNumberic] + ": ";

            #region Thay giá trị
            int pivotBin = 0;
            if(this.type == typeDiscretize.EqualWidth)
            {
                for (int i = 0; i < rawNumberic.Length; i++)
                {

                    //nếu nó không bé hơn biên phải -> nhảy sang giỏ kế tiếp
                    if((Int32.Parse(rawNumberic[i]) == arrBienGio[this.numOfBin-1, 1]))
                    {
                        arrBienGio[this.numOfBin - 1, 2]++;
                        rawNumberic[i] = "[" + arrBienGio[this.numOfBin - 1, 0] + "," + arrBienGio[this.numOfBin - 1, 1] + "]";
                    }
                    else
                    {
                        while ((Int32.Parse(rawNumberic[i]) >= arrBienGio[pivotBin, 1]))
                        {
                            pivotBin++;
                            Console.WriteLine(pivotBin);
                        }
                        rawNumberic[i] = "[" + arrBienGio[pivotBin, 0] + "," + arrBienGio[pivotBin, 1] + ")";
                        arrBienGio[pivotBin, 2]++;
                    }

                }
            }
            else if(this.type == typeDiscretize.EqualDepth)
            {
                for (int i = 0; i < rawNumberic.Length; i++)
                {
                    //nếu index của nó lớn = hơn bingrange* thứ tự giỏ -> nhảy giỏ
                    if (i > binRange * (pivotBin + 1) - 1) pivotBin++;
                    rawNumberic[i] = "[" + arrBienGio[pivotBin, 0] + "," + arrBienGio[pivotBin, 1] + "]";
                    arrBienGio[pivotBin, 2]++;
                }
            }

            outputData = fManager.ReplaceElement(inputData, this.indexNumberic, rawNumberic);
            fManager.WriteData(outputData, outputPath);
            #endregion

            for (int i = 0; i < this.numOfBin-1; i++)
            {
                log += " [ " + arrBienGio[i,0].ToString() + ", " + arrBienGio[i, 1].ToString() + " ): " + arrBienGio[i, 2].ToString();
            }
            log += " [ " + arrBienGio[this.numOfBin - 1, 0].ToString() + ", " + arrBienGio[this.numOfBin - 1, 1].ToString() + " ]: " + arrBienGio[this.numOfBin - 1, 2].ToString();


            fManager.WriteData(log, logPath);
            Console.WriteLine("Done, please check log file and output file");
            return outputData;
        }

        int getBinRange(string[] inputData)
        {
            int range = 0;

            switch (this.type)
            {
                case typeDiscretize.EqualDepth:
                    range = (inputData.Length / this.numOfBin) +1; //phần lẻ sẽ phát sinh thêm 1 giỏ
                    Console.WriteLine("range: " + range);
                    break;
                case typeDiscretize.EqualWidth:
                    range = (Int32.Parse(inputData[inputData.Length - 1]) - Int32.Parse(inputData[0])) / this.numOfBin;
                    if (Int32.Parse(inputData[inputData.Length - 1]) > (range * this.numOfBin )+ Int32.Parse(inputData[0])) range++;
                    Console.WriteLine("range: " + range);
                    break;
                default:
                    Console.WriteLine("invalid type discretize");
                    break;
            }

            return range;
        }
    }
}

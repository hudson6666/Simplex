﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 单纯形法
{
    class Program
    {
        static bool flag = false; //退化

        //输出向量
        static void cout<T>(T[] anything, int length)
        {
            for(int i = 0; i < length; ++i)
            {
                Console.Write(anything[i]);
                Console.Write(" ");
            }
            Console.Write("\n");
            Console.ReadKey();
        }

        //输出矩阵
        static void cout(double[,] mat, int m, int n)
        {
            for(int j = 0; j < n; ++j)
            {
                for(int i = 0; i < m; ++i)
                {
                    Console.Write(mat[j, i]);
                    Console.Write(" ");
                }
                Console.Write("\n");
            }
            Console.ReadKey();
        }

        /// <summary>
        /// 初等行变换
        /// </summary>
        /// <param name="mat">矩阵</param>
        /// <param name="m">列数</param>
        /// <param name="n">行数</param>
        /// <param name="k">换入为基的列</param>
        /// <param name="l">1所在的行</param>
        static void easy_change(ref double[,] mat, int m, int n, int k, int l)
        {
            double x = mat[l, k];
            for (int i = 0; i < m; ++i)
            {
                mat[l, i] = mat[l, i] / x;
            }
            for( int j = 0; j < n; ++j)
            {
                if (j == l)
                    continue;
                double div = mat[j, k] * -1 / mat[l, k];
                for(int i = 0; i < m; ++i)
                {
                    mat[j, i] = mat[j, i] + mat[l, i] * div;
                }
            }
        }
        static void Main(string[] args)
        {
            int m, n; //增广矩阵的列数、行数

            //读入增广矩阵的列数、行数
            Console.WriteLine("Input m, n: m refers count of x+1(for b), n refers count of equations");
            string str = Console.ReadLine();
            string[] strs = str.Split(' ');
            m = int.Parse(strs[0]);
            n = int.Parse(strs[1]);

            //增广矩阵，最后一列为b
            double[,] mat = new double[n, m];
            //目标向量
            double[] c = new double[m - 1];
            //基
            int[] xb = new int[n];

            //读入目标向量
            Console.WriteLine("Input c");
            str = Console.ReadLine();
            strs = str.Split(' ');
            for (int i = 0; i < m - 1; ++i)
            {
                c[i] = double.Parse(strs[i]);
            }

            //读入增广矩阵
            Console.WriteLine("Input matrix");
            for (int j = 0; j < n; ++j)
            {
                str = Console.ReadLine();
                strs = str.Split(' ');
                for (int i = 0; i < m; ++i)
                {
                    mat[j, i] = double.Parse(strs[i]);
                }
            }

            //读入初始可行基
            Console.WriteLine("Input xb");
            str = Console.ReadLine();
            strs = str.Split(' ');
            for (int j = 0; j < n; ++j)
            {
                xb[j] = int.Parse(strs[j]);
            }

            //检验向量
            double[] z = new double[m];

            //theta向量
            double[] the = new double[n];

            //调试输出
            cout(mat, m, n);
            Console.WriteLine("Start looping");

            while(true)
            {
                //Cal Z
                for(int i = 0; i < m - 1; ++i)
                {
                    z[i] = c[i];
                    for(int j = 0; j < n; ++j)
                    {
                        z[i] -= mat[j, i] * c[xb[j]];
                    }
                }
                Console.WriteLine("Z:");
                cout(z, m - 1);

                //Get Z
                double max = 0;
                int maxi = -1;

                switch(flag)
                {
                    case false:
                        //非退化
                        for (int i = 0; i < m - 1; ++i)
                        {
                            if (z[i] > max)
                            {
                                max = z[i];
                                maxi = i;
                            }
                        }
                        break;
                    case true:
                        //退化 Brand
                        for (int i = 0; i < m - 1; ++i)
                        {
                            if (z[i] > 0)
                            {
                                max = z[i];
                                maxi = i;
                                break;
                            }
                        }
                        break;
                }

                //检验数存在正数
                if (maxi != -1)
                {
                    //初始化theta向量
                    for (int j = 0; j < n; ++j)
                        the[j] = -1;

                    //Get theta
                    double minp = -1;
                    int minpthe = -1;

                    for (int j = 0; j < n; ++j)
                    {
                        if (mat[j, maxi] > 0)
                        {
                            the[j] = mat[j, m - 1] / mat[j, maxi];
                        }
                    }
                    for (int j = 0; j < n; ++j)
                    {
                        if (mat[j, maxi] > 0)
                        {
                            if (minp == -1 || mat[j, m - 1] / mat[j, maxi] < minp)
                            {
                                minp = the[j];
                                minpthe = j;
                            }
                            else if(mat[j, m - 1] / mat[j, maxi] == minp) // Bland
                            {
                                flag = true;
                                minp = the[xb.Min()];
                                minpthe = xb.Min();
                            }
                        }
                    }

                    //调试输出
                    Console.WriteLine("the:");
                    cout(the, n);
                    Console.WriteLine("xb:");
                    cout(xb, n);
                    Console.WriteLine("the:{0}:{1}", minpthe, minp);

                    //row change
                    if (minpthe != -1)
                    {
                        Console.WriteLine("onRowChange:{0}:{1}", maxi, minpthe);
                        easy_change(ref mat, m, n, maxi, minpthe);
                        cout(mat, m, n);
                        xb[minpthe] = maxi;
                    }
                    else
                    {
                        Console.WriteLine("onRowChange:{0}:{1}", maxi, 0);
                        easy_change(ref mat, m, n, maxi, 0);
                        cout(mat, m, n);
                        xb[0] = maxi;
                    }
                }
                else
                {
                    cout(mat, m, n);
                    z[m - 1] = 0;
                    for(int j = 0; j < n; ++j)
                    {
                        z[m - 1] += c[xb[j]] * mat[j, m - 1];
                    }
                    Console.WriteLine("End with:{0}", z[m - 1]);
                }
            }

        }
    }
}

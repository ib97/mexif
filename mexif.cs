using System;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Matsumori.Mexif
{
    class Program
    {
        static void Main(string[] args)
        {
            const int TAG_DATE = 0x9003;
            const int TAG_LAT  = 0x0002;
            const int TAG_LON  = 0x0004;

            Console.WriteLine("Mexif Version.1.1");
            Console.WriteLine("");

            if (args.Length <= 0)
            {
                Console.WriteLine("画像の含まれるフォルダをコマンドライン引数に指定してください。");
            }
            else
            {
                Console.WriteLine("FILENAME,TIMESTAMP,LAT_DEG,LAT_MIN,LAT_SEC,LON_DEG,LON_MIN,LON_SEC");
                try
                {
                    string path = args[0];
                    string[] imageList = Directory.GetFiles(
                        path, "*.JPG", SearchOption.TopDirectoryOnly);
                    string[] output = new string[imageList.Length];

                    for (int i = 0; i < imageList.Length; i++)
                    {
                        string image = imageList[i];
                        using (Bitmap bitmap = new Bitmap(image))
                        {
                            int[] pils = bitmap.PropertyIdList;

                            string date;
                            #region 日時の取得
                            {
                                int index = Array.IndexOf(pils, TAG_DATE);
                                if (index == -1)
                                {
                                    date = "";
                                }
                                else
                                {
                                    PropertyItem pi = bitmap.PropertyItems[index];
                                    date = Encoding.ASCII.GetString(pi.Value, 0, 19);
                                }
                            }
                            #endregion

                            string lat;
                            #region 緯度の取得
                            {
                                int index = Array.IndexOf(pils, TAG_LAT);
                                if (index == -1)
                                {
                                    lat = ",,";
                                }
                                else
                                {
                                    PropertyItem pi = bitmap.PropertyItems[index];
                                    double lat1 = (double)BitConverter.ToUInt16(new byte[] { pi.Value[0], pi.Value[1], pi.Value[2], pi.Value[3] }, 0);
                                    double lat2 = (double)BitConverter.ToUInt16(new byte[] { pi.Value[4], pi.Value[5], pi.Value[6], pi.Value[7] }, 0);
                                    double lat3 = (double)BitConverter.ToUInt16(new byte[] { pi.Value[8], pi.Value[9], pi.Value[10], pi.Value[11] }, 0);
                                    double lat4 = (double)BitConverter.ToUInt16(new byte[] { pi.Value[12], pi.Value[13], pi.Value[14], pi.Value[15] }, 0);
                                    double lat5 = (double)BitConverter.ToUInt16(new byte[] { pi.Value[16], pi.Value[17], pi.Value[18], pi.Value[19] }, 0);
                                    double lat6 = (double)BitConverter.ToUInt16(new byte[] { pi.Value[20], pi.Value[21], pi.Value[22], pi.Value[23] }, 0);

                                    double deg = Math.Floor(lat1 / lat2);
                                    double min = ((lat1 / lat2) - deg) * 60 + Math.Floor(lat3 / lat4);
                                    double sec = ((lat3 / lat4) - min) * 60 + Math.Floor(lat5 / lat6);

                                    lat = Convert.ToString(deg) + "," + Convert.ToString(min) + "," + Convert.ToString(sec);
                                }
                            }
                            #endregion

                            string lon;
                            #region 緯度の取得
                            {
                                int index = Array.IndexOf(pils, TAG_LON);
                                if (index == -1)
                                {
                                    lon = ",,";
                                }
                                else
                                {
                                    PropertyItem pi = bitmap.PropertyItems[index];
                                    double lon1 = (double)BitConverter.ToUInt16(new byte[] { pi.Value[0], pi.Value[1], pi.Value[2], pi.Value[3] }, 0);
                                    double lon2 = (double)BitConverter.ToUInt16(new byte[] { pi.Value[4], pi.Value[5], pi.Value[6], pi.Value[7] }, 0);
                                    double lon3 = (double)BitConverter.ToUInt16(new byte[] { pi.Value[8], pi.Value[9], pi.Value[10], pi.Value[11] }, 0);
                                    double lon4 = (double)BitConverter.ToUInt16(new byte[] { pi.Value[12], pi.Value[13], pi.Value[14], pi.Value[15] }, 0);
                                    double lon5 = (double)BitConverter.ToUInt16(new byte[] { pi.Value[16], pi.Value[17], pi.Value[18], pi.Value[19] }, 0);
                                    double lon6 = (double)BitConverter.ToUInt16(new byte[] { pi.Value[20], pi.Value[21], pi.Value[22], pi.Value[23] }, 0);

                                    double deg = Math.Floor(lon1 / lon2);
                                    double min = ((lon1 / lon2) - deg) * 60 + Math.Floor(lon3 / lon4);
                                    double sec = ((lon3 / lon4) - min) * 60 + Math.Floor(lon5 / lon6);

                                    lon = Convert.ToString(deg) + "," + Convert.ToString(min) + "," + Convert.ToString(sec);
                                }
                            }
                            #endregion

                            string name = image.Substring(path.Length + 1, image.Length - path.Length - 1);
                            output[i] = name + "," + date + "," + lat + "," + lon;
                        }
                    }
                    Console.Write(String.Join("\r\n", output));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                }
            }
            Console.ReadKey();
        }
    }
}

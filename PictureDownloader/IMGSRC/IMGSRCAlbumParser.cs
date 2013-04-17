using HtmlAgilityPack;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;


namespace PictureDownloader.IMGSRC
{
    public class IMGSRCAlbumParser
    {
        public delegate void PicFoundEventHandler();

        public static event PicFoundEventHandler PicFound;

        public static int PicsNum { get; set; }

        public static void GetPics(string albumUrl, string path)
        {
            PicsNum = 0;
            bool isLastPick = false;
            string currPic = albumUrl;
            WebBrowser wb = new WebBrowser();
            do
            {
                wb.Navigate(currPic);

                while (wb.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                }

                string albumName = string.Empty;
                if (PicsNum == 0)
                {
                    albumName =  wb.DocumentTitle;
                }

                HtmlElement opic = wb.Document.GetElementById("oripic");
                if (opic != null)
                {
                    // получаем ссылку на оригинал
                    string orpic = opic.GetAttribute("href");
                    DownLoadPic(orpic, albumName, path);
                    PicsNum++;                    
                    if (PicFound != null)
                    {
                        PicFound();
                    }
                }
                else
                {
                    // пытаемся получить ссылку на обычную картинку
                    HtmlElement pic = wb.Document.GetElementById("bigpic");
                    if (pic != null)
                    {
                        // получаем ссылку на картинку
                        string upic = pic.GetAttribute("src");
                        DownLoadPic(upic, albumName, path);
                        PicsNum++;

                        if (PicFound != null)
                        {
                            PicFound();
                        }
                    }
                }

                // получаем ссылку на следующую страницу
                HtmlElement newpic = wb.Document.GetElementById("new_big_url");
                if (newpic.GetAttribute("href").IndexOf("/main/user.php?user=") == -1)
                {
                    currPic = newpic.GetAttribute("href");
                }
                else
                {
                    isLastPick = true;
                }

            }
            while (!isLastPick);
        }

        public static void GetPics2(string albumUrl, string path)
        {
            PicsNum = 0;
            bool isLastPick = false;
            string currPic = albumUrl;
            WebBrowser wb = new WebBrowser();
            do
            {
                wb.Navigate(currPic);

                while (wb.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                }

                string albumName = string.Empty;
                if (PicsNum == 0)
                {
                    albumName = wb.DocumentTitle;
                }

                HtmlElement opic = wb.Document.GetElementById("oripic");
                if (opic != null)
                {
                    // получаем ссылку на оригинал
                    string orpic = opic.GetAttribute("href");
                    DownLoadPic(orpic, albumName, path);
                    PicsNum++;
                    if (PicFound != null)
                    {
                        PicFound();
                    }
                }
                else
                {
                    // пытаемся получить ссылку на обычную картинку
                    HtmlElement pic = wb.Document.GetElementById("bigpic");
                    if (pic != null)
                    {
                        // получаем ссылку на картинку
                        string upic = pic.GetAttribute("src");
                        DownLoadPic(upic, albumName, path);
                        PicsNum++;

                        if (PicFound != null)
                        {
                            PicFound();
                        }
                    }
                }

                // получаем ссылку на следующую страницу
                HtmlElement newpic = wb.Document.GetElementById("new_big_url");
                if (newpic.GetAttribute("href").IndexOf("/main/user.php?user=") == -1)
                {
                    currPic = newpic.GetAttribute("href");
                }
                else
                {
                    isLastPick = true;
                }

            }
            while (!isLastPick);
        }

        private static void DownLoadPic(string pic, string albumName, string path)
        {
            WebClient wc = new WebClient();
            string fileName = pic.Substring(pic.LastIndexOf("/")+1, pic.Length - pic.LastIndexOf("/")-1);
            //string res = path + "\\" + albumName + "\\" + fileName;
            string res = path + "\\" + fileName;
            //res = CleanFileName(res);
            wc.DownloadFile(pic, res);
        }

        private static string CleanFileName(string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }


    }
}

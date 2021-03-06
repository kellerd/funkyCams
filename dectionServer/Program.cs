﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Collections;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Specialized;

namespace DetectionServer
{
    public class DetectionServer
    {
       
        private readonly HttpListener _listener = new HttpListener();
        private readonly Func<HttpListenerRequest, string> _responderMethod;

        public DetectionServer(IReadOnlyCollection<string> prefixes, Func<HttpListenerRequest, string> method)
        {
            if (!HttpListener.IsSupported)
            {
                throw new NotSupportedException("Needs Windows XP SP2, Server 2003 or later.");
            }

            // URI prefixes are required eg: "http://localhost:8080/test/"
            if (prefixes == null || prefixes.Count == 0)
            {
                throw new ArgumentException("URI prefixes are required");
            }

            if (method == null)
            {
                throw new ArgumentException("responder method required");
            }

            foreach (var s in prefixes)
            {
                _listener.Prefixes.Add(s);
            }

            _responderMethod = method;
            _listener.Start();
        }

        public DetectionServer(Func<HttpListenerRequest, string> method, params string[] prefixes)
           : this(prefixes, method)
        {
        }


        public void Run()
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                Console.WriteLine("Webserver running...");
                try
                {
                    while (_listener.IsListening)
                    {
                        ThreadPool.QueueUserWorkItem(c =>
                        {
                            var ctx = c as HttpListenerContext;
                            try
                            {
                                if (ctx == null)
                                {
                                    return;
                                }

                                var rstr = _responderMethod(ctx.Request);
                                var buf = Encoding.UTF8.GetBytes(rstr);
                                ctx.Response.ContentLength64 = buf.Length;
                                ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                            }
                            catch( Exception e)
                            {
                                // ignored
                                Console.WriteLine(e.Message);
                            }
                            finally
                            {
                                // always close the stream
                                if (ctx != null)
                                {
                                    ctx.Response.OutputStream.Close();
                                }
                            }
                        }, _listener.GetContext());
                    }
                }
                catch (Exception ex)
                {
                    // ignored
                }
            });
        }

        public void Stop()
        {
            _listener.Stop();
            _listener.Close();
        }
    }

    internal  class Program
    {

        private static Queue qt = new Queue();
        private bool queueRunning = false;
        //private static Thread t = new Thread(processImages);
        private static object syncLock = new object();
        private static object syncLock2 = new object();

        private static ImageCodecInfo jgpEncoder;
        private static EncoderParameters myEncoderParameters;
        private static EncoderParameter myEncoderParameter;
        private static System.Drawing.Imaging.Encoder myEncoder;

        public class queueEntry
        {
            public byte[] img;
            public string filename;

        }

        //public static void processImages()
        //{
        //    // queueRunning = true;
        //    //CloudBlobClient client;
        //    //CloudBlobContainer container;
        //    while (true)
        //    {
        //        if (qt.Count > 0)
        //        {
        //            //queueEntry anEntry = (queueEntry)qt.Dequeue();
        //            //AzureMeteorDetect.queueEntry qe = (AzureMeteorDetect.queueEntry)anEntry;
        //            queueEntry qe = new queueEntry();
        //            popQueue(ref qe, true);
        //            bool found = false;
        //            float[,,] boxes = null;
        //            float[,] scores = null;
        //            float[,] classes = null;
        //            float[] num = null;

        //            Console.WriteLine("about to examine " + qe.filename);
        //            Console.WriteLine("items in queue: {0} ",qt.Count);
        //            //
        //            if (!md.isLoaded())
        //            {
        //                md.LoadModel("frozen_inference_graph.pb", "object-detection.pbtxt");

        //                myEncoder = System.Drawing.Imaging.Encoder.Quality;
        //                jgpEncoder = GetEncoder(ImageFormat.Jpeg);
        //                myEncoderParameters = new EncoderParameters(1);

        //                myEncoderParameter = new EncoderParameter(myEncoder, 50L);
        //                myEncoderParameters.Param[0] = myEncoderParameter;
        //            }
        //            DateTime start = DateTime.Now;
        //            md.examine(qe.img, qe.filename, ref boxes, ref scores, ref classes, ref num, ref found);

        //            Console.WriteLine("elapsed time: {0}", DateTime.Now - start);
        //            //totalImagesProcessed++;

        //            if (found)
        //            {
        //                //upload original image to file storage



        //                Console.WriteLine("object detected");


        //                //Microsoft.WindowsAzure.Storage
        //                //    .CloudStorageAccount storageAccount = CloudStorageAccount
        //                //    .Parse(ConfigurationManager.AppSettings["BlobConnectionString"]);

        //                //Microsoft.WindowsAzure.Storage.CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=meteorshots;AccountKey=M+rGNU1Ija+Zrs09fVL8FiVj+HVWkx1ji4MvRcSC0Yaa/G+A+MOdN3rAWWCMu8pLBBrFfxM8K4d68FBbsTOmYw==;EndpointSuffix=core.windows.net");
        //                //client = storageAccount.CreateCloudBlobClient();
        //                //container = client.GetContainerReference("found");





        //                //container.CreateIfNotExistsAsync(
        //                //  BlobContainerPublicAccessType.Blob,
        //                //  new BlobRequestOptions(),
        //                //  new OperationContext());
        //               // qe.filename = qe.filename.Replace("bmp", "png");
        //                qe.filename = qe.filename.Replace("jpg", "png");
        //                qe.filename = Path.GetFileName(qe.filename);
        //                // CloudBlockBlob blob = container.GetBlockBlobReference(Path.GetFileName(qe.filename));
        //                //blob.Properties.ContentType = "image/png";
        //                Bitmap b;
        //                using (var imagems = new MemoryStream(qe.img))
        //                {
        //                    b = new Bitmap(imagems);
        //                }
        //                Console.WriteLine("about to save bmp");
        //                Bitmap c = new Bitmap(b);
        //                c.Save("e:\\found\\" + qe.filename);
                        
        //                b.Dispose();
        //                qe.filename = qe.filename.Replace("png", "jpg");
        //                md.DrawBoxes(boxes, scores, classes, ref c, qe.filename, .35, false);

        //                Console.WriteLine("about to save jpg");
        //                c.Save("e:\\found\\" + qe.filename, jgpEncoder, myEncoderParameters);


        //                c.Dispose();
                        
        //            }
        //            else { Console.WriteLine("nothing found"); }

        //            //
        //            qe = null;

        //        }
                
        //    }
        //    //queueRunning = false;

        //}
        //public class Startup : IExtensionConfigProvider
        //{
        //    public void Initialize(ExtensionConfigContext context)
        //    {
        //        // Put your intialization code here.
        //    }
        //}
        //public static queueEntry popQueue(ref queueEntry qe, bool popoff)

        //{
        //    lock (syncLock2)
        //    {
        //        if (popoff)
        //        {
        //            queueEntry anEntry = (queueEntry)qt.Dequeue();
        //            qe = (queueEntry)anEntry;
        //        }
        //        else
        //        {

        //            qt.Enqueue(qe);
        //        }
        //    }

        //    return qe;
        //}
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public static string SendResponse(HttpListenerRequest request)

            {
            //Console.WriteLine(request.ContentLength64);


            
            //string filename = request.GetQueryNameValuePairs()
            //   .FirstOrDefault(q => string.Compare(q.Key, "file", true) == 0)
            //   .Value;
            //log.Info("file:" + filename);
            //log.Info("images processed count:" + totalImagesProcessed);
            //if (filename != null)
            //{
            //    // Get request body
            string[] myParams = request.QueryString.GetValues("file");

            string filename = myParams[0];



            //
                   ObjectDetection.TFDetector md = new ObjectDetection.TFDetector();


        queueEntry qe = new queueEntry();
                qe.filename = filename;


            var ss = request.InputStream;


            byte[] buffer = new byte[16000];
            int totalCount = 0;
            
            while (true)
            {
                int currentCount = ss.Read(buffer, totalCount, buffer.Length - totalCount);
                if (currentCount == 0)
                    break;

                totalCount += currentCount;
                if (totalCount == buffer.Length)
                    Array.Resize(ref buffer, buffer.Length * 2);
            }

            Array.Resize(ref buffer, totalCount);
            qe.img = buffer;

            //
            bool found = false;
            float[,,] boxes = null;
            float[,] scores = null;
            float[,] classes = null;
            float[] num = null;

            Console.WriteLine("about to examine " + qe.filename);
            //Console.WriteLine("items in queue: {0} ", qt.Count);
            //
            if (!md.isLoaded())
            {
                md.LoadModel("frozen_inference_graph.pb", "object-detection.pbtxt");

                myEncoder = System.Drawing.Imaging.Encoder.Quality;
                jgpEncoder = GetEncoder(ImageFormat.Jpeg);
                myEncoderParameters = new EncoderParameters(1);

                myEncoderParameter = new EncoderParameter(myEncoder, 50L);
                myEncoderParameters.Param[0] = myEncoderParameter;
            }
            DateTime start = DateTime.Now;
            md.examine(qe.img, qe.filename, ref boxes, ref scores, ref classes, ref num, ref found);

            Console.WriteLine("elapsed time: {0}", DateTime.Now - start);
            //totalImagesProcessed++;

            if (found)
            {
                //upload original image to file storage



                Console.WriteLine("object detected");


                //Microsoft.WindowsAzure.Storage
                //    .CloudStorageAccount storageAccount = CloudStorageAccount
                //    .Parse(ConfigurationManager.AppSettings["BlobConnectionString"]);

                //Microsoft.WindowsAzure.Storage.CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=meteorshots;AccountKey=M+rGNU1Ija+Zrs09fVL8FiVj+HVWkx1ji4MvRcSC0Yaa/G+A+MOdN3rAWWCMu8pLBBrFfxM8K4d68FBbsTOmYw==;EndpointSuffix=core.windows.net");
                //client = storageAccount.CreateCloudBlobClient();
                //container = client.GetContainerReference("found");





                //container.CreateIfNotExistsAsync(
                //  BlobContainerPublicAccessType.Blob,
                //  new BlobRequestOptions(),
                //  new OperationContext());
                qe.filename = qe.filename.Replace("bmp", "png");
                qe.filename = qe.filename.Replace("jpg", "png");
                qe.filename = Path.GetFileName(qe.filename);
                // CloudBlockBlob blob = container.GetBlockBlobReference(Path.GetFileName(qe.filename));
                //blob.Properties.ContentType = "image/png";
                Bitmap b;
                using (var imagems = new MemoryStream(qe.img))
                {
                    b = new Bitmap(imagems);
                }
                Console.WriteLine("about to save bmp");
                Bitmap c = new Bitmap(b);
                c.Save("e:\\found\\" + qe.filename);

                b.Dispose();
                qe.filename = qe.filename.Replace("png", "jpg");
                md.DrawBoxes(boxes, scores, classes, ref c, qe.filename, .35, false);

                Console.WriteLine("about to save jpg");
                c.Save("e:\\found\\" + qe.filename, jgpEncoder, myEncoderParameters);


                c.Dispose();

            }
            else
            {
                Console.WriteLine("nothing found");
                Bitmap b;
                using (var imagems = new MemoryStream(qe.img))
                {
                    b = new Bitmap(imagems);
                }
                Console.WriteLine("about to save bmp");
                Bitmap c = new Bitmap(b);
                c.Save("e:\\notfound\\" + qe.filename);
            }

            //
            qe = null;
            //
            //lock (syncLock)
            //{
            //    popQueue(ref qe, false);
            //    if (t.ThreadState != ThreadState.Running)
            //    {



            //        try
            //        {




            //            t.Start();
            //            //log.Info("starting queue thread");

            //        }
            //        catch (Exception e)
            //        {
            //           // log.Info(e.Message);
            //        }



            //    }

            //}

            //return string.Format("<HTML><BODY>My web page.<br>{0}</BODY></HTML>", DateTime.Now);
            //get image and filename
            return "ok";

        }





        private static void Main(string[] args)
        {

            var ws = new DetectionServer(SendResponse, "http://192.168.1.192:7071/api/detection/");
            ws.Run();
            Console.WriteLine("A simple webserver. Press a key to quit.");
            Console.ReadKey();
            ws.Stop();
        }
    }
}
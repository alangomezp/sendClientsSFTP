using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using System.IO;
using System.Globalization;

namespace createClientToSky.Models
{
    class Sftp
    {
        public static void UploadSFTPFile(string host, string username,
                                           string password, string sourcefile, int port)
        {

            using (SftpClient client = new SftpClient(host, port, username, password))
            {
              
                string today = DateTime.Now.ToString("MMddyyHmmss", CultureInfo.InvariantCulture);
                string fileName = "clientesLog" + today + ".csv";
  
                string logsPath = Environment.CurrentDirectory + @"\\logsFiles";

                Directory.CreateDirectory(logsPath);
                string destFile = Path.Combine(logsPath, fileName);

                client.Connect();

                using (FileStream fs = new FileStream(sourcefile, FileMode.Open))
                {
                    if (fs != null)
                    {
                        try
                        {
                            client.ChangeDirectory(@"/home/ole/customers/input");
                            client.BufferSize = 4 * 1024;
                            client.UploadFile(fs, Path.GetFileName(sourcefile));

                        }
                        catch (Exception ex)
                        {
                            string fileNameError = "clientesError" + today + ".csv";
                            string errorsPathError = Environment.CurrentDirectory + @"\\errorFiles";

                            Directory.CreateDirectory(errorsPathError);

                            string destFileError = Path.Combine(errorsPathError, fileNameError);
                            File.Copy(sourcefile, destFileError, true);

                            Console.WriteLine("Error" + ex.Message);

                        }

                        File.Copy(sourcefile, destFile, true);

                    }

                }


            }
        }
    }
}

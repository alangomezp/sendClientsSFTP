using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace createClientToSky.Models
{
    class Funciones
    {
        //Created by Alan Gomez
        public void createCSVClientes()
        {
            using (FIDEntities FID = new FIDEntities())
            {
                //Get the data
                var registros = FID.CLIENTES_PARA_SKY.Where(a => a.USADO == false).ToList();
                List<CLIENTES_PARA_SKY> cLIENTES_PARA_SKies = new List<CLIENTES_PARA_SKY>();

                if (registros != null)
                {

                    //cleaning data
                    foreach (var a in registros)
                    {
                        string name = a.NOMBRE.Replace(",", "");
                        string lastname= a.APELLIDOS.Replace(",", "");
                        string address = a.DIRECCION.Replace(",", "");

                        cLIENTES_PARA_SKies.Add(new CLIENTES_PARA_SKY
                        {
                            ID_CLIENTE = a.ID_CLIENTE,
                            NO_CLIENTE = a.NO_CLIENTE,
                            NO_TARJETA = a.NO_TARJETA,
                            NOMBRE = name,
                            APELLIDOS = lastname,
                            DIRECCION = address,
                            ID_SECTOR = a.ID_SECTOR,
                            TELEFONO = a.TELEFONO,
                            CELULAR = a.CELULAR,
                            ID_GENERO = a.ID_GENERO,
                            ID_ESTADO_CIVIL = a.ID_ESTADO_CIVIL,
                            CORREO_E = a.CORREO_E,
                            ID_TIENDA = a.ID_TIENDA,
                            ID_OCUPACION = a.ID_OCUPACION,
                            ID_NIVEL_EDUCACION = a.ID_NIVEL_EDUCACION,
                            FECHA_NACIMIENTO = a.FECHA_NACIMIENTO,
                            FECHA_AFILIACION = a.FECHA_AFILIACION,
                            TOTAL_PTOS_GANADOS = a.TOTAL_PTOS_GANADOS,
                            TOTAL_PTOS_REDIMIDOS = a.TOTAL_PTOS_REDIMIDOS,
                            TOTAL_PTOS_VENCIDOS = a.TOTAL_PTOS_VENCIDOS,
                            TOTAL_COMPRAS =a.TOTAL_COMPRAS,
                            CANTIDAD_VISITAS = a.CANTIDAD_VISITAS,
                            FECHA_ULT_COMPRA = a.FECHA_ULT_COMPRA,
                            PTOS_GANADOS = a.PTOS_GANADOS,
                            PTOS_REDIMIDOS = a.PTOS_REDIMIDOS,
                            PTOS_VENCIDOS = a.PTOS_VENCIDOS,
                            PTOS_DISPONIBLES = a.PTOS_DISPONIBLES,
                            COMPRAS = a.COMPRAS,
                            FECHA_ULT_REDENCION =a.FECHA_ULT_REDENCION,
                            USUARIO = a.USUARIO,
                            FECHAHORA = a.FECHAHORA,
                            INACTIVO = a.INACTIVO,
                            EMPLEADO = a.EMPLEADO,
                            PTOS_PROMOCIONES = a.PTOS_PROMOCIONES,
                            NUMERO_ASOCIADO = a.NUMERO_ASOCIADO,
                            PARTICIPANTE = a.PARTICIPANTE
                        });
                    }

                    string strFilePath = Environment.CurrentDirectory + @"\\clientes.csv";

                    //Creating csv
                    using (var writer = new StreamWriter(strFilePath))
                    using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {

                        csvWriter.Configuration.Delimiter = ",";
                        csvWriter.Configuration.HasHeaderRecord = false;

                        csvWriter.WriteRecords(cLIENTES_PARA_SKies);

                        foreach (var item in registros)
                        {
                            item.USADO = true;

                            try
                            {
                                FID.CLIENTES_PARA_SKY.Attach(item);
                                FID.Entry(item).State = System.Data.EntityState.Modified;
                                FID.SaveChanges();
                            }
                            catch (Exception)
                            {

                                throw;
                            }

                        }

                    }

                    uploadSFTPFile();
                }
            }
        }


        public void uploadSFTPFile()
        {
            //Settings for upload
            string source = Environment.CurrentDirectory + @"\\clientes.csv";
            string host = "192.168.1.1";
            string username = "user";
            string password = "password";
            int port = 22; 

            Sftp.UploadSFTPFile(host, username, password, source, port);

        }
    }
}

using BibliotecaUTP.lib;
using BibliotecaUTP.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BibliotecaUTP.Services {
    public class RSS {

        public Biblioteca GetBiblioteca(string textoBusqueda, string campo = "", string sede = "", string tipo = "", int page = 1) {
            Biblioteca biblioteca = new Biblioteca();
            try {
                //Dominio de la biblioteca virtual de la institucion educativa
                string host = "https://dominio.edu.pe/";
                string url = string.Format(host + "?idx={0}&q={1}&limit={2}&limit={3}&offset={4}&count={5}&sort_by=relevance_dsc&format=rss2", campo, textoBusqueda, sede, tipo, (page - 1) * 20, 20);

                var readerTask = FeedReader.ReadAsync(url);
                readerTask.ConfigureAwait(false);
                Feed result = readerTask.Result;

                biblioteca.Titulo = result.Title;
                biblioteca.Enlace = result.Link;
                biblioteca.Descripcion = result.Description;
                biblioteca.TotalResultado = Int32.Parse(result.totalResults);
                biblioteca.InicioIndice = Int32.Parse(String.IsNullOrEmpty(result.startIndex) ? "0" : result.startIndex);
                biblioteca.CantidadItemsPaginado = Int32.Parse(result.itemsPerPage);

                List<Documentos> documentos = new List<Documentos>();

                foreach (FeedItem item in result.Items) {
                    documentos.Add(new Documentos() {
                        Titulo = item.Title,
                        Enlace = item.Link,
                        Identificador = item.identifier,
                        Descripcion = Regex.Replace(item.Description.Substring(0, item.Description.IndexOf("Place Hold")).Replace('\n', ' ').Replace('\t', ' '), @"\s+", " ").Trim(),
                        Imagen = item.imagen,
                        Id = item.Id
                    });
                }
                biblioteca.Documentos = documentos;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return biblioteca;
        }
    }
}